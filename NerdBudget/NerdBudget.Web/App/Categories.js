function CategoryModel(data)
{
    var self = this;

    self.id = '';
    self.name = '';
    self.multiplier = 0;

    ko.track(self);

    self.update = update;

    self.update(data);

    function update(data)
    {
        if (data)
        {
            self.id = data.id || '';
            self.name = data.name || '';
            self.multiplier = data.multiplier || '';
        }
    };
};


function CategoriesViewModel(account, categories)
{
    var self = this;

    self.account = account;

    self.categories = ko.utils.arrayMap(categories, function (data) { return new CategoryModel(data); });

    self.apiUrl = 'api/Categories/' + account.id;

    //  selected category
    self.category = null;

    //  clone to work on for "cancel" purposes
    self.clone = null;

    var options = {
        onCreated: onCreated,
        onUpdated: onUpdated,
        onDeleted: onDeleted,
        model: CategoryModel
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

    function onCreated()
    {
        var onSuccess = function (data, textStatus, jqXHR)
        {
            self.categories.push(new CategoryModel(data));

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
            self.categories.remove(self.form.item);

            self.form.close();
        };

        $.delete(self.apiUrl + '/{id}', self.form.clone).then(onSuccess, self.form.onError);
    };
}