using IntusWindowDataModel;
using IntusWindowDataModel.ViewModel;
using System.Collections;

namespace IntusWindowsDAL.Interface
{
    public interface iOrderMgt
    {
        public Task<List<vmOrders>> GetAll();
        public Task<List<vmOrdersDDL>> GetAllOrder();
        public Task<vmOrders> GetById(int Id);
        public Task<object> SaveUpdate(vmOrders _object);
        public Task<object> DeleteById(int Id);
    }
}