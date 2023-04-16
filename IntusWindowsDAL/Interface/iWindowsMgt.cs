using IntusWindowDataModel;
using IntusWindowDataModel.ViewModel;
using System.Collections;

namespace IntusWindowsDAL.Interface
{
    public interface iWindowsMgt
    {
        public Task<List<vmWindows>> GetAll();
        public Task<vmWindows> GetById(int Id);
        public Task<List<vmWindowsDDL>> GetAllById(int Id);
        public Task<object> SaveUpdate(vmWindows _object);
        public Task<object> DeleteById(int Id);
    }
}