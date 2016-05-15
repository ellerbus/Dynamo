var core;
(function (core) {
    var app;
    (function (app) {
        factory.$inject = [];
        function factory() {
            return new coHoverDropdown();
        }
        var coHoverDropdown = (function () {
            function coHoverDropdown() {
                this.restrict = 'A';
                this.replace = false;
            }
            coHoverDropdown.prototype.link = function (scope, element, attrs) {
                element.dropdownHover();
            };
            return coHoverDropdown;
        }());
        angular.module('app').directive('coHoverDropdown', factory);
    })(app = core.app || (core.app = {}));
})(core || (core = {}));
//# sourceMappingURL=coHoverDropdown.js.map