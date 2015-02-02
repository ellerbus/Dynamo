/*
 *  HELPER FUNCTIONS - extended onto VM
 *
 */
var NB =
{
    hasData: false,

    buildError: function (error)
    {
        var msg = "";

        if (error.data)
        {
            if (error.data.message)
            {
                msg = error.data.message;
            }

            if (error.data.exceptionMessage)
            {
                msg += ": " + error.data.exceptionMessage;
            }
        }

        if (msg === "")
        {
            if (error.status == 404 && error.statusText)
            {
                msg = error.statusText;
            }
            else
            {
                msg = "An unexpected error occurred - dang it!  " + error;
            }
        }

        return msg;
    },

    applyError: function (vm, form, error)
    {
        var d = error.data;

        vm.serverErrors = [];

        vm.serverErrorSummary = [];

        for (var prop in vm.user)
        {
            if (form[prop])
            {
                form[prop].$setValidity('server', true);
            }
        }

        if (d && d.modelState)
        {
            for (var key in d.modelState)
            {
                var msg = d.modelState[key];

                if (form[key])
                {
                    form[key].$setValidity('server', false);

                    var x = Array.isArray(msg) ? msg.join(' ') : msg;

                    vm.serverErrors[key] = x;
                }

                if (Array.isArray(msg))
                {
                    for (var msgKey in msg)
                    {
                        vm.serverErrorSummary[vm.serverErrorSummary.length] = msg[msgKey];
                    }
                }
                else
                {
                    vm.serverErrorSummary[vm.serverErrorSummary.length] = msg;
                }
            }
        }
        else
        {
            vm.serverErrorSummary[vm.serverErrorSummary.length] = NB.buildError(error);
        }
    }
};