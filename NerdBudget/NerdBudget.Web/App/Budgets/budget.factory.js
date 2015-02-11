
(function() {
	'use strict';

	//	setup the data service factory to call the ASP.NET API
	angular.module('app.budgets').factory('BudgetFactory', BudgetFactory);

	BudgetFactory.$inject = ['$resource'];
	
	function BudgetFactory($resource)
	{
		var url = '/api/budgets/'; 

		var pk = ':accountId/:id';
		
		var defaults = { accountId: '@accountId', id: '@id' };
		
		var actions =
		{
			query:		{ method: 'GET', isArray: true },
			get:		{ method: 'GET' },
			add:		{ method: 'POST' },
			update: 	{ method: 'PUT' },
			'delete':   { method: 'DELETE' },
			sequences:  { method: 'PUT', url: url + ':accountId/sequences' }
		};

		return $resource(url + pk, defaults, actions);
	}

})();