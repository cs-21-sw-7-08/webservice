using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WASP.Models
{
    public class CitizenDTO
    {
        public CitizenDTO(Citizen citizen)
        {
            Id = citizen.Id;
            Email = citizen.Email;
            PhoneNo = citizen.PhoneNo;
            Name = citizen.Name;
            IsBlocked = citizen.IsBlocked;


        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Name { get; set; }
        public bool IsBlocked { get; set; }
    }
}
