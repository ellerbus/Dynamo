
(function() {
	'use strict';

	angular
		.module('app')
		.controller('memberRolesController', memberRolesController)
		.controller('memberRoleController', memberRoleController);

	memberRolesController.$inject = [];
	memberRoleController.$inject = [];
		
	function memberRolesController() {
		var vm = this;

		vm.search = search;

		function search() {
			/* */
		};
	}
		
	function memberRoleController() {
		var vm = this;
	
		
		vm.memberId = 0;
		vm.roleId = 0;

		vm.save = save;

		function save() {
			/* */
		};
	}
})();