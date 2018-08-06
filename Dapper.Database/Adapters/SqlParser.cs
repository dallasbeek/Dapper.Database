using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Database.Adapters
{
    internal class SqlParser
    {
        internal SqlParser(string sql)
        {
            var words = sql.Split(new string[] { " ", "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            Sql = string.Join(" ", words);

            CommandClause = words.FirstOrDefault(w => new string[] { "SELECT", "INSERT", "UPDATE", "DELETE", "CALL", "EXECUTE", "EXEC" }.Contains(w, StringComparer.OrdinalIgnoreCase));

            var fromLoc = words.IndexOf(words.FirstOrDefault(w => w.Equals("FROM", StringComparison.OrdinalIgnoreCase)));

            if (IsSelect)
            {
                var commandLoc = words.IndexOf(CommandClause);

                if (fromLoc > -1)
                {
                    SelectColumns = string.Join(" ", words.Skip(commandLoc + 1).Take(fromLoc - commandLoc - 1));
                }
                else
                {
                    SelectColumns = string.Join(" ", words.Skip(commandLoc + 1));
                }
            }

            if (fromLoc != -1)
            {
                var endFromLoc = words.IndexOf(words.Skip(fromLoc).FirstOrDefault(w =>
                    w.Equals("WHERE", StringComparison.OrdinalIgnoreCase)
                    || w.Equals("GROUP", StringComparison.OrdinalIgnoreCase)
                    || w.Equals("HAVING", StringComparison.OrdinalIgnoreCase)
                    || w.Equals("ORDER", StringComparison.OrdinalIgnoreCase)
                ));

                if (endFromLoc > -1)
                {
                    FromClause = string.Join(" ", words.Skip(fromLoc).Take(endFromLoc - fromLoc));
                }
                else
                {
                    FromClause = string.Join(" ", words.Skip(fromLoc));
                }

            }

            var orderLoc = -1;
            if (words.Count > 1)
            {
                for (var c = words.Count - 1; c != 0; c--)
                {
                    if (orderLoc == -1 && words[c].Equals("BY", StringComparison.OrdinalIgnoreCase) && words[c - 1].Equals("ORDER", StringComparison.OrdinalIgnoreCase))
                    {
                        orderLoc = c - 1;
                        break;
                    }
                }
            }
            if (orderLoc != -1)
            {
                OrderByClause = string.Join(" ", words.Skip(orderLoc));
            }
        }

        public string Sql { get; set; }

        public string CommandClause { get; set; }
        public string SelectColumns { get; set; }
        public string FromClause { get; set; }
        public string OrderByClause { get; set; }

        public bool IsSelect
        {
            get
            {
                return !string.IsNullOrEmpty(CommandClause) && CommandClause.Equals("SELECT", StringComparison.OrdinalIgnoreCase);
            }
        }

        public bool IsDelete
        {
            get
            {
                return !string.IsNullOrEmpty(CommandClause) && CommandClause.Equals("DELETE", StringComparison.OrdinalIgnoreCase);
            }
        }

    }
}
