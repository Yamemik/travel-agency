using Microsoft.Extensions.Primitives;
using System.Windows;

namespace TravelAgency.Windows.contracts
{
    public partial class AboutContratWindow : Window
    {
        public AboutContratWindow(Contract? contract)
        {
            InitializeComponent();

            if(contract is null)
            {
                
            }
            else
            {
                txt_title.Text = "Договор № " + contract.Id.ToString();

                // data
                txt_date.Text = contract.DateOfConclusion.ToString();
                txt_tour.Text = contract.TourId.ToString();
                if(contract.Tour is null)
                {
                    var ddd = GetTour(contract.TourId);
                    if(ddd is not null)
                    {
                        txt_date_start.Text = ddd.StartDate.ToString();
                        txt_date_finish.Text = ddd.FinishDate.ToString();
                        txt_country.Text = GetCountry(ddd.CountryId);
                        txt_hotel.Text = GetHotel(ddd.HotelId);
                        txt_cost.Text = ddd.CompanyServiceCost.ToString();
                    }
                }
                else
                {
                    txt_date_start.Text = contract.Tour.StartDate.ToString();
                    txt_date_finish.Text = contract.Tour.FinishDate.ToString();
                    txt_country.Text = GetCountry(contract.Tour.CountryId);
                    txt_hotel.Text = GetHotel(contract.Tour.HotelId);
                    txt_cost.Text = contract.Tour.CompanyServiceCost.ToString();
                }

                // table
                DGridCustomer.ItemsSource = contract.Customers.ToList();

            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {

        }

        private string GetCountry(int id)
        {
            using (TravelDBContext db = new())
            {
                var country = db.Countries.FirstOrDefault(c => id == c.Id);

                return country is null ? "" : country.Name;
            }

        }

        private string GetHotel(int id) 
        {
            using (TravelDBContext db = new())
            {
                var hotel = db.Hotels.FirstOrDefault(c => id == c.Id);

                return hotel is null ? "" : hotel.Name;
            }

        }

        private Tour  GetTour(int id) 
        {
            using (TravelDBContext db = new())
            {
                var tour = db.Tours.FirstOrDefault(c => id == c.Id);

                return tour;
            }

        }
    }
}
