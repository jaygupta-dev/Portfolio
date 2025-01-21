using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace DataAccessLayer.UtilityClass
{
    public class DataUtility
    {
        private readonly string _connectionString;

        public DataUtility(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DBConnectionString");
        }

        #region sql squery
        public int ExecuteSqlText(string sql)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }
        public int ExecuteSqlText(string sql, SqlParameter[] parameters = null)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }
        public int ExecuteSqlText(string sql, NameValueCollection parameters = null)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        foreach (string key in parameters)
                        {
                            command.Parameters.AddWithValue(key, parameters[key]);
                        }
                    }

                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }
        public int ExecuteSqlText(string sql, object parameters = null)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        var parameterDict = parameters as IDictionary<string, object>;
                        if (parameterDict != null)
                        {
                            foreach (var param in parameterDict)
                            {
                                command.Parameters.AddWithValue(param.Key, param.Value);
                            }
                        }
                    }

                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }

        public DataTable GetDataTableText(string sql)
        {
            DataTable result = new DataTable();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(result);
                    }
                }
            }

            return result;
        }
        public DataTable GetDataTableText(string sql, SqlParameter[] parameters = null)
        {
            DataTable result = new DataTable();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(result);
                    }
                }
            }

            return result;
        }
        public DataTable GetDataTableText(string sql, object parameters = null)
        {
            DataTable result = new DataTable();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        var paramDict = parameters as IDictionary<string, object>;
                        if (paramDict != null)
                        {
                            foreach (var param in paramDict)
                            {
                                command.Parameters.AddWithValue(param.Key, param.Value);
                            }
                        }
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(result);
                    }
                }
            }

            return result;
        }
        public DataTable GetDataTableText(string sql, NameValueCollection parameters = null)
        {
            DataTable result = new DataTable();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        foreach (string key in parameters)
                        {
                            command.Parameters.AddWithValue(key, parameters[key]);
                        }
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(result);
                    }
                }
            }

            return result;
        }

        public DataSet GetDataSetText(string sql)
        {
            DataSet result = new DataSet();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(result);
                    }
                }
            }

            return result;
        }
        public DataSet GetDataSetText(string sql, SqlParameter[] parameters = null)
        {
            DataSet result = new DataSet();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(result);
                    }
                }
            }

            return result;
        }
        public DataSet GetDataSetText(string sql, NameValueCollection parameters = null)
        {
            DataSet result = new DataSet();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        foreach (string key in parameters)
                        {
                            command.Parameters.AddWithValue(key, parameters[key]);
                        }
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(result);
                    }
                }
            }

            return result;
        }
        public DataSet GetDataSetText(string sql, object parameters = null)
        {
            DataSet result = new DataSet();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        var paramDict = parameters as IDictionary<string, object>;
                        if (paramDict != null)
                        {
                            foreach (var param in paramDict)
                            {
                                command.Parameters.AddWithValue(param.Key, param.Value);
                            }
                        }
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(result);
                    }
                }
            }

            return result;
        }

        public object ExecuteScalarText(string sql)
        {
            object result = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    result = command.ExecuteScalar();
                }
            }

            return result;
        }
        public object ExecuteScalarText(string sql, SqlParameter[] parameters = null)
        {
            object result = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    result = command.ExecuteScalar();
                }
            }

            return result;
        }
        public object ExecuteScalarText(string sql, NameValueCollection parameters = null)
        {
            object result = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        foreach (string key in parameters)
                        {
                            command.Parameters.AddWithValue(key, parameters[key]);
                        }
                    }

                    result = command.ExecuteScalar();
                }
            }

            return result;
        }
        public object ExecuteScalarText(string sql, object parameters = null)
        {
            object result = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        var paramDict = parameters as IDictionary<string, object>;
                        if (paramDict != null)
                        {
                            foreach (var param in paramDict)
                            {
                                command.Parameters.AddWithValue(param.Key, param.Value);
                            }
                        }
                    }

                    result = command.ExecuteScalar();
                }
            }

            return result;
        }


        #endregion

        #region procedure
        public int ExecuteSqlSP(string storedProcedure)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }
        public int ExecuteSqlSP(string storedProcedure, SqlParameter[] parameters = null)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }
        public int ExecuteSqlSP(string storedProcedure, NameValueCollection parameters = null)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        foreach (string key in parameters)
                        {
                            command.Parameters.AddWithValue(key, parameters[key]);
                        }
                    }

                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }
        public int ExecuteSqlSP(string storedProcedure, object parameters = null)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        var parameterDict = parameters as IDictionary<string, object>;
                        if (parameterDict != null)
                        {
                            foreach (var param in parameterDict)
                            {
                                command.Parameters.AddWithValue(param.Key, param.Value);
                            }
                        }
                    }

                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }

        public DataTable GetDataTableSP(string storedProcedure)
        {
            DataTable result = new DataTable();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(result);
                    }
                }
            }

            return result;
        }
        public DataTable GetDataTableSP(string storedProcedure, SqlParameter[] parameters = null)
        {
            DataTable result = new DataTable();

            try
            {
                // Ensure connection string is not null or empty
                if (string.IsNullOrEmpty(_connectionString))
                {
                    throw new InvalidOperationException("Connection string is not set.");
                }

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters if provided
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        // Use SqlDataAdapter to fill the DataTable with the result of the stored procedure
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (this could be to a file, database, or other logging mechanism)
                Console.Error.WriteLine($"Error executing stored procedure: {ex.Message}");
                // Optionally, you can rethrow the exception or return an empty DataTable
                throw;
            }

            return result;
        }

        public DataTable GetDataTableSP(string storedProcedure, NameValueCollection parameters = null)
        {
            DataTable result = new DataTable();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        foreach (string key in parameters)
                        {
                            command.Parameters.AddWithValue(key, parameters[key]);
                        }
                    }
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(result);
                    }
                }
            }

            return result;
        }
        public DataTable GetDataTableSP(string storedProcedure, object parameters = null)
        {
            DataTable result = new DataTable();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        var parameterDict = parameters as IDictionary<string, object>;
                        if (parameterDict != null)
                        {
                            foreach (var param in parameterDict)
                            {
                                command.Parameters.AddWithValue(param.Key, param.Value);
                            }
                        }
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(result);
                    }
                }
            }

            return result;
        }

        public DataSet GetDataSetSP(string storedProcedure)
        {
            DataSet result = new DataSet();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(result);
                    }
                }
            }

            return result;
        }
        public DataSet GetDataSetSP(string storedProcedure, SqlParameter[] parameters = null)
        {
            DataSet result = new DataSet();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(result);
                    }
                }
            }

            return result;
        }
        public DataSet GetDataSetSP(string storedProcedure, NameValueCollection parameters = null)
        {
            DataSet result = new DataSet();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        foreach (string key in parameters)
                        {
                            command.Parameters.AddWithValue(key, parameters[key]);
                        }
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(result);
                    }
                }
            }

            return result;
        }
        public DataSet GetDataSetSP(string storedProcedure, object parameters = null)
        {
            DataSet result = new DataSet();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        var parameterDict = parameters as IDictionary<string, object>;
                        if (parameterDict != null)
                        {
                            foreach (var param in parameterDict)
                            {
                                command.Parameters.AddWithValue(param.Key, param.Value);
                            }
                        }
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(result);
                    }
                }
            }

            return result;
        }

        public object ExecuteScalarSP(string storedProcedure)
        {
            object result = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    result = command.ExecuteScalar();
                }
            }

            return result;
        }
        public object ExecuteScalarSP(string storedProcedure, SqlParameter[] parameters = null)
        {
            object result = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    result = command.ExecuteScalar();
                }
            }

            return result;
        }
        public object ExecuteScalarSP(string storedProcedure, NameValueCollection parameters = null)
        {
            object result = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        foreach (string key in parameters)
                        {
                            command.Parameters.AddWithValue(key, parameters[key]);
                        }
                    }

                    result = command.ExecuteScalar();
                }
            }

            return result;
        }
        public object ExecuteScalarSP(string storedProcedure, object parameters = null)
        {
            object result = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        var parameterDict = parameters as IDictionary<string, object>;
                        if (parameterDict != null)
                        {
                            foreach (var param in parameterDict)
                            {
                                command.Parameters.AddWithValue(param.Key, param.Value);
                            }
                        }
                    }

                    result = command.ExecuteScalar();
                }
            }

            return result;
        }


        #endregion
    }
}
