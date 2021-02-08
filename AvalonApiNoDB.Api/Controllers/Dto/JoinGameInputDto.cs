using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvalonApiNoDB.Api.Controllers.Dto
{
    public class JoinGameInputDto
    {
        public int JoinCode { get; set; }
        public string PlayerName { get; set; }
    }
}
