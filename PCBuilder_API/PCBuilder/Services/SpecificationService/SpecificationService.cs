using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PCBuilder.Data;
using PCBuilder.Dtos.Specification;
using PCBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Services.SpecificationService
{
    public class SpecificationService : ISpecificationService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SpecificationService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetSpecification>>> AddSpec(AddSpecificationDto addSpecification)
        {
            ServiceResponse<List<GetSpecification>> response = new ServiceResponse<List<GetSpecification>>();
            Specification spec = _mapper.Map<Specification>(addSpecification);
            Specification specification =
                await _context.Specifications.Where(s => s.ProductType == spec.ProductType)
                .FirstOrDefaultAsync(s => s.Name == spec.Name);
            if (specification != null)
            {
                response.Success = false;
                response.Message = "Specification already exists";
            }
            else
            {
                await _context.Specifications.AddAsync(spec);
                await _context.SaveChangesAsync();
                response.Data = (_context.Specifications.Where(s => s.ProductType == spec.ProductType).Select(s => _mapper.Map<GetSpecification>(s)).ToList());
            }
            
            return response;
        }

        public async Task<ServiceResponse<List<GetSpecification>>> GetAllSpecs()
        {
            ServiceResponse<List<GetSpecification>> response = new ServiceResponse<List<GetSpecification>>();
            List<Specification> dbSpecs =
                await _context.Specifications.ToListAsync();
            response.Data = (dbSpecs.Select(s => _mapper.Map<GetSpecification>(s)).ToList());

            return response;
        }

        public async Task<ServiceResponse<List<GetSpecification>>> GetSpecificSpecs(ProductType type)
        {
            ServiceResponse<List<GetSpecification>> response = new ServiceResponse<List<GetSpecification>>();
            List<Specification> dbSpecs =
                await _context.Specifications.Where(s => s.ProductType == type)
                .ToListAsync();
            response.Data = (dbSpecs.Select(s => _mapper.Map<GetSpecification>(s)).ToList());

            return response;
        }
    }
}
