

(function() {
	'use strict';
	
	//	initialize the application for accounts
	angular.module('app.accounts', []);
	
	//	setup the application routing for accounts
	angular.module('app.accounts').config(AccountConfig);

	AccountConfig.$inject = ['$routeProvider'];
	
	function AccountConfig($routeProvider)
	{
	    $routeProvider
			.when('/accounts', {
			    templateUrl: '/App/Accounts/accounts.html',
			    controller: 'AccountListController',
			    controllerAs: 'vm'
			})
			.when('/account/:action/:id?', {
			    templateUrl: '/App/Accounts/account.html',
			    controller: 'AccountDetailController',
			    controllerAs: 'vm'
			})
			.otherwise({
			    templateUrl: '/App/Accounts/accounts.html',
			    controller: 'AccountListController',
			    controllerAs: 'vm'
			});
	};

})();