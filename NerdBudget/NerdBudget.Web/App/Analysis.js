'use strict';

function AnalysisViewModel(data)
{
    var self = this;

    self.account = data.account;

    self.details = ko.utils.arrayMap(data.details, mapDetail);

    self.headers = ko.utils.arrayMap(data.headers, mapHeader);

    self.showLedgers = showLedgers;

    self.showAdjustments = showAdjustments;

    ko.track(self);

    function mapDetail(detail)
    {
        for (var a = 0, b = detail.values.length; a < b; a++)
        {
            detail.values[a].multiplier = detail.multiplier;
            detail.values[a].variance = function () { return (this.budget - this.actual); };

            ko.track(detail.values[a]);
        }
        
        ko.track(detail);

        return detail;
    };

    function mapHeader(header)
    {
        header.dateRange = moment(header.start).format('MMM') + ' ' +
            moment(header.start).format('DD') + ' - ' +
            moment(header.end).format('DD');

        header.values = [];

        for (var a = 0, b = self.details.length; a < b; a++)
        {
            header.values.push(self.details[a].values[header.index]);
        }

        header.tooltip = function ()
        {
            if (this.isHistory)
            {
                var v = this.variance();

                if (v != 0)
                {
                    return (v > 0 ? 'Under' : 'OVER') + ' Budget';
                }

                return 'On Target';
            }

            if (this.isCurrent)
            {
                return 'Beginning Balance ' + ko.filters.fixed(this.balance);
            }

            return 'Estimated Projection';
        };

        header.actual = function ()
        {
            var x = 0;

            for (var key in this.values)
            {
                x += this.values[key].actual;
            }

            return x;
        };

        header.budget = function ()
        {
            var x = 0;

            for (var key in this.values)
            {
                x += this.values[key].budget;
            }

            return x;
        };

        header.variance = function ()
        {
            var x = 0;

            for (var key in this.values)
            {
                var val = this.values[key];

                var v = val.variance();

                if (val.multiplier == -1 && v < 0)
                {
                    //  expense and missing the expected budget
                    x += v;
                }
                else if (val.mutliplier == 1 && v > 0)
                {
                    //  income and missing the expected budget
                    x += v;
                }
            }
    
            return x;
        };

        header.projection = function ()
        {
            if (this.isHistory)
            {
                return 0;
            }

            if (this.isCurrent)
            {
                return this.balance + this.variance();
            }

            var prev = self.headers[this.index - 1];

            return prev.projection() + this.variance();
        };

        return header;
    };

    function getElement(selector)
    {
        var html = $(selector).html();

        var $html = $(html);

        return $html.get(0);
    };

    function showLedgers(budget, index)
    {
        if (budget.values[index].actual !== 0)
        {
            var header = self.headers[index];

            var dt = moment(header.start);

            var onSuccess = function (data)
            {
                var element = getElement('#ledgers');

                var vm = new LedgersViewModel(data);

                ko.applyBindings(vm, element);

                var date = dt.format('MMM DD');

                var options = nbHelper.displayDialog('Transactions Week of ' + date, element);

                options.buttons.ok.callback = function () { ko.cleanNode(element); };

                bootbox.dialog(options);
            };

            var onError = function (error) { };

            var url = 'api/Ledgers/' + self.account.id + '/' +
                budget.id + '/' + dt.format('YYYY-MM-DD') + '/weekly';

            $.retrieve(url).then(onSuccess, onError);
        }
    };

    function showAdjustments(budget, index)
    {
        var values = this;

        var header = self.headers[index];

        var dt = moment(header.start);

        var onSuccess = function (data)
        {
            var element = getElement('#adjustments');

            var vm = new AdjustmentsViewModel({ account: self.account, budget: budget, adjustments: data });

            ko.applyBindings(vm, element);

            var date = dt.format('MMM DD');

            var options = nbHelper.displayDialog('Adjustments Week of ' + date, element);

            options.buttons.ok.callback = function ()
            {
                values.budget = vm.adjustedBudget() * budget.multiplier;

                ko.cleanNode(element);
            };

            bootbox.dialog(options);
        };

        var onError = function (error) { };

        var url = 'api/Adjustments/' + self.account.id + '/' +
            budget.id + '/' + dt.format('YYYY-MM-DD') + '/weekly';

        $.retrieve(url).then(onSuccess, onError);
    };
};

function LedgersViewModel(data)
{
    var self = this;

    self.ledgers = data;

    ko.track(self);
};

function AdjustmentsViewModel(data)
{
    var self = this;

    self.url = 'api/Adjustments/' + data.account.id;

    self.account = data.account;

    self.budget = data.budget;

    self.adjustments = data.adjustments;

    self.adjustment = ko.track({ date: moment().format('MM/DD/YYYY'), name: null, amount: null });

    self.addAdjustment = addAdjustment;

    self.adjustedBudget = adjustedBudget;

    function addAdjustment(d, e)
    {
        if (e.keyCode === 13)
        {
            var onSuccess = function (data)
            {
                self.adjustments.push(data);

                self.adjustment.name = null;
                self.adjustment.amount = null;

                $('form').hideErrors();
            };

            var onError = function (error)
            {
                $('form').showErrors(error);
            };

            var adj = {
                    budgetId: self.budget.id,
                    date: self.adjustment.date,
                    name: self.adjustment.name,
                    amount: self.adjustment.amount
            };

            $.create(self.url, adj).then(onSuccess, onError);

            return false;
        }

        return true;
    };

    function adjustedBudget()
    {
        var x = 0;

        for (var a = 0, b = self.adjustments.length; a < b; a++)
        {
            x += self.adjustments[a].amount;
        }

        return x;
    };

    ko.track(self);
};