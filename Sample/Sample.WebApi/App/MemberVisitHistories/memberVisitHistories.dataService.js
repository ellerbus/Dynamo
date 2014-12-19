
(function() {
	'use strict';

	angular
		.module('app')
		.factory('MemberVisitHistoriesDataService', MemberVisitHistoriesDataService);

	MemberVisitHistoriesDataService.$inject = ['$http', 'logger'];

	function MemberVisitHistoriesDataService($http, logger) {
		return {
			getMemberVisitHistories: getMemberVisitHistories,
			getMemberVisitHistory: getMemberVisitHistory,
		};
		
		function responseComplete(caller, response) {
			return response.data.results;
		}
		
		function responseFailed(caller, error) {
			logger.error('XHR Failed for ' + caller + ': ' + error.data);
		}

		function getMemberVisitHistories() {
			return $http.get('/api/MemberVisitHistories')
				.then(function (response) { responseComplete('getMemberVisitHistories', response); })
				.catch(function (error) { responseFailed('getMemberVisitHistories', error); });
		}

		function getMemberVisitHistory(memberID, visitedAt) {
			return $http.get('/api/MemberVisitHistories')
				.then(function (response) { responseComplete('getMemberVisitHistory', response); })
				.catch(function (error) { responseFailed('getMemberVisitHistory', error); });
		}
	}

})();