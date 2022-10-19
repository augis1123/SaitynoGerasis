namespace SaitynoGerasis.Data.Dtos
{
    public record BillDto(int id,string BuyerName, string BuyerSecondName, string city, string address, DateTime DateTime);
    public record CreateBillDot(int id, string BuyerName, string BuyerSecondName, string city, string address, DateTime DateTime);
    public record UpdateBillDot(int id, string BuyerName, string BuyerSecondName, string city, string address, DateTime DateTime);
}
