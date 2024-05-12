using System.Windows;
using System.Windows.Controls;

namespace TravelAgency.Windows.customers
{
    public partial class CustomersWindow : Window
    {
        public Customer? selectedRow { get; set; }
        public CustomersWindow(bool isSelect = false)
        {
            InitializeComponent();

            QueryingEntities();

            if (isSelect)
            {
                BtnSelectColumn.Visibility = Visibility.Visible;
                BtnEditColumn.Visibility = Visibility.Hidden;

                BtnAdd.Visibility = Visibility.Hidden;
                BtnDelete.Visibility = Visibility.Hidden;

            }
            else
            {
                BtnSelectColumn.Visibility = Visibility.Hidden;
            }
        }

        private void QueryingEntities()
        {
            using (TravelDBContext db = new())
            {
                IQueryable<Customer>? ent = db.Customers;

                DGrid.ItemsSource = ent.ToList<Customer>();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditCustomersWindow addEdit = new AddEditCustomersWindow(((Button)sender).DataContext as Customer);
            addEdit.Show();
            this.Close();
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditCustomersWindow addEdit = new AddEditCustomersWindow(null!);
            addEdit.Show();
            this.Close();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var rowForRemoving = DGrid.SelectedItems.Cast<Country>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следущие {rowForRemoving.Count()} элемент?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    using (TravelDBContext db = new())
                    {
                        IQueryable<Customer>? entities = db.Customers
                            .Where(c => c.Id == rowForRemoving[0].Id);

                        if (entities is null)
                        {
                            MessageBox.Show("No entities found to delete.");
                            return;
                        }
                        else
                        {
                            db.Customers.RemoveRange(entities);
                        }
                        int affected = db.SaveChanges();
                    }
                    MessageBox.Show("Данные удалены");

                    QueryingEntities();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnTours_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSelect_Click(object sender, RoutedEventArgs e)
        {
            selectedRow = (Customer)((Button)sender).DataContext;

            this.Close();
        }
    }
}
