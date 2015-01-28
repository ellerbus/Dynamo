
(function ()
{
    'use strict';

    //	setup the application controllers for multiple and single interactions
    angular
        .module('app.categories')
        .controller('CategoriesController', CategoriesController)
        .controller('CategoryController', CategoryController);

    CategoriesController.$inject = ['AccountFactory', 'CategoryFactory', '$routeParams', '$location', '$log'];
    CategoryController.$inject = ['AccountFactory', 'CategoryFactory', '$routeParams', '$scope', '$location', '$log'];

    function CategoriesController(AccountFactory, CategoryFactory, $routeParams, $location, $log)
    {
        var vm = this;

        vm.updateSequences = updateSequences;
        
        var categoryParams = { accountId: $routeParams.accountId };

        CategoryFactory.get(categoryParams).$promise.then(handleQuerySuccess, handleQueryError);
        
        function handleQuerySuccess(data)
        {
            vm.account = data;
            
            vm.hasData = true;
        }
        
        function handleQueryError(error)
        {
            vm.serverErrorSummary = NB.buildError(error);
            
            vm.hasData = true;
        }

        function updateSequences()
        {
            var pk = { accountId: $routeParams.accountId, action: 'sequences' };

            var ids = [];

            for (var key in vm.account.categories)
            {
                ids[ids.length] = vm.account.categories[key].id;
            }

            CategoryFactory.update(pk, ids).$promise.then(function () { }, handleQueryError);
        }
    }

    function CategoryController(AccountFactory, CategoryFactory, $routeParams, $scope, $location, $log)
    {
        var vm = this;
        
        vm.action = $routeParams.action;
        
        vm.save = save;
        
        if (vm.action == 'create')
        {
            var pk =
            {
                id: $routeParams.accountId
            };

            var account = null;

            AccountFactory.get(pk).$promise.then(function (data)
            {
                account = data;

                handleGetSuccess({
                    account: account,
                    accountId: $routeParams.accountId,
                    name: ''
                });
                
                vm.hasData = true;
            }, handleGetError);
        }
        else
        {
            var pk =
            {
                accountId: $routeParams.accountId,
                id: $routeParams.id
            };
            
            CategoryFactory.get(pk).$promise.then(handleGetSuccess, handleGetError);
        }

        function save(data)
        {
            if (vm.action == 'create')
            {
                CategoryFactory.save(data).$promise.then(handleSaveSuccess, handleSaveError);
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
                    CategoryFactory.update(pk, data).$promise.then(handleSaveSuccess, handleSaveError);
                }
                else if (vm.action == 'delete')
                {
                    CategoryFactory.delete(pk).$promise.then(handleSaveSuccess, handleSaveError);
                }
            }
        }
        
        function handleSaveSuccess(data)
        {
            if (data && data.accountId)
            {
                $location.path('/categories/' + data.accountId);
            }
            else
            {
                $location.path('/categories/' + vm.category.account.id);
            }
        }
        
        function handleSaveError(error)
        {
            NB.applyError(vm, $scope.categoryForm, error);
        }
        
        function handleGetSuccess(data)
        {
            vm.category = data;
            
            vm.hasData = true;
        }
        
        function handleGetError(error)
        {
            vm.serverErrorSummary[vm.serverErrorSummary.length] = NB.buildError(error);
            
            vm.hasData = true;
        }
    }

})();