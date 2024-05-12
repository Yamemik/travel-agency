using System.Windows;
using System.Windows.Controls;


namespace TravelAgency.Windows
{
    public partial class HotelWindow : Window
    {
        public Hotel? selectedHotel { get; set; }

        public HotelWindow(bool isSelect = false)
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
                IQueryable<Hotel>? hotels = db.Hotels;

                DGridHotels.ItemsSource = hotels.ToList<Hotel>();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditPageHotel addEditPage = new AddEditPageHotel(((Button)sender).DataContext as Hotel);
            addEditPage.ShowDialog();
            QueryingEntities();
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditPageHotel addEditPage = new AddEditPageHotel(null!);
            addEditPage.ShowDialog();
            QueryingEntities();
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
            selectedHotel = (Hotel)((Button)sender).DataContext;            

            this.Close();
        }
    }
}
