using System;
using System.Windows;
using System.Windows.Controls;
using Diplom.Models;
using Diplom.User_Interface.AppFlow.OrderFlow.Order_description;

namespace Diplom.Flows.AppFlow.OrderFlow.Order_description
{
    public partial class OrderDescriptionWindow : Window
    {
        private readonly OrderDescriptionWindowModel _orderDescriptionWindowModel = new OrderDescriptionWindowModel();
        public OrderModel OrderData = new OrderModel();
        public string UserName;
        public string UserSecondName;
        public OrderDescriptionWindow()
        {
            InitializeComponent();
        }

        private void OrderDescriptionWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _orderDescriptionWindowModel.GetMechanic(UserName, UserSecondName);
            UserNameInfo.Content = UserName + " " + UserSecondName;
            _orderDescriptionWindowModel.OrderModel = OrderData;
            LoadData();

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
            SpareInOrderDataStorage.ItemsSource = null;
            ServiceInOrderDataStorage.ItemsSource = null;

            ClientsComboBox.ItemsSource = _orderDescriptionWindowModel.GetClients();
            SpareDataStorage.ItemsSource = _orderDescriptionWindowModel.GetSpares();
            ServiceDataStorage.ItemsSource = _orderDescriptionWindowModel.GetServices();
            ServiceInOrderDataStorage.ItemsSource = _orderDescriptionWindowModel.GetServicesInOrder();
            SpareInOrderDataStorage.ItemsSource = _orderDescriptionWindowModel.GetSparesInOrder();
            DescriptionTextBox.Text = _orderDescriptionWindowModel.OrderModel.Description.Description;
            DateStartPicker.SelectedDate = Convert.ToDateTime(_orderDescriptionWindowModel.OrderModel.DateStart);
            DateEndPicker.SelectedDate = Convert.ToDateTime(_orderDescriptionWindowModel.OrderModel.DateEnd);
            UpdatePrice();
        }


        private void EditOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsComboBox.SelectedItem != null && ClientCarsComboBox.SelectedItem != null &&
                DateStartPicker.Text == "" && DateEndPicker.Text == "" && DescriptionTextBox.Text == "" && ServiceInOrderDataStorage.Items.Count == 0)
            {
                return;
            }
            _orderDescriptionWindowModel.OrderModel.Description.Description = DescriptionTextBox.Text;
            _orderDescriptionWindowModel.OrderModel.DateStart = DateStartPicker.SelectedDate.ToString();
            _orderDescriptionWindowModel.OrderModel.DateEnd = DateEndPicker.SelectedDate.ToString();
            _orderDescriptionWindowModel.EditOrder();
            //ResetData();
        }

        private void ClientsComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientsComboBox.SelectedItem == null)
            {
                return;
            }
            _orderDescriptionWindowModel.OrderModel.Client = ClientsComboBox.SelectedItem as ClientModel;
            ClientCarsComboBox.ItemsSource = null;
            ClientCarsComboBox.ItemsSource = _orderDescriptionWindowModel.GetClientCars();
        }

        private void ClientCarsComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientCarsComboBox.SelectedItem == null)
            {
                return;
            }
            _orderDescriptionWindowModel.OrderModel.Car = ClientCarsComboBox.SelectedItem as CarModel;
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
