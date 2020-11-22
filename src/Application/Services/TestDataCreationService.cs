using System;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoFixture;
using Domain.Interfaces;
using Domain.Model.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class TestDataCreationService : ITestDataCreationService
    {
        private readonly Fixture _fixture;
        private readonly IRepository _repository;
        private readonly ILogger<TestDataCreationService> _logger;

        public TestDataCreationService(ILogger<TestDataCreationService> logger, IRepository repository)
        {
            _repository = repository;
            _logger = logger;
            _fixture = new Fixture();
        }

        public async Task CreateRequests(int numberToCreate)
        {
            for (int i = 0; i < 50; i++)
            {
                var request = _fixture
                    .Build<Request>()
                    .Without(x => x.Events)
                    .With(x => x.Id, DateTime.Now.ToString("yyyy-MM-dd.HH.mm.ss.fff"))
                    .With(x => x.ContactName, GetContactName())
                    .With(x => x.ContactPhone, GetContactPhone())
                    .Create();
                var result = await _repository.AddAsync(request);
            }
        }
        private string GetContactPhone()
        {
            var r = new Random();
            var result = "";
            while (result.Length < 8)
            {
                result += r.Next(1, 10);
            }

            return result;
        }

        private string GetContactName()
        {
            var firstnames = new[] { "Ole", "Kurt", "Jonna", "Claus", "Signe", "Thomas", "Andy", "Gitte", "Kasper", "Christian", "Isabella", "Victoria", "Henriette", "Jesper" };
            var lastnames = new[] { "Olsen", "Clausen", "Thomasson", "Kaspersen", "Christiansen", "Jespersen", "Jensen", "Juul", "Juel", "Hansen" };
            var r = new Random();
            return firstnames[r.Next(firstnames.Length)] + " " + lastnames[r.Next(lastnames.Length)];
        }
    }
}