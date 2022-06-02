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
using Diplom.Models;
using Diplom.User_Interface.AppFlow.OrderFlow.Order_description;

namespace Diplom.Flows.AppFlow.OrderFlow.Order_description
{
    /// <summary>
    /// Логика взаимодействия для OrderDescriptionWindow.xaml
    /// </summary>
    public partial class OrderDescriptionWindow : Window
    {
        private readonly OrderDescriptionWindowModel _orderDescriptionWindowModel = new OrderDescriptionWindowModel();
        public OrderModel OrderData = new OrderModel();
        public string UserName;
        public string UserSecondName;
        public OrderDescriptionWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void OrderDescriptionWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _orderDescriptionWindowModel.GetMechanic(UserName, UserSecondName);
            UserNameInfo.Content = UserName + " " + UserSecondName;
            _orderDescriptionWindowModel.OrderModel = OrderData;

        }

        public void LoadOrderSelection()
        {
            ////edit
            _orderDescriptionWindowModel.LoadData();
        }

        private void OrderDescriptionWindow_OnClosed(object sender, EventArgs e)
        {
            var form = this.Owner as MainAppWindow;
            form.LoadData();
        }

        public void LoadData()
        {
            ClientCarsComboBox.ItemsSource = null;
            SpareDataStorage.ItemsSource = null;
            ServiceDataStorage.ItemsSource = null;
            ClientsComboBox.ItemsSource = _orderDescriptionWindowModel.GetClients();
            SpareDataStorage.ItemsSource = _orderDescriptionWindowModel.GetSpares();
            ServiceDataStorage.ItemsSource = _orderDescriptionWindowModel.GetServices();
        }

        private void EditOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsComboBox.SelectedItem != null && ClientCarsComboBox.SelectedItem != null &&
                DateStartPicker.Text == "" && DateEndPicker.Text == "" && DescriptionTextBox.Text == "" && ServiceInOrderDataStorage.Items.Count == 0)
            {
                return;
            }


        }

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsComboBox.SelectedItem != null && ClientCarsComboBox.SelectedItem != null &&
                DateStartPicker.Text == "" && DateEndPicker.Text == "" && DescriptionTextBox.Text == "" && ServiceInOrderDataStorage.Items.Count == 0)
            {
                return;
            }

            /*_newOrderWindowModel.DescriptionModel.Description = DescriptionTextBox.Text;
            _newOrderWindowModel.OrderModel.DateStart = DateStartPicker.SelectedDate.ToString();
            _newOrderWindowModel.OrderModel.DateEnd = DateEndPicker.SelectedDate.ToString();
            _newOrderWindowModel.AddOrderToDataBase();
            ResetData();*/
        }

        private void ClientsComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientsComboBox.SelectedItem == null)
            {
                return;
            }
            _orderDescriptionWindowModel.ClientModel = ClientsComboBox.SelectedItem as ClientModel;
            ClientCarsComboBox.ItemsSource = null;
            ClientCarsComboBox.ItemsSource = _orderDescriptionWindowModel.GetClientCars();
        }

        private void ClientCarsComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientCarsComboBox.SelectedItem == null)
            {
                return;
            }
            _orderDescriptionWindowModel.CarModel = ClientCarsComboBox.SelectedItem as CarModel;
        }

        private void AddServiceToOrderButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (ServiceDataStorage.SelectedItem == null)
            {
                return;
            }
            _orderDescriptionWindowModel.ServicesOrderList.Add(ServiceDataStorage.SelectedItem as ServiceModel);
            ServiceInOrderDataStorage.ItemsSource = null;
            ServiceInOrderDataStorage.ItemsSource = _orderDescriptionWindowModel.ServicesOrderList;
            UpdatePrice();
        }

        private void AddSpareToOrderButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (SpareDataStorage.SelectedItem == null)
            {
                return;
            }
            _orderDescriptionWindowModel.SparesOrderList.Add(SpareDataStorage.SelectedItem as SpareModel);
            SpareInOrderDataStorage.ItemsSource = null;
            SpareInOrderDataStorage.ItemsSource = _orderDescriptionWindowModel.SparesOrderList;
            UpdatePrice();
        }

        private void RemoveServiceFromOrderButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (ServiceInOrderDataStorage.SelectedItem == null)
            {
                return;
            }
            _orderDescriptionWindowModel.RemoveServiceFromOrder(ServiceInOrderDataStorage.SelectedIndex);
            ServiceInOrderDataStorage.ItemsSource = null;
            ServiceInOrderDataStorage.ItemsSource = _orderDescriptionWindowModel.ServicesOrderList;
            UpdatePrice();
        }

        private void RemoveSpareFromOrderButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (SpareInOrderDataStorage.SelectedItem == null)
            {
                return;
            }
            _orderDescriptionWindowModel.RemoveSpareFromOrder(SpareInOrderDataStorage.SelectedIndex);
            SpareInOrderDataStorage.ItemsSource = null;
            SpareInOrderDataStorage.ItemsSource = _orderDescriptionWindowModel.SparesOrderList;
            UpdatePrice();
        }
        

        public void UpdatePrice()
        {
            TotalCostLabel.Content = _orderDescriptionWindowModel.CountTotalPrice();
        }

        public void ResetData()
        {
            ClientsComboBox.SelectedItem = null;
            ClientCarsComboBox.SelectedItem = null;
            ClientCarsComboBox.ItemsSource = null;

            DateStartPicker.SelectedDate = null;
            DateEndPicker.SelectedDate = null;
            DescriptionTextBox.Text = "";

            SpareInOrderDataStorage.ItemsSource = null;
            ServiceInOrderDataStorage.ItemsSource = null;
            TotalCostLabel.Content = "";
        }
    }
}
