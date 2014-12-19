
(function() {
	'use strict';

	angular
		.module('app')
		.factory('MemberRolesDataService', MemberRolesDataService);

	MemberRolesDataService.$inject = ['$http', 'logger'];

	function MemberRolesDataService($http, logger) {
		return {
			getMemberRoles: getMemberRoles,
			getMemberRole: getMemberRole,
		};
		
		function responseComplete(caller, response) {
			return response.data.results;
		}
		
		function responseFailed(caller, error) {
			logger.error('XHR Failed for ' + caller + ': ' + error.data);
		}

		function getMemberRoles() {
			return $http.get('/api/MemberRoles')
				.then(function (response) { responseComplete('getMemberRoles', response); })
				.catch(function (error) { responseFailed('getMemberRoles', error); });
		}

		function getMemberRole(memberID, roleID) {
			return $http.get('/api/MemberRoles')
				.then(function (response) { responseComplete('getMemberRole', response); })
				.catch(function (error) { responseFailed('getMemberRole', error); });
		}
	}

})();