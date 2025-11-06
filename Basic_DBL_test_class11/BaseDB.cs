using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Basic_DBL_test_class11
{
    public abstract class BaseDB<T> : DB
    {
        protected abstract string GetTableName();
        protected abstract string GetPrimaryKeyName();

        protected async Task<int> InsertAsync(Dictionary<string, object> fields)
        {
            string InKey = "(" + string.Join(",", fields.Keys) + ")";
            string InValue = "VALUES(";
            for (int i = 0; i < fields.Values.Count; i++)
            {
                string pn = "@" + i;
                InValue += pn + ',';
                cmd.Parameters.AddWithValue(pn, fields.Values.ElementAt(i));
            }
            InValue = InValue.Remove(InValue.Length - 1);//remove last ,
            InValue += ")";

            string sqlCommand = $"INSERT INTO {GetTableName()}  {InKey} {InValue};";

            cmd.CommandText = sqlCommand;
            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            await conn.CloseAsync();
            return 1;
        }
    }
}
