using System.Collections.Generic;
using CryptoWatcher.Application.FakeResponses;
using CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class AuditLogResponseExample : IExamplesProvider<AuditLogResponse>
    {
        public AuditLogResponse GetExamples()
        {
            return AuditLogFakeResponse.GetFake_Add_Indicator();
        }
    }
    public class LogListResponseExample : IExamplesProvider<List<AuditLogResponse>>
    {
        public List<AuditLogResponse> GetExamples()
        {
            return AuditLogFakeResponse.GetFake_List();
        }
    }
}