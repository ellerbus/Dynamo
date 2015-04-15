function ImportListModel(data)
{
    var self = this;

    self.url = 'api/Ledgers/' + data.account.id;

    self.ledger = null;

    self.budgets = data.budgets;

    self.mapping = null;

    self.loadMap = loadMap;

    self.mapIt = mapIt;

    ko.track(self);
    
    function mapIt(d)
    {
        self.ledger.date = moment(self.ledger.date).format('YYYY-MM-DD');

        self.ledger.budgetId = d.id;

        var onSuccess = function ()
        {
            self.loadMap();
        };

        var onError = function (error)
        {
            $('#ledgers form').showErrors(error);
        };

        $.update(self.url + '/{id}/{date}', self.ledger).then(onSuccess, onError);
    };

    function loadMap()
    {
        self.mapping = true;

        var onSuccess = function (data)
        {
            if (data.status && data.status == 302)
            {
                window.location.replace(data.url);

                return;
            }

            self.ledger = data;
        };

        var onError = function (error)
        {
            $('#ledgers form').showErrors(error);
        };

        $.retrieve(self.url + '/map').then(onSuccess, onError);
    };
};

function ImportDetailModel(data)
{
    var self = this;

    self.url = 'api/Ledgers/' + data.account.id;

    self.transactions = '';

    self.account = data.account;

    self.ledger = data.ledger;

    self.start = start;

    ko.track(self, ['account', 'ledger', 'transactions']);

    function getFormElement()
    {
        var html = $('#import-form-body').html();

        var $html = $(html);

        $html.submit(function () { return false; });

        return $html.get(0);
    };

    function start(lvm)
    {
        var element = getFormElement();

        ko.applyBindings(this, element);

        var options = nbHelper.crudDialog('create', element);

        options.buttons.ok.label = 'Import';

        options.buttons.ok.callback = function ()
        {
            var dlg = this;

            var onSuccess = function (data)
            {
                ko.cleanNode(element);

                lvm.loadMap();

                dlg.modal('hide');
            };

            var onError = function (error)
            {
                dlg.find('form').showErrors(error);
            };

            $.create(self.url + '/import', { transactions: self.transactions }).then(onSuccess, onError);

            return false;
        };

        options.buttons.cancel.callback = function ()
        {
            ko.cleanNode(element);

            lvm.loadMap();
        };

        bootbox.dialog(options);
    };
};