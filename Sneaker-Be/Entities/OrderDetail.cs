namespace Sneaker_Be.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Decimal Price { get; set; }
        public int NumberOfProduct { get; set; }
        public Decimal TotalMoney { get; set; }
        public int Size { get; set; }
    }
}
