using System.Windows;
using System.Windows.Controls;
using TravelAgency.Windows.countries;

namespace TravelAgency.Windows
{
    public partial class CountriesWindow : Window
    {
        public Country? selectedRow { get; set; }
        public CountriesWindow(bool isSelect = false)
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
                IQueryable<Country>? countries = db.Countries;

                DGrid.ItemsSource = countries.ToList<Country>();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditCountryWindow addEditPage = new AddEditCountryWindow(((Button)sender).DataContext as Country);
            addEditPage.ShowDialog();
            QueryingEntities();
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditCountryWindow addEditPage = new AddEditCountryWindow(null!);
            addEditPage.ShowDialog();
            QueryingEntities();
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
                        IQueryable<Country>? entities = db.Countries
                            .Where(c => c.Id == rowForRemoving[0].Id);

                        if (entities is null)
                        {
                            MessageBox.Show("No entities found to delete.");
                            return;
                        }
                        else
                        {
                            db.Countries.RemoveRange(entities);
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
            selectedRow = (Country)((Button)sender).DataContext;

            this.Close();
        }
    }
}
