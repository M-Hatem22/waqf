using EgyVisionCore.Entities.EgyVision;
using Microsoft.EntityFrameworkCore;

namespace EgyVisionRepository
{
    public class EgyVisionContext : CustomContext
	{
		private string _conn;
		public EgyVisionContext(string connectionString): base()
		{
			// Default Constructor
			_conn = connectionString;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(_conn);
			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AspNetUserRoles>().HasKey(c => new { c.RoleId, c.UserId });
			modelBuilder.Entity<AspNetUserLogins>().HasKey(c => new { c.LoginProvider, c.ProviderKey, c.UserId });
			modelBuilder.Entity<UsersRolesView>().HasKey(c => new { c.UserId, c.RoleId });
			modelBuilder.Entity<UsersGroupsRolesView>().HasKey(c => new { c.UserId, c.RoleId, c.GroupId });
		}
		public virtual DbSet<Addresses> Addresses { get; set; }
		public virtual DbSet<AspNetGroups> AspNetGroups { get; set; }
		public virtual DbSet<AspNetGroupsRoles> AspNetGroupsRoles { get; set; }
		public virtual DbSet<AspNetGroupsUsers> AspNetGroupsUsers { get; set; }
		public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
		public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
		public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
		public virtual DbSet<AspNetUserLoginAttempts> AspNetUserLoginAttempts { get; set; }
		public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
		public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
		public virtual DbSet<Attachments> Attachments { get; set; }
		public virtual DbSet<Contents> Contents { get; set; }
		public virtual DbSet<LKAccountTypes> LKAccountTypes { get; set; }
		public virtual DbSet<LKAttachmentKeyTypes> LKAttachmentKeyTypes { get; set; }
		public virtual DbSet<LKAttachmentTypes> LKAttachmentTypes { get; set; }
		public virtual DbSet<LKCities> LKCities { get; set; }
		public virtual DbSet<LKCountries> LKCountries { get; set; }
		public virtual DbSet<LKCurrencies> LKCurrencies { get; set; }
		public virtual DbSet<LKDistricts> LKDistricts { get; set; }
		public virtual DbSet<LKMenus> LKMenus { get; set; }
		public virtual DbSet<LKNotificationsActions> LKNotificationsActions { get; set; }
		public virtual DbSet<LkNotificationsActionsParameters> LkNotificationsActionsParameters { get; set; }
		public virtual DbSet<LKNotificationsCategory> LKNotificationsCategory { get; set; }
		public virtual DbSet<LKNotificationsTemplates> LKNotificationsTemplates { get; set; }
		public virtual DbSet<LKRegions> LKRegions { get; set; }
		public virtual DbSet<Notifications> Notifications { get; set; }
		public virtual DbSet<NotificationsCatActTemp> NotificationsCatActTemp { get; set; }
		public virtual DbSet<UsersAddresses> UsersAddresses { get; set; }
		public virtual DbSet<AspNetUsersVew> AspNetUsersVew { get; set; }
		public virtual DbSet<GroupsRolesView> GroupsRolesView { get; set; }
		public virtual DbSet<UsersGroupsRolesView> UsersGroupsRolesView { get; set; }
		public virtual DbSet<UsersRolesView> UsersRolesView { get; set; }
		public virtual DbSet<Sliders> Sliders { get; set; }
		public virtual DbSet<LKMenuCatContent> LKMenuCatContent { get; set; }
		public virtual DbSet<Projects> Projects { get; set; }
		public virtual DbSet<ContactUs> ContactUs { get; set; }
		public virtual DbSet<ProjectCoverAttachmentView> ProjectCoverAttachmentView { get; set; }
		public virtual DbSet<ProjectGalleryAttachmentView> ProjectGalleryAttachmentView { get; set; }
		public virtual DbSet<SliderAttachmentView> SliderAttachmentView { get; set; }
		public virtual DbSet<LkGovernance> LkGovernance { get; set; }
		public virtual DbSet<GovernanceAttachments> GovernanceAttachments { get; set; }
		public virtual DbSet<HomeAboutUs> HomeAboutUs { get; set; }
		public virtual DbSet<ToaaFiles> ToaaFiles { get; set; }
		public virtual DbSet<ToaaBasicData>ToaaBasicData { get; set;}

		public virtual DbSet<mediaCenter>mediaCenter {  get; set; }
        public virtual DbSet<File_last_view> File_last_view { get; set; }
    }
}
