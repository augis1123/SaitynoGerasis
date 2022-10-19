namespace SaitynoGerasis.Data.Dtos
{
    public record ItemDto(int id, string Name, string Description, double Price, int Count);
    public record CreateItemDot(int id, string Name, string Description, double Price, int Count);
    public record UpdateItemDot(int id, string Name, string Description, int Price, int Count);
}
