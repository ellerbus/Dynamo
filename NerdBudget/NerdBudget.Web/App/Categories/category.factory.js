
(function() {
	'use strict';

	//	setup the data service factory to call the ASP.NET API
	angular.module('app.categories').factory('CategoryFactory', CategoryFactory);

	CategoryFactory.$inject = ['$resource'];
	
	function CategoryFactory($resource)
	{
	    var pkPattern = ':accountId/:id/:action';
		
		var pkInputs = { accountId: '@accountId', id: '@id' };
		
		var cfg =
		{
			update: { method: 'PUT' }
		};

		return $resource('/api/categories/' + pkPattern, pkInputs, cfg);
	}

})();