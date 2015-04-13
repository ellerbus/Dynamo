'use strict';

function AccountListViewModel(data)
{
    var self = this;

    self.url = 'api/Accounts';

    self.accounts = ko.utils.arrayMap(data.accounts, function (x) { ko.track(x); return x; });

    self.create = create;

    self.update = update;

    self.delete = remove;

    self.path = path;

    ko.track(self);

    function path(type, id)
    {
        switch (type)
        {
            case 'categories':
                return $.restSetup.baseUrl + 'Categories/' + id;
            case 'budgets':
                return $.restSetup.baseUrl + 'Budgets/' + id;
            case 'import':
                return $.restSetup.baseUrl + 'Import/' + id;
            case 'analysis':
                return $.restSetup.baseUrl + 'Analysis/' + id;
        }

        return type;
    }

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

        var vm = new AccountDetailModel({ name: '' });

        ko.applyBindings(vm, element);

        var options = nbHelper.crudDialog('create', element);

        options.buttons.ok.callback= function ()
        {
            var dlg = this;

            var onSuccess = function (data)
            {
                self.accounts.push(data);

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

    function update(account)
    {
        var element = getFormElement();

        var vm = new AccountDetailModel(account);

        ko.applyBindings(vm, element);

        var options = nbHelper.crudDialog('update', element);

        options.buttons.ok.callback = function ()
        {
            var dlg = this;

            var onSuccess = function (data)
            {
                nbHelper.overlay(data, account);

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

    function remove(account)
    {
        var element = getFormElement(true);

        var vm = new AccountDetailModel(account);

        ko.applyBindings(vm, element);

        var options = nbHelper.crudDialog('delete', element);

        options.buttons.ok.callback= function ()
        {
            var dlg = this;

            var onSuccess = function (data)
            {
                self.accounts.remove(account);

                cleanNode(element);

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

function AccountDetailModel(account)
{
    var self = this;

    self.id = '';
    self.name = '';

    self.update = update;

    self.getData = getData;

    self.creating = creating;

    self.update(account);

    ko.track(self, ['id', 'name']);

    function creating()
    {
        return self.id != '';
    };

    function update(data)
    {
        if (data)
        {
            self.id = data.id || '';
            self.name = data.name || '';
        }
    };

    function getData()
    {
        return {
            id: self.id,
            name: self.name
        };
    }
};