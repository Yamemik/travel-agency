using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using TravelAgency.Windows.tours;

namespace TravelAgency.Windows.flights
{
    public partial class FlightsWindow : Window
    {
        public Flight? selectedRow { get; set; }
        public FlightsWindow(bool isSelect = false)
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
                IQueryable<Flight>? entity = db.Flights?
                    .Include(c => c.Airline)
                    .Include(c => c.DepartureCountry)
                    .Include(c => c.DestinationCountry);

                DGrid.ItemsSource = entity?.ToList<Flight>();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditFlyWindow addEditPage = new AddEditFlyWindow(((Button)sender).DataContext as Flight);
            addEditPage.ShowDialog();
            QueryingEntities();
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditFlyWindow addEditPage = new AddEditFlyWindow(null!);
            addEditPage.ShowDialog();
            QueryingEntities();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var rowsForRemoving = DGrid.SelectedItems.Cast<Flight>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следущие {rowsForRemoving.Count()} элемент?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    using (TravelDBContext db = new())
                    {
                        IQueryable<Flight>? ent = db.Flights
                            .Where(c => c.Id == rowsForRemoving[0].Id);

                        if (ent is null)
                        {
                            MessageBox.Show("No tours found to delete.");
                            return;
                        }
                        else
                        {
                            db.Flights.RemoveRange(ent);
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
            selectedRow = (Flight)((Button)sender).DataContext;

            this.Close();
        }
    }
}
