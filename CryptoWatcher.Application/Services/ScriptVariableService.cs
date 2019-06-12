using System.Threading.Tasks;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Application.Services
{
    public class ScriptVariableService
    {
        private readonly MainDbContext _mainDbContext;

        public ScriptVariableService(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task<ScriptVariables> GetScriptVariables()
        {
            // Get all lines
            var lines = await _mainDbContext.Lines.ToListAsync();

            // Response
            var response = ScriptVariablesBuilder.BuildScriptVariables(lines);

            // Return
            return response;
        }       
    }
}
