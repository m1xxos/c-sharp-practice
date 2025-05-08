using System;

namespace IncommingPost.Models;

public class RegistrationLogDto
{
    public int Id { get; set; }
    public DateTime LogDate { get; set; }
    public string Action { get; set; }
    public int DocumentId { get; set; }
    public string DocumentNumber { get; set; }
}

public class CreateLogEntryDto
{
    public int DocumentId { get; set; }
    public string Action { get; set; }
}