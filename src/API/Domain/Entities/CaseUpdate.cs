using Commentor.GivEtPraj.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commentor.GivEtPraj.Domain.Entities
{
    public class CaseUpdate
    {
        public DateTime CreatedAt { get; set; }
        public Status CurrentStatus { get; set; }
        public Employee Employee { get; set; }
        public bool SendToReporter {  get; set; }
    }
}
