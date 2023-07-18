using Book.DAL.Entityes.Base;

namespace Book.DAL.Entityes
{
    public class Category : NamedEntity 
    { 
        public virtual ICollection<BookElem> Books { get; set; } // virtual для ленивой загрузки (не всегда хорошо)

        public override string ToString() => $"Категория {Name}";
    }
}
