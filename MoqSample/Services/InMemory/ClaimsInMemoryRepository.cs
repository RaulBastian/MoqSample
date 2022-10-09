using MoqSample.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqSample.Services.InMemory
{
    public class ClaimsInMemoryRepository : IClaimsRepository
    {
        private readonly InMemoryContext inMemoryContext;

        public ClaimsInMemoryRepository(InMemoryContext inMemoryContext)
        {
            this.inMemoryContext = inMemoryContext;
        }

        public Claim Create(Claim claim)
        {
            claim.Id = Guid.NewGuid();
            this.inMemoryContext.Claims.Add(claim);
            return claim;
        }

        public Claim? GetById(Guid id)
        {
            return this.inMemoryContext.Claims.Where(c => c.Id == id).FirstOrDefault();
        }
    }
}
