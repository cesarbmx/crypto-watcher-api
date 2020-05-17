using System.Threading.Tasks;
using CryptoWatcher.Domain.ModelBuilders;
using CryptoWatcher.Domain.Models;
using CesarBmx.Shared.Persistence.Repositories;

namespace CryptoWatcher.Application.Services
{
    public class ScriptVariableSetService
    {
        private readonly IRepository<Line> _lineRepository;

        public ScriptVariableSetService(IRepository<Line> lineRepository)
        {
            _lineRepository = lineRepository;
        }

        public async Task<ScriptVariableSet> GetScriptVariableSet()
        {
            // Get all lines
            var lines = await _lineRepository.GetAll();

            // Response
            var response = ScriptVariableSetBuilder.BuildScriptVariableSet(lines);

            // Return
            return response;
        }       
    }
}
