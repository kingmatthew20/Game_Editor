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
using System.Xml;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using System.Windows.Controls.Primitives;
using System.Text.RegularExpressions;

#region Everything
namespace Game_Editor
{
    public partial class MainWindow : Window
    {
        Instructions wind;
        Character chara = new Character();
        Map map = new Map();

        public bool TilesLoaded = false;
        public int gridWidth;
        const int tileSize = 17;
        int[,] TileArray;

        ObservableCollection<Image> panelImages = new ObservableCollection<Image>();

        public MainWindow()
        {            
            InitializeComponent();
            //Create command bindings for keyboard shortcut
            CommandBindings.Add(new CommandBinding(ApplicationCommands.New, new ExecutedRoutedEventHandler(FileNew)));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, new ExecutedRoutedEventHandler(MapLoad)));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, new ExecutedRoutedEventHandler(MapSave)));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Replace, new ExecutedRoutedEventHandler(menuLoadTiles)));
            MapTiles.ItemsSource = panelImages;
            
            //Initialise the tile array
            int gridHeight = 20;
            int gridWidth = int.Parse(txtGridWidth.Text);

            TileArray = new int[gridWidth, gridHeight];
            TileArray.Initialize();

            onStart();
        }
        private void onStart()
        {
            wind = new Instructions();
            wind.Show();
            wind.Focus();
        }
        private void Instructions(object sender, RoutedEventArgs e)
        {
            wind = new Instructions();
            wind.Show();
        }

        #region Map
        private void isWidth(object sender, TextCompositionEventArgs e)
        {
            e.Handled = chara.IsTextNumeric(e.Text);
            if(txtGridWidth.Text.Length != 0)
            {
                int width = int.Parse(txtGridWidth.Text);
                if (width > 200)
                {
                    txtGridWidth.Text = "200";
                }
            }
            else 
            {
            }
       
            txtGridWidth.MaxLength = 3;
        }
        private void menuLoadTiles(object sender, RoutedEventArgs e)
        {
            map.createTilesList(this);
            //Reset the tile array to the size of the grid 
            TilesLoaded = true;
            int gridHeight = 20;
            gridWidth = int.Parse(txtGridWidth.Text);

            if (gridWidth > 200)
            {
                txtGridWidth.Text = "200";
                gridWidth = 200;
            }  
            
            TileArray = new int[gridWidth, gridHeight];
            //Set the size of the canvas and the grid according to the size of the tile width 
            Mapping.Width = gridWidth * tileSize;
            Mapping.Height = gridHeight * tileSize;

            for (int i = 0; i < gridHeight; i++)
            {
                gridMapTiles.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(tileSize) });
            }
            for (int i = 0; i < gridWidth; i++)
            {
                gridMapTiles.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(tileSize) });
            }
            for (int x = 0; x < gridWidth; x++)
            {
                for(int y = 0; y < gridHeight; y++)
                {
                    DataGrid grd = new DataGrid();
                    Grid.SetColumn(grd, x);
                    Grid.SetRow(grd, y);
                    gridMapTiles.Children.Add(grd);                    
                }
            }
        }
        void Draw(int x, int y)
        {    
            //Create a new image to hold the tile
            Image draw = new Image();

            if (MapTiles.SelectedItem == null)
            {
                MessageBox.Show("Please Select A Tile");
            }
            else
            {
                if (x >= gridWidth)
                {
                }
                else
                {
                    //Give the x and y to the tile array and add the tile id
                    TileArray[x, y] = MapTiles.SelectedIndex;
                    draw.Source = ((Image)MapTiles.SelectedItem).Source;
                }
            }

            //Set the tile to the canvas
            Canvas.SetLeft(draw, x * tileSize);
            Canvas.SetTop(draw, y * tileSize);
            Mapping.Children.Add(draw);
        }
        #endregion
   
        #region Mouse
        private void Image_MouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            //Check if the mouse left button is down
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Image_MouseMove(sender, e);  
            }
        }
        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            //Get the mouse position in reference to the canvas
            Point point = e.GetPosition(Mapping);
            //divide the position by the tilesize so the x and y are inside a grid square 
            int x = (int)point.X / tileSize; 
            int y = (int)point.Y / tileSize;
            //Recheck if the mouse button is down so you can "Paint" on the canvas
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                Draw(x, y);
            }
            
        }
        #endregion

        #region Background
        private void pickBackground(string name)
        {
            //Create an image and set it the the xaml image
            ImageSource image = new BitmapImage(new Uri(name));
            background.Source = image;
        }
        private void loadImage(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            string filename = "";
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = "."; // default extenion 
            dlg.Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpeg|JPG Files (*.jpg)|*.jpg|All Files (*.*)|*.*";

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                // Open document
                filename = dlg.FileName;
                pickBackground(filename);
            }
        }
        private void SaveImage(object sender, RoutedEventArgs e)
        {
            //Set the file name and path of the image for the user so the image is saved in the correct place
            string filename = @"..\..\resource\background.png";
            SaveTo(background, filename);
            
        }
        private void SaveTo(FrameworkElement visual, string fileName)
        {
            //Get a new png encoder for the image
            var encoder = new PngBitmapEncoder();
            SaveUsingEncoder(visual, fileName, encoder);
        }

        private void SaveUsingEncoder(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        {
            //Set the size and data setting of the new PNG image
            RenderTargetBitmap bitmap = new RenderTargetBitmap(
                (int)visual.ActualWidth,
                (int)visual.ActualHeight,
                96,
                96,
                PixelFormats.Pbgra32);
            bitmap.Render(visual);
            BitmapFrame frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);
            using (var stream = File.Create(fileName))
            {
                encoder.Save(stream);
            }
            MessageBox.Show("Your image has successfully been saved!");
        }
        #endregion

        #region Character
        private void isText(object sender, TextCompositionEventArgs e)
        {
            e.Handled = chara.IsTextNumeric(e.Text);
            TLives.MaxLength = 3;  
        }
        private bool Check()
        {
            if(TLives.Text.Length == 0)
            {
                MessageBox.Show("Please enter a number of lives");
                return false;
            }
            return true;
        }
        private void textBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Copy ||
                e.Command == ApplicationCommands.Cut ||
                e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }
        private void callColour(object sender, RoutedEventArgs e)
        {
            chara.changeColour(this);
        }
        #endregion

        #region Load
        private void MapLoad(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            string filename = "";
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".xml"; // default extenion 
            dlg.Filter = "Map Files XML (.xml)|*.xml|Map Files (*.txt)|*.txt";

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                // Open document
                filename = dlg.FileName;
                if(TilesLoaded == true)
                {
                    LoadMap(filename);
                }
                else
                {
                    MessageBox.Show("Please Load Tiles First");
                }
            }
        }
        private void LoadMap(string filename)
        {
            //Create a new document 
            XDocument doc = new XDocument();
            doc = XDocument.Load(filename);
            //Find the root element 
            XElement Root = doc.Root;

            //Get and set map width
            XElement Width = Root.Element("Width");
            int width = Convert.ToInt32(Width.Value);
            gridWidth = width;
            map.resetMapSize(this);
            //Iterate through each element 
            foreach (XElement Ele in Root.Descendants("Tile"))
            {
                //Get the x, y and ID
                int x = Convert.ToInt32((int)Ele.Attribute("X"));
                int y = Convert.ToInt32((int)Ele.Attribute("Y"));
                int id = Convert.ToInt32((int)Ele.Attribute("ID"));
                //Create a new image to hold the tile
                Image toLoad = new Image();
                //Get the tile according to the id
                toLoad.Source = ((Image)MapTiles.Items.GetItemAt(id)).Source;
                //Set the x and y of the tile array 
                TileArray[x, y] = id;
                //Add the image to the canvas
                Canvas.SetLeft(toLoad, x * tileSize);
                Canvas.SetTop(toLoad, y * tileSize);
                Mapping.Children.Add(toLoad);
            }
        }
        private void CharLoad(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            string filename = "";
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".xml"; // default extenion 
            dlg.Filter = "Character Files XML (.xml)|*.xml|Map Files (*.txt)|*.txt";

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                // Open document
                filename = dlg.FileName;
                LoadChar(filename);
            }
        }
        private void LoadChar(string filename)
        {
            XmlDocument dox = new XmlDocument();
            chara = new Character();
            dox.Load(filename);

            chara.knight_id = dox.SelectSingleNode("Mary-JordanMagee/Player/Colour").InnerText;
            TLives.Text = dox.SelectSingleNode("Mary-JordanMagee/Player/Lives").InnerText;
            Run.Text = dox.SelectSingleNode("Mary-JordanMagee/Player/Speed").InnerText;
            Jump.Text = dox.SelectSingleNode("Mary-JordanMagee/Player/Jump").InnerText;
            chara.SetImage(chara.knight_id, this);
        }
        private void EnemiesLoad(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            string filename = "";
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".xml"; // default extenion 
            dlg.Filter = "Character Files XML (.xml)|*.xml|Map Files (*.txt)|*.txt";

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                // Open document
                filename = dlg.FileName;
                LoadEnemies(filename);
            }
        }
        private void LoadEnemies(string filename)
        {
            XmlDocument dox = new XmlDocument();
            dox.Load(filename);

            WeakCount.Text = dox.SelectSingleNode("Mary-JordanMagee/WeakEnemy/Count").InnerText;
            WeakFeq.Value = int.Parse(dox.SelectSingleNode("Mary-JordanMagee/WeakEnemy/Frequency").InnerText);

            MedCount.Text = dox.SelectSingleNode("Mary-JordanMagee/MedEnemy/Count").InnerText;
            MedFeq.Value = Convert.ToInt32(dox.SelectSingleNode("Mary-JordanMagee/MedEnemy/Frequency").InnerText);

            ToughCount.Text = dox.SelectSingleNode("Mary-JordanMagee/ToughEnemy/Count").InnerText;
            HighFeq.Value = Convert.ToInt32(dox.SelectSingleNode("Mary-JordanMagee/ToughEnemy/Frequency").InnerText);
        }
        #endregion 

        #region Save
        private void MapSave(object sender, RoutedEventArgs e)
        {
            string filename = "";

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Map";
            dlg.DefaultExt = ".xml";
            dlg.Filter = "Map Files XML (.xml)|*.xml|Map Files (*.txt)|*.txt";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                filename = dlg.FileName;
                xMapSave(filename);
            }
        }
        private void CharSave(object sender, RoutedEventArgs e)
        {
            if (Check() == true)
            {
                if (chara.CheckChar() == true)
                {
                    string filename = "";

                    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                    dlg.FileName = "Character";
                    dlg.DefaultExt = ".xml";
                    dlg.Filter = "Character Files XML (.xml)|*.xml|Character Files (*.txt)|*.txt";

                    Nullable<bool> result = dlg.ShowDialog();

                    if (result == true)
                    {
                        filename = dlg.FileName;
                        xCharSave(filename);
                    }
                }
            }
        }
        private void xCharSave(string filename)
        {
            //Create a new document
            XDocument doc = new XDocument();
            //Add a root node
            XElement Root = new XElement("Mary-JordanMagee");
            //Add a new section
            XElement Ele = new XElement("Player",
                new XElement("Colour", chara.knight_id),
                new XElement("Lives", TLives.Text),
                new XElement("Speed", Run.Text),
                new XElement("Jump", Jump.Text));
            Root.Add(Ele);
            doc.Add(Root);
            // Save the document to a file
            doc.Save(filename);
        }
        private void xMapSave(string filename)
        {
            //Create new XML document and give it a root node
            XDocument doc = new XDocument();
            XElement Root = new XElement("Mary-JordanMagee");
            XElement newEle;

            int gridHeight = 20;
            int gridWidth = int.Parse(txtGridWidth.Text);

            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    //iterate through the canvas grid and give the x and y to the tile array  
                    int TileID = TileArray[x, y];
                    //add a new element for each square of the canvas and save the tileID
                    newEle = new XElement("Tile");
                    newEle.SetAttributeValue("X", x);
                    newEle.SetAttributeValue("Y", y);
                    newEle.SetAttributeValue("ID", TileID);
                    Root.Add(newEle);
                }
            }
            XElement Width = new XElement("Width", gridWidth);
            Root.Add(Width);
            doc.Add(Root);
            doc.Save(filename);
        }
        private void EnemiesSave(object sender, RoutedEventArgs e)
        {
            string filename = "";

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Enemies";
            dlg.DefaultExt = ".xml";
            dlg.Filter = "Enemies Files XML (.xml)|*.xml|Enemies Files (*.txt)|*.txt";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                filename = dlg.FileName;
                xEnemiesSave(filename);
            }
        }
        private void xEnemiesSave(string filename)
        {
            //Create a new document
            XDocument doc = new XDocument();
            //Add a root node
            XElement Root = new XElement("Mary-JordanMagee");
            //Add a new section

            XElement WeakEle = new XElement("WeakEnemy",
                new XElement("Count", WeakCount.Text),
                new XElement("Frequency", Low.Text));
            Root.Add(WeakEle);
            XElement MedEle = new XElement("MedEnemy",
                new XElement("Count", MedCount.Text),
                new XElement("Frequency", Med.Text));
            Root.Add(MedEle);
            XElement ToughEle = new XElement("ToughEnemy",
                new XElement("Count", ToughCount.Text),
                new XElement("Frequency", High.Text));
            Root.Add(ToughEle);
            doc.Add(Root);
            // Save the document to a file
            doc.Save(filename);
        }
        #endregion

        #region Exit & New
        private void FileExit(object sender, RoutedEventArgs e)
        {
            wind.close();
            Application.Current.Shutdown();
        }
        private void FileNew(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("This will reset everything!", "Are You Sure?", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                mainWindow.Top = this.Top;
                mainWindow.Left = this.Left;
                wind.close();
                this.Close();
            }
            else if (result == MessageBoxResult.Cancel)
            {
                return;
            }
        }
        private void catch_Close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            wind.close();
            e.Cancel = false;
        }
        #endregion
    }
}
#endregion