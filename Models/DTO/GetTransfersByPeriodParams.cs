﻿namespace Finances_Control_App_API.Models.DTO
{
    public class GetTransfersByPeriodParams
    {
        public int UserId { get; set; } = 1;
        public int AccountId { get; set; } = 1;
        public string? FilterType { get; set; } = "Last6Months";
    }
}