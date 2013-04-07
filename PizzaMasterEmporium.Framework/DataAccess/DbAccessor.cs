using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PizzaMasterEmporium.Framework.DataAccess
{
    public delegate T RecordReaderDelegate<out T>(SqlDataReader reader);
    public delegate void SqlCommandHandler(SqlCommand command);
    public delegate void SqlParameterHandler(SqlParameterCollection parameterCollection);

    public class DbAccessor : IDbAccessor
    {
        private readonly string _connectionString;
        public string ConnectionString { get { return _connectionString; } }

        public DbAccessor(IDatabaseConnectionStringProvider provider)
        {
            _connectionString = provider.ConnectionString;
        }

        public T PerformSpScalar<T>(String storedProcedure, SqlParameterHandler handler)
        {
            return PerformSpScalar<T>(storedProcedure, handler, 0);
        }

        public T PerformSpScalar<T>(String storeProcedure, SqlParameterHandler handler, int commandTimeout)
        {
            return ExecuteCommand(cmd =>
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storeProcedure;
                if (commandTimeout != 0)
                    cmd.CommandTimeout = commandTimeout;

                handler(cmd.Parameters);
                var x = cmd.ExecuteScalar();
                return (T)x;
            });
        }

        public void PerformSpNonQuery(String storedProcedure, SqlParameterHandler handler)
        {
            PerformSpNonQuery(storedProcedure, handler, 0);
        }

        public void PerformSpNonQuery(String storedProcedureName, SqlParameterHandler handler, int commandTimeout)
        {
            ExecuteCommand(cmd =>
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedureName;
                if (commandTimeout != 0)
                    cmd.CommandTimeout = commandTimeout;

                handler(cmd.Parameters);
                cmd.ExecuteNonQuery();
            });
        }

        public List<T> PerformSpRead<T>(ParametersAndReader<T> parametersAndReader, String storedProcedure)
        {
            return PerformSpRead(parametersAndReader, storedProcedure, 0);
        }

        public List<T> PerformSpRead<T>(ParametersAndReader<T> parametersAndReader, String storedProcedureName, int commandTimeout)
        {
            return ExecuteStoredProcedure(storedProcedureName, parametersAndReader, reader =>
            {
                var result = new List<T>();

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        result.Add(parametersAndReader.RecordReader(reader));
                    }
                }

                return result;
            }, commandTimeout);
        }
        
        public T PerformSpReadSingle<T>(ParametersAndReader<T> parametersAndReader, string storedProcedureName)
        {
            return ExecuteStoredProcedure(storedProcedureName, parametersAndReader,
                                          reader =>
                                          {
                                              T result = default(T);

                                              if (reader != null)
                                              {
                                                  if (reader.Read())
                                                  {
                                                      result = parametersAndReader.RecordReader(reader);
                                                  }
                                              }

                                              return result;

                                          });
        }

        public void PerformSQLNonQuery(String commandText, SqlParameterHandler handler)
        {
            PerformSQLNonQuery(commandText, handler, 0);
        }

        public void PerformSQLNonQuery(String commandText, SqlParameterHandler handler, int commandTimeout)
        {
            ExecuteCommand(cmd =>
                                {
                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandText = commandText;
                                    if (commandTimeout != 0)
                                        cmd.CommandTimeout = commandTimeout;

                                    handler(cmd.Parameters);
                                    cmd.ExecuteNonQuery();
                                }
                           );
        }

        public DataTable PerformSpDataTable(String storedProcedure, SqlParameterHandler handler)
        {
            return PerformSpDataTable(storedProcedure, handler, 0);
        }

        public DataTable PerformSpDataTable(String storedProcedure, SqlParameterHandler handler, int commandTimeout)
        {
            return ExecuteCommand(cmd =>
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedure;
                if (commandTimeout != 0)
                    cmd.CommandTimeout = commandTimeout;

                handler(cmd.Parameters);

                using (var reader = cmd.ExecuteReader())
                {
                    DataTable table = new DataTable();
                    table.Load(reader);
                    return table;
                }
            });
        }

        private TResult ExecuteStoredProcedure<T, TResult>(string storedProcedureName, ParametersAndReader<T> parametersAndReader, Func<SqlDataReader, TResult> func)
        {
            return ExecuteStoredProcedure(storedProcedureName, parametersAndReader, func, 0);
        }

        private TResult ExecuteStoredProcedure<T, TResult>(string storedProcedureName, ParametersAndReader<T> parametersAndReader, Func<SqlDataReader, TResult> func, int commandTimeout)
        {
            return ExecuteCommand(cmd =>
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedureName;
                if (commandTimeout != 0)
                    cmd.CommandTimeout = commandTimeout;

                parametersAndReader.Parameters(cmd.Parameters);

                using (var reader = cmd.ExecuteReader())
                {
                    return func(reader);
                }
            });
        }

        private T ExecuteCommand<T>(Func<SqlCommand, T> func)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    return func(cmd);
                }
            }
        }

        private void ExecuteCommand(Action<SqlCommand> action)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    action(cmd);
                }
            }
        }
    }
}