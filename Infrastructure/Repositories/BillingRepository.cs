using Application.Billing;
using Domain.Billing;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BillingRepository : IBillingRepository
    {
        private readonly ApplicationDbContext _context;

        public BillingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Billing billing)
        {
            _context.Add(billing);
        }

        public Task<Billing?> GetByInvoiceNumber(string invoiceNumber)
        {
            return _context.Billing.FirstOrDefaultAsync(x => x.InvoiceNumber.Equals(invoiceNumber));
        }

        public Task<bool> InvoiceNumberExistsAsync(string invoiceNumber)
        {
            return _context.Billing.AnyAsync(x => x.InvoiceNumber.Equals(invoiceNumber));
        }

        
    }
}
