using Microsoft.Build.Framework;
using SaitynoGerasis.Auth.Model;

namespace SaitynoGerasis.Data.Entities
{
    public class preke : IUserOwnedResources
    {
        public int id { get; set; }
        public string Pavadinimas { get; set; }
        public string Aprasymas { get; set; }
        public int Kiekis { get; set; }
        public double Kaina { get; set; }
        public int fk_PardavejasId { get; set; }
        [Required]
        public string UserId { get; set; }
        public naudotojas user { get; set; }
    }
}
