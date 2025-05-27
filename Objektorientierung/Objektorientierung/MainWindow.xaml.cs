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

namespace Objektorientierung
{
    class Rechteck
    {
        public double laenge = 1;
        public double breite = 2;

        public double FlaecheBerechnen()
        {
            return laenge * breite;
        }
        public Rechteck(double laenge, double breite)
        { 
            this.laenge = laenge;
            this.breite = breite;
        }
        public override string ToString()
        {
            return $"rechteck: {laenge}x{breite}";
        }
    }

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Rechteck> rechtecke = new List<Rechteck>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSpeichern_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string laengeStr = this.tbxLaenge.Text;
                double laenge = double.Parse(laengeStr);
                string breiteStr = this.tbxBreite.Text;
                double breite = double.Parse(breiteStr);
                if (lstRechtecke.SelectedItem != null)
                {
                    Rechteck r = (Rechteck)this.lstRechtecke.SelectedItem;
                    r.laenge = laenge;
                    r.breite = breite;
                    //nächstes mal weiter
                    List<Rechteck> temp = rechtecke;
                    lstRechtecke.Items.Refresh();
                }
                else
                {
                    Rechteck r = new Rechteck(laenge, breite);
                    lstRechtecke.Items.Add(r);
                    rechtecke.Add(r);
                }
                tbxLaenge.Clear();
                tbxBreite.Clear();
            }
            catch(FormatException)
            {
                MessageBox.Show("Ungültige Eingabe!");
            }
        }

        private void lstRechtecke_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Rechteck r = (Rechteck)this.lstRechtecke.SelectedItem;
            try
            {
                tbxLaenge.Text = r.laenge.ToString();
                tbxBreite.Text = r.breite.ToString();
            }
            catch (NullReferenceException) {}
        }
    }
}
