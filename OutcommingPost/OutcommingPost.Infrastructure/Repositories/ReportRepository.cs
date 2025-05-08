using Microsoft.EntityFrameworkCore;
using OutcommingPost.Domain;
using OutcommingPost.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace OutcommingPost.Infrastructure.Repositories
{
    public class ReportRepository : Repository<Report>, IReportRepository
    {
        private readonly IDocumentRepository _documentRepository;

        public ReportRepository(ApplicationDbContext context, IDocumentRepository documentRepository) 
            : base(context)
        {
            _documentRepository = documentRepository;
        }

        public async Task<Report> GenerateReportAsync(DateTime startDate, DateTime endDate)
        {
            var documents = await _documentRepository.GetDocumentsByDateRangeAsync(startDate, endDate);
            
            var report = new Report
            {
                GenerationDate = DateTime.Now,
                Documents = new System.Collections.Generic.List<Document>(documents)
            };

            await AddAsync(report);
            await SaveChangesAsync();
            
            return report;
        }

        public async Task<Report> GetReportWithDocumentsAsync(int reportId)
        {
            return await _context.Reports
                .Include(r => r.Documents)
                .FirstOrDefaultAsync(r => r.Id == reportId);
        }
    }
}