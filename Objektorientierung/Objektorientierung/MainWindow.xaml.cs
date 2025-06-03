using System;
using System.Collections.Generic;
using System.IO;
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

        public double position_x = 0;
        public double position_y = 0;

        public double FlaecheBerechnen()
        {
            return laenge * breite;
        }
        public Rechteck(double laenge, double breite, double position_x, double position_y)
        { 
            this.laenge = laenge;
            this.breite = breite;
            this.position_x = position_x;
            this.position_y = position_y;
        }
        public override string ToString()
        {
            return $"rechteck: {laenge}x{breite} ({position_x}|{position_y})";
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

            Button button = new Button();
            button.Width = 100;
            button.Content = "Click mich";
            //myCanvas.Children.Add(button);

            StreamReader reader = new StreamReader("wallsList.txt");
            string wallsList = reader.ReadToEnd();
            string[] walls = wallsList.Split('\n');
            for (int i = 0; i < walls.Length; i++)
            {
                int x = (int.Parse(walls[i].Split(',')[0]))*10;
                int y = (int.Parse(walls[i].Split(',')[1]))*10;

                Rechteck r = new Rechteck(10, 10, x, y);
                rechtecke.Add(r);
                lstRechtecke.Items.Add(r);

            }
            

        }

        private void btnSpeichern_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string laengeStr = this.tbxLaenge.Text;
                double laenge = double.Parse(laengeStr);
                string breiteStr = this.tbxBreite.Text;
                double breite = double.Parse(breiteStr);
                string position_xStr = this.tbxPosx.Text;
                double position_x = double.Parse(position_xStr);
                string position_yStr = this.tbxPosy.Text;
                double position_y = double.Parse(position_yStr);
                if (lstRechtecke.SelectedItem != null)
                {
                    Rechteck r = (Rechteck)this.lstRechtecke.SelectedItem;
                    r.laenge = laenge;
                    r.breite = breite;
                    lstRechtecke.Items.Refresh();
                }
                else
                {
                    Rechteck r = new Rechteck(laenge, breite, position_x, position_y);
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

        private void Zeichnen_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < rechtecke.Count; i++)
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Width = rechtecke[i].laenge;
                rectangle.Height = rechtecke[i].breite;
                rectangle.StrokeThickness = 2;
                rectangle.Stroke = Brushes.Black;
                Canvas.SetTop(rectangle, rechtecke[i].position_x);
                Canvas.SetLeft(rectangle, rechtecke[i].position_y); //hier nächstes mal weiter programmieren (Positionierung funktioniert nicht)
                myCanvas.Children.Add(rectangle);
            }
        }

        private void Loeschen_Click(object sender, RoutedEventArgs e)
        {
            myCanvas.Children.Clear();
        }
    }
}
