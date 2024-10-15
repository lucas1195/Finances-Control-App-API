using Dapper;
using Finances_Control_App.Domain.FinancesApp;
using Finances_Control_App_API.Models;
using Finances_Control_App_API.Models.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Finances_Control_App_API.Services
{
    public class DashBoardService(Context context)
    {
        private readonly Context _context = context;

        public async Task<IEnumerable<TransferenciaDTO>> GetTransfersByPeriod(GetTransfersByPeriodParams filter)
        {
            var query = $@"SELECT  T.TransferId,
                T.UserId,
                T.TransferAmount, 
                T.TransferDescription, 
                T.TransferDate, 
                T.CategoryId, 
                T.AccountId
                FROM Transfer T
                JOIN Account C ON T.AccountId = C.AccountId
                WHERE T.UserId = @filter.IdUsuario AND T.AccountId = @filter.IdConta";

            if (filter.FilterType == "Last7Days")
            {
                query += " AND T.TransferDate >= DATEADD(DAY, -7, GETDATE())";
            }

            else if (filter.FilterType == "Last30Days")
            {
                query += " AND T.TransferDate >= DATEADD(DAY, -30, GETDATE())";
            }

            else if (filter.FilterType == "Last6Months")
            {
                query += " AND T.TransferDate >= DATEADD(MONTH, -6, GETDATE())";
            }

            else if (filter.FilterType == "Last12Months")
            {
                query += " AND T.TransferDate >= DATEADD(MONTH, -12, GETDATE())";
            }

            using var connection = _context.Database.GetDbConnection();

            return await connection.QueryAsync<TransferenciaDTO>(query, new { filter.IdUsuario, filter.IdConta, filter.FilterType });
        }

        public async Task<IEnumerable<GetAnalyticsByMonthReturn>> GetAnalyticsByMonth(int IdUsuario, int IdConta)
        {

            var query = $@"SELECT 
                            YEAR(T.TransferDate) AS Year, 
                            MONTH(T.TransferDate) AS Month, 
                            SUM(T.TransferAmount) AS TotalAmount
                        FROM Transfer T
                        JOIN Account C ON T.AccountId = C.AccountId
                        WHERE T.UserId = @IdUsuario 
                        AND T.AccountId = @IdConta
                        GROUP BY YEAR(T.TransferDate), MONTH(T.TransferDate)
                        ORDER BY YEAR(T.TransferDate), MONTH(T.TransferDate)";

            using var connection = _context.Database.GetDbConnection();

            return await connection.QueryAsync<GetAnalyticsByMonthReturn>(query, new { IdUsuario, IdConta });

        }        

        public async Task<IEnumerable<GetCategoriesAnalyticsReturn>> GetCategoriesAnalytics(GetCategoriesAnalyticsParams filer)
        {
            var result = await (from t in _context.Transfer
                                join c in _context.Category on t.CategoryId equals c.CategoryId
                                where t.UserId == filer.IdUsuario && t.AccountId == filer.IdConta
                                group t by c.CategoryName into g
                                select new GetCategoriesAnalyticsReturn
                                {
                                    TotalTransferencias = g.Count(),
                                    NmCategoria = g.Key

                                }).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<TransferenciaDTO>> GetLatest(GetLatestParams parametros)
        {

            return await _context.Transfer
                          .Where(t => t.UserId == parametros.IdUsuario && t.AccountId == parametros.IdConta)
                          .Select(t => new TransferenciaDTO
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
