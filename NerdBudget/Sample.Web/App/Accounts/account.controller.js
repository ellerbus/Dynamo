
(function ()
{
	'use strict';

	//	setup the application controllers for multiple and single interactions
	angular
		.module('app.accounts')
		.controller('AccountsController', AccountsController)
		.controller('AccountController', AccountController);

	AccountsController.$inject = ['AccountDataService', '$log'];
	AccountController.$inject = ['AccountDataService', 'action', '$scope', '$parse', '$routeParams', '$location', '$log'];

	function AccountsController(accountDataService, $log)
	{
		var vm = this;
		
		vm.hasData = false;

		accountDataService
			.getAccounts()
			.then(function (data)
			{
				vm.accounts = data;
				
				vm.hasData = true;
			});
	}

	function AccountController(accountDataService, action, $scope, $parse, $routeParams, $location, $log)
	{
		var vm = this;
		
		vm.hasData = false;
		
		vm.save = save;
		
		if (action == 'create')
		{
			vm.hasData = true;

			vm.account = {
				id: '',
				name: '',
				type: '',
				startedAt: '',
				createdAt: '',
				updatedAt: ''
			};
		}
		else
		{
			accountDataService
				.getAccount($routeParams.id)
				.then(function (data)
				{
					vm.account = data;
					
					vm.hasData = true;
				});
		}
		
		function save(data)
		{
			switch(action)
			{
				case 'create': createAccount(data); break;
				case 'update': updateAccount(data); break;
				case 'delete': deleteAccount(data); break;
			}
		}

		function createAccount(data)
		{
			accountDataService
				.insertAccount(data)
				.then(function (data)
				{
					if (data)
					{
						assignErrors(data);
					}
					else
					{
						$location.path('#/accounts');
					}
				});
		};
			
		function updateAccount(data)
		{
			accountDataService
				.updateAccount($routeParams.id, data)
				.then(function (data)
				{
					if (data)
					{
						assignErrors(data);
					}
					else
					{
						$location.path('#/accounts');
					}
				});
		};
			
		function deleteAccount()
		{
			accountDataService
				.deleteAccount($routeParams.id)
				.then(function () { $location.path('#/accounts'); });
		};
		
		function assignErrors(data)
		{
			$scope.accountForm.$setPristine();
			
			for (var nm in vm.account)
			{
				var serverMessage = $parse('accountForm.' + nm + '.$error.serverMessage');

				$scope.accountForm.$setValidity(nm, true, $scope.accountForm);
				
				serverMessage.assign($scope, null);
			}

			if (data && data.modelState)
			{
				for (var key in data.modelState)
				{
					var message = data.modelState[key];

					var serverMessage = $parse('accountForm.' + key + '.$error.serverMessage');

					$scope.accountForm.$setValidity(key, false, $scope.accountForm);

					serverMessage.assign($scope, message);
				}
			}
		}
	}
})();