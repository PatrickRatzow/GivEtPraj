using System.Collections.Generic;

namespace Commentor.GivEtPraj.Domain.Entities
{
    public class CasePicture
    {
        public int Id { get; set; } 
        public string ImageData { get; set; }
        public int CaseId { get; set; }
        public Case Case { get; set; }
     }
}