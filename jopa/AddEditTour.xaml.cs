using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace jopa
{
    /// <summary>
    /// Логика взаимодействия для AddEditTour.xaml
    /// </summary>
    public partial class AddEditTour : Window
    {
        private Hotel _currentHotel = new Hotel();
        public AddEditTour(Hotel selectedHotel)
        {
            InitializeComponent();
            this.MinHeight = 528.4;
            this.MinWidth = 800;
            if (selectedHotel != null)
                _currentHotel = selectedHotel;
            DataContext = _currentHotel;
            ComboCountries.ItemsSource = TCEntities.GetContext().Country.ToList();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow hotels = new MainWindow();
            hotels.Show();
            this.Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentHotel.Name))
                errors.AppendLine("Укажите название отеля");
            if (_currentHotel.CountOfStars < 1 || _currentHotel.CountOfStars > 5)
                errors.AppendLine("Укажите количество звёзд - число от 1 до 5");
            if (_currentHotel.Country == null)
                errors.AppendLine("Выберите страну");
            if (_currentHotel.Discription == null)
                errors.AppendLine("Укажите описание");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentHotel.Id == 0)
                TCEntities.GetContext().Hotel.Add(_currentHotel);
            try
            {
                TCEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void BtnBack_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow hotels = new MainWindow();
            hotels.Show();
            this.Close();
        }
    }
}
