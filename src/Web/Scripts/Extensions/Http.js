angular.module('jmc.http', [])
	.factory('httpErrorInterceptor', [
		'$q',
		function ($q) {
			return {
				requestError: function (rej) {
					alert(rej);
					//logger.debug(rej);
					q.reject(rej);
				},
				responseError: function (rej) {
					alert(rej);
					//logger.debug(rej);
					q.reject(rej);
				}
			};
		}
	])
	.service('jmcHttp', [
		'$http', '$',
		'jmcHttpDeferer',
		function ($http, $, Defered) {
			return {
				get: function (url, resolver) {
					return Defered.Create($http.get(url), resolver);
				},
				post: function (url, newObj, resolver) {
					//logger.debug('Http Post');
					//logger.debug(url);
					//logger.debug(newObj);

					if (!$.isFunction(resolver)) {
						throw 'Resolver must be set';
					};

					var resolved = new resolver(newObj);

					return Defered.Create($http.post(url, resolved));
				},
				put: function (url, updateObj, resolver) {
					//logger.debug('Http Put');
					//logger.debug(url);
					//logger.debug(updateObj);

					if (!$.isFunction(resolver)) {
						throw 'Resolver must be set';
					};

					var resolved = new resolver(updateObj);

					return Defered.Create($http.put(url, resolved));
				},
				'delete': function (url) {
					//logger.debug('Http Delete');
					//logger.debug(url);
					return Defered.Create($http.delete(url));
				}
			};
		}
	])
	.service('jmcHttpDeferer', [
		'$q', '$',
		function ($q, $) {

			return {
				Create: function (httpPromise, resolver) {
					var defer = $q.defer();

					httpPromise
						.success(function (data) {
							//logger.debug('http success, data to follow');
							//logger.debug(data);

							var resolved = null;

							if ($.isFunction(resolver) && !!data) {
								if ($.isArray(data)) {
									resolved = [];

									$.each(data, function (i, d) {
										resolved.push(new resolver(d));
									});

								} else {
									resolved = new resolver(data);
								};
							};

							defer.resolve(resolved);
						})
						.error(function (error) {
							//logger.debug('http error, data to follow');
							//logger.error(error);
							alert('An error occured');
							defer.reject(error);
						});

					return defer.promise;
				}
			};
		}
	]);