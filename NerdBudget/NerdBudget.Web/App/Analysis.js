'use strict';

function AnalysisViewModel(data)
{
    var self = this;

    self.account = data.account;

    self.headers = data.headers;

    self.details = data.details;

    self.showLedgers = showLedgers;

    for (var x = 0, y = self.headers.length; x < y; x++)
    {
        updateHeader(x);

        var start = self.headers[x].start;

        var end = self.headers[x].end;

        self.headers[x].dateRange = moment(start).format('MMM') + ' ' +
            moment(start).format('DD') + ' - ' +
            moment(end).format('DD');

        self.headers[x].tooltip = headerTooltip;
    }

    for (var x = 0, y = self.details.length; x < y; x++)
    {
        for (var a = 0, b = self.details[x].values.length; a < b; a++)
        {
            self.details[x].values[a].id = self.details[x].id;
            self.details[x].values[a].header = self.headers[a];
        }
    }

    ko.track(self);

    function getLedgerElement(disableIt)
    {
        var html = $('#ledgers').html();

        var $html = $(html);

        return $html.get(0);
    };

    function showLedgers(d)
    {
        if (d.actual !== 0)
        {
            var dt = moment(d.header.start);

            var onSuccess = function (data)
            {
                var element = getLedgerElement();

                var vm = new LedgersViewModel(data);

                ko.applyBindings(vm, element);

                var date = dt.format('MMM DD');

                var options = nbHelper.displayDialog('Week of ' + date, element);

                options.buttons.ok.callback = function () { ko.cleanNode(element); };

                bootbox.dialog(options);
            };

            var onError = function (error) { };

            var url = 'api/Ledgers/' + self.account.id + '/' +
                d.id + '/' + dt.format('YYYY-MM-DD') + '/weekly';

            $.retrieve(url).then(onSuccess, onError);
        }
    };

    function updateHeader(idx)
    {
        var w = self.headers[idx];

        w.actual = getValue(idx, 'actual');
        w.budget = getValue(idx, 'budget');

        w.variance = 0;

        for (var x in self.details)
        {
            w.variance += self.details[x].values[idx].variance;
        }

        if (w.isHistory)
        {
            w.projection = 0;
        }
        else
        {
            var prev = self.headers[idx - 1];

            var balance = w.balance + prev.projection;

            if (!w.isCurrent)
            {
                balance += prev.variance;
            }

            if (w.isFuture)
            {
                w.balance = prev.projection;
            }

            w.projection = balance + w.variance;
        }
    };

    function getValue(idx, field)
    {
        var value = 0;

        for (var key in self.details)
        {
            var d = self.details[key];

            value += d.values[idx][field];
        }

        return value;
    }

    function headerTooltip()
    {
        var h = this;

        if (h.isHistory)
        {
            if (h.variance != 0)
            {
                return ko.filters.number(Math.abs(h.variance)) + ' ' + (h.variance > 0 ? 'Under' : 'OVER') + ' Budget';
            }

            return 'On Target';
        }

        if (h.isCurrent)
        {
            return 'Beginning Balance ' + ko.filters.number(parseInt(h.balance));
        }

        if (h.isProjection)
        {
            return 'Projected Balance ' + ko.filters.number(parseInt(h.balance));
        }

        return '';
    };
};

function LedgersViewModel(data)
{
    var self = this;

    self.ledgers = data;

    ko.track(self);
}