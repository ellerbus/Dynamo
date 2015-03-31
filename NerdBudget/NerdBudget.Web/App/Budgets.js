function CategoryModel(data)
{
    var self = this;

    self.id = data.id;
    self.name = data.name;
    self.budgets = ko.utils.arrayMap(data.budgets, function (data) { return new BudgetModel(data); });

    ko.track(self);
};

function BudgetModel(data)
{
    var self = this;

    self.id = '';
    self.categoryId = '';
    self.name = '';
    self.amount = 0;
    self.startDate = '';
    self.endDate = '';
    self.frequency = '';
    self.weeklyAmount = 0;
    self.monthlyAmount = 0;
    self.yearlyAmount = 0;
    
    ko.track(self);

    self.update = update;

    self.update(data);

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
            self.weeklyAmount = data.weeklyAmount || 0;
            self.monthlyAmount = data.monthlyAmount || 0;
            self.yearlyAmount = data.yearlyAmount || 0;
        }
    };
};


function BudgetsViewModel(account, categories, frequencies)
{
    var self = this;

    self.account = account;

    self.categories = ko.utils.arrayMap(categories, function (data) { return new CategoryModel(data); });

    self.apiUrl = 'api/Budgets/' + account.id;

    //  selected budget
    self.budget = null;

    //  clone to work on for "cancel" purposes
    self.clone = null;

    var options = {
        onCreated: onCreated,
        onUpdated: onUpdated,
        onDeleted: onDeleted,
        model: BudgetModel
    };

    self.form = new DetailsFormView('div.modal', options);

    self.form.categories = categories;

    self.form.frequencies = frequencies;

    ko.track(self);

    self.sequences = function (event, ui)
    {
        var ids = [];

        $('table.table-sortable tbody tr').each(function (idx)
        {
            var txt = $(this).find('td:first').attr('budget-id');

            ids[ids.length] = txt;
        });

        var onSuccess = null;

        $.update(self.apiUrl + '/sequences', { sequence: ids }).then(onSuccess, self.form.onError);
    };

    self.create = function (category)
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
    }

    function onCreated()
    {
        var onSuccess = function (data, textStatus, jqXHR)
        {
            var cat = findCategory(data.categoryId);

            cat.budgets.push(new BudgetModel(data));

            self.form.close();
        };

        $.create(self.apiUrl, self.form.clone).then(onSuccess, self.form.onError);
    };

    function onUpdated()
    {
        var onSuccess = function (data, textStatus, jqXHR)
        {
            if (self.form.item.categoryId != self.form.clone.categoryId)
            {
                var fr = findCategory(self.form.item.categoryId);
                var to = findCategory(self.form.clone.categoryId);

                fr.budgets.remove(function (x) { return x.id == self.form.item.id; });
                to.budgets.push(self.form.item);
            }

            self.form.item.update(data);

            self.form.close();
        };

        $.update(self.apiUrl + '/{id}', self.form.clone).then(onSuccess, self.form.onError);
    };

    function onDeleted()
    {
        var onSuccess = function ()
        {
            var cat = findCategory(data.categoryId);

            cat.budgets.remove(self.form.item);

            self.form.close();
        };

        $.delete(self.apiUrl + '/{id}', self.form.clone).then(onSuccess, self.form.onError);
    };
}