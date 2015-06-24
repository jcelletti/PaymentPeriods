angular.module('JMCApp')
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
		}])
	.config([
		'$routeProvider', '$httpProvider',
		'templateProvider',
	function ($routeProvider, $httpProvider, template) {
		$routeProvider
		.when('/', {
			templateUrl: template.$get().Url('temp'),
			controller: 'TempCtrl'
		})
		.otherwise({
			redirectTo: '/'
		});

		$httpProvider
			.interceptors.push('httpErrorInterceptor');
	}]);