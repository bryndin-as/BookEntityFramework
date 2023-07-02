using Book.DAL.Entityes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.DAL.Entityes
{
    public class BookElem : NamedEntity
    {
        public virtual Category Category { get; set; } // virtual для ленивой загрузки (не всегда хорошо)
    }
}
