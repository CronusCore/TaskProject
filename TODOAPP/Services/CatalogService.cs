using Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Services
{
    public class CatalogService
    {
        private readonly IDatabaseService _databaseService;

        public CatalogService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        //servicio para llenar catalogos para un ListItem
        public async Task<IEnumerable<SelectListItem>> FillListAsync<T>(string storedProcedure,Dictionary<string, Object> parms,Func<IEnumerable<T>, IEnumerable<SelectListItem>> fn)
        {
            var catalog = await _databaseService.QueryAsync<T>(storedProcedure, "TODOConnectionString", parms);
            return fn(catalog);
            
        }
    }
}
