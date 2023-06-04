namespace FirstApiProject.Entities.Dtos.Cars
{
    public class UpdateCarDto
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public Color Color { get; set; }
        public Brand Brand { get; set; }
        public int ModelYear { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }
    }
}
