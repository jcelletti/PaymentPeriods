angular.module('payer.directives', ['jmc.templates'])
	.directive('jmcPayerDetail', [
		'jmcTemplate',
		function (template) {
			return {
				restrict: 'A',
				replace: false,
				transclude: false,
				compile: function () {
					return {
						pre: function (scope) {
						},
						post: function (scope) {
						},
					}
				},
				templateUrl: template.Url('jmc-payer-detail'),
				scope: {
					detail: '=jmcPayerDetail'
				}
			}
		}
	]);