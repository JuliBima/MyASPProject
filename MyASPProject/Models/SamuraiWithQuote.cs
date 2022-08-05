namespace MyASPProject.Models
{
    public class SamuraiWithQuote
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Quote> Quotes { get; set; }
    }
}
