
(function() {
	'use strict';
	
	angular.module('app').config(MembersConfig);

	MembersConfig.$inject = ['ngRoute'];
	
	function MembersConfig($routeProvider) {
		$routeProvider
			.when('/', {
				templateUrl: 'members.html',
				controller: 'MembersController',
				controllerAs: 'vm'
			})
			.when('/member/:id', {
				templateUrl: 'member.html',
				controller: 'MemberController',
				controllerAs: 'vm'
			})
			.otherwise({
				redirectTo: '/'
			});
	};
})();