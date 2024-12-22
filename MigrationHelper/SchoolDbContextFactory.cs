using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationHelper
{
    public class SchoolDbContextFactory : IDesignTimeDbContextFactory<SchoolDbContext>
    {
        public SchoolDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SchoolDbContext>();
            
            try
            {
                string connectionString = File.ReadAllText(@"C:\Temp\ConnS.txt");

                optionsBuilder.UseSqlite(connectionString);
            }

            catch (Exception ex)
            {
                throw new Exception($"Error reading connection string in SchoolDbContextFactory: {ex.Message}");
            }

            return new SchoolDbContext(optionsBuilder.Options);
        }
    }
}