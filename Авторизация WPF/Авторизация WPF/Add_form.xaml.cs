using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using Авторизация_WPF.Connection;

namespace Авторизация_WPF
{
    /// <summary>
    /// Логика взаимодействия для Add_form.xaml
    /// </summary>
    public partial class Add_form : Window
    {
        public Add_form()
        {
            InitializeComponent();
        }
        DB DB=new DB();
        md5 md5 = new md5();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Hide();
            mainWindow.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int simvol_login;
            int simvol_password;
            int simvol_password2;

            simvol_login = tb_login.Text.Length;
            simvol_password = tb_password.Text.Length;
            simvol_password2 = tb_password2.Text.Length;

            if (tb_password.Text != tb_password2.Text)
            {
                MessageBox.Show("Пароли не совпадают!");

                return;
            }
            if (simvol_login < 6)
            {
                MessageBox.Show("Логин должен состоять из 6 и более символов!");
                return;
            }
            if (simvol_password < 6)
            {
                MessageBox.Show("Пароль должен состоять из 6 и более символов!");
                return;
            }



            if (checkuser())
            {
                return;
            }
            var login = tb_login.Text;
            var password = md5.hashe_password(tb_password.Text);


            string querystring = $"insert into registrer(Login,Password) values ('{login}','{password}')";

            SqlCommand command = new SqlCommand(querystring, DB.GetConnection());

            DB.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт успешно создан!");
            }
            else
            {
                MessageBox.Show("Аккаунт не создан!");
            }
            DB.closeConnection();
        }

        private bool checkuser()
        {
            var login = tb_login.Text;
            var password = tb_password.Text;


            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"select ID,Login,Password from registrer where Login='{login}'and Password='{password}'";

            SqlCommand command = new SqlCommand(querystring, DB.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь уже существует!");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
