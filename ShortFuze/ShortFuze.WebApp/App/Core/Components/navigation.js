var core;
(function (core) {
    var app;
    (function (app) {
        var navigation = (function () {
            function navigation() {
                this.templateUrl = 'App/Core/Templates/core.navigation.html';
            }
            return navigation;
        }());
        angular.module('app').component('navigation', new navigation());
    })(app = core.app || (core.app = {}));
})(core || (core = {}));
