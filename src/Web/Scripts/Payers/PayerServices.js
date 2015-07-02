angular.module('payer.services', ['jmc.http', 'jmc.services'])
	.service('jmcPayerService', [
		function () {

		}
	])
	.service('jmcPayerDetailService', [
		'$',
		'jmcHttp', 'jmcPayerDetail',
		function ($, http, PayerDetail) {
			var baseUrl = 'api/PayerDetails/';
			return {
				get: function (id) {
					var url = baseUrl;

					var trId = $.trim(id);
					if (trId.length > 0) {
						url += id + '/';
					};

					return http.get(url, PayerDetail);
				}
			};
		}
	])
	.factory('jmcPayerDetail', [
		function () {
			return function (detail) {
				var p = this;

				p.Id = detail.id;
				p.FullName = detail.name;
			};
		}
	]);