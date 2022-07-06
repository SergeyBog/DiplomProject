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
using Diplom.SpareFlow;

namespace Diplom
{
    public partial class SpareWindow : Window
    {
        private readonly  SpareWindowModel _spareWindowModel = new SpareWindowModel();
        public SpareWindow()
        {
            InitializeComponent();
        }

        private void SpareWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            Spare_DataStorage.ItemsSource = null;
            Spare_DataStorage.ItemsSource = _spareWindowModel.GetSpares();
        }

        private void AddSpare_Button_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInputs())
            {
                if (CheckForSize())
                {
                    if (CheckForNumber(SpareCost_TextBox.Text))
                    {
                        var spare = new SpareModel()
                        {
                            Naming = SpareNaming_TextBox.Text,
                            Cost = Convert.ToInt32(SpareCost_TextBox.Text)
                        };
                        _spareWindowModel.AddSpareToDb(spare);
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

        private void EditSpare_Button_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInputs())
            {
                if (CheckForSize())
                {
                    if (CheckForNumber(SpareCost_TextBox.Text))
                    {
                        _spareWindowModel.GetDataForModel(SpareNaming_TextBox.Text, Convert.ToInt32(SpareCost_TextBox.Text));
                        _spareWindowModel.EditSpare();
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

        private void DeleteSpare_Button_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInputs())
            {
                if (CheckForSize())
                {
                    _spareWindowModel.DeleteSpare();
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

        private void Spare_DataStorage_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Spare_DataStorage.SelectedItem == null)
            {
                return;
            }
            _spareWindowModel.SpareModel = Spare_DataStorage.SelectedItem as SpareModel;
            SpareNaming_TextBox.Text = _spareWindowModel.SpareModel.Naming;
            SpareCost_TextBox.Text = _spareWindowModel.SpareModel.Cost.ToString();
        }

        public void UpdateData()
        {
            Spare_DataStorage.ItemsSource = _spareWindowModel.GetSpares();
        }

        public bool CheckForSize()
        {
            bool result;
            if (SpareNaming_TextBox.Text.Length < 50 && SpareCost_TextBox.Text.Length < 10)
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
            if (SpareNaming_TextBox.Text != "" && SpareCost_TextBox.Text != "")
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
            SpareNaming_TextBox.Text = "";
            SpareCost_TextBox.Text = "";
        }

       
    }
}
