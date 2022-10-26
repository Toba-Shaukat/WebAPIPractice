using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
//using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AWebAPIPractice.Controllers
{
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
        public IActionResult GetCompanies()
        {
            try
            {
              var companies = _repositoryManager.CompanyRepository.GetAllCompanies(trackChanges: false);

               var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

                return Ok(companiesDto);
            }
            catch(Exception ex)
            {
                _loggerManager.LogError($"Something went wrong in the {nameof(GetCompanies)} actions {ex}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetCompany(int id)
        {
            var company = _repositoryManager.CompanyRepository.GetCompany(id, trackChanges: false);

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
        public IActionResult CreateCompany([FromBody] CompanyForCreationDto company)
        {
            if (company == null)
            {
                _loggerManager.LogError("CompanyForCreationDto object sent from client is null");
                return BadRequest("CompanyForCreationDto object is null");
            }

            var companyEntity = _mapper.Map<Company>(company);

            _repositoryManager.CompanyRepository.CreateCompany(companyEntity);
            _repositoryManager.Save();

            return Created("", "Company Added"); //201

        }
    }
}
