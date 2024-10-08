﻿using Microsoft.AspNetCore.Mvc;
using Shared.Result;

namespace Web.Controllers;
[ApiController]
[Route("/[controller]")]
public class BaseApiController : ControllerBase
{
    protected ActionResult HandleResult<T>(Result<T> result)
    {
        if (result is null)
        {
            return NotFound();
        }

        if (result.IsSuccess && result.Value != null)
        {
            return Ok(result.Value);
        }

        if (result.IsSuccess && result.Value == null)
        {
            return NotFound();
        }

        return BadRequest(result.Error);
    }

    protected ActionResult HandleResult(Result result)
    {
        if (result is null)
        {
            return NotFound();
        }

        if (result.IsSuccess)
        {
            return Ok();
        }

        return BadRequest(result.Error);
    }
}