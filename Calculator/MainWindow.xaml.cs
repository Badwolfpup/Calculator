using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public enum Räknesätt
    {
        Plus, Minus, Gånger, Delat
    }
    public partial class MainWindow : Window
    {
        string tal1, tal2;
        double resultat;
        bool vilkettal = true;
        bool nyBeräkning = true;
        bool harKommatal1 = false;
        bool harKommatal2 = false;
        Räknesätt räknesätt = new Räknesätt();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !string.IsNullOrEmpty(tal2))
            {
                if (!vilkettal)
                {
                    CaluclateResult();
                    return;
                }
            }
            else if (e.Key == Key.Back)
            {
                if (nyBeräkning)
                {
                    ResultTextBlock.Text = "";
                    nyBeräkning = !nyBeräkning;
                }
                else
                {
                    if (vilkettal)
                    {
                        tal1 = tal1.Remove(tal1.Length - 1);
                        ResultTextBlock.Text = ResultTextBlock.Text.Remove(ResultTextBlock.Text.Length - 1);
                    }
                    else
                    {
                        tal2 = tal2.Remove(tal2.Length - 1);
                        ResultTextBlock.Text = ResultTextBlock.Text.Remove(ResultTextBlock.Text.Length - 1);
                    }
                }
                
            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                if (nyBeräkning)
                {
                    ResultTextBlock.Text = "";
                    nyBeräkning = !nyBeräkning;
                }
                if (vilkettal) { tal1 += e.Key.ToString().Substring(6); ResultTextBlock.Text += e.Key.ToString().Substring(6); }
                else { tal2 += e.Key.ToString().Substring(6); ResultTextBlock.Text += e.Key.ToString().Substring(6); }
            } 
            else if(e.Key == Key.Decimal)
            {
                if (!nyBeräkning)
                {
                    if (vilkettal && !harKommatal1) { tal1 += ","; harKommatal1 = !harKommatal1; ResultTextBlock.Text += ","; }
                    else if (!vilkettal && !harKommatal2) { tal2 += ","; harKommatal2 = !harKommatal2; ResultTextBlock.Text += ","; }
                }
            }
            else if (e.Key == Key.Add || e.Key == Key.Subtract || e.Key == Key.Multiply || e.Key == Key.Divide)
            {
                if (!nyBeräkning)
                {
                    if (vilkettal)
                    {
                        switch (e.Key.ToString())
                        {
                            case "Add":
                                räknesätt = Räknesätt.Plus;
                                ResultTextBlock.Text += "+";
                                vilkettal = false;
                                break;
                            case "Subtract":
                                räknesätt = Räknesätt.Minus;
                                vilkettal = false;
                                ResultTextBlock.Text += "-";
                                break;
                            case "Multiply":
                                räknesätt = Räknesätt.Gånger;
                                vilkettal = false;
                                ResultTextBlock.Text += "*";
                                break;
                            case "Divide":
                                räknesätt = Räknesätt.Delat;
                                vilkettal = false;
                                ResultTextBlock.Text += "/";
                                break;
                        }
                    }
                }
            }
            
        }

        public void CaluclateResult()
        {
            double tal3, tal4;
            double.TryParse(tal1, out tal3 );
            double.TryParse(tal2, out tal4 );
            switch (räknesätt)
            {
                case Räknesätt.Plus:
                    resultat = tal3 + tal4;
                    break;
                case Räknesätt.Minus:
                    resultat = tal3 - tal4;
                    break;
                case Räknesätt.Gånger:
                    resultat = tal3 * tal4;
                    break;
                case Räknesätt.Delat:
                    resultat = tal3 / tal4;
                    break;
            }

            ResultTextBlock.Text = resultat.ToString();


            vilkettal = !vilkettal;
            tal1 = "";
            tal2 = "";
            nyBeräkning = !nyBeräkning;
            if (harKommatal1) harKommatal1 = !harKommatal1;
            if (harKommatal2) harKommatal2 = !harKommatal2;
        }
    }
}