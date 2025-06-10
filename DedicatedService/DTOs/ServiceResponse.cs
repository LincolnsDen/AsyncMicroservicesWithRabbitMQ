using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DedicatedService.DTOs
{
    public record ServiceResponse (bool Flag = false, string Massage = null!);
}
