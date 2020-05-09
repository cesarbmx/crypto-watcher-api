using Hangfire.Dashboard;

namespace CryptoWatcher.Api.ActionFilters
{
    public class HangfireAuthorization :  IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            //var httpContext = context.GetHttpContext();

            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            //return httpContext.User.Identity.IsAuthenticated;

            // TODO: This can't go to production like this
            return true;
        }
    }
}