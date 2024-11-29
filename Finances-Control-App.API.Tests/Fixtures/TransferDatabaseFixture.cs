using Finances_Control_App.Domain.FinancesApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finances_Control_App.API.Tests.Fixtures
{
    internal class TransferDatabaseFixture
    {
        private readonly string _connectionString;

        public TransferDatabaseFixture()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            _connectionString = configuration.GetConnectionString("DefaultConnection");

            using (var context = CreateContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                SeedData(context);
            }
        }


        private void SeedData(Context context)
        {
            context.Transfer.AddRange(
                new Transfer { TransferId = 1, UserId = 1, TransferAmount = 300, TransferDescription = "Test Shopping", TransferDate = new DateTime(2024, 11, 5, 17, 30, 0), CategoryId = 2, AccountId = 2 },
                new Transfer { TransferId = 5, UserId = 1, TransferAmount = 50, TransferDescription = "Test Shopping Cloths", TransferDate = new DateTime(2024, 11, 27, 12, 10, 0), CategoryId = 5, AccountId = 3 }
            );
            context.SaveChanges();
        }


        public Context CreateContext()
        => new Context(
            new DbContextOptionsBuilder<Context>()
                .UseSqlServer(_connectionString)
                .Options);

    }
}
