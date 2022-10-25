using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
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
    }
}
