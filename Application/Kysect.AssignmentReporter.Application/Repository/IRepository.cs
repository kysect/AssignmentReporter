using System.IO;
using System.Threading.Tasks;
using Kysect.AssignmentReporter.WebService.DAL.Entities;
using Kysect.AssignmentReporter.WebService.Shared;

namespace Kysect.AssignmentReporter.WebService.Server.Repository
{
    public interface IRepository
    {
        Task<FileEntry> Save(Stream stream);
        Task<FileDto> Get(Report report);
        Task Delete(Report report);
    }
}