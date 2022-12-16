using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace firstEntityFramework.Models
{
    public class TblUser
    {
        [Key]
        [Column("U_ID")]
        public int UID { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Psw { get; set; }
        public DateTime? EntryDateTime { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int ConditionRow { get; set; } = 0;
        public int BtDeleted { get; set; } = 0;
    }
}
