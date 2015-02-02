
(function() {
	'use strict';

	//	setup the data service factory to call the ASP.NET API
	angular.module('app.budgets').factory('BudgetFactory', BudgetFactory);

	BudgetFactory.$inject = ['$resource'];
	
	function BudgetFactory($resource)
	{
		var pkPattern = ':accountId/:id/:action';
		
		var pkInputs = { accountId: '@accountId', id: '@id' };
		
		var cfg =
		{
			update: { method: 'PUT' }
		};

		return $resource('/api/budgets/' + pkPattern, pkInputs, cfg);
	}

})();