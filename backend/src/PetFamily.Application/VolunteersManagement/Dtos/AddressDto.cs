using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteersManagement.Dtos
{
    public record class AddressDto(
        string Country,
        string Region,
        string City,
        string Street,
        int HouseNumber,
        string PostalCode);
}
