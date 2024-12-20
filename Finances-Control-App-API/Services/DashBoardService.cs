﻿using Dapper;
using Finances_Control_App.Domain.FinancesApp;
using Finances_Control_App.Domain.FinancesApp.Models;
using Finances_Control_App_API.DTO;
using Finances_Control_App_API.Repositories.Interfaces;
using Finances_Control_App_API.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq;

namespace Finances_Control_App_API.Services
{
    public class DashBoardService : IDashBoardService
    {
        private readonly IRepository<Transfer> _transferRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IUserContext _userContext;
        private readonly int _userId;

        public DashBoardService(IRepository<Transfer> transferRepository, IRepository<Category> categoryRepository, IUserContext userContext)
        {
            _transferRepository = transferRepository;
            _categoryRepository = categoryRepository;
            _userContext = userContext;

            _userId = _userContext.GetCurrentUserId();
        }

        public async Task<IEnumerable<TransferDTO>> GetTransfersByPeriod(GetTransfersByPeriodParams filter)
        {

            var query = _transferRepository.Table
                .Where(t => t.UserId == _userId && t.AccountId == filter.AccountId);

            if (filter.FilterType == "Last7Days")
            {
                query = query.Where(t => t.TransferDate >= DateTime.Now.AddDays(-7));
            }
            else if (filter.FilterType == "Last30Days")
            {
                query = query.Where(t => t.TransferDate >= DateTime.Now.AddDays(-30));
            }
            else if (filter.FilterType == "Last6Months")
            {
                query = query.Where(t => t.TransferDate >= DateTime.Now.AddMonths(-6));
            }
            else if (filter.FilterType == "Last12Months")
            {
                query = query.Where(t => t.TransferDate >= DateTime.Now.AddMonths(-12));
            }

            return await query
                .Select(t => new TransferDTO
                {
                    TransferId = t.TransferId,
                    UserId = t.UserId,
                    TransferAmount = t.TransferAmount,
                    TransferDescription = t.TransferDescription,
                    TransferDate = t.TransferDate,
                    CategoryId = t.CategoryId,
                    AccountId = t.AccountId
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<GetAnalyticsByMonthReturn>> GetAnalyticsByMonth(int IdConta)
        {
            return await _transferRepository.Table
                .Where(t => t.UserId == _userId && t.AccountId == IdConta)
                .GroupBy(t => new { Year = t.TransferDate.Year, Month = t.TransferDate.Month })
                .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                .Select(g => new GetAnalyticsByMonthReturn
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalAmount = g.Sum(t => t.TransferAmount)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<GetCategoriesAnalyticsReturn>> GetCategoriesAnalytics(GetCategoriesAnalyticsParams filer)
        {
            var result = await (from t in _transferRepository.Table
                                join c in _categoryRepository.Table on t.CategoryId equals c.CategoryId
                                where t.UserId == _userId && t.AccountId == filer.AccountId
                                group t by c.CategoryName into g
                                select new GetCategoriesAnalyticsReturn
                                {
                                    TransfersTotals = g.Count(),
                                    CategoryName = g.Key

                                }).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<TransferDTO>> GetLatest(GetLatestParams parametros)
        {

            return await _transferRepository.Table
                          .Where(t => t.UserId == _userId && t.AccountId == parametros.AccountId)
                          .Select(t => new TransferDTO
                          {
                              TransferId = t.TransferId,
                              AccountId = t.AccountId,
                              UserId = t.UserId,
                              TransferDescription = t.TransferDescription,
                              TransferDate = t.TransferDate,
                              TransferAmount = t.TransferAmount,
                              CategoryId = t.CategoryId

                          })
                          .Take(parametros.Top)
                          .ToListAsync();
        }
    }
}
