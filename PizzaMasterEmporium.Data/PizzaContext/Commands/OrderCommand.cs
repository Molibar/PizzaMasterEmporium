namespace PizzaMasterEmporium.Data.PizzaContext.Commands
{
    public class OrderCommand
    {
        public string Id { get; set; }
        public Size Size { get; set; }
        public Ingredient[] Ingredients { get; set; }
    }

    public class Size
    {
        public int Inches { get; set; }
    }

    public class Ingredient
    {
        public int Type { get; set; }
        public bool Extra { get; set; }
    }
}
