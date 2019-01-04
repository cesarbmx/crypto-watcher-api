using System.Threading.Tasks;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Repositories;

namespace CryptoWatcher.Application.Services
{
    public class ScriptVariableService
    {
        private readonly IRepository<DataPoint> _lineRepository;

        public ScriptVariableService(IRepository<DataPoint> lineRepository)
        {
            _lineRepository = lineRepository;
        }

        public async Task<ScriptVariables> GetScriptVariables()
        {
            // Get all lines
            var lines = await _lineRepository.GetAll();

            // Response
            var response = ScriptVariablesBuilder.BuildScriptVariables(lines);

            // Return
            return response;
        }       
    }
}
