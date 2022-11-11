using Microsoft.AspNetCore.Identity;

namespace SaitynoGerasis.Data.Entities
{
    public class naudotojas : IdentityUser
    {
        [PersonalData]
        public string? Authen { get; set; }
    }
}
