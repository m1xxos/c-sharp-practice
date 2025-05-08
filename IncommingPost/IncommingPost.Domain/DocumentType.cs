using System.Collections.Generic;

namespace IncommingPost.Domain;

public class DocumentType
{
    public int Id { get; set; }
    public string TypeName { get; set; }
    public string Description { get; set; }
    
    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
}