﻿angular.module('jmcApp')
	.controller('SpaCtrl', [
		'$scope',
		function ($scope) {
			$scope.test = 'hola';
		}
	])
	.controller('TempCtrl', [
		'$scope',
		function ($scope) {
			$scope.test = 'hola 2';
		}
	]);
