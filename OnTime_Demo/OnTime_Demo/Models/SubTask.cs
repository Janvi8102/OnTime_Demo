namespace OnTime_Demo.Models
{
    public class SubTask
    {
        public int id { get; set; }
        public string self { get; set; }
        public string key { get; set; }

        public SubField fields { get; set; }
    }
    public class SubField
    {
        public string summary { get; set; }
        public Status status { get; set; }
        public Priority priority { get; set; }
        public IssueType issuetype { get; set; }
    }
    public class Status
    {
        public string self { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }
}


