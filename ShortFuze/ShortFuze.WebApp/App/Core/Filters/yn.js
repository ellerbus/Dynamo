var core;
(function (core) {
    var app;
    (function (app) {
        //  usage: <td ng-bind='true | YN'></td>
        function yn() {
            return function (x) { return x ? 'X' : ''; };
        }
        angular.module('app').filter('yn', yn);
    })(app = core.app || (core.app = {}));
})(core || (core = {}));
