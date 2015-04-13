'use strict';

function BudgetListViewModel(data)
{
    var self = this;

    self.url = 'api/Budgets/' + data.account.id;

    self.frequencies = data.frequencies;

    self.account = data.account;

    self.categories = ko.utils.arrayMap(data.categories, trackCategory);

    self.create = create;

    self.update = update;

    self.delete = remove;

    self.sequences = sequences;

    self.weekly = 0;

    self.monthly = 0;

    self.yearly = 0;

    self.variance = 0;

    updateCalculations();

    ko.track(self);


    function trackCategory(x)
    {
        if (x.budgets)
        {
            for (var b in x.budgets)
            {
                ko.track(x.budgets[b]);
            }
        }
        else
        {
            x.budgets = [];
        }

        ko.track(x);

        return x;
    };

    function findCategory(id)
    {
        for (var key in self.categories)
        {
            var c = self.categories[key];

            if (c.id == id)
            {
                if (c.budgets == null)
                {
                    c.budgets = [];
                }

                return c;
            }
        }

        return null
    };


    function getFormElement(disableIt)
    {
        var html = $('#form-body').html();

        var $html = $(html);

        $html.submit(function () { return false; });

        if (disableIt)
        {
            $html.disableAll();
        }

        return $html.get(0);
    };

    function create(d)
    {
        var element = getFormElement();

        var vm = new BudgetDetailModel({ accountId: self.account.id, categoryId: d.id, name: '' }, self.categories, self.frequencies);

        ko.applyBindings(vm, element);

        var options = nbHelper.crudDialog('create', element);

        options.buttons.ok.callback = function ()
        {
            var dlg = this;

            var onSuccess = function (data)
            {
                var cat = findCategory(data.categoryId);

                ko.track(data);

                cat.budgets.push(data);

                ko.cleanNode(element);

                updateCalculations();

                dlg.modal('hide');
            };

            var onError = function (error)
            {
                dlg.find('form').showErrors(error);
            };

            $.create(self.url, vm.getData()).then(onSuccess, onError);

            return false;
        };

        options.buttons.cancel.callback = function () { ko.cleanNode(element); };

        bootbox.dialog(options);

        $(element).find('input[name="name"]').focus();
    };

    function update(budget)
    {
        var element = getFormElement();

        var vm = new BudgetDetailModel(budget, self.categories, self.frequencies);

        ko.applyBindings(vm, element);

        var options = nbHelper.crudDialog('update', element);

        options.buttons.ok.callback = function ()
        {
            var dlg = this;

            var onSuccess = function (data)
            {
                if (budget.categoryId != data.categoryId)
                {
                    var fr = findCategory(budget.categoryId);
                    var to = findCategory(data.categoryId);

                    fr.budgets.remove(function (x) { return x.id == budget.id; });
                    to.budgets.push(budget);
                }

                nbHelper.overlay(data, budget);

                ko.cleanNode(element);

                updateCalculations();

                dlg.modal('hide');
            };

            var onError = function (error)
            {
                dlg.find('form').showErrors(error);
            };

            $.update(self.url + '/{id}', vm.getData()).then(onSuccess, onError);

            return false;
        };

        options.buttons.cancel.callback = function () { ko.cleanNode(element); };

        bootbox.dialog(options);
    };

    function remove(budget)
    {
        var element = getFormElement(true);

        var vm = new BudgetDetailModel(budget, self.categories, self.frequencies);

        ko.applyBindings(vm, element);

        var options = nbHelper.crudDialog('delete', element);

        options.buttons.ok.callback = function ()
        {
            var dlg = this;

            var onSuccess = function (data)
            {
                var cat = findCategory(budget.categoryId);

                cat.budgets.remove(function (x) { return x.id == budget.id; });

                ko.cleanNode(element);

                updateCalculations();

                dlg.modal('hide');
            };

            var onError = function (error)
            {
                dlg.find('form').showErrors(error);
            };

            $.delete(self.url + '/{id}', vm.getData()).then(onSuccess, onError);

            return false;
        };

        options.buttons.cancel.callback = function () { ko.cleanNode(element); };

        bootbox.dialog(options);
    };

    function sequences(event, ui)
    {
        $.notify({ message: 'Saving Budget Order' }, { type: 'warning', delay: 2000 });

        var ids = [];

        $('table.table-sortable tbody tr').each(function (idx)
        {
            var txt = $(this).find('td:first').attr('budget-id');

            ids[ids.length] = txt;
        });

        $('#budgets').hideErrors();

        var onSuccess = function ()
        {
            $.notify({ message: 'Budget Order has been Saved' }, { delay: 2000 });
        };

        var onError = function (error)
        {
            $('#budgets').showErrors(error);
        };

        $.update(self.url + '/sequences', { sequence: ids }).then(onSuccess, onError);
    };

    function updateCalculations()
    {
        self.weekly = getTotal('weekly');

        self.monthly = getTotal('monthly');

        self.yearly = getTotal('yearly');

        self.variance = self.yearly >= 0 ? 1 : -1;
    };

    function getTotal(field)
    {
        field += 'Amount';

        var amount = 0;

        for (var c = 0, lenc = self.categories.length; c < lenc; c++)
        {
            var cat = self.categories[c];

            for (var b = 0, lenb = cat.budgets.length; b < lenb; b++)
            {
                var budget = cat.budgets[b];

                var a = cat.multiplier * budget[field];

                if (!isNaN(a))
                {
                    amount += a;
                }
            }
        }

        return amount;
    };
};

function BudgetDetailModel(budget, categories, frequencies)
{
    var self = this;

    self.categories = categories;
    self.frequencies = frequencies;

    self.id = '';
    self.categoryId = '';
    self.name = '';
    self.amount = 0;
    self.startDate = '';
    self.endDate = '';
    self.frequency = '';

    self.update = update;

    self.getData = getData;

    self.creating = creating;

    self.update(budget);

    ko.track(self, ['id', 'categoryId', 'name', 'amount', 'startDate', 'endDate', 'frequency']);

    function creating()
    {
        return self.id != '';
    };

    function update(data)
    {
        if (data)
        {
            self.id = data.id || '';
            self.categoryId = data.categoryId || '';
            self.name = data.name || '';
            self.amount = data.amount || 0;
            self.startDate = data.startDate || '';
            self.endDate = data.endDate || '';
            self.frequency = data.frequency || '';
        }
    };

    function getData()
    {
        return {
            id: self.id,
            categoryId: self.categoryId,
            name: self.name,
            amount: self.amount,
            startDate: self.startDate,
            endDate: self.endDate,
            frequency: self.frequency
        };
    }
};