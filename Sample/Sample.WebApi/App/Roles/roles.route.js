
(function() {
	'use strict';
	
	angular.module('app').config(RolesConfig);

	RolesConfig.$inject = ['ngRoute'];
	
	function RolesConfig($routeProvider) {
		$routeProvider
			.when('/', {
				templateUrl: 'roles.html',
				controller: 'RolesController',
				controllerAs: 'vm'
			})
			.when('/role/:id', {
				templateUrl: 'role.html',
				controller: 'RoleController',
				controllerAs: 'vm'
			})
			.otherwise({
				redirectTo: '/'
			});
	};
})();