using MessageBrokerRabbitMQWebAPI.DATA;
using MessageBrokerRabbitMQWebAPI.Model.DTO;
using MessageBrokerRabbitMQWebAPI.Model;
using MessageBrokerRabbitMQWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MessageBrokerRabbitMQWebAPI.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMessageService _messageService;

        public EmployeeAPIController(AppDbContext db, IMessageService messageService)
        {
            _db = db;

            _response = new ResponseDto();
            _messageService = messageService;
        }


        [HttpGet("Employees")]
        public async Task<IActionResult> Get()
        {

            IEnumerable<Employee> Couponlist = new List<Employee>();
            Couponlist = _db.Employees.ToList();

            if (Couponlist.Any())
            {

                _response.Result = Couponlist;
                _response.IsSuccess = true;
                _response.Message = "success";
                return Ok(_response);
            }
            else
            {
                _response.Result = Empty;
                _response.IsSuccess = false;
                _response.Message = "Error while fetching";
                return BadRequest(_response);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            Employee CouponData = new Employee();
            CouponData = _db.Employees.FirstOrDefault(z => z.EmployeeId == id);

            if (CouponData != null)
            {
                _response.Result = CouponData;
                _response.IsSuccess = true;
                _response.Message = "success";
                return Ok(_response);
            }
            else
            {
                _response.Result = CouponData;
                _response.IsSuccess = false;
                _response.Message = "Error while fetching";
                return BadRequest(_response);
            }
        }


        [HttpGet]
        [Route("{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            Employee CouponData = new Employee();
            CouponData = _db.Employees.FirstOrDefault(z => z.EmployeeCode == code.ToString());

            if (CouponData.EmployeeCode != null)
            {
                _response.Result = CouponData;
                _response.IsSuccess = true;
                _response.Message = "success";
                return Ok(_response);
            }
            else
            {
                _response.Result = CouponData;
                _response.IsSuccess = false;
                _response.Message = "Error while fetching";
                return BadRequest(_response);
            }
        }


        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody] Employee coupon)
        {
            try
            {
                _db.Employees.Add(coupon);
                _db.SaveChanges();


                _messageService.SendMessages<Employee>(coupon);
                //Insert in Elastic Search
                //    var _response = await _elasticSerachService.CreateDocumentAsync(coupon);

                return Ok(_response);
            }
            catch
            {
                return BadRequest(_response);
            }
        }


        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee coupon)
        {
            try
            {
                _db.Employees.Update(coupon);
                _db.SaveChanges();

                return Ok(_response);
            }
            catch
            {
                return BadRequest(_response);
            }
        }



        [HttpDelete("DeleteEmployee")]
        public async Task<IActionResult> DeleteCoupon(string _EmployeeCode)
        {
            try
            {
                var coupondata = _db.Employees.First(u => u.EmployeeCode == _EmployeeCode);
                _db.Employees.Remove(coupondata);
                _db.SaveChanges();

                return Ok(_response);
            }
            catch
            {
                return BadRequest(_response);
            }
        }

    }
}
