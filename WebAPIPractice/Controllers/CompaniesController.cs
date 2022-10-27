using AutoMapper;
using AWebAPIPractice.ActionFilters;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWebAPIPractice.Controllers
{
    //api/companies/collection POST
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public CompaniesController(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        //get: api/companies
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            //    try
            //    {
            //        var companies = _repositoryManager.CompanyRepository.GetAllCompanies(trackChanges: false);

            //        var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

            //        return Ok(companiesDto);
            //    }
            //    catch(Exception ex)
            //    {
            //        _loggerManager.LogError($"Something went wrong in the {nameof(GetCompanies)} actions {ex}");
            //        return StatusCode(500, "Internal Server Error");
            //    }

            var companies = await _repositoryManager.CompanyRepository.GetAllCompaniesAsync(trackChanges: false);

            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

            //throw new Exception("sdsd");

            return Ok(companiesDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(int id)
        {
            var company = await _repositoryManager.CompanyRepository.GetCompanyAsync(id, trackChanges: false);

            if (company == null)
            {
                _loggerManager.LogInfo($"Company with id: {id} doesn't exist in the database");
                return NotFound();
            }
            else
            {
                var companyDto = _mapper.Map<CompanyDto>(company);
                return Ok(companyDto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDto company)
        {
            if (company == null)
            {
                _loggerManager.LogError("CompanyForCreationDto object sent from client is null");
                return BadRequest("CompanyForCreationDto object is null");
            }

            var companyEntity = _mapper.Map<Company>(company);

            _repositoryManager.CompanyRepository.CreateCompany(companyEntity);
            await _repositoryManager.SaveAsync();

            return Created("", "Company Added"); //201

        }


        [HttpPost("collection")]
        public async Task<IActionResult> CreateCompanyCollection([FromBody] IEnumerable<CompanyForCreationDto> companyCollection)
        {
            if (companyCollection == null)
            {
                _loggerManager.LogError("CompanyForCreationDto object sent from client is null");
                return BadRequest("CompanyForCreationDto object is null");
            }

            var companies = _mapper.Map<IEnumerable<Company>>(companyCollection);

            foreach (var company in companies)
            {
                _repositoryManager.CompanyRepository.CreateCompany(company); //insert
            }

            await _repositoryManager.SaveAsync();

            return Created("", "All Companies Added"); //201

        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateCompanyExistsAttribute))]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var company = HttpContext.Items["company"] as Company;

            _repositoryManager.CompanyRepository.DeleteCompany(company);
            await _repositoryManager.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateCompanyExistsAttribute))]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] CompanyForUpdateDto companyDto)
        {
            var companyEntity = HttpContext.Items["company"] as Company;

            _mapper.Map(companyDto, companyEntity);
            await _repositoryManager.SaveAsync();

            return NoContent();
        }
    }
}
