

(function() {
	'use strict';
	
	//	initialize the application for categories
	angular.module('app.categories', []);
	
	//	setup the application routing for categories
	angular.module('app.categories').config(CategoryConfig);

	CategoryConfig.$inject = ['$routeProvider'];
	
	function CategoryConfig($routeProvider)
	{
		$routeProvider
			.when('/categories/:accountId', {
				templateUrl: '/App/Categories/categories.html',
				controller: 'CategoriesController',
				controllerAs: 'vm'
			})
			.when('/category/:action/:accountId/:id?', {
				templateUrl: '/App/Categories/category.html',
				controller: 'CategoryController',
				controllerAs: 'vm'
			});
	};

})();