var core;
(function (core) {
    var app;
    (function (app) {
        /**
         *
         */
        var RepositoryService = (function () {
            function RepositoryService($http) {
                this.$http = $http;
            }
            /**
             *
             * @param url
             * @param config
             */
            RepositoryService.prototype.get = function (url, config) {
                var iurl = this.interpolateUrl(url, config.params, config.data);
                this.updateConfig(config);
                return this.$http.get(iurl, config).then(function (x) { return x.data; });
            };
            /**
             *
             * @param url
             * @param config
             */
            RepositoryService.prototype.delete = function (url, config) {
                var iurl = this.interpolateUrl(url, config.params, config.data);
                this.updateConfig(config);
                return this.$http.delete(iurl, config).then(function (x) { return x.data; });
            };
            /**
             *
             * @param url
             * @param config
             */
            RepositoryService.prototype.head = function (url, config) {
                var iurl = this.interpolateUrl(url, config.params, config.data);
                this.updateConfig(config);
                return this.$http.head(iurl, config).then(function (x) { return x.data; });
            };
            /**
             *
             * @param url
             * @param config
             */
            RepositoryService.prototype.jsonp = function (url, config) {
                var iurl = this.interpolateUrl(url, config.params, config.data);
                this.updateConfig(config);
                return this.$http.jsonp(iurl, config).then(function (x) { return x.data; });
            };
            /**
             *
             * @param url
             * @param data
             * @param config
             */
            RepositoryService.prototype.post = function (url, data, config) {
                var iurl = this.interpolateUrl(url, config.params, config.data);
                this.updateConfig(config);
                return this.$http.post(iurl, data, config).then(function (x) { return x.data; });
            };
            /**
             *
             * @param url
             * @param data
             * @param config
             */
            RepositoryService.prototype.put = function (url, data, config) {
                var iurl = this.interpolateUrl(url, config.params, config.data);
                this.updateConfig(config);
                return this.$http.put(iurl, data, config).then(function (x) { return x.data; });
            };
            /**
             *
             * @param config
             */
            RepositoryService.prototype.updateConfig = function (config) {
                //if (this.ErrorService.forms.length > 0)
                //{
                //    var id = this.ErrorService.forms.shift();
                //    if (config == null)
                //    {
                //        config = {};
                //    }
                //    if (config.headers == null)
                //    {
                //        config.headers = {};
                //    }
                //    config.headers['X-FORM'] = id;
                //}
            };
            /**
             * Move values from the params and data arguments into the URL where
             * there is a match for labels. When the match occurs, the key-value
             * pairs are removed from the parent object and merged into the string
             * value of the URL.
             * @param url
             * @param params
             * @param data
             */
            RepositoryService.prototype.interpolateUrl = function (url, params, data) {
                var _this = this;
                // Make sure we have an object to work with - makes the rest of the
                // logic easier.
                params = (params || {});
                data = (data || {});
                // Replace each label in the URL (ex, :userID).
                var replacer = function ($0, key) {
                    // NOTE: Giving "data" precedence over "params".
                    return _this.popFirstKey(data, params, key) || "";
                };
                url = url.replace(/:([a-z]\w*)/gi, replacer);
                // Strip out any repeating slashes (but NOT the http:// version).
                url = url.replace(/(^|[^:])[\/]{2,}/g, "$1/");
                // Strip out any trailing slash.
                url = url.replace(/\/+$/i, "");
                return url;
            };
            /**
             *
             * @param data
             * @param params
             * @param key
             */
            RepositoryService.prototype.popFirstKey = function (data, params, key) {
                // Convert the arguments list into a true array so we can easily
                // pluck values from either end.
                var objects = Array.prototype.slice.call(arguments);
                // The key will always be the last item in the argument collection.
                objects.pop();
                var object = null;
                // Iterate over the arguments, looking for the first object that
                // contains a reference to the given key.
                while (object = objects.shift()) {
                    if (object.hasOwnProperty(key)) {
                        return this.popKey(object, key);
                    }
                }
                return null;
            };
            /**
             *
             * @param object
             * @param key
             */
            RepositoryService.prototype.popKey = function (object, key) {
                var value = object[key];
                delete object[key];
                if (typeof value == 'boolean') {
                    value = value ? 'true' : 'false';
                }
                return value;
            };
            RepositoryService.$inject = ['$http'];
            return RepositoryService;
        }());
        app.RepositoryService = RepositoryService;
        angular.module('app').service('RepositoryService', RepositoryService);
    })(app = core.app || (core.app = {}));
})(core || (core = {}));
