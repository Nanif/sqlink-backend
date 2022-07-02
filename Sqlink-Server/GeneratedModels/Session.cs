using System;
using System.Collections.Generic;

namespace Sqlink_Server.GeneratedModels
{
    public partial class Session
    {
        public int Id { get; set; }
        public string? Token { get; set; }
        public int? UserId { get; set; }

        public virtual User IdNavigation { get; set; } = null!;
    }
}
