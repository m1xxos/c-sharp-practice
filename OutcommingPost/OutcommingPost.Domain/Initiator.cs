using System;

namespace OutcommingPost.Domain
{
    public class Initiator
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        
        public Document CreateDocument()
        {
            return new Document
            {
                CreationDate = DateTime.Now,
                IsValid = false
            };
        }
    }
}