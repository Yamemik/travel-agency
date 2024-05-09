using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;

namespace TravelAgency.Windows
{
    public partial class ToursWindow : Window
    {
        public ToursWindow()
        {
            InitializeComponent();

            QueryingTours();
        }

        private void QueryingTours()
        {
            using (TravelDBContext db = new())
            {
                IQueryable<Tour>? tours = db.Tours?
                    .Include(c => c.Country)
                    .Include(c => c.Hotel);

                DGridTours.ItemsSource = tours?.ToList<Tour>();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditPageHotel addEditPage = new AddEditPageHotel(((Button)sender).DataContext as Hotel);
            addEditPage.Show();
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditPageHotel addEditPage = new AddEditPageHotel(null!);
            addEditPage.Show();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var toursForRemoving = DGridTours.SelectedItems.Cast<Tour>().ToList();
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
                            MessageBox.Show("No products found to delete.");
                            return;
                        }
                        else
                        {
                            db.Tours.RemoveRange(tours);
                        }
                        int affected = db.SaveChanges();
                    }
                    MessageBox.Show("Данные удалены");

                    QueryingTours();
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
