using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Register
{
    public class RegisterResult
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}
