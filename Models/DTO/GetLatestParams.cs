﻿namespace Finances_Control_App_API.Models.DTO
{
    public class GetLatestParams
    {
        public int UserId { get; set; }

        public int AccountId { get; set; }

        public int Top { get; set; } = 10;
    }
}