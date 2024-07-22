﻿using DataServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechnologyAPI.Services;

namespace TechnologyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnologyController : ControllerBase
    {
        private readonly ITechnologyService _technologyService;
        private readonly ILogger<TechnologyController> _logger;

        public TechnologyController(ITechnologyService technologyService, ILogger<TechnologyController> logger)
        {
            _technologyService = technologyService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Technologies>>> GetTechnologies()
        {
            _logger.LogInformation("Fetching all technologies");
            var technologies = await _technologyService.GetAllTechnologies();
            return Ok(technologies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Technologies>> GetTechnology(string id)
        {
            _logger.LogInformation("Fetching technology with id: {Id}", id);
            var technology = await _technologyService.GetTechnologyById(id);

            if (technology == null)
            {
                _logger.LogWarning("Technology with id: {Id} not found", id);
                return NotFound();
            }

            return Ok(technology);
        }

        /*        [HttpPost]
                public async Task<ActionResult<Technologies>> CreateTechnology([FromBody] Technologies technology)
                {
                    if (!ModelState.IsValid)
                    {
                        _logger.LogWarning("Invalid model state for creating technology");
                        return BadRequest(ModelState);
                    }

                    _logger.LogInformation("Creating a new technology");
                    var createdTechnology = await _technologyService.AddTechnology(technology);
                    return CreatedAtAction(nameof(GetTechnology), new { id = createdTechnology.Id }, createdTechnology);
                }*/

        [HttpPost]
        public async Task<ActionResult<Technologies>> CreateTechnology([FromBody] TechnologiesDTO techDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating technology");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating a new technology");
            var technology = new Technologies
            {
                Name = techDto.Name,
                DepartmentId = techDto.DepartmentId,
                IsActive = techDto.IsActive,
                CreatedBy = techDto.CreatedBy,
                CreatedDate = techDto.CreatedDate,
                UpdatedBy = techDto.UpdatedBy,
                UpdatedDate = techDto.UpdatedDate
            };

            var createdTechnology = await _technologyService.AddTechnology(technology);
            return CreatedAtAction(nameof(GetTechnology), new { id = createdTechnology.Id }, createdTechnology);
        }





        /*        [HttpPut("{id}")]
                public async Task<IActionResult> UpdateTechnology(string id, [FromBody] Technologies technology)
                {
                    if (id != technology.Id)
                    {
                        _logger.LogWarning("Technology id: {Id} does not match with the id in the request body", id);
                        return BadRequest();
                    }

                    _logger.LogInformation("Updating technology with id: {Id}", id);
                    await _technologyService.UpdateTechnology(technology);

                    return NoContent();
                }*/

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTechnology(string id, [FromBody] Technologies technology)
        {
            if (id != technology.Id)
            {
                _logger.LogWarning("Technology id: {Id} does not match with the id in the request body", id);
                return BadRequest("Technology ID mismatch.");
            }

            var existingTechnology = await _technologyService.GetTechnologyById(id);
            if (existingTechnology == null)
            {
                _logger.LogWarning("Technology with id: {Id} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Updating technology with id: {Id}", id);

            // Optionally update fields from the existingTechnology if needed
            existingTechnology.Name = technology.Name;
            existingTechnology.DepartmentId = technology.DepartmentId;
            existingTechnology.IsActive = technology.IsActive;
            existingTechnology.UpdatedBy = technology.UpdatedBy;
            existingTechnology.UpdatedDate = technology.UpdatedDate;

            await _technologyService.UpdateTechnology(existingTechnology);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTechnology(string id)
        {
            _logger.LogInformation("Deleting technology with id: {Id}", id);
            var success = await _technologyService.DeleteTechnology(id);

            if (!success)
            {
                _logger.LogWarning("Technology with id: {Id} not found", id);
                return NotFound();
            }

            return NoContent();
        }
    }
}
