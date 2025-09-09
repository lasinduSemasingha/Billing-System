using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class MechanicDetailsRequest
    {
        public int MechanicId { get; set; }
        public string MechanicName { get; set; } = string.Empty;
    }
}
