using Book.DAL.Entityes.Base;

namespace Book.DAL.Entityes
{
    public class Buyer : Person
    {
        public override string ToString() => $"Покупатель {Surname} {Name} {Patronymic}";
    }
}
