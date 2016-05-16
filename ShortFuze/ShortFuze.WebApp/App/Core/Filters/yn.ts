module core.app
{
    //  usage: <td ng-bind='true | YN'></td>
    function yn()
    {
        return (x) => x ? 'X' : '';
    }

    angular.module('app').filter('yn', yn);
}