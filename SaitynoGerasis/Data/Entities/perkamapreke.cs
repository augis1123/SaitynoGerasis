using Microsoft.Build.Framework;
namespace SaitynoGerasis.Data.Entities
{
    public class perkamapreke
    {
        public int id { get; set; }
        public int fk_PrekeId { get; set; }
        [Required]
        public int fk_SaskaitaId { get; set; }

    }
}
