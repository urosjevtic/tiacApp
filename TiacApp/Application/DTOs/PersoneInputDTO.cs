namespace TiacApp.Application.DTOs
{
    public class PersoneInputDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public List<string> SocialSkills { get; set; } = new();
        public List<SocialMediaDTO> SocialMedias { get; set; } = new();
    }
}
