
angular.module('app',
[
    'ngRoute', 'ngResource', 'ngMessages', 'ui',
    'app.accounts', 'app.categories', 'app.budgets'
]);

angular.module('app').directive('nbTable', function ()
{
    return {
        restrict: 'A',
        replace: true,
        link: function (scope, element, attrs)
        {
            element
                .addClass('table table-condensed table-striped');
        }
    };
});

angular.module('app').directive('nbCreateIcon', function ()
{
    return {
        restrict: 'A',
        replace: true,
        link: function (scope, element, attrs)
        {
            element
                .addClass('btn btn-default btn-sm')
                .html('<i class="fa fa-plus fa-fw text-success"></i>');
        }
    };
});

angular.module('app').directive('nbEditIcon', function ()
{
    return {
        restrict: 'A',
        replace: true,
        link: function (scope, element, attrs)
        {
            element
                .addClass('btn btn-default btn-sm')
                .html('<i class="fa fa-pencil fa-fw text-primary"></i>');
        }
    };
});

angular.module('app').directive('nbDeleteIcon', function ()
{
    return {
        restrict: 'A',
        replace: true,
        link: function (scope, element, attrs)
        {
            element
                .addClass('btn btn-default btn-sm')
                .html('<i class="fa fa-times fa-fw text-danger"></i>');
        }
    };
});

angular.module('app').directive('nbNextIcon', function ()
{
    return {
        restrict: 'A',
        replace: true,
        link: function (scope, element, attrs)
        {
            element
                .addClass('btn btn-default btn-sm')
                .html('Next <i class="fa fa-caret-right fa-fw"></i>');
        }
    };
});

angular.module('app').directive('nbBackIcon', function ()
{
    return {
        restrict: 'A',
        replace: true,
        link: function (scope, element, attrs)
        {
            element
                .addClass('btn btn-default btn-sm')
                .html('<i class="fa fa-caret-left fa-fw"></i> Back');
        }
    };
});

angular.module('app').directive('nbMoveIcon', function ()
{
    return {
        restrict: 'A',
        replace: true,
        link: function (scope, element, attrs)
        {
            element
                .addClass('btn btn-default btn-sm')
                .css({ cursor: "move" })
                .html('<i class="fa fa-bars fa-fw"></i>');
        }
    };
});

angular.module('app').directive('nbListIcon', function ()
{
    return {
        restrict: 'A',
        replace: true,
        link: function (scope, element, attrs)
        {
            element
                .addClass('btn btn-default btn-sm')
                .html('<i class="fa fa-list fa-fw"></i>');
        }
    };
});

angular.module('app').directive('nbDollarIcon', function ()
{
    return {
        restrict: 'A',
        replace: true,
        link: function (scope, element, attrs)
        {
            element
                .addClass('btn btn-default btn-sm')
                .html('<i class="fa fa-usd fa-fw text-success"></i>');
        }
    };
});

angular.module('app').directive('nbCrumbHome', function ()
{
    return {
        restrict: 'A',
        replace: true,
        link: function (scope, element, attrs)
        {
            element
                .addClass('btn btn-default')
                .html('<i class="glyphicon glyphicon-home"></i>');
        }
    };
});

angular.module('app').directive('nbCrumb', function ()
{
    return {
        restrict: 'A',
        replace: true,
        link: function (scope, element, attrs)
        {
            element
                .addClass('btn btn-default');
        }
    };
});

//angular.module('app').directive('toggleHasError', function ($compile)
//{
//    return {
//        restrict: 'A',
//        require: '^form',
//        link: function (scope, element, attrs, formCtrl)
//        {
//            // find the text box element, which has the 'name' attribute
//            var inputEl = element[0].querySelector('[name]');

//            var start = inputEl.selectionStart;
//            var end = inputEl.selectionEnd;

//            // convert the native text box element to an angular element
//            var inputNgEl = angular.element(inputEl);

//            // get the name on the text box so we know the property to check
//            // on the form controller
//            var inputName = inputNgEl.attr('name');

//            var formName = formCtrl.$name;

//            if (typeof element.attr('toggle-has-error') !== "undefined")
//            {
//                var hasWarningExpr = '%form.%input.$invalid'.replace(/%form/g, formName).replace(/%input/g, inputName);

//                var hasErrorExpr = '%form.%input.$invalid'.replace(/%form/g, formName).replace(/%input/g, inputName);

//                element.attr('ng-class', "{ 'has-warning': " + hasWarningExpr + ", 'has-error': " + hasErrorExpr + ' }');

//                element.removeAttr('toggle-has-error');

//                //  problem here!
//                $compile(element)(scope);
//            }
//        }
//    }
//});