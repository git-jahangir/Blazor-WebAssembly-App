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
    public class OrdersController : ControllerBase
    {
        #region Variable Declaration & Initialization
        private readonly iOrderMgt _manager;
        #endregion

        #region Constructor
        public OrdersController()
        {
            _manager = new OrderMgt();
        }
        #endregion

        #region All Http Methods
        [HttpGet("[action]")]
        public async Task<List<vmOrders>> getall()
        {
            List<vmOrders> resdata = new List<vmOrders>();
            try
            {
                resdata = await _manager.GetAll();
            }
            catch (Exception) { }
            return resdata;
        }

        [HttpGet("[action]/{id}")]
        public async Task<vmOrders> getbyid(int id)
        {
            vmOrders resdata = new vmOrders();
            try
            {
                resdata = await _manager.GetById(id);
            }
            catch (Exception) { }
            return resdata;
        }

        [HttpGet("[action]")]
        public async Task<List<vmOrdersDDL>> getallorder()
        {
            List<vmOrdersDDL> resdata = new List<vmOrdersDDL>();
            try
            {
                resdata = await _manager.GetAllOrder();
            }
            catch (Exception) { }
            return resdata;
        }

        [HttpPost("[action]")]
        public async Task<object> saveupdate([FromBody] vmOrders orders)
        {
            object resdata = null;
            try
            {
                if (orders!=null)
                {
                    resdata = await _manager.SaveUpdate(orders);
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
