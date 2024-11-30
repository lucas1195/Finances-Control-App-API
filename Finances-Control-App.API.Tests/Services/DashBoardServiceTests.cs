using Finances_Control_App.Domain.FinancesApp.Models;
using Finances_Control_App_API.DTO;
using Finances_Control_App_API.Repositories.Interfaces;
using Finances_Control_App_API.Services;
using Finances_Control_App_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finances_Control_App.API.Tests.Services
{
    public class DashBoardServiceTests
    {
        private readonly DashBoardService _dashBoardService;
        private readonly Mock<IRepository<Transfer>> _transferRepositoryMock;
        private readonly Mock<IRepository<Category>> _categoryRepositoryMock;
        private readonly Mock<IUserContext> _userContextMock;

        public DashBoardServiceTests() 
        {
            _transferRepositoryMock = new Mock<IRepository<Transfer>>();
            _categoryRepositoryMock = new Mock<IRepository<Category>>();
            _userContextMock = new Mock<IUserContext>();

            _userContextMock.Setup(x => x.GetCurrentUserId()).Returns(1);

            _dashBoardService = new DashBoardService(
                _transferRepositoryMock.Object,
                _categoryRepositoryMock.Object,
                _userContextMock.Object
            );
        
        }



        [Fact]
        public async Task GetTransfersByPeriod_ReturnsCorrectData()
        {
            var mockTransfers = new List<Transfer>
            {
                new Transfer
                {
                    TransferId = 1,
                    UserId = 1,
                    AccountId = 1,
                    TransferAmount = 100,
                    TransferDate = System.DateTime.Now,
                    TransferDescription = "Test Transfer",
                    CategoryId = 1
                }
            };

            var mockDbSet = new Mock<DbSet<Transfer>>();

            //mockDbSet.As<IQueryable<Transfer>>().Setup(m => m.Provider).Returns(mockTransfers.Provider);
            //mockDbSet.As<IQueryable<Transfer>>().Setup(m => m.Expression).Returns(mockTransfers.Expression);
            //mockDbSet.As<IQueryable<Transfer>>().Setup(m => m.ElementType).Returns(mockTransfers.ElementType);
            mockDbSet.As<IQueryable<Transfer>>().Setup(m => m.GetEnumerator()).Returns(mockTransfers.GetEnumerator());

            _transferRepositoryMock.Setup(repo => repo.Table).Returns(mockDbSet.Object);
        }


    }
}
