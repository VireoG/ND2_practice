using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1_Homework.Business.Models;

namespace Task1_Homework.Business.Services.IServices
{
    public interface IOrderService : ICRUD<Order>
    {
        Task<IEnumerable<Order>> GetOrders();
        Task<Order> GetOrderById(int? id);
        Task<IEnumerable<Order>> GetSalesRequestsForIdentityUser(string UserName);
        Task<IEnumerable<Order>> GetOrdersForIdentityUser(string UserName);
        Task EditSave(Order model, string TrackNumber);
    }
}
