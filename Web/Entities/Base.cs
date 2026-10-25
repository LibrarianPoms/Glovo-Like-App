using System.ComponentModel.DataAnnotations;

namespace Aymane.netProjet.Entities
{
    public class Base
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? DeleteAt { get; set; }

    }
}
