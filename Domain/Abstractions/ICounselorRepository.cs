using System.Collections.Generic;
using Domain.Model.Entities;

namespace Domain.Abstractions
{
    public interface ICounselorRepository
    {
        IEnumerable<Counselor> GetAll();
    }
}