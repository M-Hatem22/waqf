using EgyVisionCore.Entities;
using EgyVisionCore.Entities.EgyVision;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace EgyVisionRepository
{
    public interface IEgyVisionRepository<T> : IRepository<T> where T : BaseEntity
    {
        DatabaseFacade Database();
    }

    public class EgyVisionRepository<T> : EfRepository<T>, IEgyVisionRepository<T> where T : BaseEntity
    {
        EgyVisionContext context = null;
        public DatabaseFacade Database()
        {
            return context.Database;
        }

        public EgyVisionRepository() : base()
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var Configuration = builder.Build();

            string conn = Configuration.GetSection("ApplicationSettings:EgyVisionContext").Value.ToString();

            context = new EgyVisionContext(conn);
            base.SetContext(context);
        }




        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                context?.Dispose();
                GC.Collect();
                GC.SuppressFinalize(this);
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

    }
}
