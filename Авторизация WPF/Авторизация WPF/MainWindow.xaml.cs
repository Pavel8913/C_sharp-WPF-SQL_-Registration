using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using Авторизация_WPF.Connection;

namespace Авторизация_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        DB DB = new DB();
        md5 md5 = new md5();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Кнопка нажата","Уведомление",MessageBoxButton.YesNoCancel,MessageBoxImage.Information);

            int simvol_login;
            int simvol_password;

            simvol_login = tb_login.Text.Length;
            simvol_password = tb_password.Text.Length;

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



            var login = tb_login.Text;
            var password = md5.hashe_password(tb_password.Text);

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"select ID,Login,Password from registrer where Login='{login}'and Password='{password}'";

            SqlCommand command = new SqlCommand(querystring, DB.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count == 1)
            {
                MessageBox.Show("Успех!");
                
            }
            else
            {
                MessageBox.Show("Данные заполнены неверно!" + Environment.NewLine + "Или вам нужно пройти регистрацию");
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Add_form add_Form = new Add_form();
            this.Hide();
            add_Form.Show();
        }

        private void показать_image_Click(object sender, RoutedEventArgs e)
        {
            //tb_password.UseSystemPasswordChar = false;
            Скрыть_image.Visibility = Visibility.Visible;
            показать_image.Visibility=Visibility.Collapsed;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //tb_password.Text =tb_password.Text.Replace("", "•");
            //tb_password.passwordchar = '•';
            показать_image.Visibility =Visibility.Collapsed;
        }
    }
}

