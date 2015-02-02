
(function ()
{
    'use strict';

    //	setup the application controllers for multiple and single interactions
    angular
        .module('app.categories')
        .controller('CategoriesController', CategoriesController)
        .controller('CategoryController', CategoryController);

    CategoriesController.$inject = ['CategoryFactory', '$routeParams', '$location', '$log'];
    CategoryController.$inject = ['AccountFactory', 'CategoryFactory', '$routeParams', '$scope', '$location', '$log'];

    function CategoriesController(CategoryFactory, $routeParams, $location, $log)
    {
        var vm = this;

        vm.updateSequences = updateSequences;
        
        var queryParams = { accountId: $routeParams.accountId };
        
        var assignData = function (data)
        {
            vm.account = data.account;
            vm.categories = data.categories;
            vm.hasData = true;
        };

        CategoryFactory.get(queryParams).$promise.then(assignData, handleQueryError);
        
        function handleQueryError(error)
        {
            vm.serverErrorSummary = NB.buildError(error);
            
            vm.hasData = true;
        }

        function updateSequences()
        {
            var pk = { accountId: $routeParams.accountId, action: 'sequences' };

            var ids = [];

            for (var key in vm.categories)
            {
                ids[ids.length] = vm.categories[key].id;
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

            var assignData = function (data)
            {
                vm.account = data;
                vm.hasData = true;
            };

            AccountFactory.get(pk).$promise.then(assignData, handleGetError);
        }
        else
        {
            var pk =
            {
                accountId: $routeParams.accountId,
                id: $routeParams.id
            };

            var assignData = function (data)
            {
                vm.account = data.account;
                vm.category = data.category;
                vm.hasData = true;
            };
            
            CategoryFactory.get(pk).$promise.then(assignData, handleGetError);
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
            $location.path('/categories/' + vm.account.id);
        }
        
        function handleSaveError(error)
        {
            NB.applyError(vm, $scope.categoryForm, error);
        }
        
        function handleGetError(error)
        {
            vm.serverErrorSummary[vm.serverErrorSummary.length] = NB.buildError(error);
            
            vm.hasData = true;
        }
    }

})();