using System.Collections.Generic;
using Domain.Model;
using Domain.Model.Entities;

namespace Domain
{
    public interface ICounselorRepository
    {
        IEnumerable<Counselor> GetAll();
    }
}