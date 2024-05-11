using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using TravelAgency.Windows.tours;

namespace TravelAgency.Windows
{
    public partial class ToursWindow : Window
    {
        public ToursWindow()
        {
            InitializeComponent();

            QueryingEntities();
        }

        private void QueryingEntities()
        {
            using (TravelDBContext db = new())
            {
                IQueryable<Tour>? tours = db.Tours?
                    .Include(c => c.Country)
                    .Include(c => c.Hotel);

                DGrid.ItemsSource = tours?.ToList<Tour>();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditToursWindow addEditPage = new AddEditToursWindow(((Button)sender).DataContext as Tour);
            addEditPage.Show();
            this.Close();
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditToursWindow addEditPage = new AddEditToursWindow(null!);
            addEditPage.Show();
            this.Close();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var toursForRemoving = DGrid.SelectedItems.Cast<Tour>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следущие {toursForRemoving.Count()} элемент?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    using (TravelDBContext db = new())
                    {
                        IQueryable<Tour>? tours = db.Tours
                            .Where(c => c.Id == toursForRemoving[0].Id);

                        if (tours is null)
                        {
                            MessageBox.Show("No tours found to delete.");
                            return;
                        }
                        else
                        {
                            db.Tours.RemoveRange(tours);
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
    }
}
