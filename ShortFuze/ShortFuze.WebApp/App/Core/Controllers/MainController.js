var core;
(function (core) {
    var app;
    (function (app) {
        var MainController = (function () {
            function MainController() {
                this.page = { title: 'Short Fuze' };
            }
            MainController.$inject = [];
            return MainController;
        }());
        angular.module('app').controller('MainController', MainController);
    })(app = core.app || (core.app = {}));
})(core || (core = {}));
//# sourceMappingURL=MainController.js.map