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

namespace pr9
{
    /// <summary>
    /// Логика взаимодействия для Items.xaml
    /// </summary>
    public partial class Items : Page
    {
  
        
            public Items()
            {
                InitializeComponent();
                LoadProducts();
            }

  
            private void LoadProducts()
            {
                using (WaffleShopEntities db = new WaffleShopEntities())
                {
                    var products = db.Product.ToList();

                    foreach (var product in products)
                    {
                 
                        string fullPath = @"C:\Users\243450\source\repos\pr9\pr9\Images\" + product.ImagePath;
                        product.ImagePath = fullPath;
                    }

                    ProductsList.ItemsSource = products;
                }
            }

      
            private void AddToCart_Click(object sender, RoutedEventArgs e)
            {
                Button btn = sender as Button;
                Product selectedProduct = btn.Tag as Product;

                if (selectedProduct != null)
                {
                    CartManager.Add(selectedProduct);
                    MessageBox.Show($"Вафли '{selectedProduct.Name}' в корзине!");
                }
            }

 
            private void GoToOrder_Click(object sender, RoutedEventArgs e)
            {
                NavigationService.Navigate(new order());
            }
        }
    }
