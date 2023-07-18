using Book.DAL.Entityes.Base;

namespace Book.DAL.Entityes
{
    public class Seller : Person
    {
        public override string ToString() => $"Продавец {Surname} {Name} {Patronymic}";
    }
}
