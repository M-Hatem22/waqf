using EgyVisionCore.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace EgyVisionRepository
{
    public partial interface IRepository<T> where T : BaseEntity
    {
        T GetById(object id);
        bool Insert(T entity);
        bool RangeInsert(List<T> entity);
        T InsertAndReturn(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        IQueryable<T> Table { get; }

        IQueryable<T> Include<TProperty>(Expression<Func<T, TProperty>> path);
        IQueryable<T> Include(string path);

        IQueryable<T> Include<TProperty>(IQueryable<T> query, Expression<Func<T, TProperty>> path);
        IQueryable<T> Include(IQueryable<T> query, string path);
        long ExecuteStoredProcedure(string cmdTxt, params object[] parameters);
        bool ExecuteProcBool(string cmdTxt, params object[] parameters);
        string ExecuteProcString(string commandText, params object[] parameters);
        // Test Commit
        IList<T> ExecuteStoredProcedureList<T>(string commandText, params object[] parameters) where T : BaseEntity, new();
        object ExecuteStoredProcedureWithOutPutParam(string commandText, SqlParameter outParam, List<SqlParameter> parameters);
    }
}
