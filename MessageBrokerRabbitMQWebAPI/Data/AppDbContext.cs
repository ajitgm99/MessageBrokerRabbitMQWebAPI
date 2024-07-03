

using MessageBrokerRabbitMQWebAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace MessageBrokerRabbitMQWebAPI.DATA
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Employee>().HasData(new Employee
            //{
            //    EmployeeId = 1,
            //    EmployeeCode = "1101",
            //    Address = "New Work",
            //    Designation = "Software Engineer"
            //});

            //modelBuilder.Entity<Employee>().HasData(new Employee
            //{
            //    EmployeeId = 2,
            //    EmployeeCode = "1102",
            //    Address = "New Work -USA",
            //    Designation = "Chemical Engineer"
            //});
        }
    }
}
