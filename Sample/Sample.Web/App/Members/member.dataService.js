
(function() {
	'use strict';

	//	setup the data service factory to call the ASP.NET API
	angular
		.module('app.members')
		.factory('MemberDataService', MemberDataService);

	MemberDataService.$inject = ['$http', '$log'];

	function MemberDataService($http, $log) {
		return {
			getMembers: getMembers,
			getMember: getMember,
		};
		
		function responseComplete(caller, response) {
			return response.data.results;
		}
		
		function responseFailed(caller, error) {
			$log.error('XHR Failed for ' + caller + ': ' + error.data);
		}

		function getMembers() {
			return $http.get('/api/Members')
				.then(function (response) { return responseComplete('getMembers', response); })
				.catch(function (error) { responseFailed('getMembers', error); });
		}

		function getMember(id) {
			var pk = id;
			
			return $http.get('/api/Members/' + pk)
				.then(function (response) { responseComplete('getMember', response); })
				.catch(function (error) { responseFailed('getMember', error); });
		}
	}

})();