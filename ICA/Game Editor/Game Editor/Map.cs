using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Game_Editor
{
    class Map
    {
        public bool TilesLoaded = false;
        public int gridWidth;

        const int rows = 17;
        const int cols = 37;
        const int tileSize = 17;

        int[,] TileArray;

        ObservableCollection<Image> panelImages = new ObservableCollection<Image>();
        ObservableCollection<Image> mapImages = new ObservableCollection<Image>();
        internal void createTilesList(MainWindow main)
        {
            TilesLoaded = true;
            //Iterate throught the tile list and set each tile too a rectangle
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    panelImages.Add(new Image()
                    {
                        Source = new CroppedBitmap(new BitmapImage(new Uri(@"..\..\resource\Mario_Tiles.png", UriKind.Relative)),
                            new Int32Rect(x * tileSize, y * tileSize, tileSize, tileSize)),
                        Height = tileSize
                    });
                }
            }
            //Then add it to the tile list 
            main.MapTiles.ItemsSource = new ObservableCollection<Image>(panelImages);
        }
        internal void resetMapSize(MainWindow main)
        {
            //Reset the grid
            main.gridMapTiles.Children.Clear();
            main.gridMapTiles.RowDefinitions.Clear();
            main.gridMapTiles.ColumnDefinitions.Clear();

            int gridHeight = 20;
            TileArray = new int[gridWidth, gridHeight];

            //Then set the size of the canvas and the grid according to the size of the tile width that has been taken from the XML
            main.Mapping.Width = gridWidth * tileSize;
            main.Mapping.Height = gridHeight * tileSize;

            for (int i = 0; i < gridHeight; i++)
            {
                main.gridMapTiles.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(tileSize) });
            }
            for (int i = 0; i < gridWidth; i++)
            {
                main.gridMapTiles.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(tileSize) });
            }
            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    DataGrid grd = new DataGrid();
                    Grid.SetColumn(grd, x);
                    Grid.SetRow(grd, y);
                    main.gridMapTiles.Children.Add(grd);
                }
            }
        }
    }
}
