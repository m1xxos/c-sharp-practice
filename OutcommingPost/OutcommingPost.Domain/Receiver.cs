using System;

namespace OutcommingPost.Domain
{
    public class Receiver
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public Report GetReport()
        {
            // In a real application, this would retrieve a report
            // This is a placeholder for the actual implementation
            return new Report
            {
                GenerationDate = DateTime.Now
            };
        }
    }
}