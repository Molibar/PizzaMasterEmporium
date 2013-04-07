namespace PizzaMasterEmporium.Domain.OrderContext.Models
{
    public class Order
    {
        public PizzaSpec[] PizzaSpecs { get; set; }
    }

    public class PizzaSpec
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
