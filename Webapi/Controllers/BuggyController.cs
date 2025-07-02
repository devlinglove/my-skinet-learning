using Azure.Core;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Webapi.DTOs;

namespace Webapi.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly IValidator<CreateProductDto> _productValidator;

        public BuggyController(IValidator<CreateProductDto> productValidator)
        {
            _productValidator = productValidator;
        }

        [HttpGet("unauthorized")]
        [Authorize]
        public IActionResult GetUnauthorized()
        {
            return Unauthorized();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest("Not a good request");
        }

        [HttpGet("notfound")]
        public IActionResult GetNotFound()
        {
           return NotFound();
        }

        [HttpGet("servererror")]
        public IActionResult GetServerError()
        {
            throw new Exception("This is a test message");
        }

        [HttpPost("validationerror")]
        public async Task<IActionResult> GetValidationError(CreateProductDto productDto)
        {
            var validationResult = await _productValidator.ValidateAsync(productDto);
            //if (!validationResult.IsValid)
            //{
            //    foreach (var error in validationResult.Errors)
            //    {
            //        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            //    }
            //    return ValidationProblem(ModelState);
            //}

            if (!validationResult.IsValid) {

                return SendValidationError(validationResult);
            }

            return Ok();
        }

        [HttpGet("testauth")]
        [Authorize]
        public ActionResult<string> GetSecretText()
        {
            return "secret stuff";
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }
    }
}
