using PCBuilder.Dtos.Specification;
using PCBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Services.SpecificationService
{
    public interface ISpecificationService
    {
        Task<ServiceResponse<List<GetSpecification>>> GetAllSpecs();
        Task<ServiceResponse<List<GetSpecification>>> GetSpecificSpecs(ProductType type);
        Task<ServiceResponse<List<GetSpecification>>> AddSpec(AddSpecificationDto addSpecification);
    }
}
