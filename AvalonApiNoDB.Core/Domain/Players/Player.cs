using AvalonApiNoDB.Core.Domain.Avatars;
using System;

namespace AvalonApiNoDB.Core.Domain.Players
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Role RoleId { get; set; }
        public string RoleInfo { get; set; }
        public Avatar Avatar { get; set; }
        public bool IsEvil => (int)RoleId > 3;
        public string RoleName => RoleId.ToString();

        public Player()
        {
            Id = Guid.NewGuid();
        }

        public Player(string playerName)
        {
            Id = Guid.NewGuid();
            Name = playerName;
        }
    }
}
