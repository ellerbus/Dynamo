var core;
(function (core) {
    var app;
    (function (app) {
        mdyhm.$inject = ['$filter'];
        function mdyhm($filter) {
            return function (dt) { return $filter('date')(dt, 'M/d/yy h:mm a'); };
        }
        angular.module('app').filter('mdyhm', mdyhm);
    })(app = core.app || (core.app = {}));
})(core || (core = {}));
