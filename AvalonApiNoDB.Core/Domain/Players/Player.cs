using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvalonApiNoDB.Core.Domain.Players
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Role RoleId { get; set; }
        public string RoleInfo { get; set; }
        public string RoleName { get; set; }
        public bool IsEvil { get; set; }
        public int Order { get; set; }

        public Player()
        {
            Id = Guid.NewGuid();
        }
    }
}
