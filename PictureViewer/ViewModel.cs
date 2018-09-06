using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        string _big_picture;
        int _get_selected;
        public string Big_picture
        {
            get
            {
                return _big_picture;
            }
            set
            {
                _big_picture = value;
                RaisePropertyChanged(() => Big_picture);
            }
        }
        public int Get_selected
        {
            get
            {
                return _get_selected;
            }
            set
            {
                _get_selected = value;
                RaisePropertyChanged(() => Get_selected);
            }
        }
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
        ICommand _get_big_image;
        ICommand _slide_show;
        ICommand _next;
        ICommand _prev;
        public ICommand Prev
        {
            get
            {
                return _prev ?? (_prev = new RelayCommand(() =>
                {
                    int temp = Images.Count - 1;
                    if (Get_selected > 0)
                    {
                        Get_selected -= 1;
                        Big_picture = Images[Get_selected].Path_to_Pic;
                    }
                    else
                    {
                        Get_selected = temp;
                        Big_picture = Images[Get_selected].Path_to_Pic;
                    }

                }));
            }
        }
        public ICommand Next
        {
            get
            {
                return _next ?? (_next = new RelayCommand(() =>
                {
                    int temp = Images.Count - 1;
                    if (Get_selected < temp)
                    {
                        Get_selected += 1;
                        Big_picture = Images[Get_selected].Path_to_Pic;
                    }
                    else 
                    {
                        Get_selected = 0;
                        Big_picture = Images[Get_selected].Path_to_Pic;
                    }

                }));
            }
        }
        //public ICommand Slide_show
        //{
        //    get
        //    {
        //        return _slide_show ?? (_slide_show = new RelayCommand(() =>
        //        {
        //            do
        //            {
        //                Big_picture = Images[start].Path_to_Pic;
        //                Thread.Sleep(1000);
        //                start++;
        //            }
        //            while (start < Images.Count);
        //        }));
        //    }
        //}

        public ICommand Get_big_image
        {
            get
            {
                return _get_big_image ?? (_get_big_image = new RelayCommand(() =>
                {
                   Big_picture = Images[Get_selected].Path_to_Pic;
                }));
            }
        }
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
