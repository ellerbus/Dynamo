
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
    LedgerDetailController.$inject = ['LedgerFactory', '$routeParams', '$scope', '$filter', '$location', '$log'];

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
                $location.path('/ledger/update/' + $routeParams.accountId + '/map');
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
            vm.transactions = '';
        }

        function getError(error)
        {
            NB.applyError(error, vm);
        }
    }

    //
    //	Details Controller
    //
    function LedgerDetailController(LedgerFactory, $routeParams, $scope, $filter, $location, $log)
    {
        //	member variables
        
        var vm = this;

        vm.serverErrors = {};
        
        var pk =
        {
            accountId: $routeParams.accountId,
            id: $routeParams.id,
            date: $routeParams.date
        };
            
        LedgerFactory.get(pk).$promise.then(getSuccess, getError);
        
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
            var pk =
            {
                accountId: $routeParams.accountId,
                id: data.id,
                date: $filter('date')(new Date(data.date), 'yyyy-MM-dd', 'UTC')
            };

            LedgerFactory.update(pk, data).$promise.then(getSuccess, saveError);
        }

        function saveError(error)
        {
            NB.applyError(error, vm, $scope.ledgerForm);
        }
        
        function getSuccess(data)
        {
            if (data && data.mappingComplete)
            {
                $location.path('/budgets/' + data.account.id);
            }
            else
            {
                vm.account = data.account;
                vm.budgets = data.budgets;
                vm.ledger = data.ledger;
            }
        }
        
        function getError(error)
        {
            NB.applyError(error, vm);
        }
    }

})();