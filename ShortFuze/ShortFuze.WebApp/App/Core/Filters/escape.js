var core;
(function (core) {
    var app;
    (function (app) {
        //  usage: <td ng-bind='true | YN'></td>
        function escape() {
            return encodeURIComponent;
        }
        angular.module('app').filter('escape', escape);
    })(app = core.app || (core.app = {}));
})(core || (core = {}));
