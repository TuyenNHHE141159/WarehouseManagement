using System;
using System.Collections.Generic;

#nullable disable

namespace WarehouseManagement.Models
{
    public partial class GroupAccount
    {
        public int Gid { get; set; }
        public string Username { get; set; }
        public int Accid { get; set; }

        public virtual Account Acc { get; set; }
        public virtual Group GidNavigation { get; set; }
    }
}
