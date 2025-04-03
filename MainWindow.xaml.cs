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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TFOMS_testv1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //сразу запускаем страницу просмотра таблиц, когда заходим в приложение
        public MainWindow()
        {
            InitializeComponent();
            PageNavigate.Navigate(new Pages.Show());
        }

        private void NavigateShowTFOMS(object sender, RoutedEventArgs e)
        {
            PageNavigate.Navigate(new Pages.Show());
        }

        private void NabigateFoundTFOMS(object sender, RoutedEventArgs e)
        {
            PageNavigate.Navigate(new Pages.Found());
        }

        private void NavigateEditTFOMS(object sender, RoutedEventArgs e)
        {
            PageNavigate.Navigate(new Pages.EditPeople());
        }
    }
}
