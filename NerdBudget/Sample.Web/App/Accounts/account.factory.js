
(function() {
	'use strict';

	//	setup the data service factory to call the ASP.NET API
	angular.module('app.accounts').factory('AccountFactory', AccountFactory);

	AccountFactory.$inject = ['$resource'];

	function AccountFactory($resource) {
		var pkPattern = ':id';
		
		var pkInputs = 'id: '@'id';
		
		var cfg = {
			update: { method: 'PUT' }
		};
		
		return $resource('/api/Accounts/' + pkPattern, pkInputs, cfg);
	}
	
})();