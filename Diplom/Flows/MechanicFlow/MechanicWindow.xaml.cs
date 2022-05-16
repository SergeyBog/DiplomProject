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
using Diplom.MechanicFlow;
using Diplom.Models;

namespace Diplom
{
    public partial class MechanicWindow : Window
    {
        readonly MechanicWindowModel _mechanicWindowModel = new MechanicWindowModel();
        public MechanicWindow()
        {
            InitializeComponent();
        }

        private void MechanicWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Mechanik_DataStorage.ItemsSource = _mechanicWindowModel.GetMechanics();
        }

        private void Mechanik_DataStorage_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Mechanik_DataStorage.SelectedItem == null)
            {
                return;
            }
            _mechanicWindowModel.MechanicModel = Mechanik_DataStorage.SelectedItem as MechanicModel;
            MechanikName_TextBox.Text = _mechanicWindowModel.MechanicModel.Name;
            MechanikSecondName_TextBox.Text = _mechanicWindowModel.MechanicModel.SecondName;
            MechanikPhone_TextBox.Text = _mechanicWindowModel.MechanicModel.PhoneNumber;
            MechanikExperience_TextBox.Text = _mechanicWindowModel.MechanicModel.WorkExperience;
            MechanicLogin_TextBox.Text = _mechanicWindowModel.MechanicModel.LoginInfo.Login;
            MechanicPassword_TextBox.Text = _mechanicWindowModel.MechanicModel.LoginInfo.Password;
        }

        private void DeleteMechanik_Button_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInputs())
            {
                if (CheckForSize())
                {
                    _mechanicWindowModel.DeleteMechanicFromDb();
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

        private void EditMechanik_Button_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInputs())
            {
                if (CheckForSize())
                {
                    _mechanicWindowModel.MechanicModel.Name = MechanikName_TextBox.Text;
                    _mechanicWindowModel.MechanicModel.SecondName = MechanikSecondName_TextBox.Text;
                    _mechanicWindowModel.MechanicModel.PhoneNumber = MechanikPhone_TextBox.Text;
                    _mechanicWindowModel.MechanicModel.WorkExperience = MechanikExperience_TextBox.Text;
                    _mechanicWindowModel.MechanicModel.LoginInfo.Login = MechanicLogin_TextBox.Text;
                    _mechanicWindowModel.MechanicModel.LoginInfo.Password = MechanicPassword_TextBox.Text;
                    _mechanicWindowModel.EditMechanic();
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

        private void AddMechanik_Button_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInputs())
            {
                if (CheckForSize())
                {
                    var mechanic = new MechanicModel()
                    {
                        Name = MechanikName_TextBox.Text,
                        SecondName = MechanikSecondName_TextBox.Text,
                        PhoneNumber = MechanikPhone_TextBox.Text,
                        WorkExperience = MechanikExperience_TextBox.Text,
                        LoginInfo = new LoginInfoModel()
                        {
                            Login = MechanicLogin_TextBox.Text,
                            Password = MechanicPassword_TextBox.Text,
                            UserType = "mechanik"
                        }
                    };
                    _mechanicWindowModel.AddMechanicToDb(mechanic);
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

        public bool CheckInputs()
        {
            bool result;
            if (MechanikName_TextBox.Text != "" && MechanikSecondName_TextBox.Text != "" && MechanikPhone_TextBox.Text != "" &&
                MechanikExperience_TextBox.Text != "" && MechanicLogin_TextBox.Text != "" && MechanicPassword_TextBox.Text != "")
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
            if (MechanikName_TextBox.Text.Length < 50 && MechanikSecondName_TextBox.Text.Length < 50 && MechanikPhone_TextBox.Text.Length < 15
                && MechanikExperience_TextBox.Text.Length < 30 && MechanicLogin_TextBox.Text.Length < 50 && MechanicPassword_TextBox.Text.Length < 50)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public void UpdateData()
        {
            Mechanik_DataStorage.ItemsSource = _mechanicWindowModel.GetMechanics();
        }

        public void CleanInputs()
        {
            MechanikName_TextBox.Text = "";
            MechanikSecondName_TextBox.Text = "";
            MechanikPhone_TextBox.Text = "";
            MechanikExperience_TextBox.Text = "";
            MechanicLogin_TextBox.Text = "";
            MechanicPassword_TextBox.Text = "";
        }

       
    }
}
