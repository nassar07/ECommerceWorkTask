using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Token;

namespace Application.DTO.Login
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public MyTokenDTO? Token { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}
