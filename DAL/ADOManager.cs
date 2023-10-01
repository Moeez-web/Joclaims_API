using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    public class ADOManager
    {
        public static ADOManager Instance { get; } = new ADOManager();
        public string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public int ExecuteNonQuery(string cmdText, CommandType cmdType, List<SqlParameter> parameters = null)
        {
            using (var sCon = new SqlConnection(GetConnectionString()))
            {
                using (var sCmd = new SqlCommand(cmdText, sCon))
                {
                    sCmd.CommandTimeout = 600;
                    sCmd.CommandType = cmdType;
                    if (null != parameters)
                    {
                        sCmd.Parameters.AddRange(parameters.ToArray());
                    }
                    sCon.Open();
                    return sCmd.ExecuteNonQuery();
                }
            }
        }
        public object ExecuteScalar(string cmdText, CommandType cmdType, List<SqlParameter> parameters = null)
        {
            using (var sCon = new SqlConnection(GetConnectionString()))
            {
                using (var sCmd = new SqlCommand(cmdText, sCon))
                {
                    sCmd.CommandType = cmdType;
                    if (null != parameters)
                    {
                        sCmd.Parameters.AddRange(parameters.ToArray());
                    }
                    sCon.Open();
                    return sCmd.ExecuteScalar();
                }
            }
        }
        public DataTable DataTable(string cmdText, CommandType cmdType, List<SqlParameter> parameters = null)
        {
            using (var sCon = new SqlConnection(GetConnectionString()))
            {
                using (var sCmd = new SqlCommand(cmdText, sCon))
                {
                    sCmd.CommandType = cmdType;
                    sCmd.CommandTimeout = 0;
                    if (null != parameters)
                    {
                        sCmd.Parameters.AddRange(parameters.ToArray());
                    }
                    sCon.Open();
                    using (var sDReader = sCmd.ExecuteReader())
                    {
                        using (var dt = new DataTable())
                        {
                            var schemaTable = sDReader.GetSchemaTable();
                            if (schemaTable != null)
                                foreach (DataRow dataRow in schemaTable.Rows)
                                {
                                    var dColumn = new DataColumn
                                    {
                                        ColumnName = dataRow["ColumnName"].ToString(),
                                        DataType = Type.GetType(dataRow["DataType"].ToString()),
                                        ReadOnly = (bool)dataRow["IsReadOnly"],
                                        AutoIncrement = (bool)dataRow["IsAutoIncrement"],
                                        Unique = (bool)dataRow["IsUnique"]
                                    };
                                    dt.Columns.Add(dColumn);
                                }
                            while (sDReader.Read())
                            {
                                var dRow = dt.NewRow();
                                for (var i = 0; i < dt.Columns.Count; i++)
                                {
                                    dRow[i] = sDReader[i];
                                }
                                dt.Rows.Add(dRow);
                            }
                            return dt;
                        }
                    }
                }
            }
        }
        public DataRow FirstOrDefault(string cmdText, CommandType cmdType, List<SqlParameter> parameters = null)
        {
            using (var sCon = new SqlConnection(GetConnectionString()))
            {
                using (var sCmd = new SqlCommand(cmdText, sCon))
                {
                    sCmd.CommandType = cmdType;
                    sCmd.CommandTimeout = 0;
                    if (null != parameters)
                    {
                        sCmd.Parameters.AddRange(parameters.ToArray());
                    }
                    sCon.Open();
                    using (var sDReader = sCmd.ExecuteReader())
                    {
                        if (sDReader.HasRows)
                        {
                            using (var dt = new DataTable())
                            {
                                var schemaTable = sDReader.GetSchemaTable();
                                if (schemaTable != null)
                                    foreach (DataRow dataRow in schemaTable.Rows)
                                    {
                                        var dColumn = new DataColumn
                                        {
                                            ColumnName = dataRow["ColumnName"].ToString(),
                                            DataType = Type.GetType(dataRow["DataType"].ToString()),
                                            ReadOnly = (bool)dataRow["IsReadOnly"],
                                            AutoIncrement = (bool)dataRow["IsAutoIncrement"],
                                            Unique = (bool)dataRow["IsUnique"]
                                        };
                                        dt.Columns.Add(dColumn);
                                    }
                                if (sDReader.Read())
                                {
                                    var dRow = dt.NewRow();
                                    for (var i = 0; i < dt.Columns.Count; i++)
                                    {
                                        dRow[i] = sDReader[i];
                                    }
                                    dt.Rows.Add(dRow);
                                }
                                return dt.AsEnumerable().Cast<DataRow>().First();
                            }
                        }
                        return null;
                    }
                }
            }
        }
        public DataRow LastOrDefault(string cmdText, CommandType cmdType, List<SqlParameter> parameters = null)
        {
            using (var sCon = new SqlConnection(GetConnectionString()))
            {
                using (var sCmd = new SqlCommand(cmdText, sCon))
                {
                    sCmd.CommandType = cmdType;
                    sCmd.CommandTimeout = 0;
                    if (null != parameters)
                    {
                        sCmd.Parameters.AddRange(parameters.ToArray());
                    }
                    sCon.Open();
                    using (var sDReader = sCmd.ExecuteReader())
                    {
                        if (sDReader.HasRows)
                        {
                            using (var dt = new DataTable())
                            {
                                var schemaTable = sDReader.GetSchemaTable();
                                if (schemaTable != null)
                                    foreach (DataRow dataRow in schemaTable.Rows)
                                    {
                                        var dColumn = new DataColumn
                                        {
                                            ColumnName = dataRow["ColumnName"].ToString(),
                                            DataType = Type.GetType(dataRow["DataType"].ToString()),
                                            ReadOnly = (bool)dataRow["IsReadOnly"],
                                            AutoIncrement = (bool)dataRow["IsAutoIncrement"],
                                            Unique = (bool)dataRow["IsUnique"]
                                        };
                                        dt.Columns.Add(dColumn);
                                    }
                                var dRow = dt.NewRow();
                                while (sDReader.Read())
                                {
                                    for (var i = 0; i < dt.Columns.Count; i++)
                                    {
                                        dRow[i] = sDReader[i];
                                    }
                                }
                                dt.Rows.Add(dRow);
                                return dt.AsEnumerable().Cast<DataRow>().Last();
                            }
                        }
                        return null;
                    }
                }
            }
        }
        public DataSet DataSet(string cmdText, CommandType cmdType, List<SqlParameter> parameters = null)
        {
            using (var sCon = new SqlConnection(GetConnectionString()))
            {
                using (var sCmd = new SqlCommand(cmdText, sCon))
                {
                    sCmd.CommandTimeout = 300;
                    sCmd.CommandType = cmdType;
                    if (null != parameters)
                    {
                        sCmd.Parameters.AddRange(parameters.ToArray());
                    }
                    using (var sDAdapter = new SqlDataAdapter(sCmd))
                    {
                        using (var ds = new DataSet())
                        {
                            sDAdapter.Fill(ds);
                            return ds;
                        }
                    }
                }
            }
        }
    }
}