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
							scope.Functions = {
								Edit: function () {
									if (!!scope.payerId) {
										return;
									};

									scope.payerId = scope.detail.Id;
								}
							};
						},
					};
				},
				templateUrl: template.Url('jmc-payer-detail'),
				scope: {
					detail: '=jmcPayerDetail',
					payerId: '=selectedPayerId'
				}
			};
		}
	])
	.directive('jmcPayerEdit', [
		'jmcTemplate',
		function (template) {
			return {
				restrict: 'A',
				replace: false,
				transclude: false,
				compile: function () {
					return {
						pre: function (scope) { },
						post: function (scope) {
							scope.Functions = {
								Save: function () { console.log('save'); },
								Cancel: function () { console.log('cancel'); }
							};
						}
					};
				},
				templateUrl: template.Url('jmc-payer-edit'),
				scope: {
					payer: '=jmcPayerEdit',
					payerId: '=payerId'
				}
			};
		}
	]);