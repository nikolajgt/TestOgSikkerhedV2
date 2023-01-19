using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Runtime.Intrinsics.X86;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOgSikkerhed.Data;

namespace Tests
{
    public class DbConnTest
    {

        [Fact]
        public void ConnectionStringTest()
        {
            //Arange
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            string conn = "";
            try
            {

                // Act
                conn = config.GetConnectionString("DefaultConnection");

            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                // Assert
                Assert.Equal("Server=(localdb)\\mssqllocaldb;Database=TestOgSikkerhed;Trusted_Connection=True;MultipleActiveResultSets=true", conn);
            }
        }


        [Fact]
        public void DatabaseConnectionTest()
        {
            DbContextOptions<ApplicationDbContext> options = new(); ;

            try
            {
                // Arrange
                options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TestOgSikkerhed;Trusted_Connection=True;MultipleActiveResultSets=true")
                    .Options;


                // Act
                using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureCreated();
                    context.Dispose();
                }

            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);

            }
            finally
            {
                // Assert
                using (var context = new ApplicationDbContext(options))
                {
                    Assert.Equal(ConnectionState.Open, context.Database.GetDbConnection().State);
                    context.Dispose();
                }

            }
        }
    }
}
