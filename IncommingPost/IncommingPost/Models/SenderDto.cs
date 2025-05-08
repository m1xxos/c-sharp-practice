namespace IncommingPost.Models;

public class SenderDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ContactDetails { get; set; }
    public string Identifiers { get; set; }
    public string Address { get; set; }
    public int DocumentCount { get; set; }
}

public class CreateSenderDto
{
    public string Name { get; set; }
    public string ContactDetails { get; set; }
    public string Identifiers { get; set; }
    public string Address { get; set; }
}

public class UpdateSenderDto
{
    public string Name { get; set; }
    public string ContactDetails { get; set; }
    public string Address { get; set; }
}