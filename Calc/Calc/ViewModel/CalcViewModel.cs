using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Calc.Model;
using Xamarin.Forms;

namespace Calc.ViewModel
{
    internal class CalcViewModel : INotifyPropertyChanged
    {
        private readonly CalcModel calc;
        //For FontSize=55. Isn't implemented now.
        private readonly int oneDigitWidth = 35;
        private readonly double widthOfCalcDisplay;
        private bool isFirestStep = true;
        private string newNumber;
        private char newSign;
        private char oldSign;
        private double result;

        public CalcViewModel(double widthOfCalcDisplay = 0)
        {
            calc = new CalcModel();
            DigitButtonClickedCommand = new Command(DigitButtonClicked);
            FuncButtonClickedCommand = new Command(FuncButtonClicked);
            this.widthOfCalcDisplay = widthOfCalcDisplay;
            displayValue = "0";
        }

        //Comands for keys
        public ICommand DigitButtonClickedCommand { get; protected set; }
        public ICommand FuncButtonClickedCommand { get; protected set; }

        public string displayValue
        {
            get { return calc.DisplayValue; }
            set
            {
                if (calc.DisplayValue != value)
                {
                    calc.DisplayValue = value;
                    OnPropertyChanged("displayValue");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void DigitButtonClicked(object obj)
        {
            newNumber = obj.ToString();
            if (isFirestStep)
            {
                displayValue = newNumber;
                isFirestStep = false;
            }
            else if (IsCanDisplayNumber() && IsFirstDot(calc.DisplayValue + newNumber))
            {
                displayValue += newNumber;
            }
        }

        private bool IsCanDisplayNumber()
        {
            return widthOfCalcDisplay*oneDigitWidth < 1000;
        }

        private void FuncButtonClicked(object obj)
        {
            newSign = obj.ToString().First();
            if (oldSign == '\0')
            {
                oldSign = newSign;
                result = Convert.ToDouble(displayValue);
            }
            else
                switch (oldSign)
                {
                    case '+':
                        result = result + Convert.ToDouble(displayValue);
                        break;
                    case '-':
                        result = result - Convert.ToDouble(displayValue);
                        break;
                    case '*':
                        result = result*Convert.ToDouble(displayValue);
                        break;
                    default:
                        break;
                }
            displayValue = result.ToString();
            oldSign = newSign;
            isFirestStep = true;
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        //Method for verifying count of dot in the string
        private bool IsFirstDot(string str)
        {
            var countDot = str.ToCharArray().Count(c => c == '.');
            if (countDot <= 1) return true;
            return false;
        }
    }
}