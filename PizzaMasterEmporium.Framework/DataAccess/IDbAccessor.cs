using System;
using System.Collections.Generic;
using System.Data;

namespace PizzaMasterEmporium.Framework.DataAccess
{
    public interface IDbAccessor
    {
        string ConnectionString { get; }

        List<T> PerformSpRead<T>(ParametersAndReader<T> parametersAndReader, string storedProcedureName);
        List<T> PerformSpRead<T>(ParametersAndReader<T> parametersAndReader, string storedProcedureName, int commandTimeout);

        T PerformSpReadSingle<T>(ParametersAndReader<T> parametersAndReader, string storedProcedureName);

        T PerformSpScalar<T>(string storeProcedureName, SqlParameterHandler handler);
        T PerformSpScalar<T>(string storeProcedureName, SqlParameterHandler handler, int commandTimeout);

        void PerformSpNonQuery(String storedProcedureName, SqlParameterHandler handler);
        void PerformSpNonQuery(String storedProcedureName, SqlParameterHandler handler, int commandTimeout);

        void PerformSQLNonQuery(String commandText, SqlParameterHandler handler);
        void PerformSQLNonQuery(String commandText, SqlParameterHandler handler, int commandTimeout);

        DataTable PerformSpDataTable(String storedProcedure, SqlParameterHandler handler);
        DataTable PerformSpDataTable(String storedProcedure, SqlParameterHandler handler, int commandTimeout);
    }
}