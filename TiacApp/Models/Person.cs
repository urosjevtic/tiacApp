namespace TiacApp.Models
{
    public class Person
    {
        public required int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public List<string> SocialSkills { get; set; } = new();
        public List<SocialMedia> SocialMedias { get; set; } = new();

        public Person() { }
    }
}
