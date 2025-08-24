using System.ComponentModel.DataAnnotations;

namespace CotoDesafio.Domain
{
    public class Sale
    {
        [Key]
        public string CarChassisNumber { get; set; } = ""; // Guardo el numero de chassis de cada venta para futuras referencias
        public string CarModelName { get; set; } = "";
        public CarModel CarModel { get; set; } = new CarModel();// Propiedad de navegacion de EF 
        public Guid DistributionCenterId { get; set; } = Guid.Empty;
        public DistributionCenter DistributionCenter { get; set; } = new DistributionCenter(); // Propiedad de navegacion de EF 
        public DateTime Date { get; set; } = new DateTime();

        public Money GetFinalSalePrice() => CarModel.GetFinalPrice();
    }

}
