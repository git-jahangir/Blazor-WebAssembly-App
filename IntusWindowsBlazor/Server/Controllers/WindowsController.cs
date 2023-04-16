using IntusWindowDataModel;
using IntusWindowDataModel.ViewModel;
using IntusWindowsDAL.Implementation;
using IntusWindowsDAL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IntusWindowsBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WindowsController : ControllerBase
    {
        #region Variable Declaration & Initialization
        private readonly iWindowsMgt _manager;
        #endregion

        #region Constructor
        public WindowsController()
        {
            _manager = new WindowsMgt();
        }
        #endregion

        #region All Http Methods
        [HttpGet("[action]")]
        public async Task<List<vmWindows>> getall()
        {
            List<vmWindows> resdata = new List<vmWindows>();
            try
            {
                resdata = await _manager.GetAll();
            }
            catch (Exception) { }
            return resdata;
        }

        [HttpGet("[action]/{id}")]
        public async Task<vmWindows> getbyid(int id)
        {
            vmWindows resdata = new vmWindows();
            try
            {
                resdata = await _manager.GetById(id);
            }
            catch (Exception) { }
            return resdata;
        }

        [HttpGet("[action]/{id}")]
        public async Task<List<vmWindowsDDL>> getallbyorderid(int id)
        {
            List<vmWindowsDDL> resdata = new List<vmWindowsDDL>();
            try
            {
                resdata = await _manager.GetAllById(id);
            }
            catch (Exception) { }
            return resdata;
        }

        [HttpPost("[action]")]
        public async Task<object> saveupdate([FromBody] vmWindows windows)
        {
            object resdata = null;
            try
            {
                if (windows != null)
                {
                    resdata = await _manager.SaveUpdate(windows);
                }
            }
            catch (Exception) { }

            return new { resdata };
        }

        [HttpDelete("[action]/{id}")]
        public async Task<object> deletebyid(int id)
        {
            object resdata = null;
            try
            {
                resdata = await _manager.DeleteById(id);
            }
            catch (Exception) { }
            return new { resdata };
        }
        #endregion
    }
}
