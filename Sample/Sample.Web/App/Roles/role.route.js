
(function() {
	'use strict';
	
	//	initialize the app for roles
	angular.modeule('app.roles', []);

	//	setup the application routing for roles
	angular.module('app.roles').config(RoleConfig);

	RoleConfig.$inject = ['$routeProvider'];
	
	function RoleConfig($routeProvider) {
		$routeProvider
			.when('/roles', {
				templateUrl: '/App/Roles/roles.html',
				controller: 'RolesController',
				controllerAs: 'vm'
			})
			.when('/role/:id', {
				templateUrl: '/App/Roles/role.html',
				controller: 'RoleController',
				controllerAs: 'vm'
			})
			.otherwise({
				redirectTo: '/roles'
			});
	};
})();