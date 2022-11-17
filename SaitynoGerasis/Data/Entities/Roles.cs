namespace SaitynoGerasis.Data.Entities
{
    public class Roles
    {
        public const string Admin = nameof(Admin);
        public const string User = nameof(User);
        public const string Seller = nameof(Seller);
        public const string Alll = Admin + "," + User + "," + Seller;
        public static readonly IReadOnlyCollection<string> All = new[] {Admin, User, Seller}; 
    }
}
