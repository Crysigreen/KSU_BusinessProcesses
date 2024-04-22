﻿using KSU_BusinessProcesses.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KSU_BusinessProcesses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailingController : ControllerBase
    {
        private readonly MailingService _mailingService;

        public MailingController(MailingService mailingService)
        {
            _mailingService = mailingService;
        }
        [HttpPost]
        [Route("Mailing")]
        public async Task<IActionResult> SendMessage(string email)
        {
            _mailingService.SendEmail(email, "Тема письма", "Тест письма: тест!");
            return Ok();
        }
    }
}