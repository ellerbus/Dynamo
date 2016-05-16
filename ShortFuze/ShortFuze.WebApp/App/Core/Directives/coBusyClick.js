var core;
(function (core) {
    var app;
    (function (app) {
        coBusyClick.$inject = ['$q', '$timeout'];
        function coBusyClick($q, $timeout) {
            var directive = {};
            directive.restrict = 'A';
            directive.replace = false;
            directive.scope = { coBusyClick: '&coBusyClick' };
            directive.link = function (scope, element, attr) {
                // expects a promise
                // http://docs.angularjs.org/api/ng.$q
                var handler = function (e) {
                    var append = typeof attr.coIcon === 'undefined';
                    var html = element.html();
                    var enableButton = function () {
                        element.html(html);
                        attr.$set('disabled', false);
                    };
                    $timeout(function () { return attr.$set('disabled', true); })
                        .then(function () { return scope.coBusyClick().finally(enableButton); });
                };
                element.bind('click', handler);
                scope.$on('$destroy', function () {
                    element.off('click', handler);
                });
            };
            return directive;
        }
        app.coBusyClick = coBusyClick;
        angular.module('app').directive('coBusyClick', coBusyClick);
    })(app = core.app || (core.app = {}));
})(core || (core = {}));
