using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Calc
{
    public partial class MainPage : ContentPage
    {

        double result = 0;
        bool isFirestStep = true;
        string newNumber;
        char oldSign;
        char newSign;
        public MainPage()
        {
            InitializeComponent();
        }

        void DigitButtonClicked(object sender, EventArgs args)
        {
            newNumber = (sender as Button).Text;
            if (isFirestStep)
            { resultLabel.Text = (sender as Button).Text; isFirestStep = false; }
            else
                if ((resultLabel.Text.Length * 35 < resultLabel.Width) && isFirstDot(resultLabel.Text + newNumber))
            {
                resultLabel.Text += (sender as Button).Text;
            }
        }

        void FuncButtonClicked(object sender, EventArgs args)
        {
            newSign = (sender as Button).Text.ToCharArray().First();
            if (oldSign == '\0')
            {
                oldSign = newSign;
                result = Convert.ToDouble(resultLabel.Text);
            }
            else
                switch (oldSign)
                {
                    case '+': result = result + Convert.ToDouble(resultLabel.Text); break;
                    case '-': result = result - Convert.ToDouble(resultLabel.Text); break;
                    case '*': result = result * Convert.ToDouble(resultLabel.Text); break;
                    default: break;
                }
            resultLabel.Text = result.ToString();
            oldSign = newSign;
            isFirestStep = true;
        }

        //Method for verifying count of dot in the string
        bool isFirstDot(string str)
        {
            int countDot = str.ToCharArray().Count(c => c == '.');
            if (countDot <= 1) return true;
            else return false;
        }
    }
}
