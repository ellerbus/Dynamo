module core.app
{
    mdy.$inject = ['$filter'];

    function mdy($filter)
    {
        return (dt) => $filter('date')(dt, 'M/d/yy');
    }

angular.module('app').filter('mdy', mdy);
}