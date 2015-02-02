using System.ComponentModel;

namespace NerdBudget.Core.Models
{
    /// <summary>
    /// 
    /// </summary>
    public enum BudgetFrequencies
    {
        [Description("(none)")]
        NO,
        [Description("Weekly")]
        W1,
        [Description("Every 2 Weeks")]
        W2,
        [Description("Every 3 Weeks")]
        W3,
        [Description("Every 4 Weeks")]
        W4,
        [Description("Every 5 Weeks")]
        W5,
        [Description("Every 6 Weeks")]
        W6,
        /*
        [Description("Every Monday"), EnumValueMap("XM")]
        XM,
        [Description("Every Tuesday"), EnumValueMap("XT")]
        XT,
        [Description("Every Wednesday"), EnumValueMap("XW")]
        XW,
        [Description("Every Thursday"), EnumValueMap("XR")]
        XR,
        [Description("Every Friday"), EnumValueMap("XF")]
        XF,
        [Description("Every Saturday"), EnumValueMap("XS")]
        XS,
        [Description("Every Sunday"), EnumValueMap("XN")]
        XN,
         * */
        [Description("Monthly")]
        M1,
        [Description("Every 2 Months")]
        M2,
        [Description("Twice Monthly (15th / EOM)")]
        MT,
        [Description("Quarterly")]
        Q1,
        [Description("Yearly")]
        Y1
    }
}
