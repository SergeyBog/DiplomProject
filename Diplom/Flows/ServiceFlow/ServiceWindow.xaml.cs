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
using Diplom.ServiceFlow;

namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для ServiceWindow.xaml
    /// </summary>
    public partial class ServiceWindow : Window
    {
        private readonly ServiceWindowModel _serviceWindowModel = new ServiceWindowModel();
        public ServiceWindow()
        {
            InitializeComponent();
        }

        private void ServiceWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            Service_DataStorage.ItemsSource = null;
            Service_DataStorage.ItemsSource = _serviceWindowModel.GetServices();
        }

        private void AddService_Button_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInputs())
            {
                if (CheckForSize())
                {
                    if (CheckForNumber(ServiceCost_TextBox.Text))
                    {
                        var service = new ServiceModel()
                        {
                            Naming = ServiceNaming_TextBox.Text,
                            Cost = Convert.ToInt32(ServiceCost_TextBox.Text)
                        };
                        _serviceWindowModel.AddServiceToDb(service);
                        CleanInputs();
                        UpdateData();
                    }
                    else
                    {
                        CleanInputs();
                        MessageBox.Show("type correct number without coma or dot");
                    }
                   
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

        private void EditService_Button_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInputs())
            {
                if (CheckForSize())
                {
                    if (CheckForNumber(ServiceCost_TextBox.Text))
                    {
                        _serviceWindowModel.GetDataForModel(ServiceNaming_TextBox.Text, Convert.ToInt32(ServiceCost_TextBox.Text));
                        _serviceWindowModel.EditService();
                        CleanInputs();
                        UpdateData();
                    }
                    else
                    {
                        CleanInputs();
                        MessageBox.Show("type correct number without coma or dot");
                    }
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

        private void DeleteService_Button_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInputs())
            {
                if (CheckForSize())
                {
                    _serviceWindowModel.DeleteService();
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

        private void Service_DataStorage_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Service_DataStorage.SelectedItem == null)
            {
                return;
            }
            _serviceWindowModel.ServiceModel = Service_DataStorage.SelectedItem as ServiceModel;
            ServiceNaming_TextBox.Text = _serviceWindowModel.ServiceModel.Naming;
            ServiceCost_TextBox.Text = _serviceWindowModel.ServiceModel.Cost.ToString();
        }

        public void UpdateData()
        {
            Service_DataStorage.ItemsSource = _serviceWindowModel.GetServices();
        }
        public bool CheckForSize()
        {
            bool result;
            if (ServiceNaming_TextBox.Text.Length < 50 && ServiceCost_TextBox.Text.Length < 10)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public bool CheckInputs()
        {
            bool result;
            if (ServiceNaming_TextBox.Text != "" && ServiceCost_TextBox.Text != "")
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public bool CheckForNumber(string str)
        {
            int numericValue = 0;
            bool isNumber = int.TryParse(str, out numericValue);
            return isNumber;
        }


        private void CleanInputs()
        {
            ServiceNaming_TextBox.Text = "";
            ServiceCost_TextBox.Text = "";
        }
    }
}
