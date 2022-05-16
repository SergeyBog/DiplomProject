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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using Diplom.ClientFlow;
using Diplom.Models;

namespace Diplom
{
    public partial class ClientWindow : Window
    {
        readonly ClientWindowModel _clientWindowModel = new ClientWindowModel();
        public ClientWindow()
        {
            InitializeComponent();
            LoadData();
        }
        private void ClientWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            var worker = new DbWorker();
            Client_DataStorage.ItemsSource = null;
            Client_DataStorage.ItemsSource = _clientWindowModel.GetClients();
        }


        private void AddClient_Button_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInputs())   
            {
                if (CheckForSize())
                {
                    var client = new ClientModel()
                    {
                        Name = ClientName_TextBox.Text,
                        SecondName = ClientSecondName_TextBox.Text,
                        PhoneNumber = ClientPhone_TextBox.Text
                    };
                    _clientWindowModel.AddClientToDb(client);
                    CleanInputs();
                    UpdateData();
                }
                else
                {
                    CleanInputs();
                    MessageBox.Show("One or more texBox fields are too long");
                }
            }
            else
            {
                MessageBox.Show("One or more textBox fields are empty");
            }
            
        }

        private void EditClient_Button_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInputs())
            {
                if (CheckForSize())
                {
                    _clientWindowModel.GetDataForModel(ClientName_TextBox.Text,ClientSecondName_TextBox.Text,ClientPhone_TextBox.Text);
                    _clientWindowModel.EditClient();
                    UpdateData();
                    CleanInputs();
                }
                else
                {
                    CleanInputs();
                    MessageBox.Show("One or more texBox fields are too long");
                }
            }
            else
            {
                MessageBox.Show("One or more textBox fields are empty");
            }
        }

        private void DeleteClient_Button_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInputs())
            {
                if (CheckForSize())
                {
                    _clientWindowModel.DeleteClient();
                    UpdateData();
                    CleanInputs();
                }
                else
                {
                    CleanInputs();
                    MessageBox.Show("One or more texBox fields are too long");
                }
            }
            else
            {
                MessageBox.Show("One or more textBox fields are empty");
            }
        }

        private void Client_DataStorage_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Client_DataStorage.SelectedItem == null)
            {
                return;
            }
            _clientWindowModel.ClientModel = Client_DataStorage.SelectedItem as ClientModel;
            ClientName_TextBox.Text = _clientWindowModel.ClientModel.Name;
            ClientSecondName_TextBox.Text = _clientWindowModel.ClientModel.SecondName;
            ClientPhone_TextBox.Text = _clientWindowModel.ClientModel.PhoneNumber;
            ClientCars_ComboBox.ItemsSource = null;
            Cars_ComboBox.ItemsSource = null;
            ClientCars_ComboBox.ItemsSource = _clientWindowModel.GetClientCars();
            Cars_ComboBox.ItemsSource = _clientWindowModel.GetAllAvailableCars();
        }

        public void UpdateData()
        {
            Client_DataStorage.ItemsSource = _clientWindowModel.GetClients();
            ClientCars_ComboBox.SelectedItem = null;
            ClientCars_ComboBox.ItemsSource = null;
            Cars_ComboBox.SelectedItem = null;
            Cars_ComboBox.ItemsSource = null;
        }

        public void CleanInputs()
        {
            ClientName_TextBox.Text = "";
            ClientSecondName_TextBox.Text = "";
            ClientPhone_TextBox.Text = "";
        }

        public bool CheckInputs()
        {
            bool result;
            if (ClientName_TextBox.Text != "" && ClientSecondName_TextBox.Text != "" && ClientPhone_TextBox.Text != "")
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public bool CheckForSize()
        {
            bool result;
            if (ClientName_TextBox.Text.Length < 50 && ClientSecondName_TextBox.Text.Length < 50 &&
                ClientPhone_TextBox.Text.Length < 15)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        private void ClientCars_ComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CarModel carModel = ClientCars_ComboBox.SelectedItem as CarModel;
        }

        private void Delink_Button_Click(object sender, RoutedEventArgs e)
        {
            if (ClientCars_ComboBox.SelectedItem == null)
            {
                return;
            }
            var car = ClientCars_ComboBox.SelectedItem as CarModel;
            _clientWindowModel.RemoveCarFromClient(car);
            UpdateData();
            CleanInputs();
        }

        private void Link_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Cars_ComboBox.SelectedItem == null)
            {
                return;
            }
            var car = Cars_ComboBox.SelectedItem as CarModel;
            _clientWindowModel.AddCarToClient(car);
            UpdateData();
            CleanInputs();
        }

       
    }
}
