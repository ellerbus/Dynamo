
(function() {
	'use strict';

	//	setup the application for multiple and single interactions
	angular
		.module('app.roles')
		.controller('RolesController', RolesController)
		.controller('RoleController', RoleController);

	RolesController.$inject = ['RoleDataService', '$log'];
	RoleController.$inject = ['RoleDataService', '$routeParams', '$log'];

	function RolesController(roleDataService, $log)
	{
		var vm = this;
		
		roleDataService
			.getRoles()
			.then(function (data)
			{
				vm.roles = data;
			});
	}
		
	function RoleController(roleDataService, $routeParams, $log)
	{
		var vm = this;
		
		roleDataService
			.getRole($routeParams.id)
			.then(function (data)
			{
				vm.role = data;
			});
	}
})();