using IntusWindowDataModel;
using IntusWindowDataModel.ViewModel;
using IntusWindowsDAL.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Linq;

namespace IntusWindowsDAL.Implementation
{
    public class ElementsMgt : iElementsMgt
    {
        IntusDbContext _dbContext;
        public async Task<List<vmElements>> GetAll()
        {
            List<vmElements> listElement = new();
            using (_dbContext = new())
            {
                try
                {

                    listElement = await (from sel in _dbContext.subElements
                                         join win in _dbContext.windows on sel.WindowId equals win.WindowId
                                         join ord in _dbContext.orders on win.OrderId equals ord.OrderId
                                         select new vmElements
                                         {
                                             SubElementId = sel.SubElementId,
                                             Element = sel.Element,
                                             Type = sel.Type,
                                             Width = sel.Width,
                                             Height = sel.Height,
                                             WindowId = sel.WindowId,
                                             WindowName = win.WindowName,
                                             OrderName = ord.OrderName
                                         }).ToListAsync();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }

            return listElement;
        }

        public async Task<vmElements> GetById(int Id)
        {
            vmElements elements = new();
            using (_dbContext = new())
            {
                try
                {
                    elements = await (from sel in _dbContext.subElements
                                      join win in _dbContext.windows on sel.WindowId equals win.WindowId
                                      where sel.SubElementId == Id
                                      select new vmElements
                                      {
                                          SubElementId = sel.SubElementId,
                                          Element = sel.Element,
                                          Type = sel.Type,
                                          Width = sel.Width,
                                          Height = sel.Height,
                                          WindowId = sel.WindowId,
                                          OrderId= win.OrderId
                                      }).FirstOrDefaultAsync();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }

            return elements;
        }

        public async Task<object> SaveUpdate(vmElements _object)
        {
            bool resstate = false; string message = string.Empty;
            SubElements elements = new();
            using (_dbContext = new())
            {
                using (var _dbTran = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        if (_object.SubElementId != null)
                        {
                            elements = await _dbContext.subElements.Where(x => x.SubElementId == _object.SubElementId).FirstOrDefaultAsync();
                            elements.Element =_object.Element;
                            elements.Type = _object.Type;
                            elements.Width = _object.Width;
                            elements.Height = _object.Height;
                            elements.WindowId = _object.WindowId;
                        }
                        else
                        {
                            elements = new();
                            elements.Element = _object.Element;
                            elements.Type = _object.Type;
                            elements.Width = _object.Width;
                            elements.Height = _object.Height;
                            elements.WindowId =  _object.WindowId;
                            var obj = await _dbContext.subElements.AddAsync(elements);
                        }

                        await _dbContext.SaveChangesAsync();
                        await _dbTran.CommitAsync();

                        resstate = true;
                        message = (_object.SubElementId == null ? "Saved" : "Updated") + " Successfully!!!";
                    }
                    catch (Exception ex)
                    {
                        resstate = false;
                        message = "Failed to save, Please try again!!!";
                        ex.ToString();
                    }
                }
            }


            return new
            {
                resstate,
                message
            };
        }

        public async Task<object> DeleteById(int Id)
        {
            bool resstate = false; string message = string.Empty;
            SubElements subElements = new();
            using (_dbContext = new())
            {
                using (var _dbTran = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        if (Id > 0)
                        {
                            subElements = await _dbContext.subElements.Where(x => x.SubElementId == Id).FirstOrDefaultAsync();
                            var obj = _dbContext.Remove(subElements);
                        }

                        await _dbContext.SaveChangesAsync();
                        await _dbTran.CommitAsync();
                        resstate = true;
                        message = "Deleted successfully!!!";
                    }
                    catch (Exception ex)
                    {
                        resstate = true;
                        message = "Failed to delete, Please try again!!!";
                        ex.ToString();
                    }
                }
            }


            return new
            {
                resstate,
                message
            };
        }
    }
}