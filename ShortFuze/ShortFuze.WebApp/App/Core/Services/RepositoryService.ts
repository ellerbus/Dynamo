module core.app
{
    /**
     * 
     */
    export class RepositoryService
    {
        static $inject = ['$http'];

        constructor(private $http: ng.IHttpService)
        {
        }

        /**
         * 
         * @param url
         * @param config
         */
        get<T>(url: string, config?: ng.IRequestShortcutConfig): ng.IPromise<T>
        {
            var iurl = this.interpolateUrl(url, config.params, config.data);

            this.updateConfig(config);

            return this.$http.get(iurl, config).then((x) => x.data);
        }

        /**
         * 
         * @param url
         * @param config
         */
        delete<T>(url: string, config?: ng.IRequestShortcutConfig): ng.IPromise<T>
        {
            var iurl = this.interpolateUrl(url, config.params, config.data);

            this.updateConfig(config);

            return this.$http.delete(iurl, config).then((x) => x.data);
        }

        /**
         * 
         * @param url
         * @param config
         */
        head<T>(url: string, config?: ng.IRequestShortcutConfig): ng.IPromise<T>
        {
            var iurl = this.interpolateUrl(url, config.params, config.data);

            this.updateConfig(config);

            return this.$http.head(iurl, config).then((x) => x.data);
        }

        /**
         * 
         * @param url
         * @param config
         */
        jsonp<T>(url: string, config?: ng.IRequestShortcutConfig): ng.IPromise<T>
        {
            var iurl = this.interpolateUrl(url, config.params, config.data);

            this.updateConfig(config);

            return this.$http.jsonp(iurl, config).then((x) => x.data);
        }

        /**
         * 
         * @param url
         * @param data
         * @param config
         */
        post<T>(url: string, data: any, config?: ng.IRequestShortcutConfig): ng.IPromise<T>
        {
            var iurl = this.interpolateUrl(url, config.params, config.data);

            this.updateConfig(config);

            return this.$http.post(iurl, data, config).then((x) => x.data);
        }

        /**
         * 
         * @param url
         * @param data
         * @param config
         */
        put<T>(url: string, data: any, config?: ng.IRequestShortcutConfig): ng.IPromise<T>
        {
            var iurl = this.interpolateUrl(url, config.params, config.data);

            this.updateConfig(config);

            return this.$http.put(iurl, data, config).then((x) => x.data);
        }

        /**
         * 
         * @param config
         */
        updateConfig(config: ng.IRequestShortcutConfig): void
        {
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
        }
    
        /**
         * Move values from the params and data arguments into the URL where
         * there is a match for labels. When the match occurs, the key-value
         * pairs are removed from the parent object and merged into the string
         * value of the URL.
         * @param url
         * @param params
         * @param data
         */
        interpolateUrl(url: string, params: any, data: any): string
        {
            // Make sure we have an object to work with - makes the rest of the
            // logic easier.
            params = (params || {});

            data = (data || {});

            // Replace each label in the URL (ex, :userID).

            var replacer = ($0, key) =>
            {
                // NOTE: Giving "data" precedence over "params".
                return this.popFirstKey(data, params, key) || "";
            };

            url = url.replace(/:([a-z]\w*)/gi, replacer);

            // Strip out any repeating slashes (but NOT the http:// version).
            url = url.replace(/(^|[^:])[\/]{2,}/g, "$1/");

            // Strip out any trailing slash.
            url = url.replace(/\/+$/i, "");

            return url;
        }

        /**
         * 
         * @param data
         * @param params
         * @param key
         */
        popFirstKey(data: any, params: any, key: string): string
        {
            // Convert the arguments list into a true array so we can easily
            // pluck values from either end.
            var objects = Array.prototype.slice.call(arguments);
    
            // The key will always be the last item in the argument collection.
            objects.pop();

            var object = null;
    
            // Iterate over the arguments, looking for the first object that
            // contains a reference to the given key.
            while (object = objects.shift())
            {
                if (object.hasOwnProperty(key))
                {
                    return this.popKey(object, key);
                }
            }

            return null;
        }

        /**
         * 
         * @param object
         * @param key
         */
        popKey(object: string, key: string): any
        {
            var value = object[key];

            delete object[key];

            if (typeof value == 'boolean')
            {
                value = value ? 'true' : 'false';
            }

            return value;
        }
    }

    angular.module('app').service('RepositoryService', RepositoryService);
}