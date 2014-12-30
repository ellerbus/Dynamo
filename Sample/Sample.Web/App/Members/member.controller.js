
(function() {
	'use strict';

	//	setup the application for multiple and single interactions
	angular
		.module('app.members')
		.controller('MembersController', MembersController)
		.controller('MemberController', MemberController);

	MembersController.$inject = ['MemberDataService', '$log'];
	MemberController.$inject = ['MemberDataService', '$routeParams', '$log'];

	function MembersController(memberDataService, $log)
	{
		var vm = this;
		
		memberDataService
			.getMembers()
			.then(function (data)
			{
				vm.members = data;
			});
	}
		
	function MemberController(memberDataService, $routeParams, $log)
	{
		var vm = this;
		
		memberDataService
			.getMember($routeParams.id)
			.then(function (data)
			{
				vm.member = data;
			});
	}
})();