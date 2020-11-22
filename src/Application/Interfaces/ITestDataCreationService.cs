using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITestDataCreationService
    {
        Task CreateRequests(int numberToCreate);
    }
}