

function ImportDetailModel(data)
{
    var self = this;

    self.url = 'api/Ledgers/' + data.account.id + '/import';

    self.transactions = '';

    self.account = data.account;

    self.ledger = data.ledger;

    self.start = start;

    ko.track(self, ['transactions']);

    function getFormElement()
    {
        var html = $('#import-form-body').html();

        var $html = $(html);

        $html.submit(function () { return false; });

        return $html.get(0);
    };

    function start()
    {
        var element = getFormElement();

        ko.applyBindings(this, element);

        var options = nbHelper.crudDialog('create', element);

        options.buttons.ok.callback = function ()
        {
            var dlg = this;

            var onSuccess = function (data)
            {
                ko.cleanNode(element);

                dlg.modal('hide');
            };

            var onError = function (error)
            {
                dlg.find('form').showErrors(error);
            };

            $.create(self.url, { transactions: self.transactions }).then(onSuccess, onError);

            return false;
        };

        options.buttons.cancel.callback = function () { ko.cleanNode(element); };

        bootbox.dialog(options);
    };
};