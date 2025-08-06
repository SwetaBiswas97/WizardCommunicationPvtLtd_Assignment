using System.Data.Entity;
namespace WizardCommunicationPvtLtd_Assignment.Models
{

    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext() : base("name=CustomerDb")
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure any additional model settings here
            base.OnModelCreating(modelBuilder);
        }
    }
}