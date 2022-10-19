namespace SaitynoGerasis.Data.Entities
{
    public class preke
    {
        public int id { get; set; }
        public string Pavadinimas { get; set; }
        public string Aprasymas { get; set; }
        public int Kiekis { get; set; }
        public double Kaina { get; set; }
        public int fk_PardavejasId { get; set; }
    }
}
