using Microsoft.Build.Framework;
using SaitynoGerasis.Auth.Model;

namespace SaitynoGerasis.Data.Entities
{
    public class pardavejas : IUserOwnedResources
    {
        public int id { get; set; }
        public string Pavadinimas { get; set; }
        public string Miestas { get; set; }
        public string Adresas { get; set; }
        [Required]
        public string UserId { get; set; }
        public naudotojas user { get; set; }
    }
}
