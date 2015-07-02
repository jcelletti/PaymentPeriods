angular.module('payersApp', ['payer.controllers', 'payer.directives', 'payer.services'])
	.config([
		'$httpProvider',
		'jmcTemplateProvider',
		function ($httpProvider, templateProvider) {
			templateProvider.Setup('NgTemplates/');

			$httpProvider
				.interceptors.push('httpErrorInterceptor');
		}
	]);