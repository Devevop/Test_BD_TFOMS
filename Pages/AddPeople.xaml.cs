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

namespace TFOMS_testv1.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddPeople.xaml
    /// </summary>
    public partial class AddPeople : Page
    {
        public DateTime dateDr;
        List<T007> t007=new List<T007>();
        bool selectDate, selectDatex,selectDatet;
        public AddPeople()
        {
            InitializeComponent();
            foreach(var item in DB.context.T007.ToList())
            {
                t007.Add(item);
            }
        }


        private void Enp(object sender, TextCompositionEventArgs e)
        {
            int value;
            if (!Int32.TryParse(e.Text, out value))
            {
                e.Handled = true;
            }
        }
        private void Space(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void AddPeopleItem(object sender, RoutedEventArgs e)
        {
            try
            {
                if(MessageBox.Show("Вы точно хотите добавить новую запись?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    PEOPLE people = new PEOPLE();
                    if (enp.Text == "")
                    {
                        MessageBox.Show("ПОЛИС обязательное поле для заполнения");
                    }else if (enp.Text.Length < 16)
                    {
                        MessageBox.Show("ПОЛИС состоит из 16 цифр");
                    }
                    else people.enp = enp.Text;
                    if (fam.Text == "")
                    {
                        MessageBox.Show("Фамилия обязательное поле для заполнения");
                    }
                    else people.fam = fam.Text;
                    if (enp.Text == "")
                    {
                        MessageBox.Show("Имя обязательное поле для заполнения");
                    }
                    else people.im= im.Text;
                    people.ot = ot.Text;
                    if (mP.IsChecked == true)
                    {
                        if (fP.IsChecked == true)
                        {
                            MessageBox.Show("Ошибка! Можно выбрать только один пол");
                        }
                        else people.w = 1;
                    }
                    else if (fP.IsChecked == true)
                    {
                        if (mP.IsChecked == true)
                        {
                            MessageBox.Show("Ошибка! Можно выбрать только один пол");
                        }
                        else people.w = 2;
                    }
                    else MessageBox.Show("Пол обязательное поле для заполнения");
                    if (!selectDate)
                    {
                        MessageBox.Show("Дата рождения обязательное поле для заполнения");
                    }
                    else people.dr = dr.SelectedDate.Value;
                    people.lpu = lpu.Text;
                    if (!selectDatex)
                    {
                        people.lpudx = null;
                    }else people.lpudx =lpudx.SelectedDate.Value;
                    if(!selectDatet)
                    {
                        people.lpudt = null;
                    }else people.lpudt =lpudt.SelectedDate.Value;
                    people.lpuuch = lpuuch.Text;
                    HISTLPU hISTLPU = new HISTLPU();
                    hISTLPU.p_id = people.p_id;
                    hISTLPU.lpu = people.lpu;
                    hISTLPU.lpudt = people.lpudt;
                    hISTLPU.lpudx= people.lpudx;
                    hISTLPU.district = people.lpuuch;
                    var r= from t7 in t007
                           where t7.depth == hISTLPU.district && hISTLPU.lpu == t7.code_mo
                           select new { otvet=t7.nom_podr};
                    string prikol="";
                    foreach(var item in r.ToList())
                    {
                        hISTLPU.subdir = item.otvet;
                    }
                    DB.context.PEOPLE.Add(people);
                    DB.context.HISTLPU.Add(hISTLPU);
                    DB.context.SaveChanges();
                    this.NavigationService.Navigate(new Pages.EditPeople());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка. Проверьте все ли поля заполнены и соответствуют типам");
            }
        }
        //проверка выбрал ли пользователь дату
        private void DateDrSelectedDate(object sender, SelectionChangedEventArgs e)
        {
            selectDate = true;
        }

        private void DateLputSelectedDate(object sender, SelectionChangedEventArgs e)
        {
            selectDatet=true;
        }

        private void DateLpuxSelectedDate(object sender, SelectionChangedEventArgs e)
        {
            selectDatex = true;
        }
    }
}
