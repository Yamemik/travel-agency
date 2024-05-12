using System.Windows;

namespace TravelAgency.Windows.countries
{
    public partial class AddEditCountryWindow : Window
    {
        private Country _currentData = new Country();

        public AddEditCountryWindow(Country? selectedRow)
        {
            InitializeComponent();

            if (selectedRow != null)
            {
                _currentData = selectedRow;
            }

            DataContext = _currentData;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            using (TravelDBContext db = new())
            {
                try
                {
                    if (_currentData.Id == 0)
                    {
                        db.Countries.Add(_currentData);
                    }
                    else
                    {
                        Country updateRow = db.Countries
                            .First(c => c.Id == _currentData.Id);

                        updateRow.Name = _currentData.Name;
                    }

                    // сохранение отслеживаемых изменений в базе данных
                    db.SaveChanges();
                    MessageBox.Show("Информация сохранена!");

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
            var response = MessageBox.Show("Закрыть?", "Закрыть", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (response == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
