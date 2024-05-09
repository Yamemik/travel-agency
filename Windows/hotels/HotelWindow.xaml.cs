using System.Windows;
using System.Windows.Controls;


namespace TravelAgency.Windows
{
    public partial class HotelWindow : Window
    {
        public HotelWindow()
        {
            InitializeComponent();

            QueryingHotels();
        }

        private void QueryingHotels()
        {
            using (TravelDBContext db = new())
            {
                IQueryable<Hotel>? hotels = db.Hotels;

                DGridHotels.ItemsSource = hotels.ToList<Hotel>();
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
            var hotelsForRemoving = DGridHotels.SelectedItems.Cast<Hotel>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следущие {hotelsForRemoving.Count()} элемент?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    using (TravelDBContext db = new())
                    {
                        IQueryable<Hotel>? hotels = db.Hotels
                            .Where(c => c.Id == hotelsForRemoving[0].Id);

                        if (hotels is null)
                        {
                            MessageBox.Show("No hotels found to delete.");
                            return;
                        }
                        else
                        {
                            db.Hotels.RemoveRange(hotels);
                        }
                        int affected = db.SaveChanges();
                    }
                    MessageBox.Show("Данные удалены");

                    QueryingHotels();
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
