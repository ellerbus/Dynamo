module core.app
{
    class navigation implements ng.IComponentOptions
    {
        templateUrl = 'App/Core/Templates/core.navigation.html';

        constructor()
        {
        }
    }

    angular.module('app').component('navigation', new navigation());
}
