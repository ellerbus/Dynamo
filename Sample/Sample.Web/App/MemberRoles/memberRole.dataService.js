
(function() {
	'use strict';

	//	setup the data service factory to call the ASP.NET API
	angular
		.module('app.memberRoles')
		.factory('MemberRoleDataService', MemberRoleDataService);

	MemberRoleDataService.$inject = ['$http', '$log'];

	function MemberRoleDataService($http, $log) {
		return {
			getMemberRoles: getMemberRoles,
			getMemberRole: getMemberRole,
		};
		
		function responseComplete(caller, response) {
			return response.data.results;
		}
		
		function responseFailed(caller, error) {
			$log.error('XHR Failed for ' + caller + ': ' + error.data);
		}

		function getMemberRoles() {
			return $http.get('/api/MemberRoles')
				.then(function (response) { return responseComplete('getMemberRoles', response); })
				.catch(function (error) { responseFailed('getMemberRoles', error); });
		}

		function getMemberRole(memberId, roleId) {
			var pk = memberId + '/' + roleId;
			
			return $http.get('/api/MemberRoles/' + pk)
				.then(function (response) { responseComplete('getMemberRole', response); })
				.catch(function (error) { responseFailed('getMemberRole', error); });
		}
	}

})();