﻿using Dapper;
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
    public class DashBoardService(Contexto context)
    {
        private readonly Contexto _context = context;

        public async Task<IEnumerable<TransferenciaDTO>> GetTransfersByPeriod(GetTransfersByPeriodParams filter)
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

            return await connection.QueryAsync<TransferenciaDTO>(query, new { filter.IdUsuario, filter.IdConta, filter.FilterType });
        }

        public async Task<IEnumerable<GetAnalyticsByMonthReturn>> GetAnalyticsByMonth(int IdUsuario, int IdConta)
        {

            var query = $@"SELECT 
                            YEAR(T.DtTransferencia) AS Ano, 
                            MONTH(T.DtTransferencia) AS Mes, 
                            SUM(T.VlTransferencia) AS SomatorioValores
                        FROM Transferencia T
                        JOIN Conta C ON T.IdConta = C.IdConta
                        WHERE T.IdUsuario = {IdUsuario} 
                        AND T.IdConta = {IdConta}
                        GROUP BY YEAR(T.DtTransferencia), MONTH(T.DtTransferencia)
                        ORDER BY YEAR(T.DtTransferencia), MONTH(T.DtTransferencia)";

            using var connection = _context.Database.GetDbConnection();

            return await connection.QueryAsync<GetAnalyticsByMonthReturn>(query, new { IdUsuario, IdConta });

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

        public async Task<IEnumerable<TransferenciaDTO>> GetLatest(GetLatestParams parametros)
        {

            return await _context.Transferencia
                          .Where(t => t.IdUsuario == parametros.IdUsuario && t.IdConta == parametros.IdConta)
                          .Select(t => new TransferenciaDTO
                          {
                              IdTransferencia = t.IdTransferencia,
                              IdConta = t.IdConta,
                              IdUsuario = t.IdUsuario,
                              DsTransferencia = t.DsTransferencia,
                              DtTransferencia = t.DtTransferencia,
                              VlTransferencia = t.VlTransferencia,
                              IdCategoria = t.IdCategoria
                          })
                          .Take(parametros.Top)
                          .ToListAsync();
        }
    }
}
