namespace Sneaker_Be.Dtos
{
    public class HistoryOrderDto
    {
        public int Id { get; set; }
        public string status { get; set; }
        public string thumbnail { get; set; }
        public float total_money { get; set; }
        public int total_products { get; set; }
        public string product_name { get; set; }
        public DateTime order_date { get; set; }
    }
}
