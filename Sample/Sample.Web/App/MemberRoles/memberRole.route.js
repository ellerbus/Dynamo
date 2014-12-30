
(function() {
	'use strict';
	
	//	initialize the app for memberRoles
	angular.modeule('app.memberRoles', []);

	//	setup the application routing for memberRoles
	angular.module('app.memberRoles').config(MemberRoleConfig);

	MemberRoleConfig.$inject = ['$routeProvider'];
	
	function MemberRoleConfig($routeProvider) {
		$routeProvider
			.when('/memberRoles', {
				templateUrl: '/App/MemberRoles/memberRoles.html',
				controller: 'MemberRolesController',
				controllerAs: 'vm'
			})
			.when('/memberRole/:memberId:roleId', {
				templateUrl: '/App/MemberRoles/memberRole.html',
				controller: 'MemberRoleController',
				controllerAs: 'vm'
			})
			.otherwise({
				redirectTo: '/memberRoles'
			});
	};
})();