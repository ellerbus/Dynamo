
(function() {
	'use strict';

	//	setup the application for multiple and single interactions
	angular
		.module('app.memberRoles')
		.controller('MemberRolesController', MemberRolesController)
		.controller('MemberRoleController', MemberRoleController);

	MemberRolesController.$inject = ['MemberRoleDataService', '$log'];
	MemberRoleController.$inject = ['MemberRoleDataService', '$routeParams', '$log'];

	function MemberRolesController(memberRoleDataService, $log)
	{
		var vm = this;
		
		memberRoleDataService
			.getMemberRoles()
			.then(function (data)
			{
				vm.memberRoles = data;
			});
	}
		
	function MemberRoleController(memberRoleDataService, $routeParams, $log)
	{
		var vm = this;
		
		memberRoleDataService
			.getMemberRole($routeParams.memberId, $routeParams.roleId)
			.then(function (data)
			{
				vm.memberRole = data;
			});
	}
})();