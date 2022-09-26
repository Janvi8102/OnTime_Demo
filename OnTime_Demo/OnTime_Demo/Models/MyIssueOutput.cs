namespace OnTime_Demo.Models
{
    public class MyIssueOutput
    {
        public string expand { get; set; }
        public int maxResults { get; set; }
        public int total { get; set; }
        public List<MyIssues> issues { get; set; }
    }
    public class MyIssues
    {
        public string expand { get; set; }
        public string id { get; set; }
        public string self { get; set; }
        public string key { get; set; }
        public Field fields { get; set; }
    }
}
