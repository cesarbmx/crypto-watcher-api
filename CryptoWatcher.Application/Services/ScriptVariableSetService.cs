using System.Threading.Tasks;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Application.Services
{
    public class ScriptVariableSetService
    {
        private readonly MainDbContext _mainDbContext;

        public ScriptVariableSetService(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task<ScriptVariableSet> GetScriptVariableSet()
        {
            // Get all lines
            var lines = await _mainDbContext.Lines.ToListAsync();

            // Response
            var response = ScriptVariableSetBuilder.BuildScriptVariableSet(lines);

            // Return
            return response;
        }       
    }
}
