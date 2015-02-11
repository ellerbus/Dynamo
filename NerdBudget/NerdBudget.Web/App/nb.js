/*
 *  HELPER FUNCTIONS - extended onto VM
 *
 */
var NB =
{
    applyError: function (error, vm, form)
    {
        var d = error.data;

        var i = 0;

        vm.serverErrors = [];

        if (error.status == 404 && error.statusText)
        {
            vm.serverErrors[i++] = error.statusText;
        }

        if (d)
        {
            if (d.message)
            {
                vm.serverErrors[i++] = d.message;
            }

            if (d.exceptionMessage)
            {
                vm.serverErrors[i++] = d.exceptionMessage;
            }
        }

        if (form)
        {
            //for (var prop in form)
            //{
            //    if (form[prop])
            //    {
            //        form[prop].$setValidity('server', true);
            //    }
            //}
        }

        if (d && d.modelState)
        {
            for (var key in d.modelState)
            {
                var msg = d.modelState[key];

                if (form && form[key])
                {
                    form[key].$setValidity('server', false);
                }

                var isArray = Array.isArray(msg);

                vm.serverErrors[key] = isArray ? msg.join(' ') : msg;

                if (isArray)
                {
                    for (var mkey in msg)
                    {
                        vm.serverErrors[i++] = msg[mkey];
                    }
                }
                else
                {
                    vm.serverErrors[i++] = msg;
                }
            }
        }
    }
};