
(function() {
	'use strict';

	angular
		.module('app')
		.controller('memberVisitHistoriesController', memberVisitHistoriesController)
		.controller('memberVisitHistoryController', memberVisitHistoryController);

	memberVisitHistoriesController.$inject = [];
	memberVisitHistoryController.$inject = [];
		
	function memberVisitHistoriesController() {
		var vm = this;

		vm.search = search;

		function search() {
			/* */
		};
	}
		
	function memberVisitHistoryController() {
		var vm = this;
	
		
		vm.memberId = 0;
		vm.visitedAt = new Date();
		vm.pageUrl = '';

		vm.save = save;

		function save() {
			/* */
		};
	}
})();