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
        public int Id {  get; set; }
        public int CaseId {  get; set; }
        public Case Case { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public Status CurrentStatus { get; set; }
        public Employee Employee { get; set; } = null!;
        public bool SendToReporter {  get; set; }
    }
}
