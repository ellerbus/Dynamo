'use strict';

function AnalysisViewModel(data)
{
    var self = this;

    self.headers = data.headers;

    self.details = data.details;

    for (var x = 0, y = self.headers.length; x < y; x++)
    {
        updateHeader(x);

        self.headers[x].tooltip = function ()
        {
            var h = this;

            if (h.isHistory)
            {
                if (h.variance != 0)
                {
                    return ko.filters.number(Math.abs(h.variance)) + ' ' + (h.variance > 0 ? 'Under' : 'OVER') + ' Budget';
                }

                return 'On Target';
            }

            if (h.isCurrent)
            {
                return 'Beginning Balance ' + ko.filters.number(parseInt(h.balance));
            }

            return '';
        };
    }

    ko.track(self);

    function updateHeader(idx)
    {
        var w = self.headers[idx];

        w.actual = getValue(idx, 'actual');
        w.budget = getValue(idx, 'budget');

        w.variance = w.budget + w.actual;

        if (w.isHistory)
        {
            w.projection = 0;
        }
        else
        {
            var prev = self.headers[idx - 1];

            var balance = w.balance + prev.projection;

            if (!w.isCurrent)
            {
                balance += prev.variance;
            }

            w.projection = balance + w.variance;
        }
    };

    function getValue(idx, field)
    {
        var value = 0;

        for (var key in self.details)
        {
            var d = self.details[key];

            value += d.values[idx][field];
        }

        return value;
    }
};