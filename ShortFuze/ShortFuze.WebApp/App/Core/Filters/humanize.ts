module core.app
{
    //  usage: <td ng-bind='true | YN'></td>
    function humanize()
    {
        return (dt) => moment(new Date(dt)).fromNow();
    }

    angular.module('app').filter('humanize', humanize);
}