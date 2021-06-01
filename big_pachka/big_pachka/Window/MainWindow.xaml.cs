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

namespace big_pachka
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

        internal void Load_Data(string a)
        {   
            //очистка
            material_list.Children.Clear();
            //подключение к базе данных
            using (SqlConnection connection = new SqlConnection(connect.String))
            {  
                
                connection.Open();
                SqlCommand command = new SqlCommand($@"SELECT [ID]
                                                    ,[Title]
                                                    ,[MaterialTypeID]
                                                    ,[MinCount]
                                                    ,[CountInPack]
                                                    ,[Unit]
                                                    ,[CountInStock]
                                                    ,[Description]
                                                    ,[Cost]
                                                    ,[Image]
                                                FROM [dbo].[Material] WHERE (Material.Title like '%{search.Text}%' or  Material.MaterialTypeID like '%{search.Text}%' or Material.CountInStock like '%{search.Text}%')
                                               " + a, connection);
                SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        materials_control mater = new materials_control();
                        mater.lable_ID.Content = reader[0];
                        mater.lable_TypeMater_and_TitleMater.Content = $@"{reader[1]} | {reader[2]}";
                        mater.lable_min_count.Content = reader[3];
                        mater.lable_count_pack.Content = reader[4];
                        mater.lable_unit.Content = reader[5];
                        mater.lable_count_stock.Content = reader[6];
                        mater.lable_descript.Content = reader[7];
                        mater.Cost.Content = reader[8];
                        mater.image.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\" + reader[9]));
                        mater.Main = this; 
                        material_list.Children.Add(mater);
                        
                    }
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load_Data("");
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Load_Data("");
        }

        private void page_up_Click(object sender, RoutedEventArgs e)
        {
            scroll.PageUp();
        }

        private void page_down_Click(object sender, RoutedEventArgs e)
        {
            scroll.PageDown();
        }
    }


}
