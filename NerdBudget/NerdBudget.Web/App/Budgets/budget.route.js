

(function() {
	'use strict';
	
	//	initialize the application for budgets
	angular.module('app.budgets', []);
	
	//	setup the application routing for budgets
	angular.module('app.budgets').config(BudgetConfig);

	BudgetConfig.$inject = ['$routeProvider'];
	
	function BudgetConfig($routeProvider)
	{
		$routeProvider
			.when('/budgets/:accountId', {
				templateUrl: '/App/Budgets/budgets.html',
				controller: 'BudgetListController',
				controllerAs: 'vm'
			})
			.when('/budget/:action/:accountId/:id?', {
				templateUrl: '/App/Budgets/budget.html',
				controller: 'BudgetDetailController',
				controllerAs: 'vm'
			});
	};

})();