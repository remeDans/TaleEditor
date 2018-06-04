using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TestingEditor;

namespace TaleEditor
{
    /// <summary>
    /// Lógica de interacción para Window2.xaml
    /// </summary>
    public partial class wndPreview : Window
    {
        List<RowDefinition> rows0;
        List<RowDefinition> rows1;
        List<ColumnDefinition> cols0;
        List<ColumnDefinition> column;
        List<Border> borderImage;
        List<Label> label;
        List<Grid> grid;
        List<Image> image;
        Double height;
        Double width;

        wndMain wndMain2;
        TaleManager taleManager;

        int tamColums;
        int tamPic;
        int heightLabel;
        int fontSize;

        public wndPreview(TaleManager taleManager)
        {
            InitializeComponent();
            #region Inicializar listas

            this.taleManager = taleManager;

            image = new List<Image>();
            image.Add(imgPic1);
            image.Add(imgPic2);
            image.Add(imgPic3);
            image.Add(imgPic4);
            image.Add(imgPic5);
            image.Add(imgPic6);
            image.Add(imgPic7);
            image.Add(imgPic8);
            image.Add(imgPic9);
            image.Add(imgPic10);


            grid = new List<Grid>();
            grid.Add(grdPic1);
            grid.Add(grdPic2);
            grid.Add(grdPic3);
            grid.Add(grdPic4);
            grid.Add(grdPic5);
            grid.Add(grdPic6);
            grid.Add(grdPic7);
            grid.Add(grdPic8);
            grid.Add(grdPic9);
            grid.Add(grdPic10);

            column = new List<ColumnDefinition>();
            //columns.Add(col0);
            column.Add(col2);
            column.Add(col4);
            column.Add(col6);
            column.Add(col8);
            //columns.Add(col10);

            label = new List<Label>();
            label.Add(lblWordPic1);
            label.Add(lblWordPic2);
            label.Add(lblWordPic3);
            label.Add(lblWordPic4);
            label.Add(lblWordPic5);
            label.Add(lblWordPic6);
            label.Add(lblWordPic7);
            label.Add(lblWordPic8);
            label.Add(lblWordPic9);
            label.Add(lblWordPic10);

            rows0 = new List<RowDefinition>();
            rows0.Add(Pic1Row0);
            rows0.Add(Pic2Row0);
            rows0.Add(Pic3Row0);
            rows0.Add(Pic4Row0);
            rows0.Add(Pic5Row0);
            rows0.Add(Pic6Row0);
            rows0.Add(Pic7Row0);
            rows0.Add(Pic8Row0);
            rows0.Add(Pic9Row0);
            rows0.Add(Pic10Row0);

            rows1 = new List<RowDefinition>();
            rows1.Add(Pic1Row1);
            rows1.Add(Pic2Row1);
            rows1.Add(Pic3Row1);
            rows1.Add(Pic4Row1);
            rows1.Add(Pic5Row1);
            rows1.Add(Pic6Row1);
            rows1.Add(Pic7Row1);
            rows1.Add(Pic8Row1);
            rows1.Add(Pic9Row1);
            rows1.Add(Pic10Row1);

            cols0 = new List<ColumnDefinition>();
            cols0.Add(Pic1Col0);
            cols0.Add(Pic2Col0);
            cols0.Add(Pic3Col0);
            cols0.Add(Pic4Col0);
            cols0.Add(Pic5Col0);
            cols0.Add(Pic6Col0);
            cols0.Add(Pic7Col0);
            cols0.Add(Pic8Col0);
            cols0.Add(Pic9Col0);
            cols0.Add(Pic10Col0);

            borderImage = new List<Border>();
            borderImage.Add(brdPic1);
            borderImage.Add(brdPic2);
            borderImage.Add(brdPic3);
            borderImage.Add(brdPic4);
            borderImage.Add(brdPic5);
            borderImage.Add(brdPic6);
            borderImage.Add(brdPic7);
            borderImage.Add(brdPic8);
            borderImage.Add(brdPic9);
            borderImage.Add(brdPic10);

            #endregion Inicializar listas
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetResolution();
            Resize();

            wndMain2 = Application.Current.Windows.OfType<wndMain>().FirstOrDefault();

            if(taleManager.CurrentPageIndex == -1)
            {
                UpdateFrontPage();
            }
            else
            {
                UpdatePage();
            }

        }


        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        #region funciones

        public void UpdatePage()
        {
            //-- Background
            TestingEditor.Page auxPage = taleManager.CurrentPage;


            if (auxPage.Background != null)
            {
                String background = auxPage.Background;
                int tamBackground = background.Length;
                if (tamBackground > 0)
                {
                    String p = background.Substring(tamBackground - 4);
                    if (p.Contains("."))
                    {
                        String pathBackgroundPage = auxPage.Background;
                        imgBackgroundPage.Source = Utils.GetBitmapFromUri(pathBackgroundPage,UriKind.Absolute);
                        imgBackgroundPage.Stretch = Stretch.UniformToFill;
                        imgBackgroundPage.HorizontalAlignment = HorizontalAlignment.Center;
                        imgBackgroundPage.VerticalAlignment = VerticalAlignment.Center;
                    }

                    if (background.Contains("#"))
                    {
                        var bc = new BrushConverter();
                        grdPage.Background = (Brush)bc.ConvertFrom(background);
                    }

                }
            }


            //-- PICTOGRAMAS ----
            for (int i = 0; i < 10; i++)
            {
                label[i].Content = "";
                image[i].Source = null;
            }


            foreach (Pictogram pictogram in auxPage.Pictograms)
            {
                int i = pictogram.Index;
                label[i].Content = pictogram.TextToRead;

                String path = pictogram.ImageName;
                image[i].Source = Utils.GetBitmapFromUri(path, UriKind.Absolute);

                EditBorder(borderImage[i], pictogram.getColorByType(pictogram.Type), 6);
            }
            

            for (int i = 0; i < 10; i++)
            {
                if (label[i].Content == null || image[i].Source == null)
                {
                    grid[i].Visibility = Visibility.Hidden;
                }
            }

        }


        public void UpdateFrontPage()
        {
            //----------------------------------
            int a = 0;
            foreach (Grid g in grid)
            {
                grid[a].Visibility = Visibility.Hidden;
                a++;
            }
            //------------------------------------

            lblPage.Content = taleManager.Title;

            //-- Background
            String background = taleManager.Background;
            int tamBackground = background.Length;

            if (background!="")
            {
                String p = background.Substring(tamBackground - 4);
                if (p.Contains("."))
                {
                    String pathBackgroundPage = taleManager.Background;
                    imgBackgroundPage.Source = Utils.GetBitmapFromUri(pathBackgroundPage, UriKind.Absolute);
                    imgBackgroundPage.Stretch = Stretch.UniformToFill;
                    imgBackgroundPage.HorizontalAlignment = HorizontalAlignment.Center;
                    imgBackgroundPage.VerticalAlignment = VerticalAlignment.Center;
                }

                if (background.Contains("#"))
                {
                    var bc = new BrushConverter();
                    grdPage.Background = (Brush)bc.ConvertFrom(background);
                }
            }
        }


        public void EditBorder(Border border, Brush color, int borderSize)
        {
            border.BorderBrush = color;
            border.BorderThickness = new Thickness(borderSize, borderSize, borderSize, borderSize);
            border.VerticalAlignment = VerticalAlignment.Center;
            border.HorizontalAlignment = HorizontalAlignment.Center;
        }

        private void GetResolution()
        {

            height = SystemParameters.VirtualScreenHeight;
            width = SystemParameters.VirtualScreenWidth;

            if (height >= 600 && height< 720){
                tamPic = 80;
                heightLabel = 20;
                fontSize = 12;
                tamColums = 20;
            }else if (height >= 720 && height < 1024){
                tamPic = 160;
                heightLabel = 25;
                fontSize = 20;
                tamColums = 28;
                row2.Height = new GridLength(80);
            }else{
                lblPage.FontSize = 34;
                tamPic = 190;
                heightLabel = 30;
                fontSize = 25;
                tamColums = 40;
            }
        }

        private void Resize()
        {
            int i = 0;
            int j = 0;
            int k = 0;
            int u = 0;
            int v = 0;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            foreach (RowDefinition r in rows0)
            {
                rows0[i].Height = new GridLength(tamPic);
                rows1[i].Height = new GridLength(heightLabel);
                i++;
            }

            foreach (ColumnDefinition c in cols0)
            {
                cols0[j].Width = new GridLength(tamPic);

                j++;
            }

            foreach (ColumnDefinition c in column)
            {
                column[v].MaxWidth = tamColums;
                v++;
            }

            foreach (Border b in borderImage)
            {
                borderImage[k].Height = tamPic;
                borderImage[k].Width = tamPic;
                k++;
            }

            foreach (Label l in label)
            {
                label[u].FontSize = fontSize;
                label[u].Height = heightLabel;
                label[u].FontWeight = FontWeights.Bold;
                label[u].Width = tamPic;
                u++;
            }
        }

    }
    #endregion funciones
}
