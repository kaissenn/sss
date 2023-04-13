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
    /// Логика взаимодействия для Tours.xaml
    /// </summary>
    public partial class Tours : Window
    {
        public Tours()
        {
            InitializeComponent();
            var allTypes = TCEntities.GetContext().Type.ToList();
            allTypes.Insert(0, new Type { Name = "Все типы" });
            ChecActual.IsChecked = true;
            ComboType.ItemsSource = allTypes;
            ComboType.SelectedIndex = 0;
            ComboPrice.SelectedIndex = 0;
            UpdateTours();
            this.MinHeight = 624.418;
            this.MinWidth = 800;
        }

        private void BtnHotels_Click(object sender, RoutedEventArgs e)
        {
            MainWindow hotels = new MainWindow();
            hotels.Show();
            this.Close();
        }

        private void ComboPrice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTours();
        }

        private void TboxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTours();
        }
        private void UpdateTours()
        {
            var currentTours = TCEntities.GetContext().Tour.ToList();

            if (ComboType.SelectedIndex > 0)
                currentTours = currentTours.Where(p => p.Type.Contains(ComboType.SelectedItem as Type)).ToList();

            currentTours = currentTours.Where(p => p.Name.ToLower().Contains(TboxSearch.Text.ToLower())).ToList();

            if (ChecActual.IsChecked.Value)
                currentTours = currentTours.Where(p => p.IsActual).ToList();

            if (ComboPrice.SelectedIndex == 1)
                currentTours = currentTours.OrderBy(p => p.Price).ToList();
            if (ComboPrice.SelectedIndex == 2)
                currentTours = currentTours.OrderByDescending(p => p.Price).ToList();
            if (ComboPrice.SelectedIndex == 0)
                ListViewTours.ItemsSource = currentTours;

            TotalTourPrice.Text = "Общая стоимость туров: \n" + currentTours.Sum(p => p.Price * p.TicketCount).ToString();

            ListViewTours.ItemsSource = currentTours;
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTours();
        }

        private void ChecActual_Unchecked(object sender, RoutedEventArgs e)
        {
           
        }

        private void ChecActual_Checked(object sender, RoutedEventArgs e)
        {
            UpdateTours();
        }
    }
}
