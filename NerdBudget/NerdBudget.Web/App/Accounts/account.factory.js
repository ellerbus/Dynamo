
(function() {
	'use strict';

	//	setup the data service factory to call the ASP.NET API
	angular.module('app.accounts').factory('AccountFactory', AccountFactory);

	AccountFactory.$inject = ['$resource'];
	
	function AccountFactory($resource)
	{
		var url = '/api/accounts/'; 

		var pk = ':id';
		
		var defaults = { id: '@id' };
		
		var actions =
		{
			query:		{ method: 'GET', isArray: true },
			get:		{ method: 'GET' },
			add:		{ method: 'POST' },
			update: 	{ method: 'PUT' },
			'delete':	{ method: 'DELETE' }
		};

		return $resource(url + pk, defaults, actions);
	}

})();