using System.ComponentModel.DataAnnotations.Schema;

namespace ZeiterfassungsAPI.Models
{
    [Table("uuser")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
    }
}
