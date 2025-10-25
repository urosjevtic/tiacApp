namespace TiacApp.Models
{
    public class SocialMedia
    {
        public int Id { get; set; }
        public string Platform { get; set; }
        public string Username { get; set; }

        public SocialMedia() { }
        public SocialMedia(string platform, string username)
        {
            Platform = platform;
            Username = username;
        }
    }
}
