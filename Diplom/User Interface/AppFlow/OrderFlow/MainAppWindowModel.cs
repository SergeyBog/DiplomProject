using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diplom.Models;

namespace Diplom.Flows.AppFlow
{
    public class MainAppWindowModel
    {
        public OrderModel OrderModel = new OrderModel();
        private readonly DbWorker _worker = new DbWorker();

        public List<OrderModel> GetAllOrders()
        {
            return _worker.GetOrders();
        }

        public void DeleteOrder()
        {
            _worker.DeleteOrderServices(OrderModel.Description.Id);
            _worker.DeleteOrderSpares(OrderModel.Description.Id);
            _worker.DeleteOrderFromDb(OrderModel);
            _worker.DeleteOrderDescription(OrderModel.Description.Id);
        }

       
    }
}
