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
    /// Логика взаимодействия для order.xaml
    /// </summary>
    public partial class order : Page
    {
        public order()
        {
            InitializeComponent();
            LoadCart();
        }

        private void LoadCart()
        {
            CartList.ItemsSource = CartManager.CartItems;
            decimal total = CartManager.CartItems.Sum(p => p.Price);
            TotalText.Text = $"Итого: {total:N0} руб.";
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Items());
        }

        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            if (CartManager.CartItems.Count == 0)
            {
                MessageBox.Show("Вы не выбрали ни одной вафли!");
                return;
            }
            if (string.IsNullOrWhiteSpace(NameBox.Text) || string.IsNullOrWhiteSpace(EmailBox.Text) || string.IsNullOrWhiteSpace(AddressBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните ФИО, Email и Адрес!");
                return;
            }

            string selectedSauce = (SauceBox.SelectedItem as ComboBoxItem).Content.ToString();

            using (WaffleShopEntities db = new WaffleShopEntities())
            {
                Order newOrder = new Order
                {
                    FullName = NameBox.Text,
                    Email = EmailBox.Text,
                    Address = AddressBox.Text,
                    Sauce = selectedSauce,
                    TotalAmount = CartManager.CartItems.Sum(p => p.Price)
                };
                db.Order.Add(newOrder);
                db.SaveChanges();

                foreach (var product in CartManager.CartItems)
                {
                    OrderDetail detail = new OrderDetail
                    {
                        OrderId = newOrder.Id,
                        ProductId = product.Id,
                        Quantity = 1
                    };
                    db.OrderDetail.Add(detail);
                }
                db.SaveChanges();
            }

            MessageBox.Show($"Заказ оформлен! Соус: {selectedSauce}. Ждите курьера!");
            CartManager.Clear();
            NavigationService.Navigate(new Items());
        }
    }
}