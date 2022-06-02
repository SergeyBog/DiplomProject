using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Diplom
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Login_TextBox.Text = "apostol@gmail.com";
            Password_TextBox.Text = "1234567";
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            var worker = new DbWorker();
            if (CheckInputs())
            {
                if (worker.CheckLogin(Login_TextBox.Text, Password_TextBox.Text))
                {
                    if (worker.GetUserType(Login_TextBox.Text, Password_TextBox.Text) == "admin")
                    {
                        var form = new MainAppWindow() { Owner = this };
                        form.UserType = "admin";
                        form.UserName = worker.GetAdmin(Login_TextBox.Text, Password_TextBox.Text).Name;
                        form.UserSecondName = worker.GetAdmin(Login_TextBox.Text, Password_TextBox.Text).SecondName;
                        CleanInputs();
                        form.ShowDialog();
                    }
                    else
                    {
                        var form = new MainAppWindow() { Owner = this };
                        form.UserType = "mechanic";
                        form.UserName = worker.GetMechanic(Login_TextBox.Text, Password_TextBox.Text).Name;
                        form.UserSecondName = worker.GetMechanic(Login_TextBox.Text, Password_TextBox.Text).SecondName;
                        CleanInputs();
                        form.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid login or password please try again");
                    CleanInputs();
                }
            }
            else
            {
                MessageBox.Show("One or more Fields are empty");
                CleanInputs();
            }
           
        }

        public bool CheckInputs()
        {
            bool result;
            if (Login_TextBox.Text != "" && Password_TextBox.Text != "")
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
            Login_TextBox.Text = "";
            Password_TextBox.Text = "";
        }
    }
}
