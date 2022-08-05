namespace MyASPProject.Models
{
    public class SamuraiWithSwordTypeSwordElement
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Sword> Swords { get; set; }
    }
}
