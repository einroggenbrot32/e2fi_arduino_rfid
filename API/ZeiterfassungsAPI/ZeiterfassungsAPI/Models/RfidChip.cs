using System.ComponentModel.DataAnnotations.Schema;

namespace ZeiterfassungsAPI.Models
{
    [Table("rfid_chip")]
    public class RfidChip
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("rfid")]
        public string Rfid { get; set; }
    }
}
