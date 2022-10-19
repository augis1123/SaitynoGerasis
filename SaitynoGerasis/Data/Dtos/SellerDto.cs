namespace SaitynoGerasis.Data.Dtos
{
    public record SellerDto(int id, string Name, string City, string address);
    public record CreateSellerDot(int id, string Name, string City, string address);
    public record UpdateSellerDot(int id, string Name, string City, string address);
}
