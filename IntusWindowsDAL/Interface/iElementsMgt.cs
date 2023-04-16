using IntusWindowDataModel;
using IntusWindowDataModel.ViewModel;
using System.Collections;

namespace IntusWindowsDAL.Interface
{
    public interface iElementsMgt
    {
        public Task<List<vmElements>> GetAll();
        public Task<vmElements> GetById(int Id);
        public Task<object> SaveUpdate(vmElements _object);
        public Task<object> DeleteById(int Id);
    }
}