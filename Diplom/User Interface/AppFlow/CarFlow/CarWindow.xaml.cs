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
using Diplom.Models;

namespace Diplom
{
    public partial class CarWindow : Window
    {
        private readonly CarWindowModel _carWindowModel = new CarWindowModel();
        public CarWindow()
        {
            InitializeComponent();
        }
        private void CarWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            Car_DataStorage.ItemsSource = null;
            Car_DataStorage.ItemsSource = _carWindowModel.GetCars();
        }

        private void AddCar_Button_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInputs()) 
            {
                if (CheckForSize())
                {
                    var car = new CarModel()
                    {
                        Mark = CarMark_TextBox.Text,
                        Model = CarModel_TextBox.Text,
                        Year = CarYear_TextBox.Text,
                        LastTo = CarLastTO_TextBox.Text
                    };
                    _carWindowModel.AddCarToDb(car);
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

        private void EditCar_Button_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInputs())
            {
                if (CheckForSize())
                {
                    _carWindowModel.GetDataForModel(CarMark_TextBox.Text,CarModel_TextBox.Text,CarYear_TextBox.Text,CarLastTO_TextBox.Text);
                    _carWindowModel.EditCar();
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

        private void DeleteCar_Button_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInputs())
            {
                if (CheckForSize())
                {
                    _carWindowModel.DeleteCar();
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

        private void Car_DataStorage_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Car_DataStorage.SelectedItem == null)
            {
                return;
            }
            _carWindowModel.CarModel = Car_DataStorage.SelectedItem as CarModel;
            CarMark_TextBox.Text = _carWindowModel.CarModel.Mark;
            CarModel_TextBox.Text = _carWindowModel.CarModel.Model;
            CarYear_TextBox.Text = _carWindowModel.CarModel.Year;
            CarLastTO_TextBox.Text = _carWindowModel.CarModel.LastTo;
        }
        public void UpdateData()
        {
            Car_DataStorage.ItemsSource = _carWindowModel.GetCars();
        }
        public bool CheckForSize()
        {
            bool result;
            if (CarMark_TextBox.Text.Length < 30 && CarModel_TextBox.Text.Length < 30 &&
                CarYear_TextBox.Text.Length < 15 && CarLastTO_TextBox.Text.Length < 30)
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
            if (CarMark_TextBox.Text != "" && CarModel_TextBox.Text != "" && CarYear_TextBox.Text != "" &&
                CarLastTO_TextBox.Text != "")
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public void CleanInputs()
        {
            CarMark_TextBox.Text = "";
            CarModel_TextBox.Text = "";
            CarYear_TextBox.Text = "";
            CarLastTO_TextBox.Text = "";
        }

    }
}
