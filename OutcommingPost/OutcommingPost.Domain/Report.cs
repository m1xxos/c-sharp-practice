using System;
using System.Collections.Generic;

namespace OutcommingPost.Domain
{
    public class Report
    {
        public int Id { get; set; }
        public DateTime GenerationDate { get; set; }
        public List<Document> Documents { get; set; } = new List<Document>();

        public void Generate()
        {
            // In a real application, this would generate a report with documents
            // This is a placeholder for the actual implementation
            GenerationDate = DateTime.Now;
        }
    }
}