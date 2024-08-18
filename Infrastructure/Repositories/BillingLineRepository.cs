using Application.Billing;
using Domain.BillingLines;

namespace Infrastructure.Repositories
{
    public class BillingLineRepository : IBillingLineRepository
    {
        private readonly ApplicationDbContext _context;

        public BillingLineRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(BillingLine billingLine)
        {
            _context.BillingLines.Add(billingLine);
        }
    }
}
