using ElsaWeb.Models;

namespace ElsaWeb.Services.Abstracts;

public interface IDataAccessService
{
    public Task<Request> GetRequestById(Guid Id);
    public Task<bool> AddRequest(Request? request);

}
