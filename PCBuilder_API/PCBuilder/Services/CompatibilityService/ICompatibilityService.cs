using PCBuilder.Dtos.Compatibility;
using PCBuilder.Dtos.PartsList;
using PCBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Services.CompatibilityService
{
    public interface ICompatibilityService
    {
        Task<ServiceResponse<GetCompatibilityProblemsDto>> CheckCompatibility(PartListDto partList);

        Task<List<string>> CheckCpuWithCoolerCompatibility(int? cpuId, int? coolerId);
        Task<List<string>> CheckCpuMoboCompatibility(int? cpuId, int? moboId);
        Task<List<string>> CheckCpuRamCompatibility(int? cpuId, int? ramId);
        Task<List<string>> CheckRamMoboCompatibility(int? ramId, int? moboId);
        Task<List<string>> CheckStorageMoboCompatibility(int? moboId, int? storageId);
        Task<List<string>> CheckGPUMoboCompatibility(int? gpuId, int? moboId);

        Task<List<string>> CheckGPUCaseCompatibility(int? gpuId, int? caseId);
        Task<List<string>> CheckMoboCaseCompatibility(int? moboId, int? caseId);
        Task<List<string>> CheckStorageCaseCompatibility(int? storageId, int? caseId);
        Task<List<string>> CheckPSUCaseCompatibility(int? psuId, int? caseId);
        Task<List<string>> CheckPSUGPUCompatibility(int? psuId, int? gpuId);

    }
}
