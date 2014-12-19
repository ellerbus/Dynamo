
(function() {
	'use strict';

	angular
		.module('app')
		.controller('membersController', membersController)
		.controller('memberController', memberController);

	membersController.$inject = [];
	memberController.$inject = [];
		
	function membersController() {
		var vm = this;

		vm.search = search;

		function search() {
			/* */
		};
	}
		
	function memberController() {
		var vm = this;
	
		
		vm.id = 0;
		vm.name = '';
		vm.createdAt = new Date();
		vm.updatedAt = null /*new Date()*/;

		vm.save = save;

		function save() {
			/* */
		};
	}
})();