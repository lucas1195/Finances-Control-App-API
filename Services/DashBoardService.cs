using Dapper;
using Finances_Control_App.Domain.FinancesApp;
using Finances_Control_App_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Finances_Control_App_API.Services
{
    public class DashBoardService
    {
        private readonly Contexto _context;

        public DashBoardService(Contexto context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transferencia>> GetTransfersByPeriod(GetTransfersByPeriodParams filter)
        {
            var query = $@"SELECT  T.IdTransferencia,
                T.IdUsuario,
                T.VlTransferencia, 
                T.DsTransferencia, 
                T.DtTransferencia, 
                T.IdCategoria, 
                T.IdConta
                FROM Transferencia T
                JOIN Conta C ON T.IdConta = C.IdConta
                WHERE T.IdUsuario = {filter.IdUsuario} AND T.IdConta = {filter.IdConta}";

            if (filter.FilterType == "Last7Days")
            {
                query += " AND T.DtTransferencia >= DATEADD(DAY, -7, GETDATE())";
            }

            else if (filter.FilterType == "Last30Days")
            {
                query += " AND T.DtTransferencia >= DATEADD(DAY, -30, GETDATE())";
            }

            else if (filter.FilterType == "Last6Months")
            {
                query += " AND T.DtTransferencia >= DATEADD(MONTH, -6, GETDATE())";
            }

            else if (filter.FilterType == "Last12Months")
            {
                query += " AND T.DtTransferencia >= DATEADD(MONTH, -12, GETDATE())";
            }

            using var connection = _context.Database.GetDbConnection();

            return await connection.QueryAsync<Transferencia>(query, new { filter.IdUsuario, filter.IdConta, filter.FilterType });
        }

        public async Task<IEnumerable<GetCategoriesAnalyticsReturn>> GetCategoriesAnalytics(GetCategoriesAnalyticsParams filer)
        {
            var result = await (from t in _context.Transferencia
                                join c in _context.Categoria on t.IdCategoria equals c.IdCategoria
                                where t.IdUsuario == filer.IdUsuario && t.IdConta == filer.IdConta
                                group t by c.NmCategoria into g
                                select new GetCategoriesAnalyticsReturn
                                {
                                    TotalTransferencias = g.Count(),
                                    NmCategoria = g.Key

                                }).ToListAsync();

            return result;
        }

    }
}
