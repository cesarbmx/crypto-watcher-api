using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Repositories;

namespace CryptoWatcher.Application.Services
{
    public class ScriptVariableService
    {
        private readonly IRepository<Line> _lineRepository;

        public ScriptVariableService(IRepository<Line> lineRepository)
        {
            _lineRepository = lineRepository;
        }

        public async Task<Dictionary<string, Dictionary<string, Dictionary<string, decimal>>>> GetAllScriptVariables()
        {
            // Get all lines
            var lines = await _lineRepository.GetAll();

            // Response
            var response = ScriptVariableBuilder.BuildScriptVariables(lines);

            // Return
            return response;
        }       
    }
}
