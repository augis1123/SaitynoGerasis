using Microsoft.Build.Framework;
using SaitynoGerasis.Auth.Model;

namespace SaitynoGerasis.Data.Entities
{
    public class saskaita : IUserOwnedResources
    {
        public int Id { get; set; }
        public DateTime PirkimoData { get; set; }
        public string Vardas { get; set; }
        public string Pavarde { get; set; }
        public string Miestas { get; set; }
        public string Adresas { get; set; }
        [Required]
        public string UserId { get; set; }
        public naudotojas user { get; set; }
    }
}
