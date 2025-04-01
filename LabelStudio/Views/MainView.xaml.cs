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
using System.Windows.Shapes;
using HandyControl.Controls;
using LabelStudio.Controls.DrawControlLibrary;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LabelStudio.Utils;

namespace LabelStudio.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView 
    {
        public MainView()
        {
            InitializeComponent();
            color_picker.SelectedColorChanged += delegate { this.drawCanvas.Brush = color_picker.SelectedBrush; btn_color.IsChecked = false; };
            color_picker.Canceled += delegate { btn_color.IsChecked = false; };

           this.toolBars.AddHandler(RadioButton.CheckedEvent, new RoutedEventHandler(OnDrawToolChecked));
        }

        //绘图
        private void OnDrawToolChecked(Object sender, RoutedEventArgs e)
        {
            if (e.Source is RadioButton btn && btn.Tag is String typeStr)
                drawCanvas.DrawingToolType = (DrawToolType)Enum.Parse(typeof(DrawToolType), typeStr);
        }

        //清除
        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            drawCanvas.Clear();
        }

        //保存XML
        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            if (this.drawCanvas.GetDrawGeometries().Count() == 0)
                return;

            var folder = System.IO.Path.Combine(Environment.CurrentDirectory, "Draws");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory("Draws");

            var dlg = new SaveFileDialog
            {
                Filter = "XML files (*.xml)|*.xml",
                OverwritePrompt = true,
                DefaultExt = "xml",
                InitialDirectory = folder,
                RestoreDirectory = true
            };

            if ((Boolean)dlg.ShowDialog())
                this.drawCanvas.Save(dlg.FileName);
        }

        //打开XML
        private void OnOpenClick(object sender, RoutedEventArgs e)
        {
            var folder = System.IO.Path.Combine(Environment.CurrentDirectory, "Draws");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory("Draws");

            var dlg = new OpenFileDialog
            {
                Filter = "XML files (*.xml)|*.xml",
                DefaultExt = "xml",
                InitialDirectory = folder,
                RestoreDirectory = true
            };

            if ((Boolean)dlg.ShowDialog())
                this.drawCanvas.Load(dlg.FileName);
        }

        //打印
        private void OnPrintClick(object sender, RoutedEventArgs e)
        {
            var backgroundImage = this.drawViewer.BackgroundImage;

            this.drawCanvas.Print(backgroundImage.PixelWidth, backgroundImage.PixelHeight, DpiHelper.GetDpiFromVisual(this.drawCanvas), backgroundImage);
        }

        //保存图片
        private void OnSaveImageClick(object sender, RoutedEventArgs e)
        {
            var backgroundImage = this.drawViewer.BackgroundImage;

            var frame = this.drawCanvas.ToBitmapFrame(backgroundImage.PixelWidth, backgroundImage.PixelHeight, DpiHelper.GetDpiFromVisual(this.drawCanvas), backgroundImage);

            if (frame == null)
                return;

            var folder = System.IO.Path.Combine(Environment.CurrentDirectory, "Images");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory("Images");

            var dlg = new SaveFileDialog
            {
                Filter = "Images files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp",
                OverwritePrompt = true,
                DefaultExt = "jpg",
                InitialDirectory = folder,
                RestoreDirectory = true
            };

            if ((Boolean)dlg.ShowDialog())
                ImageHelper.Save(dlg.FileName, frame);
        }


        //打开单张照片
        private void OnOpenPicClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "图片文件|*.jpg;*.jpeg;*.png;*.bmp|所有文件|*.*",
                Title = "选择图片"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                LoadImage(openFileDialog.FileName);
            }
        }

        /// <summary>
        /// 打开单张照片
        /// </summary>
        /// <param name="filePath"></param>
        private void LoadImage(string filePath)
        {
            try
            {
                // 使用 BitmapImage 加载图片
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(filePath);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;  // 立即加载
                bitmap.EndInit();

                drawViewer.BackgroundImage = bitmap;
       
            }
            catch
            {
                //MessageBox.Show("图片加载失败", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
