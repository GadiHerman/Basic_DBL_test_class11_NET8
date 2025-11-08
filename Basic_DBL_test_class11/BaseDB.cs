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
        protected abstract T CreateModel(object[] row);

        protected async Task<int> InsertAsync(Dictionary<string, object> fields)
        {
            string InKey = "(" + string.Join(",", fields.Keys) + ")";
            string InValue = "VALUES(";
            cmd.Parameters.Clear();
            for (int i = 0; i < fields.Values.Count; i++)
            {
                string pn = "@" + fields.Keys.ElementAt(i);
                InValue += pn + ',';
                cmd.Parameters.AddWithValue(pn, fields.Values.ElementAt(i));
            }
            InValue = InValue.Remove(InValue.Length - 1);//remove last ,
            InValue += ")";

            string sqlCommand = $"INSERT INTO {GetTableName()}  {InKey} {InValue};";

            cmd.CommandText = sqlCommand;
            await conn.OpenAsync();
            int affectedRows = await cmd.ExecuteNonQueryAsync();
            await conn.CloseAsync();
            return affectedRows;
        }


        public async Task<List<T>> SelectAllAsync()
        {
            List<T> list = new List<T>();
            cmd.CommandText = $"SELECT * FROM {GetTableName()};";
            if (conn.State != System.Data.ConnectionState.Open)
                await conn.OpenAsync();

            reader = (MySql.Data.MySqlClient.MySqlDataReader)await cmd.ExecuteReaderAsync();
            //var readOnlyData = await reader.GetColumnSchemaAsync();
            //int size = readOnlyData.Count;
            int size = reader.FieldCount;
            object[] row;
            while (await reader.ReadAsync())
            {
                row = new object[size];
                reader.GetValues(row);
                T dto = CreateModel(row);
                list.Add(dto);
            }

            if (reader != null && !reader.IsClosed)
                await reader.CloseAsync();
            cmd.Parameters.Clear();
            if (conn.State == System.Data.ConnectionState.Open)
                await conn.CloseAsync();

            return list;
        }

        protected async Task<int> UpdateAsync(Dictionary<string, object> fields, object id)
        {
            string setClause = "SET ";
            cmd.Parameters.Clear();
            foreach (var field in fields)
            {
                string pn = "@" + field.Key;
                setClause += $"{field.Key} = {pn},";
                cmd.Parameters.AddWithValue(pn, field.Value);
            }
            setClause = setClause.Remove(setClause.Length - 1); // remove last

            string idParamName = "@" + GetPrimaryKeyName();
            cmd.Parameters.AddWithValue(idParamName, id);

            string sqlCommand = $"UPDATE {GetTableName()} {setClause} WHERE {GetPrimaryKeyName()} = {idParamName};";

            cmd.CommandText = sqlCommand;
            await conn.OpenAsync();
            int affectedRows = await cmd.ExecuteNonQueryAsync();
            await conn.CloseAsync();

            return affectedRows;
        }

        public async Task<int> DeleteAsync(object id)
        {
            cmd.Parameters.Clear();
            string idParamName = "@" + GetPrimaryKeyName();
            cmd.Parameters.AddWithValue(idParamName, id);

            string sqlCommand = $"DELETE FROM {GetTableName()} WHERE {GetPrimaryKeyName()} = {idParamName};";

            cmd.CommandText = sqlCommand;
            await conn.OpenAsync();
            int affectedRows = await cmd.ExecuteNonQueryAsync();
            await conn.CloseAsync();

            return affectedRows;
        }
    }
}
