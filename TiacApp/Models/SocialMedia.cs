namespace TiacApp.Models
{
    public class SocialMedia
    {
        public required int Id { get; set; }
        public required string Platform { get; set; }
        public required string Username { get; set; }

        public SocialMedia() { }
        public SocialMedia(int id, string platform, string username)
        {
            Id = id;
            Platform = platform;
            Username = username;
        }
    }
}
