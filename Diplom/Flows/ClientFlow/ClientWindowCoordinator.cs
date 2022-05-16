using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diplom.Models;

namespace Diplom.ClientFlow
{
    public class ClientWindowCoordinator
    {
        public void StartClientWindow()
        {
            var form = new ClientWindow();
            form.ShowDialog();
        }

    }
}
