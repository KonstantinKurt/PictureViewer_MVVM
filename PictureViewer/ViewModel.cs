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
    class ViewModel : ViewModelBase
    {
        List<string> filter = new List<string>() { @"bmp", @"jpg", @"gif", @"png", @"ico", @"jpeg" };
        public ObservableCollection<Picture> Images
        {
            get;
            private set;
        }
        public ViewModel()
        {
            Images = new ObservableCollection<Picture>();
            
        }
        
        #region Commands
        ICommand _get_images;
        public ICommand Get_images
        {
            get
            {
                return _get_images ?? (_get_images = new RelayCommand(Load_files));
            }
        }

        #endregion
        void Load_files()
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Images.Clear();
                foreach (string file in Directory.GetFiles(dialog.SelectedPath))
                {
                    if (filter.Exists(n => n == file.Split(new char[] { '.' }).Last().ToLower()))
                    {
                        try
                        {
                            var fileinfo = new FileInfo(file);
                            Images.Add(new Picture {Name = fileinfo.Name, Path_to_Pic = fileinfo.FullName,Size =fileinfo.Length/1000});
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                   
            }
        }
    }
}
