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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using big_pachka.Class;
using big_pachka.User_Controle;
using System.IO;

namespace big_pachka.User_Controle
{
    /// <summary>
    /// Логика взаимодействия для materials_control.xaml
    /// </summary>
    public partial class materials_control : UserControl
    {
        public materials_control()
        {
            InitializeComponent();
        }
        //отвечает за обновление
        public MainWindow Main; 
        private void material_delete_Click(object sender, RoutedEventArgs e)
        {
            //подключаем базу данных
            using (SqlConnection connection = new SqlConnection(connect.String))
            { //вывод сообщения на поддтверждение операции
                if (MessageBox.Show("Хотите удалить товар?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    connection.Open();// подключение к базе данных
                    SqlCommand command = new SqlCommand($@"DELETE FROM [dbo].[Material] WHERE ID = '{lable_ID.Content}'", connection);// берет данные из базы данных
                    command.ExecuteNonQuery();
                    Main.Load_Data("");
                } 
                else
                {
                    MessageBox.Show("Отлично!");
                }
            }
        }
    }
}
