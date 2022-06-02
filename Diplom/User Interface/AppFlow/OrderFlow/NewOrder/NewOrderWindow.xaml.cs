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
using Diplom.Flows.AppFlow.OrderFlow.NewOrder;
using Diplom.Models;

namespace Diplom.Flows.AppFlow.NewOrder
{
    /// <summary>
    /// Логика взаимодействия для NewOrderWindow.xaml
    /// </summary>
    public partial class NewOrderWindow : Window
    {
        private readonly NewOrderWindowModel _newOrderWindowModel = new NewOrderWindowModel();
        public string UserName;
        public string UserSecondName;
        public NewOrderWindow()
        {
            InitializeComponent();
            LoadData();
            
        }
        
        private void NewOrderWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _newOrderWindowModel.GetMechanic(UserName, UserSecondName);
            UserNameInfo.Content = UserName + " " + UserSecondName;
        }

        public void LoadData()
        {
            ClientCarsComboBox.ItemsSource = null;
            SpareDataStorage.ItemsSource = null;
            ServiceDataStorage.ItemsSource = null;
            ClientsComboBox.ItemsSource = _newOrderWindowModel.GetClients();
            SpareDataStorage.ItemsSource = _newOrderWindowModel.GetSpares();
            ServiceDataStorage.ItemsSource = _newOrderWindowModel.GetServices();
        }

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsComboBox.SelectedItem != null && ClientCarsComboBox.SelectedItem != null &&
                DateStartPicker.Text == "" && DateEndPicker.Text == "" && DescriptionTextBox.Text == "" && ServiceInOrderDataStorage.Items.Count == 0)
            {
                return;
            }
            
            _newOrderWindowModel.DescriptionModel.Description = DescriptionTextBox.Text;
            _newOrderWindowModel.OrderModel.DateStart = DateStartPicker.SelectedDate.ToString();
            _newOrderWindowModel.OrderModel.DateEnd = DateEndPicker.SelectedDate.ToString();
            _newOrderWindowModel.AddOrderToDataBase();
            ResetData();
        }

        private void AddServiceToOrder_Click(object sender, RoutedEventArgs e)
        {
            if (ServiceDataStorage.SelectedItem == null)
            {
                return;
            }
            _newOrderWindowModel.ServicesOrderList.Add(ServiceDataStorage.SelectedItem as ServiceModel);
            ServiceInOrderDataStorage.ItemsSource = null;
            ServiceInOrderDataStorage.ItemsSource = _newOrderWindowModel.ServicesOrderList;
            UpdatePrice();
        }

        private void AddSpareToOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (SpareDataStorage.SelectedItem == null)
            {
                return;
            }
            _newOrderWindowModel.SparesOrderList.Add(SpareDataStorage.SelectedItem as SpareModel);
            SpareInOrderDataStorage.ItemsSource = null;
            SpareInOrderDataStorage.ItemsSource = _newOrderWindowModel.SparesOrderList;
            UpdatePrice();
        }

        private void RemoveServiceFromOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (ServiceInOrderDataStorage.SelectedItem == null)
            {
                return;
            }
            _newOrderWindowModel.RemoveServiceFromOrder(ServiceInOrderDataStorage.SelectedIndex);
            ServiceInOrderDataStorage.ItemsSource = null;
            ServiceInOrderDataStorage.ItemsSource = _newOrderWindowModel.ServicesOrderList;
            UpdatePrice();
        }

        private void RemoveSpareFromOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (SpareInOrderDataStorage.SelectedItem == null)
            {
                return;
            }
            _newOrderWindowModel.RemoveSpareFromOrder(SpareInOrderDataStorage.SelectedIndex);
            SpareInOrderDataStorage.ItemsSource = null;
            SpareInOrderDataStorage.ItemsSource = _newOrderWindowModel.SparesOrderList;
            UpdatePrice();
        }

        private void ClientsComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientsComboBox.SelectedItem == null)
            {
                return;
            }
            _newOrderWindowModel.ClientModel = ClientsComboBox.SelectedItem as ClientModel;
            ClientCarsComboBox.ItemsSource = null;
            ClientCarsComboBox.ItemsSource = _newOrderWindowModel.GetClientCars();
        }

        private void ClientCarsComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientCarsComboBox.SelectedItem == null)
            {
                return;
            }
            _newOrderWindowModel.CarModel = ClientCarsComboBox.SelectedItem as CarModel;
        }

        public void UpdatePrice()
        {
            TotalCostLabel.Content = _newOrderWindowModel.CountTotalPrice();
        }

        public void ResetData()
        {
            ClientsComboBox.SelectedItem = null;
            ClientCarsComboBox.SelectedItem = null;
            ClientCarsComboBox.ItemsSource = null;

            DateStartPicker.SelectedDate = null;
            DateEndPicker.SelectedDate = null;
            DescriptionTextBox.Text = "";

            SpareDataStorage.SelectedItem = null;
            ServiceDataStorage.SelectedItem = null;

            SpareInOrderDataStorage.ItemsSource = null;
            ServiceInOrderDataStorage.ItemsSource = null;
            TotalCostLabel.Content = "";
        }

        private void NewOrderWindow_OnClosed(object sender, EventArgs e)
        {
            var form = this.Owner as MainAppWindow;
            form.LoadData();
        }
    }
}
