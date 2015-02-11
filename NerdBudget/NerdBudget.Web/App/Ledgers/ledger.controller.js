
(function ()
{
	'use strict';
	
	function buildGenericError(error)
	{
		var msg = "";
		
		if (error.data && error.data.message)
		{
			msg = error.data.message;
		}
		
		if (error.data && error.data.exceptionMessage)
		{
			msg += ": " + error.data.exceptionMessage;
		}
		
		if (msg === "")
		{
			msg = "An unexpected error occurred - dang it!";
		}
		
		return msg;
	}

	//	setup the application controllers for multiple and single interactions
	angular
		.module('app.ledgers')
		.controller('LedgersController', LedgersController)
		.controller('LedgerController', LedgerController);

	LedgersController.$inject = ['LedgerFactory', '$log'];
	LedgerController.$inject = ['LedgerFactory', '$routeParams', '$scope', '$location', '$log'];

	function LedgersController(LedgerFactory, $log)
	{
		var vm = this;
		
		vm.hasData = false;
		
		vm.serverErrorSummary = "";
		
		var queryParams = {};

		LedgerFactory.query(queryParams).$promise.then(handleQuerySuccess, handleQueryError);
		
		function handleQuerySuccess(data)
		{
			vm.ledgers = data;
			
			vm.hasData = true;
		}
		
		function handleQueryError(error)
		{
			vm.serverErrorSummary = buildGenericError(error);
			
			vm.hasData = true;
		}
	}

	function LedgerController(LedgerFactory, $routeParams, $scope, $location, $log)
	{
		var vm = this;
		
		vm.hasData = false;
			
		vm.serverErrorSummary = [];

		vm.serverErrors = {};
		
		vm.action = $routeParams.action;
		
		vm.save = save;
		
		if (vm.action == 'create')
		{
			handleGetSuccess({ });
				
			vm.hasData = true;
		}
		else
		{
			var pk =
			{
				accountId: $routeParams.accountId,
				id: $routeParams.id,
				date: $routeParams.date
			};
			
			LedgerFactory.get(pk).$promise.then(handleGetSuccess, handleGetError);
		}

		function save(data)
		{
			if (vm.action == 'create')
			{
				LedgerFactory.add(data).$promise.then(handleSaveSuccess, handleSaveError);
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
					LedgerFactory.update(pk, data).$promise.then(handleSaveSuccess, handleSaveError);
				}
				else if (vm.action == 'delete')
				{
					LedgerFactory.delete(pk).$promise.then(handleSaveSuccess, handleSaveError);
				}
			}
		}
		
		function handleSaveSuccess(data)
		{
			$location.path('/ledgers');
		}
		
		function handleSaveError(error)
		{
		    var d = error.data;
			
			vm.serverErrorSummary = [];

		    for (var prop in vm.user)
		    {
		        if ($scope.ledgerForm[prop])
		        {
		            $scope.ledgerForm[prop].$setValidity('server', true);
		        }
		    }

		    if (d && d.modelState)
		    {
		        for (var key in d.modelState)
		        {
		            var msg = d.modelState[key];

		            if ($scope.ledgerForm[key])
		            {
		                $scope.ledgerForm[key].$setValidity('server', false);
					
						var x = Array.isArray(msg) ? msg.join(' ') : msg;

		                vm.serverErrors[key] = x;
		            }

					if (Array.isArray(msg))
					{
						for (var msgKey in msg)
						{
							vm.serverErrorSummary[vm.serverErrorSummary.length] = msg[msgKey];
						}
					}
					else
					{
						vm.serverErrorSummary[vm.serverErrorSummary.length] = msg;
					}
		        }
		    }
			else
			{
				vm.serverErrorSummary[vm.serverErrorSummary.length] = buildGenericError(error);
			}
		}
		
		function handleGetSuccess(data)
		{
			vm.ledger = data;
			
			vm.hasData = true;
		}
		
		function handleGetError(error)
		{
			vm.serverErrorSummary[vm.serverErrorSummary.length] = buildGenericError(error);
			
			vm.hasData = true;
		}
	}

})();