namespace DeliveryKursAPI1.DTO
{
    public class DishDTO
    {
        public int Id { get; set; }

        public string Cost { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Image { get; set; } = null!;
    }
}
