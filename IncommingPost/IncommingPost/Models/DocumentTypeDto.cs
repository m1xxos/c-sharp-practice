namespace IncommingPost.Models;

public class DocumentTypeDto
{
    public int Id { get; set; }
    public string TypeName { get; set; }
    public string Description { get; set; }
}

public class CreateDocumentTypeDto
{
    public string TypeName { get; set; }
    public string Description { get; set; }
}

public class UpdateDocumentTypeDto
{
    public string TypeName { get; set; }
    public string Description { get; set; }
}