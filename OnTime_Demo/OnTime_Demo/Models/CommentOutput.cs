namespace OnTime_Demo.Models
{
    public class CommentOutput
    {
        public string self { get; set; }
        public int id { get; set; }
        public AutherComment author { get; set; }
        public string body { get; set; }

    }
    public class AutherComment
    {
        public string self { get; set; }
        public string accountId { get; set; }
        public string emailAddress { get; set; }
        public string displayName { get; set; }
        public string active { get; set; }
        public string timeZone { get; set; }
        public string accountType { get; set; }
    }
}
