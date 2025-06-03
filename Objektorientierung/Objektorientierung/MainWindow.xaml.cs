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

    class Spieler
    {
        public int x;
        public int y;
        public Image image;

        public Spieler()
        {
            x = 1;
            y = 1;
        }
        public void Move(Key key)
        {
            if (key == Key.Left) x--;
            else if (key == Key.Right) x++;
            else if (key == Key.Up) y--;
            else if (key == Key.Down) y++;
            Canvas.SetLeft(image, x * MainWindow.GRID_SIZE);
            Canvas.SetTop(image, y * MainWindow.GRID_SIZE);
        }
    }

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int GRID_SIZE = 20;
        List<Rechteck> rechtecke = new List<Rechteck>();
        Spieler spieler = new Spieler();

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
                int x = (int.Parse(walls[i].Split(',')[0]))*GRID_SIZE;
                int y = (int.Parse(walls[i].Split(',')[1]))*GRID_SIZE;

                Rechteck r = new Rechteck(GRID_SIZE, GRID_SIZE, x, y);
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
                Canvas.SetLeft(rectangle, rechtecke[i].position_y);
                myCanvas.Children.Add(rectangle);
            }
            spieler.image = new Image();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("spieler.jpeg", UriKind.Relative);
            bitmap.EndInit();
            spieler.image.Source = bitmap;
            spieler.image.Width = GRID_SIZE;
            spieler.image.Height = GRID_SIZE;
            Canvas.SetTop(spieler.image, spieler.y*GRID_SIZE);
            Canvas.SetLeft(spieler.image, spieler.x*GRID_SIZE);
            myCanvas.Children.Add(spieler.image);
        }

        private void Loeschen_Click(object sender, RoutedEventArgs e)
        {
            myCanvas.Children.Clear();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            spieler.Move(e.Key);
        }

        private void Starten_Click(object sender, RoutedEventArgs e)
        {
            if(stp_SideBar.Visibility == Visibility.Collapsed) stp_SideBar.Visibility = Visibility.Visible;
            else stp_SideBar.Visibility = Visibility.Collapsed;
        }
    }
}
