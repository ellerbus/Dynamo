var core;
(function (core) {
    var app;
    (function (app) {
        //  usage: <td ng-bind='true | YN'></td>
        function humanize() {
            return function (dt) { return moment(new Date(dt)).fromNow(); };
        }
        angular.module('app').filter('humanize', humanize);
    })(app = core.app || (core.app = {}));
})(core || (core = {}));
