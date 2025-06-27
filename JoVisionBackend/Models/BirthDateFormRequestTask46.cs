namespace JoVisionBackend.Models
{
    public class BirthDateFormRequest
    {
        public string Name {get; set;}="anonymous";
        public int? Years { get; set; }
        public int? Months { get; set; }
        public int? Days { get; set; }
    }
}
