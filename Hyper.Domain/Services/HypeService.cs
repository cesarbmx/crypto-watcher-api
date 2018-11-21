using System.Collections.Generic;
using System.Threading.Tasks;
using Hyper.Domain.Messages;
using Hyper.Domain.Models;
using Hyper.Domain.Repositories;
using Hyper.Shared.Exceptions;

namespace Hyper.Domain.Services
{
    public class HypeService
    {
        private readonly IHypeRepository _hypeRepository;

        public HypeService(IHypeRepository hypeRepository)
        {
            _hypeRepository = hypeRepository;
        }

        public async Task<List<Hype>> GetHype()
        {
            // Get Hype
            return await _hypeRepository.GetAll();
        }
        public async Task<Hype> GetHype(string id)
        {
            // Get Hype by key
            var hype = await _hypeRepository.GetByKey(id);

            // Throw NotFound exception if it does not exist
            if (hype == null) throw new NotFoundException(HypeMessages.NotFound);

            // Return
            return hype;
        }
        public void Hype(Hype hype)
        {
            // Add hype
            _hypeRepository.Add(hype);
        }
    }
}
