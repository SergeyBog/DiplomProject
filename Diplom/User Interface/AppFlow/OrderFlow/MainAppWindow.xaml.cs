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
using Diplom.Flows.AppFlow;
using Diplom.Flows.AppFlow.NewOrder;
using Diplom.MechanicFlow;
using Diplom.Models;
using Diplom.ServiceFlow;
using Diplom.SpareFlow;

namespace Diplom
{
    public partial class MainAppWindow : Window
    {
        public string UserName;
        public string UserSecondName;
        public string UserType;

        private readonly MainAppWindowModel _mainAppWindowModel = new MainAppWindowModel();


        public MainAppWindow()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            OrderDataStorage.ItemsSource = null;
           OrderDataStorage.ItemsSource = _mainAppWindowModel.GetAllOrders();
        }
        private void MainAppWindow_OnClosed(object sender, EventArgs e)
        {
            var form = this.Owner as MainWindow;
        }

        private void MainAppWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            UserInfo_Label.Content = UserName + " " + UserSecondName;
            GetAvailableOptions();
        }

        public void GetAvailableOptions()
        {
            if (UserType == "admin")
            {
                AddOrderButton.IsEnabled = false;
                AddOrderButton.Visibility = Visibility.Hidden;
                SeeDetailsButton.IsEnabled = false;
                SeeDetailsButton.Visibility = Visibility.Hidden;
                DeleteOrderButton.IsEnabled = false;
                DeleteOrderButton.Visibility = Visibility.Hidden;
            }
            else
            {
                PresentMechanicWindow.IsEnabled = false;
                PresentMechanicWindow.Visibility = Visibility.Hidden;
                PresentSpareWindow_Button.IsEnabled = false;
                PresentSpareWindow_Button.Visibility = Visibility.Hidden;
            }
        }

        private void PresentCarWindow_Button_Click(object sender, RoutedEventArgs e)
        {
            var form = new CarWindow() { Owner = this };
            form.ShowDialog();
        }

        private void PresentClientWindow_Button_Click(object sender, RoutedEventArgs e)
        {
            var form = new ClientWindow() { Owner = this };
            form.ShowDialog();
        }

        private void PresentSpareWindow_Button_Click(object sender, RoutedEventArgs e)
        {
            var form = new SpareWindow() { Owner = this };
            form.ShowDialog();
        }

        private void PresentServiceWindow_Button_Click(object sender, RoutedEventArgs e)
        {
            var form = new ServiceWindow() { Owner = this };
            form.ShowDialog();
        }

        private void PresentMechanicWindow_Click(object sender, RoutedEventArgs e)
        {
            var form = new MechanicWindow() { Owner = this };
            form.ShowDialog();
        }

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new NewOrderWindow() { Owner = this };
            form.UserName = UserName;
            form.UserSecondName = UserSecondName;
            form.ShowDialog();
            
        }

        private void OrderDataStorage_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrderDataStorage.SelectedItem == null)
            {
                return;
            }
            _mainAppWindowModel.OrderModel = OrderDataStorage.SelectedItem as OrderModel;
        }

        private void SeeDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            OrderDataStorage.SelectedItem = null;
        }

        private void DeleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrderDataStorage.SelectedItem == null)
            {
                return;
            }
            _mainAppWindowModel.DeleteOrder();
            LoadData();
        }
    }
}
