using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWebAPIPractice.Controllers
{
    //[Route("api/{v:apiversion}/companies")]
    [Route("api/companies")]
    [ApiController]
    //[ApiVersion("2.0", Deprecated = true)]
    public class CompaniesV2Controller : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public CompaniesV2Controller(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {

            var companies = await _repositoryManager.CompanyRepository.GetAllCompaniesAsync(trackChanges: false);

            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

            //throw new Exception("sdsd");

            return Ok(companiesDto);
        }
    }
}
