using EgyVisionCore.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace EgyVisionRepository
{
    public interface ISTSRepository<T> : IRepository<T> where T : BaseEntity
	{
	}

	public class STSRepository<T> : EfRepository<T>, ISTSRepository<T> where T : BaseEntity
	{
        STSContext context = null;
		public STSRepository(): base()
		{
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            var Configuration = builder.Build();

            string conn = Configuration.GetSection("ApplicationSettings:STSContext").Value.ToString();

            context = new STSContext(conn);
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
