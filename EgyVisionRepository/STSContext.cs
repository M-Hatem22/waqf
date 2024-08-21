using EgyVisionCore.Entities.STS;
using Microsoft.EntityFrameworkCore;

namespace EgyVisionRepository
{
    public class STSContext : CustomContext
	{
		private string _conn;
		public STSContext(string connectionString): base()
		{
			// Default Constructor
			_conn = connectionString;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(_conn);
			base.OnConfiguring(optionsBuilder);
		}

		public virtual DbSet<AudienceKeys> AudienceKeys { get; set; }
		public virtual DbSet<ClientsAudiences> ClientsAudiences { get; set; }
      
    }
}
