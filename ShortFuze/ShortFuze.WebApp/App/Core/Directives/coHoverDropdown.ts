module core.app
{
    factory.$inject = [];

    function factory(): ng.IDirective
    {
        return new coHoverDropdown();
    }

    interface HoverDropdown extends JQuery
    {
        dropdownHover(): void;
    }

    class coHoverDropdown implements ng.IDirective
    {
        restrict = 'A';
        replace = false;

        link(scope: ng.IScope, element: HoverDropdown, attrs: ng.IAttributes): void
        {
            element.dropdownHover();
        }
    }

    angular.module('app').directive('coHoverDropdown', factory);
}