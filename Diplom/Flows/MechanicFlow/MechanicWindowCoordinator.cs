using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.MechanicFlow
{
    public class MechanicWindowCoordinator
    {
        public void StartMechanicWindow()
        {
            var form = new MechanicWindow();
            form.ShowDialog();
        }
    }
}
