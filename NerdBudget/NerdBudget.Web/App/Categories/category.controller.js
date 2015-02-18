
(function ()
{
    'use strict';

    //	setup the application controllers for multiple and single interactions
    angular
        .module('app.categories')
        .controller('CategoryListController', CategoryListController)
        .controller('CategoryDetailController', CategoryDetailController);

    CategoryListController.$inject = ['CategoryFactory', '$routeParams', '$location', '$log'];
    CategoryDetailController.$inject = ['AccountFactory', 'CategoryFactory', '$routeParams', '$scope', '$location', '$log'];

    function CategoryListController(CategoryFactory, $routeParams, $location, $log)
    {
        var vm = this;

        vm.hasData = function () { return typeof vm.categories !== "undefined" };

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

        CategoryFactory.get(queryParams).$promise.then(assignData, assignError);

        function updateSequences()
        {
            var pk = { accountId: $routeParams.accountId };

            var ids = [];

            for (var key in vm.categories)
            {
                ids[ids.length] = vm.categories[key].id;
            }

            CategoryFactory.sequences(pk, ids).$promise.then(function () { }, assignError);
        }
    }

    function CategoryDetailController(AccountFactory, CategoryFactory, $routeParams, $scope, $location, $log)
    {
        var vm = this;

        vm.hasData = function () { return typeof vm.category !== "undefined" };
        
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
                vm.category = { accountId: vm.account.id };
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
            };
            
            CategoryFactory.get(pk).$promise.then(assignData, handleGetError);
        }

        function save(data)
        {
            if (vm.action == 'create')
            {
                CategoryFactory.add(data).$promise.then(handleSaveSuccess, handleSaveError);
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
            NB.applyError(error, vm, $scope.categoryForm);
        }
        
        function handleGetError(error)
        {
            NB.applyError(error, vm);
        }
    }

})();