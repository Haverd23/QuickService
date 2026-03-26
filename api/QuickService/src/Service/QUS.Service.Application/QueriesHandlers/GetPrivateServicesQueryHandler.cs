using QUS.Core.Mediator.Queries;
using QUS.Service.Application.DTOs;
using QUS.Service.Application.Queries;
using QUS.Services.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Service.Application.QueriesHandlers
{
    public class GetPrivateServicesQueryHandler : IQueryHandler<GetPrivateServicesQuery,
        IEnumerable<AllServicesDTO>>
    {
        private readonly IServiceRepository _serviceRepository;

        public GetPrivateServicesQueryHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        public async Task<IEnumerable<AllServicesDTO>> HandleAsync(GetPrivateServicesQuery query)
        {
            var providerId = new GetPrivateServicesQuery(query.ProviderId);
            var services = await _serviceRepository.GetServiceByProvider(providerId.ProviderId);

            return services.Select(s => new AllServicesDTO
            {
                Title = s.Title,
                Description = s.Description,
                Price = s.Price,
                ProviderId = s.ProviderId,
                Category = s.Category,
                City = s.City
            }).ToList();

        }
    }
}
