namespace ZeiterfassungsAPI.Models
{
    public class Session
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Startzeit { get; set; }
        public Nullable<DateTime> Endzeit { get; set; }
    }
}
