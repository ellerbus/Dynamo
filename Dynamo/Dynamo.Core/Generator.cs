using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DotLiquid;

namespace Dynamo.Core
{
    class Generator
    {
        #region Members

        static Generator()
        {
            Template.RegisterFilter(typeof(DotLiquidFilters));

            Template.RegisterSafeType(
                typeof(GeneratorTable),
                typeof(GeneratorTable).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(x => x.Name).ToArray()
                );

            Template.RegisterSafeType(
                typeof(GeneratorColumn),
                typeof(GeneratorColumn).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(x => x.Name).ToArray()
                );
        }

        private GeneratorTable _table;
        private Context _context;

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="variables"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public string GetContents(GeneratorTable table, IDictionary<string, string> variables, string source)
        {
            _table = table;

            Hash h = new Hash();

            h.Add("TABLE", _table);

            foreach (var item in variables)
            {
                h.Add(item.Key, item.Value);
            }

            _context = new Context(new List<Hash>(), h, new Hash(), true);

            RenderParameters p = new RenderParameters { Context = _context };

            Template t = Template.Parse(source);

            return t.Render(p);
        }

        private string GetVariable(string key)
        {
            Hash h = _context.Scopes.FirstOrDefault();

            return h[key].ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        public string FileName { get { return GetVariable("FILENAME"); } }

        #endregion
    }
}
