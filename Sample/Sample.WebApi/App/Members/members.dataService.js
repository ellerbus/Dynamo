
(function() {
	'use strict';

	angular
		.module('app')
		.factory('MembersDataService', MembersDataService);

	MembersDataService.$inject = ['$http', 'logger'];

	function MembersDataService($http, logger) {
		return {
			getMembers: getMembers,
			getMember: getMember,
		};
		
		function responseComplete(caller, response) {
			return response.data.results;
		}
		
		function responseFailed(caller, error) {
			logger.error('XHR Failed for ' + caller + ': ' + error.data);
		}

		function getMembers() {
			return $http.get('/api/Members')
				.then(function (response) { responseComplete('getMembers', response); })
				.catch(function (error) { responseFailed('getMembers', error); });
		}

		function getMember(memberID) {
			return $http.get('/api/Members')
				.then(function (response) { responseComplete('getMember', response); })
				.catch(function (error) { responseFailed('getMember', error); });
		}
	}

})();