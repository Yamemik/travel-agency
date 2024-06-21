using Microsoft.EntityFrameworkCore;
using System.Windows;
using TravelAgency.Windows.flights;

namespace TravelAgency.Windows.contracts
{
    public partial class AddEditContractWindow : Window
    {
        private Contract _currentData = new Contract();
        public AddEditContractWindow(Contract? selectedTour)
        {
            InitializeComponent();

            if (selectedTour != null)
            {
                _currentData = selectedTour;

                dPstartDate.SelectedDate = _currentData.DateOfConclusion.ToDateTime(TimeOnly.MinValue);
            }
            else
            {
                DateTime thisDay = DateTime.Today;

                dPstartDate.SelectedDate = thisDay;
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
                        _currentData.DateOfConclusion = DateOnly.FromDateTime(dPstartDate.SelectedDate.Value);
                        db.Contracts.Add(_currentData);
                    }
                    else
                    {
                        Contract updateTour = db.Contracts
                            .First(c => c.Id == _currentData.Id);

                        updateTour.TourId = _currentData.TourId;
                        updateTour.DateOfConclusion = DateOnly.FromDateTime(dPstartDate.SelectedDate.Value);
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

        private void CMHotels_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ToursWindow win = new ToursWindow(true);
            win.Owner = this;
            win.ShowDialog();

            var select = win.selectedRow;

            if (select is not null)
            {
                _currentData.Tour = null!;
                _currentData.TourId = select.Id;

                DataContext = _currentData;

                txtTour.Text = select.Id.ToString();
            }
        }
    }
}
