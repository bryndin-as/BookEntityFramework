using Book.Interfaces;

namespace Book.DAL.Entityes.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; } 
    }
}
