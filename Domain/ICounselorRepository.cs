using System.Collections.Generic;

namespace Domain
{
    public interface ICounselorRepository
    {
        IEnumerable<Counselor> GetAll();
    }
}