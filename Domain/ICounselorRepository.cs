using System.Collections.Generic;
using Domain.Model;

namespace Domain
{
    public interface ICounselorRepository
    {
        IEnumerable<Counselor> GetAll();
    }
}