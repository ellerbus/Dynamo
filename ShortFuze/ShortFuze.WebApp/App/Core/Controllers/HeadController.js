var core;
(function (core) {
    var app;
    (function (app) {
        var HeadController = (function () {
            function HeadController() {
                this.page = { title: 'Short Fuze' };
            }
            HeadController.$inject = [];
            return HeadController;
        }());
        angular.module('app').controller('HeadController', HeadController);
    })(app = core.app || (core.app = {}));
})(core || (core = {}));
