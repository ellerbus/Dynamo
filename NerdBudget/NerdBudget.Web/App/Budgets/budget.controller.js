
(function ()
{
    'use strict';

    //	setup the application controllers for multiple and single interactions
    angular
        .module('app.budgets')
        .controller('BudgetListController', BudgetListController)
        .controller('BudgetDetailController', BudgetDetailController);

    BudgetListController.$inject = ['BudgetFactory', '$routeParams', '$location', '$log'];
    BudgetDetailController.$inject = ['BudgetFactory', '$routeParams', '$scope', '$location', '$log'];

    function BudgetListController(BudgetFactory, $routeParams, $location, $log)
    {
        var vm = this;

        vm.hasData = function () { return typeof vm.account !== "undefined" };

        vm.updateSequences = updateSequences;

        var queryParams = { accountId: $routeParams.accountId };

        var assignData = function (data)
        {
            vm.account = data.account;
            vm.categories = data.categories;
        };

        var assignError = function (error)
        {
            NB.applyError(error, vm);
        };
        
        BudgetFactory.get(queryParams).$promise.then(assignData, assignError);

        function updateSequences()
        {
            var pk = { accountId: $routeParams.accountId };

            var ids = [];

            for (var kc in vm.categories)
            {
                var cat = vm.categories[kc];

                for (var kb in cat.budgets)
                {
                    ids[ids.length] = cat.budgets[kb].id;
                }
            }

            BudgetFactory.sequences(pk, ids).$promise.then(function () { }, assignError);
        }
    }

    function BudgetDetailController(BudgetFactory, $routeParams, $scope, $location, $log)
    {
        var vm = this;

        vm.hasData = function () { return typeof vm.budget !== "undefined" };

        vm.action = $routeParams.action;

        vm.save = save;

        var pk =
        {
            accountId: $routeParams.accountId,
            id: $routeParams.id || 'xx'
        };

        var assignData = function (data)
        {
            vm.account = data.account;
            vm.categories = data.categories;
            vm.budget = data.budget;
            vm.frequencies = data.frequencies;

            if (vm.action == 'create')
            {
                vm.budget.categoryId = $routeParams.categoryId;
            }
        };
            
        BudgetFactory.get(pk).$promise.then(assignData, handleGetError);

        function save(data)
        {
            if (vm.action == 'create')
            {
                BudgetFactory.add(data).$promise.then(handleSaveSuccess, handleSaveError);
            }
            else
            {
                var pk =
                {
                    accountId: $routeParams.accountId,
                    id: $routeParams.id
                };

                if (vm.action == 'update')
                {
                    BudgetFactory.update(pk, data).$promise.then(handleSaveSuccess, handleSaveError);
                }
                else if (vm.action == 'delete')
                {
                    BudgetFactory.delete(pk).$promise.then(handleSaveSuccess, handleSaveError);
                }
            }
        }
        
        function handleSaveSuccess(data)
        {
            $location.path('/budgets/' + $routeParams.accountId);
        }
        
        function handleSaveError(error)
        {
            NB.applyError(error, vm, $scope.budgetForm);
        }
        
        function handleGetError(error)
        {
            NB.applyError(error, vm);
        }
    }

})();