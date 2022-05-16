using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.ServiceFlow
{
   public class ServiceWindowCoordinator
   {
        public void StartServiceWindow()
        {
            var form = new ServiceWindow();
            form.ShowDialog();
        }
   }
}
