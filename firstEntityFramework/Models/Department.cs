using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace firstEntityFramework.Models
{
    public class Department
    {
        [Key]
        public int Did { get; set; }
        public string Dname { get; set; }

        [Column("TblUserId")]
        public int TId { get; set; }
        [JsonIgnore]
        public TblUser TblUser { get; set; }
    }
}