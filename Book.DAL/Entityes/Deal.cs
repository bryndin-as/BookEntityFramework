using Book.DAL.Entityes.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book.DAL.Entityes
{
    public class Deal : Entity
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public virtual BookElem BookElem { get; set; }
        public virtual Seller Seller { get; set; }
        public virtual Buyer Buyer { get; set; }
    }
}
