angular.module('payer.controllers', ['jmc.templates'])
	.controller('PayersController', [
		'$scope', '$element', '$',
		'jmcPayerDetailService', 'jmcPayerService',
		function ($scope, $element, $, payerDetailSvc, payerSvc) {
			$scope.Contents = {
				PayerId: null,
				Loaded: false,
				PayerDetails: [],
				Payer: null,
				CurrentPayerSet: false
			};
			
			var watcher = $scope.$watch('Contents.PayerId', function (newVal) {
				var trId = $.trim(newVal);
				if (trId.length > 0) {
					payerSvc.get(trId)
						.then(function (payer) {
							$scope.Contents.Payer = payer;
							$scope.Contents.CurrentPayerSet = true;
						});
					return;
				};

				$scope.Contents.CurrentPayerSet = false;

				payerDetailSvc.get()
					.then(function (details) {
						$scope.Contents.PayerDetails = details;
						$scope.Contents.Loaded = true;
					});
			});

			$element.on('destroy', function () {
				watcher();
			});
		}
	]);