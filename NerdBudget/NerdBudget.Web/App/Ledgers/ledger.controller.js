
(function ()
{
    'use strict';
    
    //	setup the application controllers for multiple and single interactions
    angular
        .module('app.ledgers')
        .controller('LedgerListController', LedgerListController)
        .controller('LedgerImportController', LedgerImportController)
        .controller('LedgerDetailController', LedgerDetailController);

    LedgerListController.$inject = ['LedgerFactory', '$log'];
    LedgerImportController.$inject = ['LedgerFactory', '$routeParams', '$scope', '$location', '$log'];
    LedgerDetailController.$inject = ['LedgerFactory', '$routeParams', '$scope', '$location', '$log'];

    //
    //	List Controller
    //
    function LedgerListController(LedgerFactory, $log)
    {
        //	member variables
        var vm = this;
        
        var queryParams = {};

        LedgerFactory.query(queryParams).$promise.then(querySuccess, queryError);
        
        //	public methods (via VM - View Model)
        
        vm.hasData = hasData;
        
        //	private methods (of the controller)
        
        function hasData()
        {
            return typeof vm.ledgers != 'undefined';
        }
        
        function querySuccess(data)
        {
            vm.ledgers = data;
        }
        
        function queryError(error)
        {
            NB.applyError(error, vm);
        }
    }

    //
    //	Import Controller
    //
    function LedgerImportController(LedgerFactory, $routeParams, $scope, $location, $log)
    {
        //	member variables

        var vm = this;

        vm.transactions = '02/09/2015	NN NNNNN 9999 NNN# 9999993 99999 NNNNNNN NNNN	$1,499.99		$1,138.52	NNNNNNNNNNN NNNNNNN';
        //02/09/2015	NNNNNNNN'N N99999 NNNNNNN NN 02/08/15 NNN 9999	$9.82		$2,538.52	NNNNNNNNNNN NNNNNNN
        //02/09/2015	NNNNNNNN : NNNNNNNN NN: 9999999999NN: NNNNNNNN NNN NNNNN 999999999999999		$1,406.99	$2,548.34	NNNNNNNNNNN NNNNNNN';

        var pk = { accountId: $routeParams.accountId };

        LedgerFactory.getImport(pk).$promise.then(getSuccess, getError);

        //	public methods (via VM - View Model)

        vm.hasData = hasData;

        vm.import = importTransactions;

        //	private methods (of the controller)

        function hasData()
        {
            return typeof vm.account != 'undefined';
        }

        function importTransactions(data)
        {
            var success = function (data)
            {
                $location.path('/accounts');
            };

            var error = function (error)
            {
                NB.applyError(error, vm);
            };

            var pk = { accountId: $routeParams.accountId };

            LedgerFactory.import(pk, '"' + data + '"').$promise.then(success, error);
        };

        function getSuccess(data)
        {
            vm.account = data.account;
            vm.ledger = data.ledger;
        }

        function getError(error)
        {
            NB.applyError(error, vm);
        }
    }

    //
    //	Details Controller
    //
    function LedgerDetailController(LedgerFactory, $routeParams, $scope, $location, $log)
    {
        //	member variables
        
        var vm = this;

        vm.serverErrors = {};
        
        vm.action = $routeParams.action;

        if (vm.action == 'create')
        {
            getSuccess({ });
        }
        else
        {
            var pk =
            {
                accountId: $routeParams.accountId,
                id: $routeParams.id,
                date: $routeParams.date
            };
            
            LedgerFactory.get(pk).$promise.then(getSuccess, getError);
        }
        
        //	public methods (via VM - View Model)
        
        vm.hasData = hasData;

        vm.save = save;
        
        //	private methods (of the controller)
        
        function hasData()
        {
            return typeof vm.ledger != 'undefined';
        }

        function save(data)
        {
            if (vm.action == 'create')
            {
                LedgerFactory.add(data).$promise.then(saveSuccess, saveError);
            }
            else
            {
                var pk =
                {
                    accountId: $routeParams.accountId,
                    id: $routeParams.id,
                    date: $routeParams.date
                };

                if (vm.action == 'update')
                {
                    LedgerFactory.update(pk, data).$promise.then(saveSuccess, saveError);
                }
                else if (vm.action == 'delete')
                {
                    LedgerFactory.delete(pk).$promise.then(saveSuccess, saveError);
                }
            }
        }
        
        function saveSuccess(data)
        {
            $location.path('/ledgers');
        }
        
        function saveError(error)
        {
            NB.applyError(error, vm, $scope.ledgerForm);
        }
        
        function getSuccess(data)
        {
            vm.ledger = data;
        }
        
        function getError(error)
        {
            NB.applyError(error, vm);
        }
    }

})();