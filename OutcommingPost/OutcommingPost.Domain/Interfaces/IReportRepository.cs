using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OutcommingPost.Domain.Interfaces
{
    public interface IReportRepository : IRepository<Report>
    {
        Task<Report> GenerateReportAsync(DateTime startDate, DateTime endDate);
        Task<Report> GetReportWithDocumentsAsync(int reportId);
    }
}