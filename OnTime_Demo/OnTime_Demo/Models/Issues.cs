namespace OnTime_Demo.Models
{
    public class Issues
    {
        public string expand { get; set; }
        public string id { get; set; }
        public string self { get; set; }
        public string key { get; set; }
        public Fields fields { get; set; }
    }
    public class Fields
    {
        public string summary { get; set; }
        public IssueType issuetype { get; set; }
        public SubTask parent { get; set; }
        public string timespent { get; set; }
        public Project project { get; set; }
        public List<SubTask> subtasks { get; set; }
    }
}
