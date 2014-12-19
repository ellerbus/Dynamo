
(function() {
	'use strict';
	
	angular.module('app').config(MemberRolesConfig);

	MemberRolesConfig.$inject = ['ngRoute'];
	
	function MemberRolesConfig($routeProvider) {
		$routeProvider
			.when('/', {
				templateUrl: 'memberRoles.html',
				controller: 'MemberRolesController',
				controllerAs: 'vm'
			})
			.when('/memberRole/:id', {
				templateUrl: 'memberRole.html',
				controller: 'MemberRoleController',
				controllerAs: 'vm'
			})
			.otherwise({
				redirectTo: '/'
			});
	};
})();