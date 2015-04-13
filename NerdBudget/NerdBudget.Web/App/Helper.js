var nbHelper = {
    sortable: function (e, tr)
    {
        var $originals = tr.children();
        var $helper = tr.clone();
        $helper.children().each(function (index)
        {
            // Set helper cell sizes to match the original sizes
            $(this).width($originals.eq(index).width());
        });
        return $helper;
    },
    overlay: function (source, target)
    {
        for (var key in source)
        {
            if (target[key])
            {
                target[key] = source[key];
            }
        }
    },
    crudDialog: function (crud, element)
    {
        var setup = {
            'create': { label: 'Create', className: 'btn-success' },
            'update': { label: 'Update', className: 'btn-warning' },
            'delete': { label: 'Delete', className: 'btn-danger' }
        };

        var item = setup[crud];

        var options = {
            title: item.label,
            message: element,
            buttons: {
                ok: {
                    label: item.label,
                    className: item.className,
                    callback: null
                },
                cancel: {
                    label: 'Cancel',
                    className: 'btn-default',
                    callback: null
                }
            }
        };

        return options;
    }
};

$(function ()
{
    $('[nb-create-icon]').addClass('btn btn-default btn-sm').html('<i class="fa fa-plus fa-fw text-success"></i>');
    $('[nb-update-icon]').addClass('btn btn-default btn-sm').html('<i class="fa fa-pencil fa-fw text-primary"></i>');
    $('[nb-delete-icon]').addClass('btn btn-default btn-sm').html('<i class="fa fa-times fa-fw text-danger"></i>');
    
    $('[nb-list-icon]').addClass('btn btn-default btn-sm').html('<i class="fa fa-list fa-fw"></i>');
    $('[nb-dollar-icon]').addClass('btn btn-default btn-sm').html('<i class="fa fa-usd fa-fw text-success"></i>');
    $('[nb-import-icon]').addClass('btn btn-default btn-sm').html('<i class="fa fa-download fa-fw"></i>');
    $('[nb-analysis-icon]').addClass('btn btn-default btn-sm').html('<i class="fa fa-cogs fa-fw"></i>');

    ko.filters.fixed = function (value, n)
    {
        if (n)
        {
            return ko.filters.number(value.toFixed(n));
        }

        return ko.filters.number(value.toFixed(0));
    };

    ko.filters.date = function (value, format)
    {
        if (format)
        {
            return moment(value).format(format);
        }

        return moment(value).format('MM/DD/YYYY');
    };
});


/*
 *  Attach errors to form
 */

(function ($)
{
    'use strict';

    $.fn.disableAll = function ()
    {
        var el = this;

        el.find("input:not(:disabled), select:not(:disabled), textarea:not(:disabled)").prop("disabled", true);

        return el;
    };

    $.fn.hideErrors = function ()
    {
        var el = this;

        el.find("div.alert-danger").hide();

        el.find('div.form-group').removeClass('has-error');

        return el;
    };

    $.fn.showErrors = function (jqXHR, textStatus, errorThrown)
    {
        var res = jqXHR.responseJSON;

        var el = this;

        var $error = el.find("div.alert-danger");

        if ($error.length == 0)
        {
            var html = '<div class="form-group alert alert-danger">' +
                '<ul class="col-sm-offset-4 col-sm-8"></ul>' +
                '</div>';

            $error = $(html);

            el.prepend($error);
        }

        $error.find("ul").empty();

        var errors = [];

        if (jqXHR.status == 500)
        {
            if (res)
            {
                if (res.message) errors.push(res.message);
                if (res.exceptionMessage) errors.push(res.exceptionMessage);
            }
        }
        
        if (jqXHR.status == 400)
        {
            if (res)
            {
                if (res.message) errors.push(res.message);

                if (res.modelState)
                {
                    el.find('div.form-group').removeClass('has-error');

                    for (var key in res.modelState)
                    {
                        errors.push(res.modelState[key]);

                        el.find('[name=' + key + ']').closest('div.form-group').addClass('has-error');
                    }
                }
            }
        }

        if (errors.length == 0)
        {
            if (jqXHR.status != 200)
            {
                var message = '[' + jqXHR.status + '] ' + jqXHR.statusText;

                errors.push(message);
            }
        }

        if (errors.length > 0)
        {
            var html = '';

            for (var key in errors)
            {
                html += '<li>' + errors[key] + '</li>';
            }

            $error.find("ul").html(html);
        }

        $error.show();

        return el;
    };

})(jQuery);

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
        baseUrl: '',
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
            options.url = $.restSetup.baseUrl + fillUrl(url, data);

            options.data = data;
        }
        else
        {
            options.url = $.restSetup.baseUrl + url;
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