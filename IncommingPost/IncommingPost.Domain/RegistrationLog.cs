using System;

namespace IncommingPost.Domain;

public class RegistrationLog
{
    public int Id { get; set; }
    public DateTime LogDate { get; set; }
    public string Action { get; set; }
    public int DocumentId { get; set; }
    
    public virtual Document Document { get; set; }
}