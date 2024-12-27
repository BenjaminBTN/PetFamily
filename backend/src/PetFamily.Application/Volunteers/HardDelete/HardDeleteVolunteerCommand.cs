using PetFamily.Domain.Volunteers.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.HardDelete
{
    public record HardDeleteVolunteerCommand(VolunteerId VolunteerId);
}
