using Domain.Interfaces;

namespace Domain.Model.Entities
{
    public class Result : BaseEntity, IAggregateRoot
    {
        public Result(string id)
        {
            Id = id;
        }
    }
}