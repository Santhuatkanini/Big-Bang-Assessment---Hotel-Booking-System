using Microsoft.AspNetCore.Identity;

namespace HotelBookingSample.Models
{
    public class User : IdentityUser<int>
    {
      
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string MobileNo { get; set; }
    }
}
