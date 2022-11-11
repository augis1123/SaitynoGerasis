namespace SaitynoGerasis.Data.Entities
{
    public class Roles
    {
        public const string Admin = nameof(Admin);
        public const string User = nameof(User);
        public const string Seller = nameof(Seller);
        public static readonly IReadOnlyCollection<string> All = new[] {Admin, User, Seller}; 
    }
}
