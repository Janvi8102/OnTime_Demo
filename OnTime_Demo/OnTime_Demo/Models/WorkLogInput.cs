using System.ComponentModel.DataAnnotations;

namespace OnTime_Demo.Models
{
    public class WorkLogInput
    {
        public string comment { get; set; }
        public string started { get; set; }
        public int timeSpentSeconds { get; set; }
    }
}
