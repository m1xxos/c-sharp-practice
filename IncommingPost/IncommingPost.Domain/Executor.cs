using System;
using System.Collections.Generic;

namespace IncommingPost.Domain;

public class Executor
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Position { get; set; }
    public string Department { get; set; }
    public DateTime DueDate { get; set; }
    public string ExecutionStatus { get; set; }
    
    public virtual ICollection<Document> AssignedDocuments { get; set; } = new List<Document>();
}