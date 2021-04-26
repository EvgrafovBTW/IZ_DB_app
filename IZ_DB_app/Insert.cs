using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IZ_DB_app
{
    class Insert
    {
        public string insertCommand;

        private List<string> vars = new List<string>();
        public Insert()
        {

        }

        public void SetVars(List<string> list)
        {
            vars = list;
        }

        public string GetTableCommand()
        {

            string result = insertCommand;
            int i = 0;
            while (result.Contains("?"))
            {
                int n = result.IndexOf("?");
                result = result.Remove(n, 1).Insert(n, vars[i]);
                i++;
            }
            return result;
        }
    }
}
