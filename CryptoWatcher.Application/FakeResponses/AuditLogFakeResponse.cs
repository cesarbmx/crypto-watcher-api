using System;
using System.Collections.Generic;
using CryptoWatcher.Application.Responses;

namespace CryptoWatcher.Application.FakeResponses
{
    public static class AuditLogFakeResponse
    {
        public static AuditLogResponse GetFake_Add_Indicator()
        {
            return new AuditLogResponse
            {              
                AuditLogId = Guid.NewGuid(),
                Action = "Add",
                Entity = "Indicator",
                EntityId = "master_price",
                CreatedAt = DateTime.Now,
                Json = "{}"
            };
        }
        public static List<AuditLogResponse> GetFake_List()
        {
            return new List<AuditLogResponse>
            {
                GetFake_Add_Indicator()
            };
        }
    }
}
