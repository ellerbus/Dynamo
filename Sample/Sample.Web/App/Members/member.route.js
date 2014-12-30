
(function() {
	'use strict';
	
	//	initialize the app for members
	angular.modeule('app.members', []);

	//	setup the application routing for members
	angular.module('app.members').config(MemberConfig);

	MemberConfig.$inject = ['$routeProvider'];
	
	function MemberConfig($routeProvider) {
		$routeProvider
			.when('/members', {
				templateUrl: '/App/Members/members.html',
				controller: 'MembersController',
				controllerAs: 'vm'
			})
			.when('/member/:id', {
				templateUrl: '/App/Members/member.html',
				controller: 'MemberController',
				controllerAs: 'vm'
			})
			.otherwise({
				redirectTo: '/members'
			});
	};
})();