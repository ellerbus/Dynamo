'use strict';

function CategoryListViewModel(data)
{
    var self = this;

    self.url = 'api/Categories/' + data.account.id;

    self.account = data.account;

    self.categories = ko.utils.arrayMap(data.categories, trackCategory);

    self.create = create;

    self.update = update;

    self.delete = remove;

    self.sequences = sequences;

    ko.track(self);


    function trackCategory(x)
    {
        x.isIncome = function () { return this.multiplier > 0 ? 'Yes' : ''; };
        
        ko.track(x);
        
        return x;
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

        var vm = new CategoryDetailModel({ accountId: self.account.id, name: '' });

        ko.applyBindings(vm, element);

        var options = nbHelper.crudDialog('create', element);

        options.buttons.ok.callback = function ()
        {
            var dlg = this;

            var onSuccess = function (data)
            {
                self.categories.push(trackCategory(data));

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

    function update(category)
    {
        var element = getFormElement();

        var vm = new CategoryDetailModel(category);

        ko.applyBindings(vm, element);

        var options = nbHelper.crudDialog('update', element);

        options.buttons.ok.callback = function ()
        {
            var dlg = this;

            var onSuccess = function (data)
            {
                nbHelper.overlay(data, category);

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

    function remove(category)
    {
        var element = getFormElement(true);

        var vm = new CategoryDetailModel(category);

        ko.applyBindings(vm, element);

        var options = nbHelper.crudDialog('delete', element);

        options.buttons.ok.callback = function ()
        {
            var dlg = this;

            var onSuccess = function (data)
            {
                self.categories.remove(category);

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
    
    function sequences(event, ui)
    {
        $.notify({ message: 'Saving Category Order' }, { type: 'warning' });

        var ids = [];

        $('table.table-sortable tbody tr').each(function (idx)
        {
            var txt = $(this).find('td:first').attr('category-id');

            ids[ids.length] = txt;
        });

        $('#categories').hideErrors();

        var onSuccess = function ()
        {
            $.notify({ message: 'Category Order has been Saved' });
        };

        var onError = function (error)
        {
            $('#categories').showErrors(error);
        };

        $.update(self.url + '/sequences', { sequence: ids }).then(onSuccess, onError);
    };
};

function CategoryDetailModel(category)
{
    var self = this;

    self.id = '';
    self.name = '';

    self.update = update;

    self.getData = getData;

    self.creating = creating;

    self.update(category);

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
