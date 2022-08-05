namespace MyASPProject.Models
{
    public class Sword
    {
        public string Name { get; set; }
        public int Weight { get; set; }

        public TypeSword TypeSword { get; set; }
        public List<Element> Elements { get; set; }
    }
}
