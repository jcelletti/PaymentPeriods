angular.module('payer.controllers', ['jmc.templates'])
	.config([
		'$httpProvider',
		'jmcTemplateProvider',
		function ($httpProvider, templateProvider) {
			templateProvider.Setup('NgTemplates/');

			$httpProvider
				.interceptors.push('httpErrorInterceptor');
		}
	])
	.controller('PayersController', [
		'$scope',
		'jmcPayerDetailService',
		function ($scope, payerDetailSvc) {
			$scope.loaded = false;

			payerDetailSvc.get()
				.then(function (details) {
					$scope.PayerDetails = details;
					$scope.loaded = true;
				});
		}
	]);