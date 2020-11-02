using AmazingTODOApp.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AmazingTODOApp.Repositories
{
    public class AmazingTodoEFContext : DbContext
    {
        public AmazingTodoEFContext()
        {
        }

        public AmazingTodoEFContext(DbContextOptions<AmazingTodoEFContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                Password = "mario",
                UserId = "1",
                UserName = "mario",
                TodoItems = new List<TodoItem>()
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Password = "alex",
                UserId = "2",
                UserName = "alex",
                TodoItems = new List<TodoItem>()
            });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer("Data Source=TDG-MBL-MTY0096;Initial Catalog=AwesomeTODO;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
