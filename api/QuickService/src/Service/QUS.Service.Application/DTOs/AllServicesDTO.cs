using QUS.Services.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Service.Application.DTOs
{
    public class AllServicesDTO
    {
        public string Title { get;  set; }
        public string Description { get;  set; }
        public decimal Price { get;  set; }
        public Guid ProviderId { get;  set; }
        public Category Category { get;  set; }
        public string City { get;  set; }
    }
}
