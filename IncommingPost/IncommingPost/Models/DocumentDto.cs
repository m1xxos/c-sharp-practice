using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IncommingPost.Models;

public class DocumentDto
{
    public int Id { get; set; }
    public DateTime ReceivedDate { get; set; }
    
    [JsonConverter(typeof(TimeSpanJsonConverter))]
    public TimeSpan ReceivedTime { get; set; }
    public string RegistrationNumber { get; set; }
    public string Summary { get; set; }
    public bool HasAttachments { get; set; }
    public string ProcessingStatus { get; set; }
    
    public int SenderId { get; set; }
    public string SenderName { get; set; }
    public int DocumentTypeId { get; set; }
    public string DocumentTypeName { get; set; }
    
    // Упрощенное представление исполнителей (только их ID)
    public List<int> ExecutorIds { get; set; } = new List<int>();
}

public class CreateDocumentDto
{
    [Required]
    public DateTime ReceivedDate { get; set; }
    
    [Required]
    [JsonConverter(typeof(TimeSpanJsonConverter))]
    public TimeSpan ReceivedTime { get; set; }
    
    [Required]
    public string RegistrationNumber { get; set; }
    
    [Required]
    public string Summary { get; set; }
    
    public bool HasAttachments { get; set; }
    
    [Required]
    public int SenderId { get; set; }
    
    [Required]
    public int DocumentTypeId { get; set; }
}

public class UpdateDocumentDto
{
    public string Summary { get; set; }
    public string ProcessingStatus { get; set; }
    public bool HasAttachments { get; set; }
}