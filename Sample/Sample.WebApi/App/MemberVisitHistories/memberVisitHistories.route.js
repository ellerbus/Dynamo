
(function() {
	'use strict';
	
	angular.module('app').config(MemberVisitHistoriesConfig);

	MemberVisitHistoriesConfig.$inject = ['ngRoute'];
	
	function MemberVisitHistoriesConfig($routeProvider) {
		$routeProvider
			.when('/', {
				templateUrl: 'memberVisitHistories.html',
				controller: 'MemberVisitHistoriesController',
				controllerAs: 'vm'
			})
			.when('/memberVisitHistory/:id', {
				templateUrl: 'memberVisitHistory.html',
				controller: 'MemberVisitHistoryController',
				controllerAs: 'vm'
			})
			.otherwise({
				redirectTo: '/'
			});
	};
})();