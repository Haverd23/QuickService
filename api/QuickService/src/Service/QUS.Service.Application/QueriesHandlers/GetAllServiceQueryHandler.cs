using QUS.Core.Exceptions;
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
    public class GetAllServiceQueryHandler : IQueryHandler<GetAllServicesQuery, IEnumerable<AllServicesDTO>>
    {
        private readonly IServiceRepository _serviceRepository;
        public GetAllServiceQueryHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        public async Task<IEnumerable<AllServicesDTO>> HandleAsync(GetAllServicesQuery query)
        {
            var services = await _serviceRepository.GetAllAsync();
           
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
