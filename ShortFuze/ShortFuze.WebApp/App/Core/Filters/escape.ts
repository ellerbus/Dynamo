module core.app
{
    //  usage: <td ng-bind='true | YN'></td>
    function escape()
    {
        return encodeURIComponent
    }

    angular.module('app').filter('escape', escape);
}