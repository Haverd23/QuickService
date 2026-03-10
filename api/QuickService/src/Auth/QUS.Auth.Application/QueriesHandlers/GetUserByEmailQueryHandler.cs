using QUS.Auth.Application.Queries;
using QUS.Auth.Domain.Interfaces;
using QUS.Core.Mediator.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Auth.Application.QueriesHandlers
{
    public class GetUserByEmailQueryHandler : IQueryHandler<GetUserByEmailQuery, bool>
    {
        public IAuthRepository _repository;

        public GetUserByEmailQueryHandler(IAuthRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> HandleAsync(GetUserByEmailQuery query)
        {
            var user = await _repository.GetByEmailAsync(query.Email);
            if (user == null)
            {
                return false;
            }
            return true;
        }
    }
}
