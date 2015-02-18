
(function ()
{
	'use strict';

	//	setup the application controllers for multiple and single interactions
	angular
		.module('app.accounts')
		.controller('AccountListController', AccountListController)
		.controller('AccountDetailController', AccountDetailController);

	AccountListController.$inject = ['AccountFactory', '$log'];
	AccountDetailController.$inject = ['AccountFactory', '$routeParams', '$scope', '$location', '$log'];

	//
	//	List Controller
	//
	function AccountListController(AccountFactory, $log)
	{
		//	member variables
		var vm = this;
		
		var queryParams = {};

		AccountFactory.query(queryParams).$promise.then(querySuccess, queryError);
		
		//	public methods (via VM - View Model)
		
		vm.hasData = hasData;
		
		//	private methods (of the controller)
		
		function hasData()
		{
			return typeof vm.accounts != 'undefined';
		}
		
		function querySuccess(data)
		{
			vm.accounts = data;
		}
		
		function queryError(error)
		{
			NB.applyError(error, vm);
		}
	}

	//
	//	Details Controller
	//
	function AccountDetailController(AccountFactory, $routeParams, $scope, $location, $log)
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
				id: $routeParams.id
			};
			
			AccountFactory.get(pk).$promise.then(getSuccess, getError);
		}
		
		//	public methods (via VM - View Model)
		
		vm.hasData = hasData;

		vm.save = save;
		
		//	private methods (of the controller)
		
		function hasData()
		{
			return typeof vm.account != 'undefined';
		}

		function save(data)
		{
			if (vm.action == 'create')
			{
				AccountFactory.add(data).$promise.then(saveSuccess, saveError);
			}
			else
			{
				var pk =
				{
					id: $routeParams.id
				};

				if (vm.action == 'update')
				{
					AccountFactory.update(pk, data).$promise.then(saveSuccess, saveError);
				}
				else if (vm.action == 'delete')
				{
					AccountFactory.delete(pk).$promise.then(saveSuccess, saveError);
				}
			}
		}
		
		function saveSuccess(data)
		{
			$location.path('/accounts');
		}
		
		function saveError(error)
		{
			NB.applyError(error, vm, $scope.accountForm);
		}
		
		function getSuccess(data)
		{
			vm.account = data;
		}
		
		function getError(error)
		{
		    NB.applyError(error, vm);
		}
	}

})();