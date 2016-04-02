using System.Collections.Generic;
using WorkingWithWebApi2.Models.ViewModels.Shared;
using WorkingWithWebApi2.Models.ViewModels.Suppliers;

namespace WorkingWithWebApi2.BusinessServices.Services.Abstract
{
    /// <summary>
    /// Suupliers business service class contract
    /// </summary>
    public interface ISupplierService
    {
        SupplierEditVM GetById(int id);
        IEnumerable<SupplierListVM> GetAll(string orderBy, string search);
        IEnumerable<DropListVM> GetDropList();
        PagedResultViewModel<SupplierListVM> GetPageResult(int pageNo, int pageSize, string orderBy, string search);
        bool Create(SupplierEditVM vm);
        bool Update(SupplierEditVM vm);
        bool Delete(int id);
    }
}
