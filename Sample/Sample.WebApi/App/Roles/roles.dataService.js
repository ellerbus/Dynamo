
(function() {
	'use strict';

	angular
		.module('app')
		.factory('RolesDataService', RolesDataService);

	RolesDataService.$inject = ['$http', 'logger'];

	function RolesDataService($http, logger) {
		return {
			getRoles: getRoles,
			getRole: getRole,
		};
		
		function responseComplete(caller, response) {
			return response.data.results;
		}
		
		function responseFailed(caller, error) {
			logger.error('XHR Failed for ' + caller + ': ' + error.data);
		}

		function getRoles() {
			return $http.get('/api/Roles')
				.then(function (response) { responseComplete('getRoles', response); })
				.catch(function (error) { responseFailed('getRoles', error); });
		}

		function getRole(roleID) {
			return $http.get('/api/Roles')
				.then(function (response) { responseComplete('getRole', response); })
				.catch(function (error) { responseFailed('getRole', error); });
		}
	}

})();