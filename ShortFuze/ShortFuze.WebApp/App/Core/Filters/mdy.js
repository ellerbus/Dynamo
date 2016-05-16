var core;
(function (core) {
    var app;
    (function (app) {
        mdy.$inject = ['$filter'];
        function mdy($filter) {
            return function (dt) { return $filter('date')(dt, 'M/d/yy'); };
        }
        angular.module('app').filter('mdy', mdy);
    })(app = core.app || (core.app = {}));
})(core || (core = {}));
