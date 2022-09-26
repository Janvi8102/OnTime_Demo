namespace OnTime_Demo.Models
{
    public class IssueOutput
    {
        public string expand { get; set; }
        public int id { get; set; }
        public string self { get; set; }
        public string key { get; set; }
        public Field fields { get; set; }
       
     }
    public class Field
    {
        public string statuscategorychangedate { get; set; }
        public IssueType issuetype { get; set; }
        public string timespent { get; set; }
        public string customfield_10030 { get; set; }
        public Project project { get; set; }
        public string aggregatetimespent { get; set; }
        public Watch watches { get; set; }
        public string lastViewed { get; set; }
        public string created { get; set; }
        public Priority priority { get; set; }
        public Assignee assignee { get; set; }
        public string summary { get; set; }
        public Creator creator { get; set; }
        public List<SubTask> subtasks { get; set; }
        public Creator reporter { get; set; }
    }

    public class IssueType
    {
        public string self { get; set; }
        public int id { get; set; }
        public string description { get; set; }
        public string name { get; set; }
       
        public string avatarId { get; set; }
        public string hierarchyLevel { get; set; }
    }

    public class Project
    {
        public string self { get; set; }
        public int id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string projectTypeKey { get; set; }
        public string simplified { get; set; }
    }

    public class Watch
    {
        public string self { get; set; }
        public string watchCount { get; set; }
        public string iswatching { get; set; }
    }

    public class Priority
    {
        public string self { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }

    public class Assignee
    {
        public string self { get; set; }
        public string accountId { get; set; }
        public string displayName { get; set; }
        public string active { get; set; }
        public string accountType { get; set; }
    }

    public class Creator
    {
        public string self { get; set; }
        public string accountId { get; set; }
        public string emailAddress { get; set; }
        public string displayName { get; set; }
        public string active { get; set; }
        public string accountType { get; set; }
    }
}
