using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Augment;

namespace Dynamo.Core
{
    public static class DotLiquidFilters
    {
        #region Members

        private static readonly Regex _alluc = new Regex(
            "^[A-Z0-9_]+$",
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled
            );

        private static readonly Regex _alllc = new Regex(
            "^[a-z0-9_]+$",
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled
            );

        private static readonly Regex _uclcSplit = new Regex(
            @"  (?<=[A-Z])(?=[A-Z][a-z])    #   uc before, uc lc after
            |   (?<=[^A-Z])(?=[A-Z])        #   no uc before, uc after
            |   (?<=[A-Za-z])(?=[^A-Za-z])  #   letter before not after
                ",
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled
            );

        #endregion

        #region Methods

        public static string LPad(string input, int pad)
        {
            return input.PadLeft(pad, ' ');
        }

        public static string RPad(string input, int pad)
        {
            return input.PadRight(pad, ' ');
        }

        public static string Pascal(string input)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string x in Split(input))
            {
                sb.Append(Title(x));
            }

            return sb.ToString();
        }

        public static string Camel(string input)
        {
            string x = Pascal(input);

            if (x.Length == 1)
            {
                return x.ToLower();
            }

            return x.Substring(0, 1).ToLower() + x.Substring(1);
        }

        public static string Label(string input)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string x in Split(input))
            {
                sb.AppendIf(sb.Length > 0, " ").Append(Title(x));
            }

            return sb.ToString();
        }

        public static string Title(string input)
        {
            if (input.Length == 1)
            {
                return input.ToUpper();
            }

            return input.Substring(0, 1).ToUpper() + input.Substring(1).ToLower();
        }

        private static IEnumerable<string> Split(string input)
        {
            if (input.IsNullOrEmpty())
            {
                return new string[0];
            }

            if (input.Length == 1)
            {
                return new string[] { input };
            }

            if (input.Contains("_"))
            {
                return input.Split('_');
            }

            return _uclcSplit.Split(input);
        }

        #endregion
    }
}
