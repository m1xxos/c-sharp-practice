using System;

namespace IncommingPost.Models;

public class ExecutorDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Position { get; set; }
    public string Department { get; set; }
    public DateTime DueDate { get; set; }
    public string ExecutionStatus { get; set; }
}

public class CreateExecutorDto
{
    public string FullName { get; set; }
    public string Position { get; set; }
    public string Department { get; set; }
}

public class UpdateExecutorDto
{
    public string Position { get; set; }
    public string Department { get; set; }
    public DateTime? DueDate { get; set; }
    public string ExecutionStatus { get; set; }
}