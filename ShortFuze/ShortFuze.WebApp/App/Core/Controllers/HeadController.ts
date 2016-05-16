module core.app
{
    class HeadController
    {
        page: any = { title: 'Short Fuze'};

        static $inject = [];

        constructor()
        {
        }
    }

    angular.module('app').controller('HeadController', HeadController);
}