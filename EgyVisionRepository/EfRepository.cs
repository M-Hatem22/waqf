using EgyVisionCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EgyVisionRepository
{
    public partial class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private IDbContext _context;
        private DbSet<T> _entities;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public EfRepository()
        {
      
        }

        public void SetContext(IDbContext cntxt)
        {
              _context = cntxt;
        }

        public virtual T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        public virtual bool Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.Entities.Add(entity);

                int result = this._context.SaveChanges();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public virtual bool RangeInsert(List<T> entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.Entities.AddRange(entity);

                int result = this._context.SaveChanges();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
       

        public virtual T InsertAndReturn(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.Entities.Add(entity);

                int result = this._context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public virtual bool Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
               
                int result = this._context.SaveChanges();

                return true;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public virtual bool Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.Entities.Remove(entity);

                int result = this._context.SaveChanges();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                //var sqlEx = ex.InnerException.InnerException
                //                       as System.Data.SqlClient.SqlException;
                //if (sqlEx != null && (sqlEx.Number == 547))
                //{
                //    throw new Exception(ex.Message);
                //    //throw new ArabiaDeleteException(sqlEx.Message);
                //}
                throw ex;
            }

        }
        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new()
        {
            return _context.ExecuteStoredProcedureList<TEntity>(commandText, parameters);
        }
        public virtual long ExecuteStoredProcedure(string cmdTxt, params object[] parameters)
        {
            return _context.ExecuteStoredProcedure(cmdTxt, parameters);
        }
        public virtual bool ExecuteProcBool(string cmdTxt, params object[] parameters)
        {
            return _context.ExecuteProcBool(cmdTxt, parameters);
        }
      
        public virtual string ExecuteProcString(string commandText, params object[] parameters)
        {
            return _context.ExecuteProcString(commandText, parameters);
        }
        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }
        protected DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.IDbSet<T>();
                return _entities;
            }
        }
        public virtual IQueryable<T> Include<TProperty>(System.Linq.Expressions.Expression<Func<T, TProperty>> path)
        {
            return Entities.Include(path);

        }
        public virtual IQueryable<T> Include(string path)
        {
            return Entities.Include(path);
        }
        public IQueryable<T> Include<TProperty>(IQueryable<T> query, System.Linq.Expressions.Expression<Func<T, TProperty>> path)
        {

            return query.Include(path);


        }
        public IQueryable<T> Include(IQueryable<T> query, string path)
        {

            return query.Include(path);

        }

        public virtual object ExecuteStoredProcedureWithOutPutParam(string commandText, SqlParameter outParam, List<SqlParameter> parameters)
        {
            return _context.ExecuteStoredProcedureWithOutPutParam(commandText, outParam, parameters);
        }
    }
}
