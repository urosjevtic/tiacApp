namespace TiacApp.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> SocialSkills { get; set; } = new();
        public List<SocialMedia> SocialMedias { get; set; } = new();

        public Person() { }
    }
}
