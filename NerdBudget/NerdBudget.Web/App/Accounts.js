﻿function AccountModel(data)
{
    var self = this;

    self.id = '';
    self.name = '';

    ko.track(self);

    self.update = update;

    self.update(data);

    function update(data)
    {
        if (data)
        {
            self.id = data.id || '';
            self.name = data.name || '';
        }
    };
};


function AccountsViewModel(accounts)
{
    var self = this;

    self.accounts = ko.utils.arrayMap(accounts, function (data) { return new AccountModel(data); });

    self.apiUrl = 'api/Accounts';

    //  selected account
    self.account = null;

    //  clone to work on for "cancel" purposes
    self.clone = null;

    var options = {
        onCreated: onCreated,
        onUpdated: onUpdated,
        onDeleted: onDeleted,
        model: AccountModel
    };

    self.form = new DetailsFormView('div.modal', options);

    ko.track(self);

    self.create = function ()
    {
        self.form.open('create');
    };

    self.update = function (data)
    {
        self.form.open('update', data);
    };

    self.delete = function (data)
    {
        self.form.open('delete', data);
    };

    self.categoryPath = function (id)
    {
        return $.restSetup.rootUrl + 'Categories/' + id;
    };

    self.budgetPath = function (id)
    {
        return $.restSetup.rootUrl + 'Budgets/' + id;
    };

    self.importPath = function (id)
    {
        return $.restSetup.rootUrl + 'Import/' + id;
    };

    function onCreated()
    {
        var onSuccess = function (data, textStatus, jqXHR)
        {
            self.accounts.push(new AccountModel(data));

            self.form.close();
        };

        $.create(self.apiUrl, self.form.clone).then(onSuccess, self.form.onError);
    };

    function onUpdated()
    {
        var onSuccess = function (data, textStatus, jqXHR)
        {
            self.form.item.update(data);

            self.form.close();
        };

        $.update(self.apiUrl + '/{id}', self.form.clone).then(onSuccess, self.form.onError);
    };

    function onDeleted()
    {
        var onSuccess = function ()
        {
            self.accounts.remove(self.form.item);

            self.form.close();
        };

        $.delete(self.apiUrl + '/{id}', self.form.clone).then(onSuccess, self.form.onError);
    };
}