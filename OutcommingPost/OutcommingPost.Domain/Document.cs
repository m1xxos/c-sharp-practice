using System;

namespace OutcommingPost.Domain
{
    public class Document
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string RegistrationNumber { get; set; }
        public bool IsValid { get; set; }

        public void AssignNumber()
        {
            RegistrationNumber = $"{CreationDate.ToString("yyyyMMdd")}-{new Random().Next(1000, 9999)}";
        }

        public bool ValidateData()
        {
            // Simple validation logic
            IsValid = !string.IsNullOrEmpty(Subject) && 
                      !string.IsNullOrEmpty(Content) && 
                      !string.IsNullOrEmpty(RegistrationNumber);
            
            return IsValid;
        }

        public void Save()
        {

        }
    }
}