using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Model.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class MergeService : IMergeService
    {
        private readonly ILogger<MergeService> _logger;
        private readonly IRepository _repository;

        public MergeService(ILogger<MergeService> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Merge()
        {
            var counselors = await _repository.ListAsync<Counselor>();
            _logger.LogInformation("Vejledere: " + counselors.Count);

            var events = await _repository.ListAsync<Event>();
            _logger.LogInformation("Arrangementer: " + events.Count);

            var locations = await _repository.ListAsync<Location>();
            _logger.LogInformation("Steder: " + locations.Count);

            var requests = await _repository.ListAsync<Request>();
            _logger.LogInformation("Ønsker: " + requests.Count);

            var results = new List<Result>();

            foreach (var request in requests)
            {
                var result=new Result();
                results.Add(result);
            }

            foreach (var result in results)
            {
                await _repository.AddOrUpdateAsync(result);
            }
        }
    }
}