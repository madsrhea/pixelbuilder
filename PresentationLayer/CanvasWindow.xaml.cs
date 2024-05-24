using DataObjects;
using LogicLayer;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for CanvasWindow.xaml
    /// </summary>
    public partial class CanvasWindow : Window
    {

        private User _user = null;
        private List<Line> linesX = new List<Line>();
        private List<Line> linesY = new List<Line>();
        private int pixels = 8;
        private Color currentColor;
        SolidColorBrush brush = new SolidColorBrush();
        private Point currentPoint = new Point();
        private BeadManager _beadManager;

        public CanvasWindow()
        {
            InitializeComponent();
        }

        public CanvasWindow(User user)
        {
            _user = user;
        }

        /// CREATE CANVAS GRID
        public void CreateGrid()
        {
            int pixels = 8;
            var color = Brushes.DarkSlateGray;

            for (int i = 0; i <= (canvasMain.Width / pixels); i++)
            {
                linesX.Add(new Line());                
                linesX[i].Stroke = color;
                linesX[i].X1 = 0;
                linesX[i].X2 = canvasMain.Width;
                linesX[i].Y1 = i * pixels;
                linesX[i].Y2 = i * pixels; 
                linesX[i].StrokeThickness = 0.5;
                canvasMain.Children.Add(linesX[i]);

                linesY.Add(new Line());
                linesY[i].Y1 = 0;
                linesY[i].Y2 = canvasMain.Height;
                linesY[i].X1 = i * pixels;
                linesY[i].X2 = i * pixels;
                linesY[i].Stroke = color;
                linesY[i].StrokeThickness = 0.5;
                canvasMain.Children.Add(linesY[i]);
            }

        }

        private void btnGrid_Checked(object sender, RoutedEventArgs e)
        {
            // show grid stuff here
           CreateGrid();
        }

        private void btnGrid_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var line in linesX)
            {
                canvasMain.Children.Remove(line);
            }
            foreach (var line in linesY)
            {
                canvasMain.Children.Remove(line);
            }
        }




        private void scrollColors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Bead b = (Bead)scrollColors.SelectedItem;
            GetHexCode(b);
        }

        private void canvasMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                currentPoint = e.GetPosition(canvasMain);
            }

        }

        private void scrollColors_Loaded(object sender, RoutedEventArgs e)
        {
            _beadManager = new BeadManager();
            var beads = _beadManager.RetrieveAllBeads();

            for(int i = 0; i < beads.Count; i++)
            {
                scrollColors.Items.Add(beads[i]);
            }



        }

       private SolidColorBrush GetHexCode(Bead bead)
        { 
            brush = new SolidColorBrush();

            currentColor = (Color)ColorConverter.ConvertFromString("#" + bead.HexValue);
            brush.Color = currentColor;
           
            rctColor.Fill = brush;
            statbarText.Text = "Current selected color: " + bead.ColorName;

            return brush;
       }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // TODO hook up database to here
            if (txtCanvasName.Text == null || txtCanvasName.Text == "")
            {
                MessageBox.Show("You'll need to name your masterpiece before you save it!", "Uh-oh!", MessageBoxButton.OK);
            }
            else
            {
                SaveImageAsPNG(sender, e);
                MessageBox.Show("Image saved!", "Yippie!", MessageBoxButton.OK);
                this.Close();
            }    
        }

        private void SaveImageAsPNG(object sender, EventArgs e)
        {
            string path = "C:\\Users\\schoo\\source\\repos\\PixelBuilder2\\PresentationLayer\\images\\" + txtCanvasName.Text + ".png";
            FileStream fs = new FileStream(path, FileMode.Create);
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)canvasMain.Width, (int)canvasMain.Height, 1 / 96, 1 / 96, PixelFormats.Pbgra32);
            bmp.Render(canvasMain);
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            encoder.Save(fs);
            fs.Close();

        }

        private void canvasMain_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Line line = new Line();

                line.Stroke = brush;
                line.StrokeThickness = pixels;
                line.X1 = currentPoint.X;
                line.Y1 = currentPoint.Y;
                line.X2 = e.GetPosition(canvasMain).X;
                line.Y2 = e.GetPosition(canvasMain).Y;

                currentPoint = e.GetPosition(canvasMain);

                canvasMain.Children.Add(line);
            }
        }

    }
}
