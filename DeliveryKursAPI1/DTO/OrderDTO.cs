namespace DeliveryKursAPI1.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public int DishId { get; set; }

        public string Total { get; set; } = null!;

        public string Adress { get; set; } = null!;

        public int UserId { get; set; }

        public int CourierId { get; set; }

        public string Status { get; set; } = null!;

        public string Number { get; set; } = null!;
    }
}
