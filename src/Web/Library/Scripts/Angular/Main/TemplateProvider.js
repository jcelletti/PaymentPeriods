angular.module('JMCApp')
	.provider('template',
		function () {
			var base = 'NgTemplates/';
			this.$get = function () {
				return {
					Url: function (name, folder) {
						var url = base;
						if (folder) {
							url += folder + '/';
						};
						return url + name + '.html';
					},
					Route: function (controller, action) {
						throw 'Not Implemented';
					}
				};
			}
		}
	);