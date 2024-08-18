using Application.Customers;
using Application.Products;
using Domain.Billing;
using Domain.Billing.Exceptions;
using Domain.BillingLines;
using MediatR;
using System.Net.Http.Json;

namespace Application.Billing.ProcessByInvoiceId
{
    internal class ProcessByInvoiceIdCommandHandler : IRequestHandler<ProcessByInvoiceIdCommand>
    {
        private readonly IHttpClientFactory _billingApiService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBillingRepository _billingRepository;
        private readonly IBillingLineRepository _billingLineRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProcessByInvoiceIdCommandHandler(IHttpClientFactory billingApiService, ICustomerRepository customerRepository, IProductRepository productRepository, IBillingRepository billingRepository, 
            IBillingLineRepository billingLineRepository, IUnitOfWork unitOfWork)
        {
            _billingApiService = billingApiService;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _billingRepository = billingRepository;
            _billingLineRepository = billingLineRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ProcessByInvoiceIdCommand request, CancellationToken cancellationToken)
        {
            var client = _billingApiService.CreateClient("billingApi");
            var billingInfo = await client.GetFromJsonAsync<BillingInformationDto>($"/billing/{request.Id}");

            if (billingInfo is null)
            {
                throw new BillingInfoNotFoundException(request.Id);
            }

            if(billingInfo.Details == null)
            {
                throw new InvalidInvoiceException();
            }

            var customer = await _customerRepository.GetByIdAsync(billingInfo.Details.Customer.Id);
            if (customer is null) { 
                throw new MissingCustomerException(billingInfo.Details.Customer.Id);
            }

            var lines = billingInfo.Details.Lines;

            if (lines == null || !lines.Any())
            {
                throw new EmptyBillingLineException();
            }

            List<string> missingIds = new();

            foreach (var item in lines)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);

                if (product is null)
                {
                    missingIds.Add(item.ProductId.ToString());
                }
            }

            if (missingIds.Count > 0)
            {
                throw new MissingProductException([.. missingIds]);
            }

            var invoiceNumber = billingInfo.Details.InvoiceNumber;
            if (invoiceNumber is null)
            {
                throw new InvalidInvoiceNumberException();
            }

            if (await _billingRepository.InvoiceNumberExistsAsync(billingInfo.Details.InvoiceNumber))
            {
                throw new InvoiceIsAlreadyImportedException(invoiceNumber);
            }

            var billing = new Domain.Billing.Billing
            {
                Id = Guid.NewGuid(),
                CustomerId = billingInfo.Details.Customer.Id,
                InvoiceNumber = invoiceNumber,
                Currency = billingInfo.CurrencyCode,
                Date = DateOnly.Parse(billingInfo.Details.Date),
                DueDate = DateOnly.Parse(billingInfo.Details.DueDate),
                TotalAmount = billingInfo.Details.TotalAmount
            };

            _billingRepository.Add(billing);

            var billingLinesInsert = lines.Select(x => new BillingLine
            {
                Id = Guid.NewGuid(),
                BillingId = billing.Id,
                ProductId = x.ProductId,
                Description = x.Description,
                Quantity = x.Quantity,
                SubTotal = x.Subtotal,
                UnitPrice = x.UnitPrice
            });

            foreach (var i in billingLinesInsert)
            {
                _billingLineRepository.Add(i);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
