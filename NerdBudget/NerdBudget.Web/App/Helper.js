$(function ()
{
    $('[nb-create-icon]').addClass('btn btn-default btn-sm').html('<i class="fa fa-plus fa-fw text-success"></i>');
    $('[nb-update-icon]').addClass('btn btn-default btn-sm').html('<i class="fa fa-pencil fa-fw text-primary"></i>');
    $('[nb-delete-icon]').addClass('btn btn-default btn-sm').html('<i class="fa fa-times fa-fw text-danger"></i>');
    
    $('[nb-list-icon]').addClass('btn btn-default btn-sm').html('<i class="fa fa-list fa-fw"></i>');
    $('[nb-dollar-icon]').addClass('btn btn-default btn-sm').html('<i class="fa fa-usd fa-fw text-success"></i>');
    $('[nb-import-icon]').addClass('btn btn-default btn-sm').html('<i class="fa fa-download fa-fw"></i>');
});

function DetailsFormView(modalSelector, options)
{
    var self = this;

    self.modalSelector = modalSelector;

    self.options = options;

    self.item = null;

    self.clone = null;

    //  creating, updating, deleting
    self.state = '';

    self.errors = [];

    ko.track(self);

    self.showId = function ()
    {
        return self.state != 'create';
    };

    // 
    //  'Save' Button Appearance
    //
    self.buttonClass = function ()
    {
        if (self.state == 'create')
        {
            return "btn-success";
        }
        if (self.state == 'update')
        {
            return "btn-warning";
        }
        if (self.state == 'delete')
        {
            return "btn-danger";
        }

        return "btn-default";
    };

    self.buttonText = function ()
    {
        if (self.state == 'create')
        {
            return "Creating";
        }
        if (self.state == 'update')
        {
            return "Updating";
        }
        if (self.state == 'delete')
        {
            return "Deleting";
        }

        return "{unknown}";
    };

    //
    //  modal action results (ok / cancel)
    //
    self.cancel = function ()
    {
        self.item = null;

        self.clone = null;

        if (self.options.onCancelled) self.options.onCancelled();

        self.close();
    };

    self.save = function ()
    {
        self.errors = [];

        if (self.state == 'create')
        {
            if (self.options.onCreated) self.options.onCreated();
        }
        else if (self.state == 'update')
        {
            if (self.options.onUpdated) self.options.onUpdated();
        }
        else if (self.state == 'delete')
        {
            if (self.options.onDeleted) self.options.onDeleted();
        }

        return false;
    };

    self.open = function (state, data)
    {
        self.errors = [];

        self.state = state;

        self.item = data;

        self.clone = new self.options.model(data);

        $(self.modalSelector).modal('show');
    };

    self.close = function ()
    {
        self.state = '';

        self.item = null;

        self.clone = null;

        $(self.modalSelector).modal('hide');
    };

    self.onError = function (jqXHR, textStatus, errorThrown)
    {
        var res = jqXHR.responseJSON;

        if (jqXHR.status == 500)
        {
            if (res)
            {
                if (res.message) self.errors.push(res.message);
                if (res.exceptionMessage) self.errors.push(res.exceptionMessage);
            }
        }
        
        if (jqXHR.status == 400)
        {
            if (res)
            {
                if (res.message) self.errors.push(res.message);

                if (res.modelState)
                {
                    var $form = $(modalSelector + ' form');

                    $form.find('div.form-group').removeClass('has-error');

                    for (key in res.modelState)
                    {
                        self.errors.push(res.modelState[key]);

                        $form.find('[name=' + key + ']').closest('div.form-group').addClass('has-error');
                    }
                }
            }
        }
    };
}



/*
 * based on : https://github.com/lyconic/jquery.rest
 *
 */

(function ($)
{
    'use strict';

    var self = this;

    // Change the values of this global object if your method parameter is different.
    $.restSetup = {
        rootUrl: '',
        verbs: {
            'create': 'POST',
            'retrieve': 'GET',
            'update': 'PUT',
            'delete': 'DELETE'
        }
    };


    //  basic 'crud'operations
    $.create = function (url, data)
    {
        var options = collectOptions(url, data, $.restSetup.verbs.create);

        return $.ajax(options);
    }

    $.retrieve = function (url, data)
    {
        var options = collectOptions(url, data, $.restSetup.verbs.retrieve);

        return $.ajax(options);
    }

    $.update = function (url, data)
    {
        var options = collectOptions(url, data, $.restSetup.verbs.update);

        return $.ajax(options);
    }

    $.delete = function (url, data)
    {
        var options = collectOptions(url, data, $.restSetup.verbs.delete);

        return $.ajax(options);
    }

    //  collect and fill options
    function collectOptions(url, data, verb)
    {
        var options = { /*dataType: 'json',*/ type: verb.toUpperCase() };

        if (data)
        {
            options.url = $.restSetup.rootUrl + fillUrl(url, data);

            options.data = data;
        }
        else
        {
            options.url = $.restSetup.rootUrl + url;
        }

        return options;
    }

    //  fill URL with data
    function fillUrl(url, data)
    {
        var key, u, val;

        for (key in data)
        {
            val = data[key];

            u = url.replace('{' + key + '}', val);

            if (u != url)
            {
                url = u;

                delete data[key];
            }
        }

        return url;
    }

})(jQuery);