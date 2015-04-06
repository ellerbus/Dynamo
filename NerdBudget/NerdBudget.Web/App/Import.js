function ImportModel(data)
{
    var self = this;

    self.transactions = '';

    ko.track(self);
};

$(function ()
{
    var im = $('#import-form');

    bootbox.dialog({
        message: im.get(0),
        title: "Custom title",
        buttons: {
            success: {
                label: "Success!",
                className: "btn-success",
                callback: function (a)
                {
                    var dlg = this;

                    alert("great success");

                    var os = function ()
                    {
                    };

                    var err = function ()
                    {
                        dlg.modal('hide');
                    };

                    var url = 'api/Accounts';

                    $.create(url, { name: '' }).then(os, err);

                    return false;
                }
            },
            danger: {
                label: "Danger!",
                className: "btn-danger",
                callback: function ()
                {
                    alert("uh oh, look out!");
                }
            },
            main: {
                label: "Click ME!",
                className: "btn-primary",
                callback: function ()
                {
                    alert("Primary button");

                    return false;
                }
            }
        }
    });
});