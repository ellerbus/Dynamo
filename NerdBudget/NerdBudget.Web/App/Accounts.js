function AccountModel(data)
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

    //  selected account
    self.account = null;

    //  clone to work on for "cancel" purposes
    self.clone = null;

    var options = {
        onCreated: onCreated,
        onUpdated: onUpdated,
        onDeleted: onDeleted
    };

    self.form = new DetailsFormView('div.modal', options);

    ko.track(this);

    self.create = function ()
    {
        self.clone = new AccountModel({ id: '', name: '' });

        self.form.open('create');
    };

    self.update = function (data)
    {
        self.account = data;

        self.clone = new AccountModel(data);

        self.form.open('update');
    };

    self.delete = function (data)
    {
        self.account = data;

        self.clone = new AccountModel(data);

        self.form.open('delete');
    };

    function onCreated()
    {
        var onSuccess = function (data, textStatus, jqXHR)
        {
            self.accounts.push(new AccountModel(data));

            self.form.close();
        };

        $.create('api/Accounts', self.clone).then(onSuccess, onError);
    }

    function onUpdated()
    {
        var onSuccess = function (data, textStatus, jqXHR)
        {
            self.account.update(data);

            self.form.close();
        };

        $.update('api/Accounts/{id}', self.clone).then(onSuccess, onError);
    }

    function onDeleted()
    {
        var onSuccess = function ()
        {
            self.accounts.remove(self.account);

            self.form.close();
        };

        $.delete('api/Accounts/{id}', self.clone).then(onSuccess, onError);
    }
    
    function onError(jqXHR, textStatus, errorThrown)
    {
        self.form.loadErrors(jqXHR);
    };
}