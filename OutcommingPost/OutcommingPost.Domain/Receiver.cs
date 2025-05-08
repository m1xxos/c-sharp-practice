using System;

namespace OutcommingPost.Domain
{
    public class Receiver
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public Report GetReport()
        {
            return new Report
            {
                GenerationDate = DateTime.Now
            };
        }
    }
}