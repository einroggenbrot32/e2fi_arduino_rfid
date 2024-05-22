using System.ComponentModel.DataAnnotations.Schema;

namespace ZeiterfassungsAPI.Models
{
    [Table("session")]
    public class Session
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("startzeit", TypeName = "timestamp(6)")]
        public DateTime Startzeit { get; set; } 
        [Column("endzeit", TypeName = "timestamp(6)")]
        public Nullable<DateTime> Endzeit { get; set; }
    }
}
