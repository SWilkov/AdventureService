using Microsoft.Azure.Mobile.Server.Tables;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace AdventureService.Models
{
    public class AdventureContext : DbContext
    {
        private const string connectionStringName = "Name=Adventure_Connection";

        public AdventureContext() : base(connectionStringName)
        {

        }

        public DbSet<AdventureEvent> AdventureEvents { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ExperienceExtra> Extras { get; set; }
        public DbSet<HealthProblem> HealthProblems { get; set; }
        public DbSet<EventInfo> Infos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));

            //// Many to Many relationship - Customers to AdventureEvents
            //modelBuilder.Entity<Customer>()
            //            .HasMany(c => c.AdventureEvents)
            //            .WithMany(ae => ae.Customers)
            //            .Map(map =>
            //            {
            //                map.ToTable("CustomerEvents");
            //                map.MapLeftKey("CustomerId");
            //                map.MapRightKey("AdventureEventId");
            //            });
            modelBuilder.Entity<Customer>()
                        .HasMany(c => c.BookedEvents)
                        .WithMany(e => e.Customers)
                        .Map(map =>
                        {
                            map.ToTable("CustomerEvents");
                            map.MapLeftKey("CustomerId");
                            map.MapRightKey("EventInfoId");
                        });

            modelBuilder.Entity<Customer>()
                        .HasMany(c => c.HealthProblems)
                        .WithMany(hp => hp.Customers)
                        .Map(map =>
                        {
                            map.ToTable("CustomerHealthProblems");
                            map.MapLeftKey("CustomerId");
                            map.MapRightKey("HealthProblemId");                            
                        });                     
        }
    }
}