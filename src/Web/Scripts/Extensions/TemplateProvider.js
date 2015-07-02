angular.module('jmc.templates', [])
	.provider('jmcTemplate', [
		function () {
			var base;

			var self = this;
			self.Setup = function (url) {
				base = $.trim(url);
			};

			this.$get = function () {
				if (!base) {
					throw 'Base Url must be set';
				};
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
	]);