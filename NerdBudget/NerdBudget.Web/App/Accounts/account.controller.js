
(function ()
{
	'use strict';

	//	setup the application controllers for multiple and single interactions
	angular
		.module('app.accounts')
		.controller('AccountsController', AccountsController)
		.controller('AccountController', AccountController);

	AccountsController.$inject = ['AccountFactory', '$log'];
	AccountController.$inject = ['AccountFactory', '$routeParams', '$scope', '$location', '$log'];

	function AccountsController(AccountFactory, $log)
	{
	    var vm = this;
		
	    var queryParams = {};

	    var assignData = function (data)
	    {
	        vm.accounts = data;
	        vm.hasData = true;
	    };

	    AccountFactory.query(queryParams).$promise.then(assignData, handleQueryError);
		
		function handleQueryError(error)
		{
		    vm.serverErrorSummary = NB.buildError(error);
			
			vm.hasData = true;
		}
	}

	function AccountController(AccountFactory, $routeParams, $scope, $location, $log)
	{
	    var vm = this;
		
		vm.action = $routeParams.action;
		
		vm.save = save;
		
		if (vm.action == 'create')
		{
		    vm.account = {};
				
			vm.hasData = true;
		}
		else
		{
			var pk =
			{
				id: $routeParams.id
			};

			var assignData = function (data)
			{
			    vm.account = data;
			    vm.hasData = true;
			};
			
			AccountFactory.get(pk).$promise.then(assignData, handleGetError);
		}

		function save(data)
		{
			if (vm.action == 'create')
			{
				AccountFactory.save(data).$promise.then(handleSaveSuccess, handleSaveError);
			}
			else
			{
				var pk =
				{
					id: $routeParams.id
				};

				if (vm.action == 'update')
				{
					AccountFactory.update(pk, data).$promise.then(handleSaveSuccess, handleSaveError);
				}
				else if (vm.action == 'delete')
				{
					AccountFactory.delete(pk).$promise.then(handleSaveSuccess, handleSaveError);
				}
			}
		}
		
		function handleSaveSuccess(data)
		{
		    if (vm.action == 'delete')
		    {
		        $location.path('/accounts/');
		    }
		    else
		    {
		        $location.path('/categories/' + data.id);
		    }
		}
		
		function handleSaveError(error)
		{
		    NB.applyError(vm, $scope.accountForm, error);
		}
		
		function handleGetError(error)
		{
		    if (typeof vm.serverErrorSummary === 'undefined')
		    {
		        vm.serverErrorSummary = [];
		    }

		    vm.serverErrorSummary[vm.serverErrorSummary.length] = NB.buildError(error);
			
			vm.hasData = true;
		}
	}

})();