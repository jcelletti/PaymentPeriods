angular.module('payer.services', ['jmc.http', 'jmc.services'])
	.service('jmcPayerService', [
		'$',
		'jmcHttp', 'jmcPayer',
		function ($, http, Payer) {
			var baseUrl = 'api/Payers/';

			return {
				get: function (id) {
					var url = baseUrl;

					var trId = $.trim(id);
					if (trId.length > 0) {
						url += id + '/';
					};

					return http.get(url, Payer);
				}
			};
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
				p.Name = detail.name;
			};
		}
	])
	.factory('jmcPayer', [
		function () {
			return function (detail) {
				var p = this;

				p.Id = detail.id;
				p.First = detail.first;
				p.Last = detail.last;
			};
		}
	]);