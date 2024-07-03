using System.ComponentModel.DataAnnotations;

namespace MessageBrokerRabbitMQWebAPI.Model.DTO
{
    public class EmployeeDto
    {
       
        public int EmployeeId { get; set; }
     
        public string EmployeeCode { get; set; }

        public string Address { get; set; }
     
        public string Designation { get; set; }
    }
}
