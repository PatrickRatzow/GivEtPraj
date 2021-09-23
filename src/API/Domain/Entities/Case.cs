using System.Collections.Generic;

namespace Commentor.GivEtPraj.Domain.Entities
{
    public class Case
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<CasePicture> Pictures { get; set; } = new();
    }
}