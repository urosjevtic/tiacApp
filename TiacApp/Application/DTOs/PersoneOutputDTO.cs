namespace TiacApp.Application.DTOs
{
    public class PersoneOutputDTO
    {
        public int NumberOfVowels { get; set; }
        public int NumberOfConsonants { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string ReversedName { get; set; } = string.Empty;
        public object JsonData { get; set; } = new {  };

    }
}
