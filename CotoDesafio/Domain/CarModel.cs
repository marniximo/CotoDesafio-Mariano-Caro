using System.ComponentModel.DataAnnotations;

namespace CotoDesafio.Domain
{
    public class CarModel
    {
        [Key]
        public string CarModelName { get; set; } = "";  // Uso un string en vez de un enum. En caso de querer agregar un nuevo tipo de auto
                                                        // es mas facil introducir un nuevo tipo de auto en la base de datos que modificar y deployar el codigo fuente
        public decimal Price { get; set; } = 0m;
        public decimal Tax { get; set; } // Porcentaje de impuesto para este modelo de auto. Por ejemplo 7.5% se carga como 7.5

        public List<Sale> Sales { get; set; } = new List<Sale>(); // Propiedad de Navegacion de EF

        public decimal GetFinalPrice() =>
            Price * (1 + Tax / 100);
    }
}
