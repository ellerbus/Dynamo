
(function() {
	'use strict';

	angular
		.module('app')
		.controller('rolesController', rolesController)
		.controller('roleController', roleController);

	rolesController.$inject = [];
	roleController.$inject = [];
		
	function rolesController() {
		var vm = this;

		vm.search = search;

		function search() {
			/* */
		};
	}
		
	function roleController() {
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