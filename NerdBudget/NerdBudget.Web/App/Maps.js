'use strict';

function MapListViewModel(data)
{
    var self = this;

    self.url = 'api/Maps';

    self.account = data.account;

    self.budgets = data.budgets;

    self.maps = ko.utils.arrayMap(data.maps, function (x) { ko.track(x); return x; });

    self.update = update;

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

    function update(map)
    {
        var element = getFormElement();

        var vm = new MapDetailModel(map, self.budgets);

        ko.applyBindings(vm, element);

        var options = nbHelper.crudDialog('update', element);

        options.buttons.ok.callback = function ()
        {
            var dlg = this;

            var onSuccess = function (data)
            {
                nbHelper.overlay(data, map);

                ko.cleanNode(element);

                dlg.modal('hide');
            };

            var onError = function (error)
            {
                dlg.find('form').showErrors(error);
            };

            $.update(self.url + '/{accountId}/{id}', vm.getData()).then(onSuccess, onError);

            return false;
        };

        options.buttons.cancel.callback = function () { ko.cleanNode(element); };

        bootbox.dialog(options);
    };
};

function MapDetailModel(map, budgets)
{
    var self = this;

    self.budgets = budgets;
    
    self.accountId = '';
    self.id = '';
    self.budgetId = '';
    self.regexPattern = '';

    self.update = update;

    self.getData = getData;

    self.creating = creating;

    self.update(map);

    ko.track(self, ['accountId', 'id', 'budgetId', 'regexPattern']);

    function creating()
    {
        return self.accountId != '' && self.budgetId != '' && self.id != '';
    };

    function update(data)
    {
        if (data)
        { 
            self.accountId = data.accountId || '';
            self.id = data.id || '';
            self.budgetId = data.budgetId || '';
            self.regexPattern = data.regexPattern || '';
        }
    };

    function getData()
    {
        return { 
            accountId: self.accountId,
            id: self.id,
            budgetId: self.budgetId,
            regexPattern: self.regexPattern
        };
    }
};