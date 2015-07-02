angular.module('jmcApp', ['ngRoute', 'jmc.extensions', 'jmc.header', 'jmc.footer'])
	.config([
		'$routeProvider', '$httpProvider',
		'jmcTemplateProvider',
		function ($routeProvider, $httpProvider, templateProvider) {
			templateProvider.Setup('NgTemplates/');

			var template = templateProvider.$get();

			$routeProvider
				.when('/', {
					templateUrl: template.Url('temp'),
					controller: 'TempCtrl'
				})
				.otherwise({
					redirectTo: '/'
				});

			$httpProvider
				.interceptors.push('httpErrorInterceptor');
		}
	]);