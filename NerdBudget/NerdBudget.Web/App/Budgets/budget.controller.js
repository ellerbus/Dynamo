
(function ()
{
    'use strict';

    //	setup the application controllers for multiple and single interactions
    angular
        .module('app.budgets')
        .controller('BudgetsController', BudgetsController)
        .controller('BudgetController', BudgetController);

    BudgetsController.$inject = ['BudgetFactory', '$routeParams', '$location', '$log'];
    BudgetController.$inject = ['BudgetFactory', '$routeParams', '$scope', '$location', '$log'];

    function BudgetsController(BudgetFactory, $routeParams, $location, $log)
    {
        var vm = this;

        var queryParams = { accountId: $routeParams.accountId };

        var assignData = function (data)
        {
            vm.account = data.account;
            vm.categories = data.categories;
            vm.hasData = true;
        };
        
        BudgetFactory.get(queryParams).$promise.then(assignData, handleQueryError);
        
        function handleQueryError(error)
        {
            vm.serverErrorSummary = NB.buildError(error);
            
            vm.hasData = true;
        }
    }

    function BudgetController(BudgetFactory, $routeParams, $scope, $location, $log)
    {
        var vm = this;

        vm.action = $routeParams.action;

        vm.save = save;

        var pk =
        {
            accountId: $routeParams.accountId,
            id: $routeParams.id || 'x'
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

            vm.hasData = true;
        };
            
        BudgetFactory.get(pk).$promise.then(assignData, handleGetError);

        function save(data)
        {
            if (vm.action == 'create')
            {
                BudgetFactory.save(data).$promise.then(handleSaveSuccess, handleSaveError);
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
            NB.applyError(vm, $scope.budgetForm, error);
        }
        
        function handleGetError(error)
        {
            vm.serverErrorSummary[vm.serverErrorSummary.length] = NB.buildError(error);
            
            vm.hasData = true;
        }
    }

})();