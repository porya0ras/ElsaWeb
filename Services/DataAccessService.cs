using ElsaWeb.Models;
using ElsaWeb.Services.Abstracts;

namespace ElsaWeb.Services;

public class DataAccessService(MainDbContext mainDbContext) : IDataAccessService
{
    public async Task<bool> AddRequest(Request? request)
    {
        if (request == null)
            return false;

        var row = await GetRequestById(request.Id);
        if(row != null) 
            return false;

        await mainDbContext.AddAsync(request);
        await mainDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<Request> GetRequestById(Guid Id)
    {
        return await mainDbContext.FindAsync<Request>(Id);
    }

    
}
