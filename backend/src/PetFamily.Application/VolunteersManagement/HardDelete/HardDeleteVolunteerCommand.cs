using PetFamily.Domain.VolunteersManagement.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteersManagement.HardDelete
{
    public record HardDeleteVolunteerCommand(VolunteerId VolunteerId);
}
