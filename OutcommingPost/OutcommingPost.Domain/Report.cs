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
            GenerationDate = DateTime.Now;
        }
    }
}