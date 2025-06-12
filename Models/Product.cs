namespace firmacityBackend.Models
{
    public class Product
    {
        public int Id { get; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

        public Product(int id, string name, int quantity, int price)    
        {
            this.Id = id;
            this.Name = name;
            this.Quantity = quantity;
            this.Price = price;
        }
    }
}
