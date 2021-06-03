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
using Lopushok.Windows;

namespace Lopushok
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
            Product_List.Children.Clear();
            using (SqlConnection connection = new SqlConnection(Connect_BD.String))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($@"SELECT  Product.ID
                                                              ,Product.Title
                                                              ,ProductType.Title AS Title2
                                                              ,Product.ArticleNumber
                                                              ,Product.Description
                                                              ,Product.Image
                                                              ,Product.ProductionPersonCount
                                                              ,Product.ProductionWorkshopNumber
                                                              ,Product.MinCostForAgent
                                                              ,Material.Title
                                                          FROM [dbo].[Product]
                                        INNER JOIN ProductType ON Product.ProductTypeID = ProductType.ID
                                        INNER JOIN Material ON Product.ID = Material.ID
                              WHERE (Product.Title like '%{Search.Text}%' or Product.Description like '%{Search.Text}%')
                                                    
                         AND ProductType.Title like '{(Filtr.SelectedIndex == 0 ? "" : ((ComboBoxItem)Filtr.SelectedItem).Content)}%'
                                                         ORDER BY {(Sort.SelectedIndex == 0 ? "Product.ID" : (Sort.SelectedIndex == 1 ? "MinCostForAgent" : "MinCostForAgent"))}
" + a, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Pruduct_Control product = new Pruduct_Control();
                        product.label_id.Content = reader[0];
                        product.label_type_and_title.Content = $"{reader[2]} | {reader[1]}";
                        product.label_arti.Content = reader[3];
                        product.label_description.Content = reader[4];
                        product.label_image.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\" + reader[5].ToString().Replace("jpg", "jpeg")));
                        product.label_person.Content = reader[6];
                        product.label_workshop.Content = reader[7];
                        product.label_cont.Content = reader[8];
                        product.label_material.Text = $"Материалы: {reader[9]}";
                        product.Main = this;
                        Product_List.Children.Add(product);
                    }
                }


            }


        }

        private void btn_UP_Click(object sender, RoutedEventArgs e)
        {
            Page_Scroll.PageUp();
        }

        private void btn_DOWN_Click(object sender, RoutedEventArgs e)
        {
            Page_Scroll.PageDown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load_Data("");
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Filtr != null)
             Load_Data("");
        }

        private void Sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Filtr != null)
            {
                Load_Data("");
            }
        }


        private void Filtr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Load_Data("");
        }

    

        private void btn_go_add_product_Click_1(object sender, RoutedEventArgs e)
        {
            Add_Product add = new Add_Product();
            add.Show();
            this.Hide();
        }
    }
}