using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Forms;
using System.IO;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Controls;
namespace PictureViewer
{
    class Picture
    {
        public string Name
        {
            get;
            set;
        }
        public string Path_to_Pic
        {
            get;
            set;
        }
        public double Size
        {
            get;
            set;
        }
    }
}
