using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.SpareFlow
{
    public class SpareWindowCoordinator
    {
        public void StartSpareWindow()
        {
            var form = new SpareWindow();
            form.ShowDialog();
        }
    }
}
