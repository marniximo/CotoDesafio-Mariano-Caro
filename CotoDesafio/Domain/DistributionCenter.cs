using System.ComponentModel.DataAnnotations;

namespace CotoDesafio.Domain
{
    public class DistributionCenter
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "";
        public List<Sale> Sales { get; set; } = new List<Sale>();
    }
}
