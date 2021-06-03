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
using System.IO;
using Lopushok.User_Controls;
namespace Lopushok.User_Controls
{
    /// <summary>
    /// Логика взаимодействия для Pruduct_Control.xaml
    /// </summary>
    public partial class Pruduct_Control : UserControl
    {
        public Pruduct_Control()
        {
            InitializeComponent();
        }
        public MainWindow Main;

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Connect_BD.String))
            {
                if (MessageBox.Show("Хотите удалить агента?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand($@"DELETE FROM [dbo].[Product] WHERE ID = '{label_id.Content}'", connection);
                    command.ExecuteNonQuery();
                    Main.Load_Data("");
                }
                else
                {

                }
            }
        }
    }
}