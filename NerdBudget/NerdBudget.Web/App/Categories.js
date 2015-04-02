function CategoryModel(data)
{
    var self = this;

    self.id = '';
    self.name = '';
    self.multiplier = 0;

    ko.track(self);

    self.update = update;

    self.isIncome = function () { return self.multiplier == 1 ? "Yes" : ""; };

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

    self.sequences = function (event, ui)
    {
        $.notify({ message: 'Saving Category Order' }, { type: 'warning' });

        var ids = [];

        $('table.table-sortable tbody tr').each(function (idx)
        {
            var txt = $(this).find('td:first').attr('category-id');

            ids[ids.length] = txt;
        });

        var onSuccess = function ()
        {
            $.notify({ message: 'Category Order has been Saved' });
        };

        $.update(self.apiUrl + '/sequences', { sequence: ids }).then(onSuccess, self.form.onError);
    };

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