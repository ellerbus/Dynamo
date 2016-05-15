module core.app
{
    class MainController
    {
        page: any = { title: 'Short Fuze'};

        static $inject = [];

        constructor()
        {
        }
    }

    angular.module('app').controller('MainController', MainController);
}