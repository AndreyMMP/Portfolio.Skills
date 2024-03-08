using AndreyMMP.Portfolio.Skills.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace AndreyMMP.Portfolio.Skills.API
{
    public class PortfolioDbContext : DbContext
    {
        public DbSet<Skill> Skills { get; set; }

        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> dbContextOptions) : base(dbContextOptions)
        {
            try
            {
                RelationalDatabaseCreator? dataBaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (dataBaseCreator != null)
                {
                    if (!dataBaseCreator.CanConnect())
                    {
                        dataBaseCreator.Create();
                    }

                    if (!dataBaseCreator.HasTables())
                    {
                        dataBaseCreator.CreateTables();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}