using System.Collections.Generic;

namespace IncommingPost.Domain;

public class Sender
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ContactDetails { get; set; }
    public string Identifiers { get; set; }
    public string Address { get; set; }
    
    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
}