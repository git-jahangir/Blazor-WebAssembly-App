using IntusWindowDataModel;
using IntusWindowDataModel.ViewModel;
using IntusWindowsDAL.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Xml;
using System.Xml.Linq;

namespace IntusWindowsDAL.Implementation
{
    public class WindowsMgt : iWindowsMgt
    {
        IntusDbContext _dbContext;
        public async Task<List<vmWindows>> GetAll()
        {
            List<vmWindows> listWindow = new();
            using (_dbContext = new())
            {
                try
                {

                    listWindow = await (from win in _dbContext.windows
                                        join ord in _dbContext.orders on win.OrderId equals ord.OrderId
                                        select new vmWindows
                                        {
                                            WindowId = win.WindowId,
                                            WindowName = win.WindowName,
                                            QuantityOfWindows = win.QuantityOfWindows,
                                            TotalSubElements = win.TotalSubElements,
                                            OrderId = win.OrderId,
                                            OrderName=ord.OrderName
                                        }).ToListAsync();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }

            return listWindow;
        }

        public async Task<vmWindows> GetById(int Id)
        {
            vmWindows Window = new();
            using (_dbContext = new())
            {
                try
                {

                    Window = await (from win in _dbContext.windows
                                    where win.WindowId == Id
                                    select new vmWindows
                                    {
                                        WindowId = win.WindowId,
                                        WindowName = win.WindowName,
                                        QuantityOfWindows = win.QuantityOfWindows,
                                        TotalSubElements = win.TotalSubElements,
                                        OrderId = win.OrderId
                                    }).FirstOrDefaultAsync();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }

            return Window;
        }

        public async Task<List<vmWindowsDDL>> GetAllById(int Id)
        {
            List<vmWindowsDDL> Window = new();
            using (_dbContext = new())
            {
                try
                {

                    Window = await (from win in _dbContext.windows
                                    where win.OrderId == Id
                                    select new vmWindowsDDL
                                    {
                                        WindowId = win.WindowId,
                                        WindowName = win.WindowName
                                    }).ToListAsync();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }

            return Window;
        }

        public async Task<object> SaveUpdate(vmWindows _object)
        {
            bool resstate = false; string message = string.Empty;
            Windows windows = new();
            using (_dbContext = new())
            {
                using (var _dbTran = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        if (_object.WindowId != null)
                        {
                            windows = await _dbContext.windows.Where(x => x.WindowId == _object.WindowId).FirstOrDefaultAsync();
                            windows.WindowName = _object.WindowName;
                            windows.QuantityOfWindows = _object.QuantityOfWindows;
                            windows.TotalSubElements = _object.TotalSubElements;
                            windows.OrderId = _object.OrderId;
                        }
                        else
                        {
                            windows = new();
                            windows.WindowName = _object.WindowName;
                            windows.QuantityOfWindows = _object.QuantityOfWindows;
                            windows.TotalSubElements = _object.TotalSubElements;
                            windows.OrderId = _object.OrderId;
                            var obj = await _dbContext.windows.AddAsync(windows);
                        }

                        await _dbContext.SaveChangesAsync();
                        await _dbTran.CommitAsync();

                        resstate = true;
                        message = (_object.WindowId == null ? "Saved" : "Updated") + " Successfully!!!";

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
            Windows windows = new();
            using (_dbContext = new())
            {
                using (var _dbTran = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        if (Id > 0)
                        {
                            windows = await _dbContext.windows.Where(x => x.WindowId == Id).FirstOrDefaultAsync();
                            var obj = _dbContext.Remove(windows);
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