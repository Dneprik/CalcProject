using Calc.ViewModel;
using Xamarin.Forms;

namespace Calc
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new CalcViewModel();
        }
    }
}