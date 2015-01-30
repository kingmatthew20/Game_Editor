using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Game_Editor
{
    class Character
    {
        public string knight_id;
        internal void SetImage(string name, MainWindow main)
        {
            //Load the character image and set it to the xaml image
            ImageSource image = new BitmapImage(new Uri(name, UriKind.Relative));
            main.knight.Source = image;
        }
        internal string changeColour(MainWindow main)
        {
            //Set the knight id for saving and pass the file path of the 
            if (main.Blue.IsSelected == true)
            {
                string filename = @"..\..\resource\singular_knight_blue.png";
                SetImage(filename, main);
                knight_id = filename;
                return knight_id;
            }
            if (main.Green.IsSelected == true)
            {
                string filename = @"..\..\resource\singular_knight_green.png";
                SetImage(filename, main);
                knight_id = filename;
                return knight_id;
            }
            if (main.Red.IsSelected == true)
            {
                string filename = @"..\..\resource\singular_knight_red.png";
                SetImage(filename, main);
                knight_id = filename;
                return knight_id;
            }
            if (main.Grey.IsSelected == true)
            {
                string filename = @"..\..\resource\singular_knight.png";
                SetImage(filename, main);
                knight_id = filename;
                return knight_id;
            }
            return knight_id;
        }
        internal bool CheckChar()
        {
            if (knight_id == null)
            {
                MessageBox.Show("Please choose a character colour");
                return false;
            }
            return true;
        }
        internal bool IsTextNumeric(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9]");
            return reg.IsMatch(str);
        }
    }
}
