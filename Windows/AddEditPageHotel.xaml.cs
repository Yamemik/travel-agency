using System.Diagnostics;
using System.Text;
using System.Windows;


namespace TravelAgency.Windows
{
    public partial class AddEditPageHotel : Window
    {
        private Hotel _currentHotel = new Hotel();
        public AddEditPageHotel(Hotel selectedHotel)
        {
            InitializeComponent();

            if (selectedHotel != null)
                _currentHotel = selectedHotel;
            DataContext = _currentHotel;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentHotel.Name))
                errors.AppendLine("Укажите название отеля");
            if (_currentHotel.CompanyPrice < 1 || _currentHotel.CompanyPrice > 5)
                errors.AppendLine("Укажите количество звёзд - число от 1 до 5");
            if (_currentHotel.CustomerPrice == 0)
                errors.AppendLine("Укажите цену");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            using (TravelDBContext db = new())
            {
                try
                {
                    if (_currentHotel.Id == 0)
                    {
                        IQueryable<Hotel>? hotel = db.Hotels;
                        Random rnd = new Random();
                        _currentHotel.Id = rnd.Next(1,1000);
                        //_currentHotel.Id = hotel.Max(c => c.Id).;
                        db.Hotels.Add(_currentHotel);
                    }
                    else if (_currentHotel.Id == 0)
                    {

                    }

                    // сохранение отслеживаемых изменений в базе данных
                    db.SaveChanges();
                    MessageBox.Show("Информация сохранена!");
                    HotelWindow hotelWindow = new HotelWindow();
                    hotelWindow.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
