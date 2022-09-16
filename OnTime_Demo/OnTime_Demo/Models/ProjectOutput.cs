namespace OnTime_Demo.Models
{
    public class ProjectOutput
    {
        public string self { get; set; }
        public int id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
       
        public string projectTypeKey { get; set; }
        public Category ProjectCategory { get; set; }
        public string simplified { get; set; }
        public string style { get; set; }
        public string isPrivate { get; set; }
        public List<ProjectOutput> Project;

    }
}
