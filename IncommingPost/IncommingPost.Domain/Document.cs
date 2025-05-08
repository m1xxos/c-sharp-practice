using System;
using System.Collections.Generic;

namespace IncommingPost.Domain;

public class Document
{
    public int Id { get; set; }
    public DateTime ReceivedDate { get; set; }
    public TimeSpan ReceivedTime { get; set; }
    public string RegistrationNumber { get; set; }
    public string Summary { get; set; }
    public bool HasAttachments { get; set; }
    public string ProcessingStatus { get; set; }
    
    public int SenderId { get; set; }
    public virtual Sender ReceivedFrom { get; set; }
    
    public int DocumentTypeId { get; set; }
    public virtual DocumentType ClassifiedAs { get; set; }
    
    public virtual ICollection<Executor> AssignedTo { get; set; } = new List<Executor>();
    public virtual ICollection<RegistrationLog> RecordedIn { get; set; } = new List<RegistrationLog>();
}