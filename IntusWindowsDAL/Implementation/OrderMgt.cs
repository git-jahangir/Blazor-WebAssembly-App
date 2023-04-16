using IntusWindowDataModel;
using IntusWindowDataModel.ViewModel;
using IntusWindowsBLL;
using IntusWindowsDAL.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Transactions;
using System.Xml;
using System.Xml.Linq;

namespace IntusWindowsDAL.Implementation
{
    public class OrderMgt : iOrderMgt
    {
        private IGenericFactoryEF<Orders> order_ef;
        public OrderMgt()
        {
            order_ef = new GenericFactoryEF<IntusDbContext, Orders>();
        }

        public async Task<List<vmOrders>> GetAll()
        {
            List<vmOrders> listOrder = new();
            try
            {
                List<Orders> orders = await order_ef.GetListAsync();
                listOrder = (from ordr in orders
                             select new vmOrders
                             {
                                 OrderId = ordr.OrderId,
                                 OrderName = ordr.OrderName,
                                 State = ordr.State
                             }).ToList();

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return listOrder;
        }

        public async Task<List<vmOrdersDDL>> GetAllOrder()
        {
            List<vmOrdersDDL> listOrder = new();
            try
            {
                List<Orders> orders = await order_ef.GetListAsync();
                listOrder = (from ordr in orders
                             select new vmOrdersDDL
                             {
                                 OrderId = ordr.OrderId,
                                 OrderName = ordr.OrderName
                             }).ToList();

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return listOrder;
        }

        public async Task<vmOrders> GetById(int Id)
        {
            vmOrders Order = new();
            try
            {
                Orders ordr = await order_ef.GetByIdAsync(x => x.OrderId == Id);
                Order = new vmOrders
                {
                    OrderId = ordr.OrderId,
                    OrderName = ordr.OrderName,
                    State = ordr.State
                };
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return Order;
        }

        public async Task<object> SaveUpdate(vmOrders _object)
        {
            bool resstate = false; string message = string.Empty;
            Orders orders = new();
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (_object.OrderId != null)
                    {
                        orders = await order_ef.GetByIdAsync(x => x.OrderId == _object.OrderId);
                        orders.OrderName = _object.OrderName;
                        orders.State = _object.State;
                        await order_ef.UpdateAsync(orders);
                    }
                    else
                    {
                        orders.OrderName = _object.OrderName;
                        orders.State = _object.State;
                        await order_ef.InsertAsync(orders);
                    }

                    await order_ef.SaveAsync();
                    transaction.Complete();

                    resstate = true;
                    message = (_object.OrderId == null ? "Saved" : "Updated") + " Successfully!!!";

                }
                catch (Exception ex)
                {
                    resstate = false;
                    message = "Failed to save, Please try again!!!";
                    ex.ToString();
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
            Orders orders = new();

            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (Id > 0)
                    {
                        orders = await order_ef.GetByIdAsync(x => x.OrderId == Id);
                        await order_ef.DeleteAsync(orders);
                    }

                    await order_ef.SaveAsync();
                    transaction.Complete();
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

            return new
            {
                resstate,
                message
            };
        }
    }
}