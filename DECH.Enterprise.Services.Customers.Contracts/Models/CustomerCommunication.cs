using System.ComponentModel.DataAnnotations;

namespace DECH.Enterprise.Services.Customers.Contracts.Models
{

    public class CustomerDepartment: Transaction
    {
        public CustomerCommunication customerCommunication { get; set; }
    }


    public class CustomerCommunication
    {
        [Required(ErrorMessage = "Required")]
        public string Value { get; set; }

        [Required(ErrorMessage = "Required")]
        public PriorityType? Priority { get; set; }

        //[JsonConverter(typeof(ForceDefaultConverter))]
        [Required(ErrorMessage = "Required")]
        public DepartmentType? Department { get; set; }
    }


    /// <summary>
    /// Phone Model
    /// </summary>
    public class Phone //: IValidatableObject
    {
        [Required(ErrorMessage = "Required")]
        public string Number { get; set; }

        
        [Required(ErrorMessage = "Required type")]
        public PhoneType? PhoneType { get; set; }

    }
}
