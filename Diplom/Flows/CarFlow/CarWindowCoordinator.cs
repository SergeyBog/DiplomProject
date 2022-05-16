using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.CarFlow
{
    public class CarWindowCoordinator
    {
        public void StartCarWindow()
        {
            var form = new CarWindow();
            form.ShowDialog();
        }
    }
}
