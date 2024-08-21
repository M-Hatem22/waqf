using EgyVisionCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EgyVisionRepository
{
    public partial class CustomContext : DbContext, IDbContext
    {
        public CustomContext()
            : base()
        {

        }
        public CustomContext(DbContextOptions existingConnection, bool contextOwnsConnection)
        : base(existingConnection)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        public DbSet<TEntity> IDbSet<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public virtual IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new()
        {
            var connection = this.Database.GetDbConnection();

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = commandText;
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                    foreach (object p in parameters)
                        cmd.Parameters.Add(p);

                var reader = cmd.ExecuteReader();
                var result = DataReaderMapToList<TEntity>(reader);

                reader.Dispose();

                return (IList<TEntity>)result;
            }
        }
        public virtual long ExecuteStoredProcedure(string commandText, params object[] parameters)
        {
            var connection = this.Database.GetDbConnection();

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            object id = null;
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = commandText;
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                    foreach (var p in parameters)
                        cmd.Parameters.Add(p);

                id = cmd.ExecuteScalar();

                connection.Close();

            }

            long ret = 0;
            if (id != null)
                long.TryParse(id.ToString(), out ret);
            return ret;

        }

        public virtual bool ExecuteProcBool(string commandText, params object[] parameters)
        {
            var connection = this.Database.GetDbConnection();

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            object id = null;
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = commandText;
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                    foreach (var p in parameters)
                        cmd.Parameters.Add(p);

                id = cmd.ExecuteScalar();
                connection.Close();
            }
            bool ret = false;
            bool.TryParse(id.ToString(), out ret);
            return ret;
        }
        public virtual string ExecuteProcString(string commandText, params object[] parameters)
        {
            var connection = this.Database.GetDbConnection();

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            object id = null;
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = commandText;
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                    foreach (var p in parameters)
                        cmd.Parameters.Add(p);

                id = cmd.ExecuteScalar();

                connection.Close();
            }
            string ret = id.ToString();
            return ret;
        }
        protected List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (System.Reflection.PropertyInfo prop in obj.GetType().GetProperties())
                {
                    try
                    {
                        if (!object.Equals(dr[prop.Name], DBNull.Value))
                        {
                            prop.SetValue(obj, dr[prop.Name], null);
                        }
                    }
                    catch
                    {

                    }
                }
                list.Add(obj);
            }
            return list;
        }

        public virtual object ExecuteStoredProcedureWithOutPutParam(string commandText, SqlParameter outParam, List<SqlParameter> parameters)
        {
            var connection = this.Database.GetDbConnection();

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            using (SqlCommand cmd = (SqlCommand)connection.CreateCommand())
            {
                cmd.CommandText = commandText;
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                    foreach (var p in parameters)
                    {
                        cmd.Parameters.Add(p);
                    }

                outParam.Direction = ParameterDirection.Output;
                SqlParameter parameter = new SqlParameter(outParam.ParameterName, SqlDbType.VarChar, 1000);
                parameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parameter);

                object s = cmd.ExecuteScalar();
                if (parameter.Value != null && !string.IsNullOrEmpty(parameter.Value.ToString()))
                    outParam.Value = parameter.Value;

                connection.Close();
                return s;
            }
        }
    }
}
