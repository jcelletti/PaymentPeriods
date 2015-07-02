angular.module('jmc.http.interceptor', [])
	.factory('httpErrorInterceptor', [
		'$q',
		function ($q) {
			return {
				requestError: function (rej) {
					alert(rej);
					console.log(rej);
					q.reject(rej);
				},
				responseError: function (rej) {
					alert(rej);
					console.log(rej);
					q.reject(rej);
				}
			};
		}
	]);