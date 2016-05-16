module core.app
{
    mdyhm.$inject = ['$filter'];

    function mdyhm($filter)
    {
        return (dt) => $filter('date')(dt, 'M/d/yy h:mm a');
    }

angular.module('app').filter('mdyhm', mdyhm);
}