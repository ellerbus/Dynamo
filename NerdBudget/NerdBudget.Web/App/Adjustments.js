'use strict';

function AdjustmentListViewModel(data)
{
    var self = this;

    self.account = data.account;

    self.url = 'api/Adjustments/' + data.account.id;

    self.budgets = data.budgets;

    self.adjustments = ko.utils.arrayMap(data.adjustments, function (x) { ko.track(x); return x; });

    self.create = create;

    self.update = update;

    self.delete = remove;

    ko.track(self);

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

    function create()
    {
        var element = getFormElement();

        var vm = new AdjustmentDetailModel({ name: '', date: moment().format('MM/DD/YYYY') }, self.budgets);

        ko.applyBindings(vm, element);

        var options = nbHelper.crudDialog('create', element);

        options.buttons.ok.callback = function ()
        {
            var dlg = this;

            var onSuccess = function (data)
            {
                self.adjustments.push(data);

                ko.cleanNode(element);

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
    };

    function update(adjustment)
    {
        var element = getFormElement();

        var vm = new AdjustmentDetailModel(adjustment, self.budgets);

        ko.applyBindings(vm, element);

        var options = nbHelper.crudDialog('update', element);

        options.buttons.ok.callback = function ()
        {
            var dlg = this;

            var onSuccess = function (data)
            {
                nbHelper.overlay(data, adjustment);

                ko.cleanNode(element);

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

    function remove(adjustment)
    {
        var element = getFormElement(true);

        var vm = new AdjustmentDetailModel(adjustment, self.budgets);

        ko.applyBindings(vm, element);

        var options = nbHelper.crudDialog('delete', element);

        options.buttons.ok.callback= function ()
        {
            var dlg = this;

            var onSuccess = function (data)
            {
                self.adjustments.remove(adjustment);

                ko.cleanNode(element);

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
};

function AdjustmentDetailModel(adjustment, budgets)
{
    var self = this;

    self.budgets = budgets;

    self.id = '';
    self.budgetId = '';
    self.name = '';
    self.date = '';
    self.amount = '';

    self.update = update;

    self.getData = getData;

    self.creating = creating;

    self.update(adjustment);

    ko.track(self, ['id', 'budgetId', 'name', 'date', 'amount']);

    function creating()
    {
        return self.id != '';
    };

    function update(data)
    {
        if (data)
        { 
            self.id = data.id || '';
            self.budgetId = data.budgetId || '';
            self.name = data.name || '';
            self.date = data.date || '';
            self.amount = data.amount || '';
        }
    };

    function getData()
    {
        return { 
            id: self.id,
            budgetId: self.budgetId,
            name: self.name,
            date: self.date,
            amount: self.amount
        };
    }
};