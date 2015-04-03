'use strict';

function AccountListViewModel(accounts)
{
    var self = this;

    self.url = 'api/Accounts';

    self.accounts = ko.utils.arrayMap(accounts, function (data) { return ko.mapping.fromJS(data); });

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

    function cleanNode(element)
    {
        ko.cleanNode(element);
    };

    function create()
    {
        var element = getFormElement();

        var vm = new AccountDetailModel({ name: '' }, element);

        ko.applyBindings(vm, element);

        //  create bootbox({ success: foo, error: foo, title: 'Account', for: 'create', using: element });

        bootbox.dialog({
            title: 'Create Account',
            message: element,
            buttons: {
                'Create': {
                    className: 'btn-success',
                    callback: function ()
                    {
                        var dlg = this;

                        var onSuccess = function (data)
                        {
                            self.accounts.push(data);   //--> ok:SUCCESS

                            cleanNode(element);

                            dlg.modal('hide');
                        };

                        var onError = function (error)
                        {
                            dlg.find('form').showErrors(error); //--> ok:FAIL
                        };

                        $.create(self.url, vm.getData()).then(onSuccess, onError);  //--> ok:CLICK

                        return false;
                    }
                },
                'Cancel': {
                    className: 'btn-default',
                    callback: function ()
                    {
                        cleanNode(element);
                    }
                }
            }
        });
    };

    function update(account)
    {
        var element = getFormElement();

        var vm = new AccountDetailModel(account, element);

        ko.applyBindings(vm, element);

        bootbox.dialog({
            title: 'Update Account',
            message: element,
            buttons: {
                'Update': {
                    className: 'btn-warning',
                    callback: function ()
                    {
                        var dlg = this;

                        var onSuccess = function (data)
                        {
                            nbHelper.overlay(data, account);

                            cleanNode(element);

                            dlg.modal('hide');
                        };

                        var onError = function (error)
                        {
                            dlg.find('form').showErrors(error);
                        };

                        $.update(self.url + '/{id}', vm.getData()).then(onSuccess, onError);

                        return false;
                    }
                },
                'Cancel': {
                    className: 'btn-default',
                    callback: function ()
                    {
                        cleanNode(element);
                    }
                }
            }
        });
    };

    function remove(account)
    {
        var element = getFormElement(true);

        var vm = new AccountDetailModel(account, element);

        ko.applyBindings(vm, element);

        bootbox.dialog({
            title: 'Delete Account',
            message: element,
            buttons: {
                'Delete': {
                    className: 'btn-danger',
                    callback: function ()
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
                    }
                },
                'Cancel': {
                    className: 'btn-default',
                    callback: function ()
                    {
                        cleanNode(element);
                    }
                }
            }
        });
    };
};

function AccountDetailModel(account)
{
    var self = this;

    self.id = '';
    self.name = '';

    self.update = update;

    self.getData = getData;

    self.update(account);

    ko.track(self, ['id', 'name']);

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



//function AccountModel(data)
//{
//    var self = this;

//    self.id = '';
//    self.name = '';

//    ko.track(self);

//    self.update = update;

//    self.update(data);

//    function update(data)
//    {
//        if (data)
//        {
//            self.id = data.id || '';
//            self.name = data.name || '';
//        }
//    };
//};


//function AccountsViewModel(accounts)
//{
//    var self = this;

//    self.accounts = ko.utils.arrayMap(accounts, function (data) { return new AccountModel(data); });

//    self.apiUrl = 'api/Accounts';

//    //  selected account
//    self.account = null;

//    //  clone to work on for 'cancel' purposes
//    self.clone = null;

//    var options = {
//        onCreated: onCreated,
//        onUpdated: onUpdated,
//        onDeleted: onDeleted,
//        model: AccountModel
//    };

//    self.form = new DetailsFormView('div.modal', options);

//    ko.track(self);

//    self.create = function ()
//    {
//        self.form.open('create');
//    };

//    self.update = function (data)
//    {
//        self.form.open('update', data);
//    };

//    self.delete = function (data)
//    {
//        self.form.open('delete', data);
//    };

//    function onCreated()
//    {
//        var onSuccess = function (data, textStatus, jqXHR)
//        {
//            window.location.href = $.restSetup.baseUrl + 'Categories/' + data.id;
//        };

//        var promise = $.create(self.apiUrl, self.form.clone).then(onSuccess, self.form.onError).promise();
//    };

//    function onUpdated()
//    {
//        var onSuccess = function (data, textStatus, jqXHR)
//        {
//            window.location.href = $.restSetup.baseUrl + 'Categories/' + data.id;
//        };

//        $.update(self.apiUrl + '/{id}', self.form.clone).then(onSuccess, self.form.onError);
//    };

//    function onDeleted()
//    {
//        var onSuccess = function ()
//        {
//            self.accounts.remove(self.form.item);

//            self.form.close();
//        };

//        $.delete(self.apiUrl + '/{id}', self.form.clone).then(onSuccess, self.form.onError);
//    };
//}