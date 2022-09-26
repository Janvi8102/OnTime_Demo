namespace OnTime_Demo.Models
{
    public class AllIssue
    {
        public string expand { get; set; }
        public int maxResults { get; set; }
        public int total { get; set; }
        public List<Issues> issues { get; set; }
    }
}
