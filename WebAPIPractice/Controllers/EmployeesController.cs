using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AWebAPIPractice.Controllers
{
    //this a route for companies and employees both
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public EmployeesController(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetEmployeesForCompany(int companyId)
        {
            var company = _repositoryManager.CompanyRepository.GetCompany(companyId, trackChanges: false);

            if(company == null)
            {
                _loggerManager.LogInfo($"Company with id: {companyId} doesn't exist in the database");
                return NotFound();
            }

            var employeesFromDb = _repositoryManager.EmployeeRepository.GetEmployees(companyId, trackChanges: false);

            var employeeDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);
            return Ok(employeeDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeForCompany(int companyId, int id)
        {
            var company = _repositoryManager.CompanyRepository.GetCompany(companyId, trackChanges: false);

            if (company == null)
            {
                _loggerManager.LogInfo($"Company with id: {companyId} doesn't exist in the database");
                return NotFound();
            }

            var employeeFromDb = _repositoryManager.EmployeeRepository.GetEmployee(companyId, id, trackChanges: false);

            if(employeeFromDb == null)
            {
                _loggerManager.LogInfo($"Employee with id: {id} doesn't exist in the database");
                return NotFound();
            }

            var employeeDto = _mapper.Map<EmployeeDto>(employeeFromDb);
            return Ok(employeeDto);
        }

        [HttpPost]
        public IActionResult CreateEmployeeForCompany(int companyId, [FromBody] EmployeeForCreationDto employee)
        {
            if (employee == null)
            {
                _loggerManager.LogError("EmployeeForCreationDto object sent from client is null.");
                return BadRequest("EmployeeForCreationDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _loggerManager.LogError("Invalid model state for the EmployeeForCreationDto object");
                return UnprocessableEntity(ModelState);
            }
            var company = _repositoryManager.CompanyRepository.GetCompany(companyId, trackChanges: false);

            if (company == null)
            {
                _loggerManager.LogInfo($"Company with id : {companyId} doesn't exists in the database");
                return NotFound();
            }

            var employeeEntity = _mapper.Map<Employee>(employee);

            _repositoryManager.EmployeeRepository.CreateEmployeeForCompany(companyId, employeeEntity);
            _repositoryManager.Save();

            return Created("", "Employee Added");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployeeForCompany(int companyId, int id)
        {
            var company = _repositoryManager.CompanyRepository.GetCompany(companyId, trackChanges: false);
            if (company == null)
            {
                _loggerManager.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
                return NotFound();
            }

            var employeeForCompany = _repositoryManager.EmployeeRepository.GetEmployee(companyId, id, trackChanges: false);

            if (employeeForCompany == null)
            {
                _loggerManager.LogInfo($"Employee with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _repositoryManager.EmployeeRepository.DeleteEmployee(employeeForCompany);
            _repositoryManager.Save();

            return NoContent(); //204
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployeeForCompany(int companyId, int id, [FromBody] EmployeeForUpdateDto employeeDto)
        {
            if (employeeDto == null)
            {
                _loggerManager.LogError("EmployeeForUpdateDto object sent from client is null.");
                return BadRequest("EmployeeForUpdateDto object is null");
            }

            if (!ModelState.IsValid)
            {
                _loggerManager.LogError("Invalid model state for the EmployeeForCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            var company = _repositoryManager.CompanyRepository.GetCompany(companyId, trackChanges: false);

            if (company == null)
            {
                _loggerManager.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
                return NotFound();
            }

            var employeeEntity = _repositoryManager.EmployeeRepository.GetEmployee(companyId, id, trackChanges: true);

            if (employeeEntity == null)
            {
                _loggerManager.LogInfo($"Employee with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(employeeDto, employeeEntity);
            _repositoryManager.Save();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateEmployeeForCompany(int companyId, int id, [FromBody] JsonPatchDocument<EmployeeForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _loggerManager.LogError("patchDoc object sent from client is null");
                return BadRequest("patchDoc object is null");
            }

            var company = _repositoryManager.CompanyRepository.GetCompany(companyId, trackChanges: false);

            if (company == null)
            {
                _loggerManager.LogInfo($"Company with id: {companyId} doesn't exist in the database");
                return NotFound();
            }

            var employeeEntity = _repositoryManager.EmployeeRepository.GetEmployee(companyId, id, trackChanges: true);

            if (employeeEntity == null)
            {
                _loggerManager.LogInfo($"Employee with id: {id} doesn't exist in the database");
                return NotFound();
            }

            //dto <- model
            var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);

            patchDoc.ApplyTo(employeeToPatch, ModelState);

            //TryValidateModel
            TryValidateModel(employeeToPatch);

            if (!ModelState.IsValid)
            {
                _loggerManager.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }

            //dto -> model
            _mapper.Map(employeeToPatch, employeeEntity);

            _repositoryManager.Save();

            return NoContent(); //204
        }
    }
}
