
(function() {
	'use strict';

	//	setup the data service factory to call the ASP.NET API
	angular.module('app.ledgers').factory('LedgerFactory', LedgerFactory);

	LedgerFactory.$inject = ['$resource'];
	
	function LedgerFactory($resource)
	{
		var url = '/api/ledgers/'; 

		var pk = ':accountId/:id/:date';
		
		var defaults = { accountId: '@accountId', id: '@id', date: '@date' };
		
		var actions =
		{
		    //query:		{ method: 'GET', isArray: true, url: url + ':accountId/history/:startDate/:endDate' },
			//get:		{ method: 'GET' },
			//add:		{ method: 'POST' },
			//update:     { method: 'PUT' },
		    //'delete':   { method: 'DELETE' },
		    'getImport':    { method: 'GET', url: url + ':accountId/import' },
			'import':       { method: 'POST', url: url + ':accountId/import' }
		};

		return $resource(url + pk, defaults, actions);
	}

})();