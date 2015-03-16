

(function() {
	'use strict';
	
	//	initialize the application for ledgers
	angular.module('app.ledgers', []);
	
	//	setup the application routing for ledgers
	angular.module('app.ledgers').config(LedgerConfig);

	LedgerConfig.$inject = ['$routeProvider'];
	
	function LedgerConfig($routeProvider)
	{
	    $routeProvider
			//.when('/ledger/history/:accountId/:startDate/:endDate/:budgetId', {
			//    templateUrl: '/App/Ledgers/ledgers.html',
			//    controller: 'LedgerListController',
			//    controllerAs: 'vm'
			//})
		    .when('/ledger/import/:accountId', {
		        templateUrl: '/App/Ledgers/ledger-import.html',
		        controller: 'LedgerImportController',
		        controllerAs: 'vm'
		    })
			.when('/ledger/update/:accountId/:id/:date?', {
			    templateUrl: '/App/Ledgers/ledger.html',
	            controller: 'LedgerDetailController',
			    controllerAs: 'vm'
			});
	};

})();