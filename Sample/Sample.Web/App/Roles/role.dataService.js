
(function() {
	'use strict';

	//	setup the data service factory to call the ASP.NET API
	angular
		.module('app.roles')
		.factory('RoleDataService', RoleDataService);

	RoleDataService.$inject = ['$http', '$log'];

	function RoleDataService($http, $log) {
		return {
			getRoles: getRoles,
			getRole: getRole,
		};
		
		function responseComplete(caller, response) {
			return response.data.results;
		}
		
		function responseFailed(caller, error) {
			$log.error('XHR Failed for ' + caller + ': ' + error.data);
		}

		function getRoles() {
			return $http.get('/api/Roles')
				.then(function (response) { return responseComplete('getRoles', response); })
				.catch(function (error) { responseFailed('getRoles', error); });
		}

		function getRole(id) {
			var pk = id;
			
			return $http.get('/api/Roles/' + pk)
				.then(function (response) { responseComplete('getRole', response); })
				.catch(function (error) { responseFailed('getRole', error); });
		}
	}

})();