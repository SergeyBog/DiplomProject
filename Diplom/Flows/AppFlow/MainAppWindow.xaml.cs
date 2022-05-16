using Diplom.ClientFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Diplom.CarFlow;
using Diplom.MechanicFlow;
using Diplom.ServiceFlow;
using Diplom.SpareFlow;

namespace Diplom
{
    public partial class MainAppWindow : Window
    {
        public string UserName;
        public string UserSecondName;
        public string UserType;

        public MainAppWindow()
        {
            InitializeComponent();
        }

        private void MainAppWindow_OnClosed(object sender, EventArgs e)
        {
            var form = this.Owner as MainWindow;
        }

        private void MainAppWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            UserInfo_Label.Content = UserName + " " + UserSecondName;
        }

        private void PresentCarWindow_Button_Click(object sender, RoutedEventArgs e)
        {
            var carCoordinator = new CarWindowCoordinator();
            carCoordinator.StartCarWindow();
        }

        private void PresentClientWindow_Button_Click(object sender, RoutedEventArgs e)
        {
            var clientWindowCoordinator = new ClientWindowCoordinator();
            clientWindowCoordinator.StartClientWindow();
        }

        private void PresentSpareWindow_Button_Click(object sender, RoutedEventArgs e)
        {
            var spareWindowCoordinator = new SpareWindowCoordinator();
            spareWindowCoordinator.StartSpareWindow();
        }

        private void PresentServiceWindow_Button_Click(object sender, RoutedEventArgs e)
        {
            var serviceWindowCoordinator = new ServiceWindowCoordinator();
            serviceWindowCoordinator.StartServiceWindow();
        }

        private void PresentMechanicWindow_Click(object sender, RoutedEventArgs e)
        {
            var mechanicCoordinator = new MechanicWindowCoordinator();
            mechanicCoordinator.StartMechanicWindow();
        }
    }
}
