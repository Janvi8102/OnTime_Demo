﻿namespace OnTime_Demo.Entities
{
    public class JiraTokenModel
    {
        public int UserId { get; set; }
        public string JiraAuthToken { get; set; }
        public string JiraRefreshToken { get; set; }
        public string CloudId { get; set; }
    }
}
