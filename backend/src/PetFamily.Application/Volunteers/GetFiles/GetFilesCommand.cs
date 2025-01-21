using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.GetFiles
{
    public record GetFilesCommand(string ObjectName, string BucketName);
}
