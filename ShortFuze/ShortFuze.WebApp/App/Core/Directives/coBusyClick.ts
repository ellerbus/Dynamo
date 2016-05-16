module core.app
{
    interface IBusyClickAttributes extends ng.IAttributes
    {
        coBusyClick: string;
        coIcon?: string;
    }

    interface IBusyClickScope extends ng.IScope
    {
        coBusyClick(): ng.IPromise<any>;
    }

    coBusyClick.$inject = ['$q', '$timeout'];

    export function coBusyClick($q: ng.IQService, $timeout: ng.ITimeoutService): ng.IDirective
    {
        var directive: ng.IDirective = {};

        directive.restrict = 'A';
        directive.replace = false;
        directive.scope = { coBusyClick: '&coBusyClick' };

        directive.link = (scope: IBusyClickScope, element: JQuery, attr: IBusyClickAttributes) =>
        {
            // expects a promise
            // http://docs.angularjs.org/api/ng.$q

            var handler = (e) =>
            {
                var append = typeof attr.coIcon === 'undefined';

                var html = element.html();

                var enableButton = function ()
                {
                    element.html(html);

                    attr.$set('disabled', false);
                };

                $timeout(() => attr.$set('disabled', true))
                    .then(() => scope.coBusyClick().finally(enableButton));
            };

            element.bind('click', handler);

            scope.$on('$destroy', () =>
            {
                element.off('click', handler);
            });
        };

        return directive;
    }

    angular.module('app').directive('coBusyClick', coBusyClick);
}
