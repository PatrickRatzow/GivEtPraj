using System;
using System.Collections.Generic;

namespace Commentor.GivEtPraj.Domain.Entities
{
    public class CasePicture
    {
        public Guid Id { get; set; } 
        public int CaseId { get; set; }
        public Case Case { get; set; }
     }
}