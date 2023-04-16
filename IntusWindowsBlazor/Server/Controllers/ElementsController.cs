using IntusWindowDataModel.ViewModel;
using IntusWindowsDAL.Implementation;
using IntusWindowsDAL.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IntusWindowsBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElementsController : ControllerBase
    {
        #region Variable Declaration & Initialization
        private readonly iElementsMgt _manager;
        #endregion

        #region Constructor
        public ElementsController()
        {
            _manager = new ElementsMgt();
        }
        #endregion

        #region All Http Methods
        [HttpGet("[action]")]
        public async Task<List<vmElements>> getall()
        {
            List<vmElements> resdata = new List<vmElements>();
            try
            {
                resdata = await _manager.GetAll();
            }
            catch (Exception) { }
            return resdata;
        }

        [HttpGet("[action]/{id}")]
        public async Task<vmElements> getbyid(int id)
        {
            vmElements resdata = new vmElements();
            try
            {
                resdata = await _manager.GetById(id);
            }
            catch (Exception) { }
            return resdata;
        }

        [HttpPost("[action]")]
        public async Task<object> SaveUpdate([FromBody] vmElements elements)
        {
            object resdata = null;
            try
            {
                if (elements != null)
                {
                    resdata = await _manager.SaveUpdate(elements);
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
