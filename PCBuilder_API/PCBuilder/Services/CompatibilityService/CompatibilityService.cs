using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PCBuilder.Data;
using PCBuilder.Dtos.Compatibility;
using PCBuilder.Dtos.PartsList;
using PCBuilder.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Services.CompatibilityService
{
    public class CompatibilityService : ICompatibilityService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CompatibilityService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetCompatibilityProblemsDto>> CheckCompatibility(PartListDto partList)
        {
            ServiceResponse<GetCompatibilityProblemsDto> response = new ServiceResponse<GetCompatibilityProblemsDto>
            {
                Data = new GetCompatibilityProblemsDto()
            };
            try
            {
                if (partList.CpuCoolerId == null)
                    response.Data.Problems.AddRange(await CheckCpuHasCooler(partList.ProcessorId));
                else
                    response.Data.Problems.AddRange(await CheckCpuWithCoolerCompatibility(partList.ProcessorId, partList.CpuCoolerId));
                response.Data.Problems.AddRange(await CheckCpuMoboCompatibility(partList.ProcessorId, partList.MotherboardId));
                response.Data.Problems.AddRange(await CheckCpuRamCompatibility(partList.ProcessorId, partList.MemoryId));
                response.Data.Problems.AddRange(await CheckRamMoboCompatibility(partList.MemoryId, partList.MotherboardId));
                response.Data.Problems.AddRange(await CheckStorageMoboCompatibility(partList.MotherboardId, partList.StorageId));
                response.Data.Problems.AddRange(await CheckGPUMoboCompatibility(partList.GpuId, partList.MotherboardId));
                response.Data.Problems.AddRange(await CheckGPUCaseCompatibility(partList.GpuId, partList.CaseId));
                response.Data.Problems.AddRange(await CheckStorageCaseCompatibility(partList.StorageId, partList.CaseId));
                response.Data.Problems.AddRange(await CheckMoboCaseCompatibility(partList.MotherboardId, partList.CaseId));
                response.Data.Problems.AddRange(await CheckPSUCaseCompatibility(partList.PSUId, partList.CaseId));
                response.Data.Problems.AddRange(await CheckPSUGPUCompatibility(partList.PSUId, partList.GpuId));
                

            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }

        private async Task<List<string>> CheckCpuHasCooler(int? cpuId)
        {
            List<string> problems = new List<string>();
            if (cpuId != null)
            {
                Product cpu = await _context.Products.
                FirstOrDefaultAsync(p => p.Id == cpuId && p.ProductType == ProductType.CPU);
                List<ProductSpecifications> cpuSpecs = await _context.ProductSpecifications
                    .Include(s => s.Specification)
                    .Where(ps => ps.ProductId == cpuId)
                    .ToListAsync();

                if (cpu != null && cpuSpecs.Count != 0)
                {
                    var coolerSpec = cpuSpecs.Find(s =>
                    s.Specification.Name.Equals("Cooler", StringComparison.OrdinalIgnoreCase));
                    if (coolerSpec.Value.Equals("no", StringComparison.OrdinalIgnoreCase))
                        problems.Add($"{cpu.Name} does not come with cooler. Please select a cooler.");
                }
                else
                {
                    problems.Add($"Information about selected CPU not found.");
                }
            }
            return problems;
        }

        public async Task<List<string>> CheckCpuWithCoolerCompatibility(int? cpuId, int? coolerId)
        {
            List<string> problems = new List<string>();
            if (cpuId != null && coolerId != null)
            {
                Product cpu = await _context.Products.
                    FirstOrDefaultAsync(p => p.Id == cpuId && p.ProductType == ProductType.CPU);
                Product cooler = await _context.Products
                        .FirstOrDefaultAsync(p => p.Id == coolerId && p.ProductType == ProductType.CPU_Cooler);
                List<ProductSpecifications> cpuSpecs = await _context.ProductSpecifications
                .Include(s => s.Specification)
                .Where(ps => ps.ProductId == cpuId)
                .ToListAsync();
                List<ProductSpecifications> coolerSpecs = await _context.ProductSpecifications
                    .Include(s => s.Specification)
                    .Where(ps => ps.ProductId == coolerId)
                    .ToListAsync();


                if (cpu != null && cpuSpecs != null && cooler != null && coolerSpecs != null)
                {

                    // check if sockets match
                    var cpuSocket = cpuSpecs.Find(s =>
                    s.Specification.Name.Equals("Socket", StringComparison.OrdinalIgnoreCase));
                    var coolerSockets = coolerSpecs.FindAll(s =>
                    s.Specification.Name.Equals("Socket", StringComparison.OrdinalIgnoreCase));
                    // if cpu socket is not found amongst cooler supported sockets
                    if (coolerSockets.Find(s => s.Value.ToLower().Trim().Equals(cpuSocket.Value.ToLower().Trim())) == null)
                        problems.Add($"{cpu.Name} is not compatible with {cooler.Name}");

                }
                else
                {
                    problems.Add($"Information about selected CPU or CPU Cooler not found.");
                }
            }
            

            return problems;
        }
        public async Task<List<string>> CheckCpuMoboCompatibility(int? cpuId, int? moboId)
        {
            List<string> problems = new List<string>();
            if (cpuId != null && moboId != null)
            {
                Product cpu = await _context.Products.
                FirstOrDefaultAsync(p => p.Id == cpuId && p.ProductType == ProductType.CPU);
                Product mobo = await _context.Products.
                FirstOrDefaultAsync(p => p.Id == moboId && p.ProductType == ProductType.Motherboard);

                if (cpu != null && mobo != null)
                {
                    List<ProductSpecifications> cpuSpecs = await _context.ProductSpecifications
                    .Include(s => s.Specification)
                    .Where(ps => ps.ProductId == cpuId)
                    .ToListAsync();

                    List<ProductSpecifications> moboSpecs = await _context.ProductSpecifications
                    .Include(s => s.Specification)
                    .Where(ps => ps.ProductId == moboId)
                    .ToListAsync();

                    //get cpu core family
                    var cpuFamily = cpuSpecs.Find(s => s.Specification.Name.Equals("Family", StringComparison.OrdinalIgnoreCase));
                    var cpuSocket = cpuSpecs.Find(s =>
                    s.Specification.Name.Equals("Socket", StringComparison.OrdinalIgnoreCase));
                    //get mobo chipset
                    var chipset = moboSpecs.Find(s => s.Specification.Name.Equals("chipset", StringComparison.OrdinalIgnoreCase));
                    var moboSocket = moboSpecs.Find(s => s.Specification.Name.Equals(
                        "socket", StringComparison.OrdinalIgnoreCase)); ;

                    //check sockets
                    if (cpuFamily != null)
                    {
                        if (!moboSocket.Value.Equals(cpuSocket.Value, StringComparison.OrdinalIgnoreCase))
                        {
                            problems.Add($"{cpu.Name} {cpuSocket.Value} is not compatible with {mobo.Name} {moboSocket.Value}.");
                            return problems;
                        }
                    }
                    

                    //check chipset compatibility
                    if (cpuFamily != null && chipset != null)
                    {
                        
                        bool found = await _context.FamilyChipsets.FirstOrDefaultAsync(
                            fc => fc.CoreFamily.ToLower().Trim() == cpuFamily.Value.ToLower().Trim() &&
                            fc.Chipset.ToLower().Trim() == chipset.Value.ToLower().Trim()) == null ? false : true;
                        if (!found)
                        {
                            problems.Add($"{cpu.Name} is not compatible with {mobo.Name}. The chipset {chipset.Value} is not supported by the CPU. ");
                            return problems;
                        }
                    }
                }
                else
                {
                    problems.Add($"Information about selected CPU or Motherboard not found.");
                }
            }
            
            return problems;
        }
        public async Task<List<string>> CheckCpuRamCompatibility(int? cpuId, int? ramId)
        {
            List<string> problems = new List<string>();
            if (cpuId != null & ramId != null)
            {
                Product cpu = await _context.Products.
                FirstOrDefaultAsync(p => p.Id == cpuId && p.ProductType == ProductType.CPU);
                Product ram = await _context.Products.
                FirstOrDefaultAsync(p => p.Id == ramId && p.ProductType == ProductType.Memory);

                if (cpu != null && ram != null)
                {
                    List<ProductSpecifications> cpuSpecs = await _context.ProductSpecifications
                    .Include(s => s.Specification)
                    .Where(ps => ps.ProductId == cpuId)
                    .ToListAsync();

                    List<ProductSpecifications> ramSpecs = await _context.ProductSpecifications
                    .Include(s => s.Specification)
                    .Where(ps => ps.ProductId == ramId)
                    .ToListAsync();

                    /* type = cpu supported type
                     * check if size > cpu supported size
                     * if ecc support matched
                     * 
                     * 
                     */

                    var cpuRamType = cpuSpecs.Find(s => s.Specification.Name.Equals(
                        "Memory Type", StringComparison.OrdinalIgnoreCase));
                    var RamType = ramSpecs.Find(s =>
                        s.Specification.Name.Equals("Type", StringComparison.OrdinalIgnoreCase));

                    var cpuMaxRamSize = cpuSpecs.Find(s => s.Specification.Name.Equals(
                        "Maximum Supported Memory", StringComparison.OrdinalIgnoreCase));
                    var moduleCount = ramSpecs.Find(s =>
                        s.Specification.Name.Equals("Module Count", StringComparison.OrdinalIgnoreCase));
                    var moduleSize = ramSpecs.Find(s =>
                        s.Specification.Name.Equals("Module Size", StringComparison.OrdinalIgnoreCase));
                    //type check
                    if (cpuRamType != null &&
                        RamType != null)
                    {
                       
                        if (!cpuRamType.Value.ToLower().Trim().Equals(RamType.Value.ToLower().Trim()))
                        {
                            problems.Add($"Selected memory type {RamType.Value} is" +
                                $" not compatible with the CPU ({cpuRamType.Value})");
                            return problems;
                        }
                    }



                    //size check
                    if (cpuMaxRamSize!= null &&
                        moduleCount != null && 
                        moduleSize != null)
                    {
                        int RamSize = int.Parse(moduleSize.Value) * int.Parse(moduleCount.Value);
                        int cpuMaxSize = int.Parse(cpuMaxRamSize.Value);
                        if (cpuMaxSize < RamSize)
                        {
                            problems.Add($"Selected memory capacity ({RamSize}GB) exceeds" +
                                $" maximum supported by the CPU ({cpuMaxSize}GB).");
                            return problems;
                        }
                    }
                    //ecc check
                    var cpuEcc = cpuSpecs.Find(s => s.Specification.Name.Equals(
                        "ecc", StringComparison.OrdinalIgnoreCase));
                    var ramEcc = ramSpecs.Find(s => s.Specification.Name.Equals(
                        "ecc", StringComparison.OrdinalIgnoreCase));
                    if (cpuEcc != null &&
                        ramEcc != null)
                    {
                        if (ramEcc.Value.ToLower().Trim().Equals("yes") &&
                            cpuEcc.Value.ToLower().Trim().Equals("no"))
                        {
                            problems.Add($"{cpu.Name} does not support ECC memory.");
                            return problems;
                        }
                    }

                }
                else
                {
                    problems.Add($"Information about selected CPU or RAM not found.");
                }
            }

            return problems;
        }
        public async Task<List<string>> CheckRamMoboCompatibility(int? ramId, int? moboId)
        {
            /* check type
             * check max memory
             * ecc
             * check memory slots
             * check memory speed
             * 
             */
            List<string> problems = new List<string>();
            if (ramId != null & moboId != null)
            {
                Product mobo = await _context.Products.
                FirstOrDefaultAsync(p => p.Id == moboId && p.ProductType == ProductType.Motherboard);
                Product ram = await _context.Products.
                FirstOrDefaultAsync(p => p.Id == ramId && p.ProductType == ProductType.Memory);

                if (mobo != null && ram != null)
                {
                    List<ProductSpecifications> moboSpecs = await _context.ProductSpecifications
                    .Include(s => s.Specification)
                    .Where(ps => ps.ProductId == moboId)
                    .ToListAsync();

                    List<ProductSpecifications> ramSpecs = await _context.ProductSpecifications
                    .Include(s => s.Specification)
                    .Where(ps => ps.ProductId == ramId)
                    .ToListAsync();


                    
                    //type check
                    var ramType = ramSpecs.Find(s => s.Specification.Name.Equals(
                        "Type", StringComparison.OrdinalIgnoreCase));
                    var moboRamType = moboSpecs.Find(s =>
                        s.Specification.Name.Equals("Memory Type", StringComparison.OrdinalIgnoreCase));
                    if (ramType != null &&
                        moboRamType != null)
                    {

                        if (!ramType.Value.ToLower().Trim().Equals(moboRamType.Value.ToLower().Trim()))
                        {
                            problems.Add($"Selected memory type {ramType.Value} is" +
                                $" not compatible with the motherboard ({moboRamType.Value})");
                            return problems;
                        }
                    }


                    //size check
                    var moboMaxRamSize = moboSpecs.Find(s => s.Specification.Name.Equals(
                        "Max Memory", StringComparison.OrdinalIgnoreCase));
                    var moduleCount = ramSpecs.Find(s =>
                        s.Specification.Name.Equals("Module Count", StringComparison.OrdinalIgnoreCase));
                    var moduleSize = ramSpecs.Find(s =>
                        s.Specification.Name.Equals("Module Size", StringComparison.OrdinalIgnoreCase));
                    if (moboMaxRamSize != null &&
                        moduleCount != null &&
                        moduleSize != null)
                    {
                        int RamSize = int.Parse(moduleSize.Value) * int.Parse(moduleCount.Value);
                        int moboMax = int.Parse(moboMaxRamSize.Value);
                        if (moboMax < RamSize)
                        {
                            problems.Add($"Selected memory capacity ({RamSize}GB) exceeds" +
                                $" maximum supported by the motherboard ({moboMax}GB).");
                            return problems;
                        }
                    }
                    //ecc check
                    var moboEcc = moboSpecs.Find(s => s.Specification.Name.Equals(
                        "ecc", StringComparison.OrdinalIgnoreCase));
                    var ramEcc = ramSpecs.Find(s => s.Specification.Name.Equals(
                        "ecc", StringComparison.OrdinalIgnoreCase));
                    if (moboEcc != null &&
                        ramEcc != null)
                    {
                        if (ramEcc.Value.ToLower().Trim().Equals("yes") &&
                            moboEcc.Value.ToLower().Trim().Equals("no"))
                        {
                            problems.Add($"{mobo.Name} does not support ECC memory.");
                            return problems;
                        }
                    }
                    //check for mobo slots
                    var moboRamSlots = moboSpecs.Find(s => s.Specification.Name.Equals(
                        "Memory Slots", StringComparison.OrdinalIgnoreCase));
                    if (moboRamSlots != null &&
                        moduleCount != null)
                    {
                        int moboSlots = int.Parse(moboRamSlots.Value);
                        int ramModules = int.Parse(moduleCount.Value);
                        if (ramModules > moboSlots)
                        {
                            problems.Add($"{mobo.Name} does not have enough" +
                                $" ({ramModules}) memory slots for {ram.Name}");
                            return problems;
                        }
                    }

                    //speed check
                    var ramSpeed = ramSpecs.Find(s => s.Specification.Name.Equals(
                        "Speed", StringComparison.OrdinalIgnoreCase));
                    List<ProductSpecifications> moboRamSpeed = moboSpecs.Where(s => s.Specification.Name.Equals(
                        "Memory Speed", StringComparison.OrdinalIgnoreCase)).ToList();
                    if (ramSpeed != null &&
                        moboRamSpeed != null)
                    {
                        bool supported = moboRamSpeed.Find(s => int.Parse(s.Value) == int.Parse(ramSpeed.Value)) == null ?
                            false : true;
                        if (!supported)
                        {
                            problems.Add($"{mobo.Name} might not support {ramSpeed.Value}Mhz RAM speed.");
                            return problems;
                        }
                    }
                }
                else
                {
                    problems.Add($"Information about selected RAM or Motherboard not found.");
                }
            }
            return problems;
        }

        public async Task<List<string>> CheckStorageMoboCompatibility(int? moboId, int? storageId)
        {
            List<string> problems = new List<string>();
            if (moboId != null & storageId != null)
            {
                Product mobo = await _context.Products.
                FirstOrDefaultAsync(p => p.Id == moboId && p.ProductType == ProductType.Motherboard);
                Product storage = await _context.Products.
                FirstOrDefaultAsync(p => p.Id == storageId && p.ProductType == ProductType.Storage);

                if (mobo != null && storage != null)
                {
                    List<ProductSpecifications> moboSpecs = await _context.ProductSpecifications
                    .Include(s => s.Specification)
                    .Where(ps => ps.ProductId == moboId)
                    .ToListAsync();

                    List<ProductSpecifications> storageSpecs = await _context.ProductSpecifications
                    .Include(s => s.Specification)
                    .Where(ps => ps.ProductId == storageId)
                    .ToListAsync();

                    /* interface supported by mobo
                     * nvme support
                     * 
                     * 
                     */

                    var inter = storageSpecs.Find(s => s.Specification.Name.Equals("interface", StringComparison.OrdinalIgnoreCase));
                    var moboInterfaces = moboSpecs.Find(s => s.Specification.Name.StartsWith(inter.Value, StringComparison.OrdinalIgnoreCase));
                    if (inter != null &&
                        moboInterfaces != null)
                    {
                        if (int.Parse(moboInterfaces.Value) == 0)
                            problems.Add($"{mobo.Name} is not compatible with {storage.Name}. {inter.Value} is not supported by the motherboard");
                        return problems;
                    }
                    var IsNvme = storageSpecs.Find(s => s.Specification.Name.Equals("nvme", StringComparison.OrdinalIgnoreCase));
                    
                    if (IsNvme.Value.Equals("yes", StringComparison.OrdinalIgnoreCase))
                    {
                        var moboNvme = moboSpecs.Find(s => s.Specification.Name.StartsWith("nvme", StringComparison.OrdinalIgnoreCase));
                        if (moboNvme != null)
                            if (int.Parse(moboNvme.Value) == 0)
                            {
                                problems.Add($"{mobo.Name} does not support NVME. Selected storage option will work slower.");
                            }
                    }

                }
                else
                {
                    problems.Add($"Information about selected motherboard or storage not found.");
                }
            }
            return problems;
        }
        public async Task<List<string>> CheckGPUMoboCompatibility(int? gpuId, int? moboId)
        {
            List<string> problems = new List<string>();
            if (moboId != null & gpuId != null)
            {
                Product mobo = await _context.Products.
                FirstOrDefaultAsync(p => p.Id == moboId && p.ProductType == ProductType.Motherboard);
                Product gpu = await _context.Products.
                FirstOrDefaultAsync(p => p.Id == gpuId && p.ProductType == ProductType.Video_Card);

                if (mobo != null && gpu != null)
                {
                    List<ProductSpecifications> moboSpecs = await _context.ProductSpecifications
                    .Include(s => s.Specification)
                    .Where(ps => ps.ProductId == moboId)
                    .ToListAsync();

                    List<ProductSpecifications> gpuSpecs = await _context.ProductSpecifications
                    .Include(s => s.Specification)
                    .Where(ps => ps.ProductId == gpuId)
                    .ToListAsync();

                    /* interface supported by mobo
                     
                     */

                    var inter = gpuSpecs.Find(s => s.Specification.Name.Equals("interface", StringComparison.OrdinalIgnoreCase));
                    var moboInterfaces = moboSpecs.Find(s => s.Specification.Name.StartsWith(inter.Value, StringComparison.OrdinalIgnoreCase));
                    if (inter != null &&
                        moboInterfaces != null)
                    {
                        if (int.Parse(moboInterfaces.Value) == 0)
                            problems.Add($"{mobo.Name} is not compatible with {gpu.Name}. Motherboard does not have enough {inter.Value} slots");
                        return problems;
                    }
                    else if (moboInterfaces == null) {
                        problems.Add($"{mobo.Name} is not compatible with {gpu.Name}. Motherboard does not have enough {inter.Value} slots");
                        return problems;
                    }

                }
                else
                {
                    problems.Add($"Information about selected motherboard or video card not found.");
                }
            }
            return problems;
        }
        public async Task<List<string>> CheckGPUCaseCompatibility(int? gpuId, int? caseId)
        {
            List<string> problems = new List<string>();
            if (caseId != null & gpuId != null)
            {
                Product compCase = await _context.Products.
                FirstOrDefaultAsync(p => p.Id == caseId && p.ProductType == ProductType.Case);
                Product gpu = await _context.Products.
                FirstOrDefaultAsync(p => p.Id == gpuId && p.ProductType == ProductType.Video_Card);

                if (compCase != null && gpu != null)
                {
                    List<ProductSpecifications> caseSpecs = await _context.ProductSpecifications
                    .Include(s => s.Specification)
                    .Where(ps => ps.ProductId == caseId)
                    .ToListAsync();

                    List<ProductSpecifications> gpuSpecs = await _context.ProductSpecifications
                    .Include(s => s.Specification)
                    .Where(ps => ps.ProductId == gpuId)
                    .ToListAsync();

                    /* check case is long enough for gpu
                     * 
                     * 
                     * 
                     */

                    var gpuLength = gpuSpecs.Find(s => s.Specification.Name.Equals("length", StringComparison.OrdinalIgnoreCase));
                    var caseMaxLength = caseSpecs.Find(s => s.Specification.Name.Equals("gpu length", StringComparison.OrdinalIgnoreCase));
                    if (gpuLength != null &&
                        caseMaxLength != null)
                    {
                        if (int.Parse(caseMaxLength.Value) < int.Parse(gpuLength.Value))
                            problems.Add($"{gpu.Name} is not compatible with {compCase.Name}. Video card is too long for this case");
                        return problems;
                    }
                    

                }
                else
                {
                    problems.Add($"Information about selected case or video card not found.");
                }
            }
            return problems;
        }
        public async Task<List<string>> CheckMoboCaseCompatibility(int? moboId, int? caseId)
        {
            List<string> problems = new List<string>();
            if (caseId != null & moboId != null)
            {
                Product compCase = await _context.Products.
                FirstOrDefaultAsync(p => p.Id == caseId && p.ProductType == ProductType.Case);
                Product mobo = await _context.Products.
                FirstOrDefaultAsync(p => p.Id == moboId && p.ProductType == ProductType.Motherboard);

                if (compCase != null && mobo != null)
                {
                    List<ProductSpecifications> caseSpecs = await _context.ProductSpecifications
                    .Include(s => s.Specification)
                    .Where(ps => ps.ProductId == caseId)
                    .ToListAsync();

                    List<ProductSpecifications> moboSpecs = await _context.ProductSpecifications
                    .Include(s => s.Specification)
                    .Where(ps => ps.ProductId == moboId)
                    .ToListAsync();

                    /* check if case supports mobo format
                     
                     */

                    var moboFactor = moboSpecs.Find(s => s.Specification.Name.Equals("Form factor", StringComparison.OrdinalIgnoreCase));
                    var supportedMoboFormats = caseSpecs.FindAll(s => s.Specification.Name.Equals("MOTHERBOARD FORM FACTOR", StringComparison.OrdinalIgnoreCase));
                    if (!supportedMoboFormats.Exists( smf => smf.Value.Equals(moboFactor.Value, StringComparison.OrdinalIgnoreCase)))
                    {
                        
                        problems.Add($"{compCase.Name} is not compatible with {mobo.Name}.");
                        return problems;
                    }


                }
                else
                {
                    problems.Add($"Information about selected case or motherboard not found.");
                }
            }
            return problems;
        }
        public async Task<List<string>> CheckStorageCaseCompatibility(int? storageId, int? caseId)
        {
            List<string> problems = new List<string>();
            if (caseId != null & storageId != null)
            {
                Product compCase = await _context.Products.
                FirstOrDefaultAsync(p => p.Id == caseId && p.ProductType == ProductType.Case);
                Product storage = await _context.Products.
                FirstOrDefaultAsync(p => p.Id == storageId && p.ProductType == ProductType.Storage);

                if (compCase != null && storage != null)
                {
                    List<ProductSpecifications> caseSpecs = await _context.ProductSpecifications
                    .Include(s => s.Specification)
                    .Where(ps => ps.ProductId == caseId)
                    .ToListAsync();

                    List<ProductSpecifications> storageSpecs = await _context.ProductSpecifications
                    .Include(s => s.Specification)
                    .Where(ps => ps.ProductId == storageId)
                    .ToListAsync();

                    /* check if case has enough slots for form factor
                     
                     */

                    var storageFromFactor = storageSpecs.Find(s => s.Specification.Name.Equals("Form factor", StringComparison.OrdinalIgnoreCase));

                    if (storageFromFactor.Value == "3.5\"")
                    {
                        var numberofBays = caseSpecs.Find(s => s.Specification.Name.Equals("INTERNAL 3.5 BAYS", StringComparison.OrdinalIgnoreCase));
                        if (numberofBays != null)
                        {
                            if (int.Parse(numberofBays.Value) == 0)
                            {
                                problems.Add($"{compCase.Name} is not compatible with {storage.Name}. Case does not have enough required bays.");
                                return problems;
                            }
                        }
                        else
                        {
                            problems.Add($"{compCase.Name} is not compatible with {storage.Name}. Case does not have enough required bays.");
                            return problems;
                        }
                    }
                    else if (storageFromFactor.Value == "2.5\"")
                    {
                        var numberofBays = caseSpecs.Find(s => s.Specification.Name.Equals("INTERNAL 2.5 BAYS", StringComparison.OrdinalIgnoreCase));
                        if (numberofBays != null)
                        {
                            if (int.Parse(numberofBays.Value) == 0)
                            {
                                problems.Add($"{compCase.Name} is not compatible with {storage.Name}. Case does not have enough required bays.");
                                return problems;
                            }
                        }
                        else
                        {
                            problems.Add($"{compCase.Name} is not compatible with {storage.Name}. Case does not have enough required bays.");
                            return problems;
                        }
                    }


                }
                else
                {
                    problems.Add($"Information about selected case or storage not found.");
                }
            }
            return problems;
        }

        public async Task<List<string>> CheckPSUCaseCompatibility(int? psuId, int? caseId)
        {
            List<string> problems = new List<string>();
            if (caseId != null & psuId != null)
            {
                Product compCase = await _context.Products.
                FirstOrDefaultAsync(p => p.Id == caseId && p.ProductType == ProductType.Case);
                Product psu = await _context.Products.
                FirstOrDefaultAsync(p => p.Id == psuId && p.ProductType == ProductType.Power_Supply);

                if (compCase != null && psu != null)
                {
                    List<ProductSpecifications> caseSpecs = await _context.ProductSpecifications
                    .Include(s => s.Specification)
                    .Where(ps => ps.ProductId == caseId)
                    .ToListAsync();

                    List<ProductSpecifications> psuSpecs = await _context.ProductSpecifications
                    .Include(s => s.Specification)
                    .Where(ps => ps.ProductId == psuId)
                    .ToListAsync();

                    /* check if case supports psu form factor
                     
                     */

                    var psuFromFactor = psuSpecs.Find(s => s.Specification.Name.Equals("Form factor", StringComparison.OrdinalIgnoreCase));
                    var caseFormFactors = caseSpecs.FindAll(s => s.Specification.Name.Equals("psu form factor", StringComparison.OrdinalIgnoreCase));
                    if (caseFormFactors != null)
                    {
                        if (!caseFormFactors.Exists(cff => cff.Value.Equals(psuFromFactor.Value, StringComparison.OrdinalIgnoreCase)))
                        {
                            problems.Add($"{compCase.Name} is not compatible with {psu.Name}.");
                            return problems;
                        }
                    }
                    else
                    {
                        problems.Add($"{compCase.Name} is not compatible with {psu.Name}.");
                    }


                }
                else
                {
                    problems.Add($"Information about selected case or power supply not found.");
                }
            }
            return problems;
        }
        public async Task<List<string>> CheckPSUGPUCompatibility(int? psuId, int? gpuId)
        {
            List<string> problems = new List<string>();
            if (gpuId != null & psuId != null)
            {
                Product gpu = await _context.Products.
                FirstOrDefaultAsync(p => p.Id == gpuId && p.ProductType == ProductType.Video_Card);
                Product psu = await _context.Products.
                FirstOrDefaultAsync(p => p.Id == psuId && p.ProductType == ProductType.Power_Supply);

                if (gpu != null && psu != null)
                {
                    List<ProductSpecifications> gpuSpecs = await _context.ProductSpecifications
                    .Include(s => s.Specification)
                    .Where(ps => ps.ProductId == gpuId)
                    .ToListAsync();

                    List<ProductSpecifications> psuSpecs = await _context.ProductSpecifications
                    .Include(s => s.Specification)
                    .Where(ps => ps.ProductId == psuId)
                    .ToListAsync();

                    /* check if gpu needs external power and psu has enough power connectors
                     
                     */

                    var gpuPowerConnectors = gpuSpecs.FindAll(s => s.Specification.Name.Equals("external power", StringComparison.OrdinalIgnoreCase));

                    if (gpuPowerConnectors.Count > 0)
                    {
                        // check if psu has enough
                        var psuPCIEConnectors = psuSpecs.Find(s => s.Specification.Name.Equals("PCIe 6+2-Pin Connectors", StringComparison.OrdinalIgnoreCase));
                        if (gpuPowerConnectors.Count > int.Parse(psuPCIEConnectors.Value))
                        {
                            problems.Add($"{psu.Name} does not have enough power connectors for {gpu.Name}.");
                            return problems;
                        }
                    }



                }
                else
                {
                    problems.Add($"Information about selected video card or power supply not found.");
                }
            }
            return problems;
        }
    }
}
