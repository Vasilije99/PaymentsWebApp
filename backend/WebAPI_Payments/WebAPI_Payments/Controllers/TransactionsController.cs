using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Payments.Dtos;
using WebAPI_Payments.Interfaces;
using WebAPI_Payments.Models;

namespace WebAPI_Payments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public TransactionsController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetUserTransactions(int id)
        {
            try
            {
                List<TransactionsHistory> transactions = await uow.TransactionsRepository.GetTransactionsAsync(id);
                List<TransactionsDto> transactionsDto = mapper.Map<List<TransactionsDto>>(transactions);

                return Ok(transactionsDto);
            }
            catch 
            {
                return BadRequest("User with this id does not exist");
            }
        }
    }
}
