using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Domain.Entities.Common;

namespace Dictionary.Domain.Entities
{
    public sealed class EmailConfirmation:BaseEntity
    {
        public string OldEmailAddress { get; set; }
        public string NewEmailAddress { get; set; }
    }
}
