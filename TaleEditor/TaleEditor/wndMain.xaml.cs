using System;
using System.Windows;
using TestingEditor;
using TPage = TestingEditor.Page;
using System.Windows.Input;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Controls;
using Microsoft.Win32;
using System.IO;
using System.IO.Compression;
using System.Speech.Synthesis;
using System.Xml;
using System.Web;
using System.Windows.Media.Imaging;
using System.Security.AccessControl;

namespace TaleEditor
{
    /// <summary>
    /// Lógica de interacción para wndMain2.xaml x
    /// </summary>
    public partial class wndMain : Window
    {
        public TaleManager taleManager;

        MediaPlayer mediaPlayerTale;
        DispatcherTimer timerMusic;
        List<Word> words;
        DBManager dbManager;
        DateTime timer;
        DateTime timer2;
        String pathSave;

        Boolean isImportTale;
        Boolean isChangedTale;
        Boolean isNewTale;
        Boolean isOpenTale;
        Boolean isSaveTale;

        int[] colorPer;

        List<String> imagesPictogramas;
        //LogStore logStore;

        Boolean hasMusicFrontPage;
        Boolean hasMusicPage;
        Boolean hasSoundPictogram;


        public PromptBuilder builder;
        PromptStyle style;
        Speak speak;

        Boolean isImageCut;
        Boolean isImageSquare;

        Boolean isPlayingFrontPage;
        Boolean isPlayingPage;
        Boolean isPlayingPic;

        String locationOpen;
        //está en appData //Roaming
        String locationSaveImport;

        String nameArchiveImport;
        Boolean hasImgExist;

        String locationChooseImage;
        String locationOpenImport;

        Boolean hasSentence;

        #region brdPagePicto

        List<Image> images;
        List<Label> labelsWord;
        List<Label> labelsPic;
        List<Label> labelsNumber;
        List<Border> borders;
        List<Grid> grid;
        List<TextBox> textBoxesTextToRead;

        #endregion brdPagePicto

        public wndMain()
        {
            InitializeComponent();
            words = new List<Word>();
            imagesPictogramas = new List<String>();
            dbManager = new DBManager();

            mediaPlayerTale = new MediaPlayer();
            timerMusic = new DispatcherTimer();
            timerMusic.Interval = TimeSpan.FromSeconds(1);
            timerMusic.Tick += timer_Tick;

            builder = new PromptBuilder();
            style = new PromptStyle();
            style.Rate = PromptRate.Slow;
            style.Volume = PromptVolume.ExtraLoud;
            style.Emphasis = PromptEmphasis.Strong;

            mediaPlayerTale.MediaEnded += MediaPlayerTale_MediaEnded;

            isImportTale = false;
            isChangedTale = false;
            isNewTale = false;
            isOpenTale = false;
            isSaveTale = false;

            pathSave = "";

            hasMusicPage = false;
            hasMusicFrontPage = false;
            hasSoundPictogram = false;

            isImageCut = false;
            isImageSquare = false;

            isPlayingFrontPage = false;
            isPlayingPage = false;
            isPlayingPic = false;

            locationSaveImport = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Tale Editor" + "\\imports";
            locationOpen = "";

            nameArchiveImport = "";

            hasImgExist = false;

            locationChooseImage = "";
            locationOpenImport = "";

            hasSentence = false;

            #region brdPagePictoIni

            textBoxesTextToRead = new List<TextBox>();
            textBoxesTextToRead.Add(txtTextToRead1);
            textBoxesTextToRead.Add(txtTextToRead2);
            textBoxesTextToRead.Add(txtTextToRead3);
            textBoxesTextToRead.Add(txtTextToRead4);
            textBoxesTextToRead.Add(txtTextToRead5);
            textBoxesTextToRead.Add(txtTextToRead6);
            textBoxesTextToRead.Add(txtTextToRead7);
            textBoxesTextToRead.Add(txtTextToRead8);
            textBoxesTextToRead.Add(txtTextToRead9);
            textBoxesTextToRead.Add(txtTextToRead10);

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



            labelsWord = new List<Label>();
            labelsWord.Add(lblWordPic1);
            labelsWord.Add(lblWordPic2);
            labelsWord.Add(lblWordPic3);
            labelsWord.Add(lblWordPic4);
            labelsWord.Add(lblWordPic5);
            labelsWord.Add(lblWordPic6);
            labelsWord.Add(lblWordPic7);
            labelsWord.Add(lblWordPic8);
            labelsWord.Add(lblWordPic9);
            labelsWord.Add(lblWordPic10);

            images = new List<Image>();
            images.Add(imgPic1);
            images.Add(imgPic2);
            images.Add(imgPic3);
            images.Add(imgPic4);
            images.Add(imgPic5);
            images.Add(imgPic6);
            images.Add(imgPic7);
            images.Add(imgPic8);
            images.Add(imgPic9);
            images.Add(imgPic10);

            labelsPic = new List<Label>();
            labelsPic.Add(lblPic1);
            labelsPic.Add(lblPic2);
            labelsPic.Add(lblPic3);
            labelsPic.Add(lblPic4);
            labelsPic.Add(lblPic5);
            labelsPic.Add(lblPic6);
            labelsPic.Add(lblPic7);
            labelsPic.Add(lblPic8);
            labelsPic.Add(lblPic9);
            labelsPic.Add(lblPic10);

            labelsNumber = new List<Label>();
            labelsNumber.Add(lblNumberPicto1);
            labelsNumber.Add(lblNumberPicto2);
            labelsNumber.Add(lblNumberPicto3);
            labelsNumber.Add(lblNumberPicto4);
            labelsNumber.Add(lblNumberPicto5);
            labelsNumber.Add(lblNumberPicto6);
            labelsNumber.Add(lblNumberPicto7);
            labelsNumber.Add(lblNumberPicto8);
            labelsNumber.Add(lblNumberPicto9);
            labelsNumber.Add(lblNumberPicto10);

            borders = new List<Border>();
            borders.Add(brdPic1);
            borders.Add(brdPic2);
            borders.Add(brdPic3);
            borders.Add(brdPic4);
            borders.Add(brdPic5);
            borders.Add(brdPic6);
            borders.Add(brdPic7);
            borders.Add(brdPic8);
            borders.Add(brdPic9);
            borders.Add(brdPic10);

            #endregion brdPagePictoIni

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ShowStartInterface();


        }


        #region eventos

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {



        }

        private void Window_StateChanged(object sender, EventArgs e)
        {


        }

        #region navigation

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (taleManager.GoToNextPage())
            {
                UpdateGUI();
                ShowMenuGUI();
            }
        }

        private void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (taleManager.GoToPreviousPage())
            {
                UpdateGUI();
                ShowMenuGUI();
            }
        }

        private void btnGoToFrontPage_Click(object sender, RoutedEventArgs e)
        {
            taleManager.GoToFrontPage();
            ShowMenuGUI();
            UpdateGUI();
        }

        private void btnGoToEndPage_Click(object sender, RoutedEventArgs e)
        {
            taleManager.GoToEndPage();
            UpdateGUI();
            ShowMenuGUI();
        }

        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            wndPreview wndPreview = new wndPreview(taleManager);
            wndPreview.WindowStyle = WindowStyle.None;
            wndPreview.WindowState = WindowState.Maximized;
            wndPreview.Show();
        }

        #endregion navigation

        #region menu
        private void menuShowHelp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("Iconos/manual.pdf");

                /*System.Diagnostics.Process proc = new System.Diagnostics.Process();
                Uri path = new Uri("Iconos\\manual.pdf", UriKind.Relative);
                proc.StartInfo.FileName = "Iconos/manual.pdf";
                proc.Start();
                proc.Close();*/

                


            }
            catch
            {
                MessageBox.Show("No se puede abrir el archivo.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void menuAbout_Click(object sender, RoutedEventArgs e)
        {
            wndAbout wnd = new wndAbout();
            wnd.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            wnd.ShowDialog();
        }
        #endregion menu

        #region quickMenu
        private void menuCloseTale_Click(object sender, RoutedEventArgs e)
        {

            StopMusic();

            if (taleManager == null)
            {
                CloseTale();
            }
            else
            {
                if (isChangedTale)
                {
                    MessageBoxResult result = MessageBox.Show("¿Desea guardar el cuento actual antes de cerrarlo?", "Tale Editor", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        SaveTale();
                        CloseTale();
                        OpenFileTale();
                    }
                    if (result == MessageBoxResult.No)
                    {
                        CloseTale();
                    }
                }
                else
                {
                    CloseTale();
                }
            }

        }
        private void menuOpenTale_Click(object sender, RoutedEventArgs e)
        {
            StopMusic();

            if (taleManager == null)
            {
                OpenFileTale();
            }
            else
            {
                if (isChangedTale)
                {
                    MessageBoxResult result = MessageBox.Show("¿Desea guardar el cuento actual antes de abrir otro?", "Tale Editor", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        SaveTale();
                        CloseTale();
                        OpenFileTale();
                    }
                    if (result == MessageBoxResult.No)
                    {
                        OpenFileTale();
                    }
                }
                else
                {
                    OpenFileTale();
                }
            }
        }

        private void menuNewTale_Click(object sender, RoutedEventArgs e)
        {
            StopMusic();
            if (taleManager == null)
            {
                NewTale();
            }
            else
            {
                if (isChangedTale)
                {
                    MessageBoxResult result = MessageBox.Show("¿Desea guardar cambios en el cuento actual antes de crear uno nuevo?", "Tale Editor", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        SaveTale();
                        CloseTale();
                        NewTale();
                    }
                    if (result == MessageBoxResult.No)
                    {
                        NewTale();
                    }
                }
                else
                {
                    NewTale();
                }
            }
        }

        private void btnAddPage_Click(object sender, RoutedEventArgs e)
        {
            bool result = false;
            result = taleManager.InsertPage(new TPage(taleManager.CurrentPageIndex + 1));

            if (result == true)
            {
                taleManager.GoToNextPage();
                UpdateGUI();
            }
        }

        private void btnDeletePage_Click(object sender, RoutedEventArgs e)
        {
            if (taleManager.IsPage() && !taleManager.IsFrontPage())
            {
                MessageBoxResult result = MessageBox.Show("Estás apunto de borrar la página " + (taleManager.CurrentPageIndex + 1) + ", si la borras no podrás recuperarla, ¿Estás seguro de querer querer hacerlo?", "¿Eliminar la página " + (taleManager.CurrentPageIndex + 1) + "? ", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    taleManager.RemovePage(taleManager.CurrentPage.Index);
                    taleManager.GoToPreviousPage();
                    UpdateGUI();
                    ShowMenuGUI();
                    isChangedTale = true;
                }
            }
            else
            {
                ShowMenuGUI();
            }
        }

        private void btnSaveTale_Click(object sender, RoutedEventArgs e)
        {
            SaveTale();
        }

        private void menuImportTale_Click(object sender, RoutedEventArgs e)
        {
            String location = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Tale Editor\\";

            Utils.CreateDirectory(locationSaveImport);

            OpenFileDialog file = new OpenFileDialog();
            file.Title = "Abrir cuento";
            file.Filter = "Tale Tale|*.tale";
            file.InitialDirectory = location;
            file.RestoreDirectory = true;

            if (file.ShowDialog() == true)
            {
                String path = file.FileName;
                String nameArchive = file.SafeFileName;

                int tam = nameArchive.Length - 5;//obtnego el nombre del archivo sin la extension
                nameArchive = nameArchive.Substring(0, tam);
                nameArchiveImport = nameArchive;
                locationOpenImport = file.FileName;
                String nameDirectory = locationSaveImport + "\\" + nameArchive;
                String archive = nameDirectory + ".zip";
                Utils.DeleteDirectory(nameDirectory);
                //File.Delete(archive);
                String archiveCopy = nameDirectory + "copy";
                File.Copy(path, archive);

                ZipFile.ExtractToDirectory(path, nameDirectory);

                //ZipFile.ExtractToDirectory(path, Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+"\\"+nameArchiveImport);//hago una copia para poder moverla 

                File.Delete(archive);//elimino el zip

                LogStore lgImport = new LogStore(nameDirectory + "\\index.xml");
                if (lgImport != null)
                {
                    taleManager = new TaleManager();
                    taleManager = lgImport.GetTale();

                    ChangeToAbsolutePath(nameDirectory);

                    isImportTale = true;
                    isNewTale = false;
                    isOpenTale = false;

                    UpdateGUI();
                    ShowTaleGUI();
                    ShowMenuGUI();
                }

                lgImport.closeXML();

                isChangedTale = false;
            }
        }

        private void menuExportTale_Click(object sender, RoutedEventArgs e)
        {
            if (taleManager != null)
            {

                //Pregunto si quieres guardar antes de exportarlo
                if(!isSaveTale)
                {
                    MessageBoxResult result2 = MessageBox.Show("¿Desea guarda el cuento actual antes de salir?", "Tale Editor", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                    if (result2 == MessageBoxResult.Yes)
                    {
                        SaveTale();
                    }
                }

                String pathSaveExport = "";
                String location = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Tale Editor\\";
                XmlDocument docExport = null;
                LogStore lgExport = new LogStore();

                Utils.CreateDirectory(location);

                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.InitialDirectory = location;
                saveFile.Filter = "Tale Tale|*.tale";
                saveFile.Title = "Exportar cuento";
                saveFile.FileName = "nuevoCuento";
                saveFile.RestoreDirectory = true;

                Nullable<bool> result = saveFile.ShowDialog();

                if (result == true)
                {

                    if (saveFile.FileName != "")
                    {
                        try
                        {
                            pathSaveExport = saveFile.FileName;
                            String name = saveFile.SafeFileName;

                            int tam = pathSaveExport.Length - 5;
                            String nameDirectory = pathSaveExport.Substring(0, tam);
                            string zipPath = pathSaveExport;

                            CopyArchives(nameDirectory);
                            ChangeNameFiles();

                            docExport = lgExport.SaveTaleManagerXML(taleManager, nameDirectory, "index.xml");

                            CompressFile(location, nameDirectory, zipPath, name);

                            Utils.DeleteDirectory(nameDirectory);

                            CloseTale();
                        }
                        catch (Exception)
                        { }

                        lgExport.closeXML();

                        isChangedTale = false;
                    }
                }
            }


        }

        private void menuSaveAsTale_Click(object sender, RoutedEventArgs e)
        {
            String location = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Tale Editor\\";
            Utils.CreateDirectory(location);
            LogStore lgSaveAs = new LogStore();
            XmlDocument docSaveAs = null;

            if (taleManager != null)
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.InitialDirectory = location;
                saveFile.Filter = "Tproject Proyecto Tale|*.tproject";
                saveFile.Title = "Guardar como...";
                saveFile.FileName = "nuevoCuento-Copia";
                saveFile.RestoreDirectory = true;

                Nullable<bool> result = saveFile.ShowDialog();

                if (result == true)
                {
                    if (saveFile.FileName != "")
                    {
                        try
                        {
                            String pathSaveAs = saveFile.FileName;
                            docSaveAs = lgSaveAs.SaveTaleManagerXML(taleManager);

                            using (var writer = new StreamWriter(pathSaveAs))
                            {
                                docSaveAs.Save(writer);
                            }

                        }
                        catch (Exception)
                        { }

                        lgSaveAs.closeXML();

                        isChangedTale = false;
                        isSaveTale = true;
                    }
                }
            }
        }
        #endregion quickMenu

        #region TitlePanel

        #region title
        private void txtTitle_MouseLeave(object sender, MouseEventArgs e)
        {
            txtFrontPage.Text = txtTitle.Text;
            taleManager.Title = txtTitle.Text;
        }

        private void txtTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                txtFrontPage.Text = txtTitle.Text;
                taleManager.Title = txtTitle.Text;
                isChangedTale = true;
            }
        }

        private void txtTitle_KeyUp(object sender, KeyEventArgs e)
        {
            txtFrontPage.Text = txtTitle.Text;
            taleManager.Title = txtTitle.Text;
            lblStatusBarTitle.Content = "Cuento: " + txtTitle.Text;
            isChangedTale = true;
        }
        #endregion title

        #region language
        private void txtLanguage_MouseLeave(object sender, MouseEventArgs e)
        {
            taleManager.Language = txtLanguage.Text;
            isChangedTale = true;
        }


        private void txtLanguage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                taleManager.Language = txtLanguage.Text;
                isChangedTale = true;
            }
        }
        #endregion language

        #region author
        private void txtAuthor_MouseLeave(object sender, MouseEventArgs e)
        {
            taleManager.Author = txtAuthor.Text;
            isChangedTale = true;
        }

        private void txtAuthor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                taleManager.Author = txtAuthor.Text;
                isChangedTale = true;
            }

        }

        #endregion author

        #region url
        private void txtUrl_MouseLeave(object sender, MouseEventArgs e)
        {
            taleManager.Url = txtUrl.Text;
            isChangedTale = true;
        }

        private void txtUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                taleManager.Url = txtUrl.Text;
                isChangedTale = true;
            }
        }
        #endregion url

        #region license
        private void txtLicense_MouseLeave(object sender, MouseEventArgs e)
        {
            taleManager.License = txtLicense.Text;
            isChangedTale = true;
        }

        private void txtLicense_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                taleManager.License = txtLicense.Text;
                isChangedTale = true;
            }

        }
        #endregion license

        #region DateOfCreation
        private void txtDateOfCreation_MouseLeave(object sender, MouseEventArgs e)
        {
            taleManager.DateOfCreation = DateTime.Now;
            isChangedTale = true;
        }

        private void txtDateOfCreation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                taleManager.DateOfCreation = DateTime.Now;
                isChangedTale = true;
            }

        }
        #endregion DateOfCreation

        #region background

        private void rdbImageFrontPage_Checked(object sender, RoutedEventArgs e)
        {
            btnBackgroundColorFrontPage.IsEnabled = false;
            btnBackgroundImageFrontPage.IsEnabled = true;
        }

        private void rdbColorFrontPage_Checked(object sender, RoutedEventArgs e)
        {
            btnBackgroundColorFrontPage.IsEnabled = true;
            btnBackgroundImageFrontPage.IsEnabled = false;
        }

        private void btnBackgroundImageFrontPage_Click(object sender, RoutedEventArgs e)
        {
            if (rdbImageFrontPage.IsChecked == true)
            {
                String path = ChooseImage(imgFrontPageBackground, txtBackgroundImageFrontPage);
                if (path != "")
                {
                    grdFrontPage.Background = null;
                    txtBackgroundColorFrontPage.Text = "";
                    txtBackgroundImageFrontPage.Foreground = Brushes.Black;
                    taleManager.Background = path;
                    isChangedTale = true;

                }
            }
        }

        private void btnBackgroundColorFrontPage_Click(object sender, RoutedEventArgs e)
        {
            if (rdbColorFrontPage.IsChecked == true)
            {
                Boolean changeColor;
                changeColor = ChooseColor(grdFrontPage, txtBackgroundColorFrontPage);
                if (changeColor == true)
                {
                    imgFrontPageBackground.Source = null;
                    taleManager.Background = grdFrontPage.Background.ToString();
                    txtBackgroundImageFrontPage.Text = "";
                    isChangedTale = true;
                }
            }
        }
        #endregion background

        #region musicTale


        private void btnMusicFrontPage_Click(object sender, RoutedEventArgs e)
        {
            if (hasMusicFrontPage == false)
            {
                string path;
                mediaPlayerTale.Stop();
                path = ChooseMusic(txtMusicFrontPage, mediaPlayerTale);
                if (path != "")
                {
                    btnPlayFrontPage.IsEnabled = true;
                    btnStopFrontPage.IsEnabled = true;

                    txtMusicFrontPage.Foreground = Brushes.Black;
                    taleManager.Music = path;
                    isChangedTale = true;

                    timerMusic.Start();

                    hasMusicFrontPage = true;
                    btnMusicFrontPage.Content = "x";
                }
            }
            else
            {
                btnPlayFrontPage.IsEnabled = false;
                btnStopFrontPage.IsEnabled = false;

                btnMusicFrontPage.Content = "...";
                txtMusicFrontPage.Text = "";
                taleManager.Music = "";
                lblStatusMusic.Content = "";
                timerMusic.Stop();
                mediaPlayerTale.Stop();

                hasMusicFrontPage = false;
                isChangedTale = true;
            }
        }

        private void btnStopFrontPage_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayerTale.Stop();
            imgBtnPlayPauseFrontPage.Source = Utils.GetBitmapFromUri("Iconos\\mplay.png", UriKind.Relative);
        }

        private void btnPlayFrontPage_Click(object sender, RoutedEventArgs e)
        {
            if (!isPlayingFrontPage)
            {
                imgBtnPlayPauseFrontPage.Source = Utils.GetBitmapFromUri("Iconos\\mpause.png", UriKind.Relative);
                mediaPlayerTale.Play();
                timerMusic.Start();
                isPlayingFrontPage = true;
            }
            else
            {
                imgBtnPlayPauseFrontPage.Source = Utils.GetBitmapFromUri("Iconos\\mplay.png", UriKind.Relative);
                mediaPlayerTale.Pause();
                isPlayingFrontPage = false;
            }
        }

        #endregion musicTale

        #endregion TitlePanel

        #region pagePanel

        private void rdbImagePage_Checked(object sender, RoutedEventArgs e)
        {
            btnBackgroundColorPage.IsEnabled = false;
            btnBackgroundImagePage.IsEnabled = true;
        }

        private void rdbColorPage_Checked(object sender, RoutedEventArgs e)
        {
            btnBackgroundColorPage.IsEnabled = true;
            btnBackgroundImagePage.IsEnabled = false;
        }

        private void btnBackgroundImagePage_Click_1(object sender, RoutedEventArgs e)
        {
            if (rdbImagePage.IsChecked == true)
            {
                String path = ChooseImage(imgPageBackground, txtBackgroundImagePage);
                if (path != "")
                {
                    grdPage.Background = null;
                    taleManager.CurrentPage.Background = path;
                    txtBackgroundColorPage.Text = "";
                    txtBackgroundColorPage.Foreground = Brushes.Black;
                    isChangedTale = true;
                }
            }
        }

        private void btnBackgroundColorPage_Click(object sender, RoutedEventArgs e)
        {
            if (rdbColorPage.IsChecked == true)
            {
                Boolean changeColor;
                changeColor = ChooseColor(grdPage, txtBackgroundColorPage);
                if (changeColor == true)
                {
                    imgPageBackground.Source = null;
                    taleManager.CurrentPage.Background = grdPage.Background.ToString();
                    txtBackgroundImagePage.Text = "";
                    isChangedTale = true;
                }
            }
        }

        private void grdPage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            double ellapsedTime = DateTime.Now.Subtract(timer).TotalMilliseconds;
            double ellapsedTime2 = DateTime.Now.Subtract(timer2).TotalMilliseconds;

            if (ellapsedTime > 200 && ellapsedTime2 > 200)
            {
                CleanPageGUI();
                UpdateGUI();
            }
        }

        private void btnMusicPage_Click(object sender, RoutedEventArgs e)
        {
            if (hasMusicPage == false)
            {
                string path;
                path = ChooseMusic(txtMusicPage, mediaPlayerTale);
                if (path != "")
                {
                    btnPlayPage.IsEnabled = true;
                    btnStopPage.IsEnabled = true;

                    txtMusicPage.Foreground = Brushes.Black;
                    taleManager.CurrentPage.Music = path;

                    timerMusic.Start();

                    hasMusicPage = true;
                    btnMusicPage.Content = "x";
                    isChangedTale = true;
                }
            }
            else
            {
                btnPlayPage.IsEnabled = false;
                btnStopPage.IsEnabled = false;

                btnMusicPage.Content = "...";
                txtMusicPage.Text = "";
                taleManager.CurrentPage.Music = "";
                lblStatusMusic.Content = "";
                timerMusic.Stop();
                mediaPlayerTale.Stop();

                hasMusicPage = false;
                isChangedTale = true;
            }

        }

        private void btnPlayPage_Click(object sender, RoutedEventArgs e)
        {
            if (!isPlayingPage)
            {
                imgBtnPlayPausePage.Source = Utils.GetBitmapFromUri("Iconos\\mpause.png", UriKind.Relative);
                mediaPlayerTale.Play();
                timerMusic.Start();
                isPlayingPage = true;
            }
            else
            {
                imgBtnPlayPausePage.Source = Utils.GetBitmapFromUri("Iconos\\mplay.png", UriKind.Relative);
                mediaPlayerTale.Pause();
                isPlayingPage = false;
            }
        }

        private void btnStopPage_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayerTale.Stop();
            imgBtnPlayPausePage.Source = Utils.GetBitmapFromUri("Iconos\\mplay.png", UriKind.Relative);
        }

        private void grdPic_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CleanPictogramGUI();

            timer = DateTime.Now;
            double ellapsedTime = DateTime.Now.Subtract(timer2).TotalMilliseconds;
            taleManager.CurrentPictogramIndex = int.Parse(((Grid)sender).Uid);

            if (ellapsedTime > 200)
            {
                this.ClicKPictogram();
            }
        }

        private void btnReproduce_Click(object sender, RoutedEventArgs e)
        {
            builder.ClearContent();
            speakBuilder();

            speak = new Speak(builder);
            speak.Speaker();
        }


        public void speakBuilder()
        {
            builder.StartStyle(style);
            foreach (Pictogram picto in taleManager.CurrentPage.Pictograms)
            {
                if (picto != null)
                {
                    if (picto.Sound != "")
                    {
                        builder.AppendAudio(picto.Sound);
                    }
                    else
                    {
                        builder.AppendText(picto.TextToRead);
                    }
                }
            }
            builder.EndStyle();
        }


        #endregion pagePanel

        #region music 

        void timer_Tick(object sender, EventArgs e)
        {
            TimerTick();
        }

        #endregion music

        #region PictogramPanel

        private void btnDeletePic_Click(object sender, RoutedEventArgs e)
        {
            DeletePictogramGUI();

            //------- guardar -------------------------
            taleManager.CurrentPage.RemovePictogram(taleManager.CurrentPictogramIndex);
            //------------------------------------

            rdbImageGaleryPic.IsEnabled = true;
            isChangedTale = true;
        }


        private void btnImgExist_Click(object sender, RoutedEventArgs e)
        {
            if (rdbImageExistPic.IsChecked == true)
            {
                if (!hasImgExist)
                {
                    String path = ChooseImagePic(images[taleManager.CurrentPictogramIndex], txtImgExist);
                    if (path != "")
                    {
                        cmbImgExistPic.IsEnabled = true;
                        cmbImgExistPic.SelectedIndex = 6;
                        btnDeletePic.IsEnabled = true;
                        EditBorder(borders[taleManager.CurrentPictogramIndex], Brushes.DarkGray, 1);
                        labelsPic[taleManager.CurrentPictogramIndex].Visibility = Visibility.Hidden;
                        labelsNumber[taleManager.CurrentPictogramIndex].Background = Brushes.White;
                        txtImgExist.Foreground = Brushes.Black;

                        #region guardar
                        if (taleManager.PictogramExist())
                        {
                            taleManager.SetPictogramImageName(path);
                        }
                        else
                        {
                            taleManager.CurrentPage.InsertPictogram(new Pictogram(taleManager.CurrentPictogramIndex));
                            taleManager.SetPictogramImageName(path);
                        }

                        #endregion guardar
                        isChangedTale = true;
                        hasImgExist = true;
                        btnImgExist.Content = "x";
                    }
                }
                else
                {
                    taleManager.CurrentPictogram.ImageName = "";

                    clearImagePicto();
                    cmbImgExistPic.IsEnabled = false;
                    cmbImgExistPic.SelectedIndex = -1;

                    txtImgExist.Text = "";
                    btnImgExist.Content = "...";

                    hasImgExist = false;
                }
            }
        }

        private void txtTextToReadPic_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                labelsWord[taleManager.CurrentPictogramIndex].Content = txtTextToReadPic.Text;

                //---- guardar ------
                if (taleManager.PictogramExist())
                {
                    taleManager.SetPictogramTextToRead(txtTextToReadPic.Text);
                }
                else
                {
                    taleManager.CurrentPage.InsertPictogram(new Pictogram(taleManager.CurrentPictogramIndex));
                    taleManager.SetPictogramTextToRead(txtTextToReadPic.Text);
                }

                isChangedTale = true;
                //-------------------------

                btnDeletePic.IsEnabled = true;
            }
        }

        private void txtTextToReadPic_MouseLeave(object sender, MouseEventArgs e)
        {
            if (txtTextToReadPic.Text != "")
            {
                labelsWord[taleManager.CurrentPictogramIndex].Content = txtTextToReadPic.Text;

                //---- guardar ------
                if (taleManager.PictogramExist())
                {
                    taleManager.SetPictogramTextToRead(txtTextToReadPic.Text);
                }
                else
                {
                    taleManager.CurrentPage.InsertPictogram(new Pictogram(taleManager.CurrentPictogramIndex));
                    taleManager.SetPictogramTextToRead(txtTextToReadPic.Text);
                }
                isChangedTale = true;
                //-------------------------
                btnDeletePic.IsEnabled = true;
            }
        }

        private void txtTextToReadPic_KeyUp(object sender, KeyEventArgs e)
        {
            labelsWord[taleManager.CurrentPictogramIndex].Content = txtTextToReadPic.Text;
            isChangedTale = true;
        }

        private void txtTextToSearchPic_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                SearchImageGalery(txtTextToSearchPic.Text);
            }
        }

        private void cmbImgPic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedImage();
        }

        private void cmbImgExistPic_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var type in Enum.GetValues(typeof(WordType)))
            {
                cmbImgExistPic.Items.Add(type);
            }
        }

        private void rdbImageGaleryPic_Checked(object sender, RoutedEventArgs e)
        {
            txtTextToSearchPic.IsEnabled = true;
            txtTextToSearchPic.IsReadOnly = false;
            txtTextToSearchPic.Background = Brushes.White;

            btnDeleteImageGalery.IsEnabled = true;
            btnDeleteImageGalery.Background = Brushes.White;
            btnImgExist.IsEnabled = false;
            cmbImgExistPic.IsEnabled = false;
            txtImgExist.Text = "";
            cmbImgExistPic.SelectedIndex = -1;
        }

        private void rdbImageExistPic_Checked(object sender, RoutedEventArgs e)
        {

            btnImgExist.IsEnabled = true;
            //txtTextToSearchPic.IsEnabled = false;
            txtTextToSearchPic.IsReadOnly = true;
            txtTextToSearchPic.Background = Brushes.Transparent;
            //btnDeleteImageGalery.IsEnabled = false;
            btnDeleteImageGalery.Background = Brushes.Transparent;
            cmbImgExistPic.IsEnabled = false;
            txtTextToSearchPic.Text = "";
            cmbImgPic.Items.Clear();
            cmbImgPic.IsEnabled = false;
        }

        private void cmbImgExistPic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            #region guardar
            if (taleManager.PictogramExist())
            {
                if (cmbImgExistPic.SelectedValue != null)
                {
                    taleManager.SetPictogramType((WordType)cmbImgExistPic.SelectedValue);
                    isChangedTale = true;
                }
            }
            else
            {
                if (cmbImgExistPic.SelectedValue != null)
                {
                    taleManager.CurrentPage.InsertPictogram(new Pictogram(taleManager.CurrentPictogramIndex));
                    isChangedTale = true;
                }
            }
            #endregion guardar

            if (cmbImgExistPic.SelectedValue != null)
            {
                Brush color = taleManager.CurrentPictogram.getColorByType((WordType)cmbImgExistPic.SelectedValue);
                if (taleManager.CurrentPictogram.Type == WordType.Ninguno)
                {
                    EditBorder(borders[taleManager.CurrentPictogramIndex], Brushes.DarkGray, 1);
                }
                else
                {
                    EditBorder(borders[taleManager.CurrentPictogramIndex], color, 4);
                }
            }
            btnDeletePic.IsEnabled = true;
        }

        private void btnSoundPic_Click(object sender, RoutedEventArgs e)
        {
            if (hasSoundPictogram == false)
            {

                String path = ChooseSound(txtSoundPic, mediaPlayerTale);

                if (path != "")
                {
                    btnPlayPictogram.IsEnabled = true;
                    btnStopPictogram.IsEnabled = true;

                    txtSoundPic.Foreground = Brushes.Black;
                    txtTextToReadPic.Text = HttpUtility.HtmlDecode("&#9834;&#9834;").ToString();
                    labelsWord[taleManager.CurrentPictogramIndex].Content = HttpUtility.HtmlDecode("&#9834;&#9834;").ToString();
                    timerMusic.Start();

                    #region guardar
                    if (taleManager.PictogramExist())
                    {
                        taleManager.SetPictogramSound(path);
                        taleManager.SetPictogramTextToRead(HttpUtility.HtmlDecode("&#9834;&#9834;").ToString());
                    }
                    else
                    {
                        taleManager.CurrentPage.InsertPictogram(new Pictogram(taleManager.CurrentPictogramIndex));
                        taleManager.SetPictogramSound(path);
                        taleManager.SetPictogramTextToRead(HttpUtility.HtmlDecode("&#9834;&#9834;").ToString());
                    }
                    isChangedTale = true;
                    #endregion guardar
                    btnDeletePic.IsEnabled = true;

                    hasSoundPictogram = true;
                    btnSoundPic.Content = "x";
                }
            }
            else
            {
                btnPlayPictogram.IsEnabled = false;
                btnStopPictogram.IsEnabled = false;

                btnSoundPic.Content = "...";
                txtSoundPic.Text = "";

                taleManager.CurrentPictogram.TextToRead = "";
                taleManager.CurrentPictogram.Sound = "";
                lblStatusSoundPictogram.Content = "";
                txtTextToReadPic.Text = "";
                timerMusic.Stop();
                mediaPlayerTale.Stop();

                hasSoundPictogram = false;
                isChangedTale = true;
            }
        }

        private void btnPlayPictogram_Click(object sender, RoutedEventArgs e)
        {
            if (!isPlayingPic)
            {
                imgBtnPlayPausePict.Source = Utils.GetBitmapFromUri("Iconos\\mpause.png", UriKind.Relative);
                mediaPlayerTale.Play();
                timerMusic.Start();
                isPlayingFrontPage = true;
            }
            else
            {
                imgBtnPlayPausePict.Source = Utils.GetBitmapFromUri("Iconos\\mplay.png", UriKind.Relative);
                mediaPlayerTale.Pause();
                isPlayingPic = false;
            }

        }

        private void MediaPlayerTale_MediaEnded(object sender, EventArgs e)
        {
            if (taleManager.IsFrontPage())
            {
                imgBtnPlayPauseFrontPage.Source = Utils.GetBitmapFromUri("Iconos\\mplay.png", UriKind.Relative);
            }

            if (taleManager.IsPage())
            {
                imgBtnPlayPausePage.Source = Utils.GetBitmapFromUri("Iconos\\mplay.png", UriKind.Relative);
            }

            if (taleManager.IsPictogram())
            {
                imgBtnPlayPausePict.Source = Utils.GetBitmapFromUri("Iconos\\mplay.png", UriKind.Relative);
            }
        }


        private void btnStopPictogram_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayerTale.Stop();
            imgBtnPlayPausePict.Source = Utils.GetBitmapFromUri("Iconos\\mplay.png", UriKind.Relative);
        }

        #endregion PictogramPanel

        #endregion eventos


        #region funciones

        private void DeletePictogramGUI()
        {
            CleanPictogramGUI();

            //------- gridPage --------------------
            CleanSelected();
            labelsWord[taleManager.CurrentPictogramIndex].Content = "";
            labelsPic[taleManager.CurrentPictogramIndex].Visibility = Visibility.Visible;
            labelsNumber[taleManager.CurrentPictogramIndex].Background = Brushes.Transparent;
            String path = "Iconos/none.png";
            images[taleManager.CurrentPictogramIndex].Source = Utils.GetBitmapFromUri(path, UriKind.Relative);
            EditBorder(borders[taleManager.CurrentPictogramIndex], Brushes.DarkGray, 1);
            //----------------------------------
        }

        private void ShowStartInterface()
        {
            grdStart.Visibility = Visibility.Visible;
            grdWorkSpace.Visibility = Visibility.Collapsed;
            grdNavigation.Visibility = Visibility.Hidden;
        }

        private void ShowTaleGUI()
        {
            grdStart.Visibility = Visibility.Collapsed;
            grdWorkSpace.Visibility = Visibility.Visible;

            brdTitlePanel.Visibility = Visibility.Visible;
            brdPagePanel.Visibility = Visibility.Collapsed;
            brdPictogramPanel.Visibility = Visibility.Collapsed;

            grdTale.Visibility = Visibility.Visible;
            grdPages.Visibility = Visibility.Collapsed;

            grdNavigation.Visibility = Visibility.Visible;
        }

        private void ShowPageGUI()
        {
            grdStart.Visibility = Visibility.Collapsed;
            grdWorkSpace.Visibility = Visibility.Visible;

            brdTitlePanel.Visibility = Visibility.Collapsed;
            brdPagePanel.Visibility = Visibility.Visible;
            brdPictogramPanel.Visibility = Visibility.Collapsed;

            grdTale.Visibility = Visibility.Collapsed;
            grdPages.Visibility = Visibility.Visible;

            grdNavigation.Visibility = Visibility.Visible;
        }

        private void ShowPictogramGUI()
        {
            grdStart.Visibility = Visibility.Collapsed;
            grdWorkSpace.Visibility = Visibility.Visible;

            brdTitlePanel.Visibility = Visibility.Collapsed;
            brdPagePanel.Visibility = Visibility.Collapsed;
            brdPictogramPanel.Visibility = Visibility.Visible;

            grdTale.Visibility = Visibility.Collapsed;
            grdPages.Visibility = Visibility.Visible;

            grdNavigation.Visibility = Visibility.Visible;
        }

        private void UpdateNavigation()
        {
            if (taleManager != null)
            {
                if (taleManager.CurrentPageIndex == -1)
                {
                    btnGoToFrontPage.IsEnabled = false;
                    btnPreviousPage.IsEnabled = false;
                    btnGoToEndPage.IsEnabled = true;
                    btnNextPage.IsEnabled = true;

                    if (taleManager.NumberOfPages == 0)
                    {
                        btnGoToFrontPage.IsEnabled = false;
                        btnPreviousPage.IsEnabled = false;
                        btnGoToEndPage.IsEnabled = false;
                        btnNextPage.IsEnabled = false;
                    }
                }
                else
                {
                    if (taleManager.CurrentPageIndex >= 0 && taleManager.CurrentPageIndex < taleManager.NumberOfPages - 1)
                    {
                        btnGoToFrontPage.IsEnabled = true;
                        btnPreviousPage.IsEnabled = true;
                        btnGoToEndPage.IsEnabled = true;
                        btnNextPage.IsEnabled = true;
                    }
                    else
                    {
                        btnGoToFrontPage.IsEnabled = true;
                        btnPreviousPage.IsEnabled = true;
                        btnGoToEndPage.IsEnabled = false;
                        btnNextPage.IsEnabled = false;
                    }

                }
                lblNavigate.Content = taleManager.CurrentPageIndex + 1;
            }

        }

        private void UpdateGUI()
        {
            if (taleManager == null)
            {
                ShowStartInterface();
            }

            if (taleManager != null)
            {
                if (taleManager.CurrentPageIndex == -1)
                {
                    lblNavigate.Content = 0;
                    UpdateTaleGUI();
                    ShowTaleGUI();
                }
                else
                {
                    if (taleManager.CurrentPageIndex >= 0 && taleManager.CurrentPageIndex < taleManager.NumberOfPages - 1)
                    {
                        UpdatePageGUI();
                        ShowPageGUI();
                    }
                    else
                    {
                        lblNavigate.Content = taleManager.NumberOfPages;
                        UpdatePageGUI();
                        ShowPageGUI();
                    }
                }
            }


            UpdateNavigation();
            ShowMenuGUI();
        }

        private void CleanTaleGUI()
        {
            txtAuthor.Text = "";
            txtTitle.Text = "";
            txtLanguage.Text = "";
            txtFrontPage.Text = "";
            txtLicense.Text = "";
            txtUrl.Text = "";
            txtDateOfCreation.Text = "";

            imgFrontPageBackground.Source = null;
            rdbImageFrontPage.IsChecked = true;
            rdbColorFrontPage.IsChecked = false;
            btnBackgroundImageFrontPage.IsEnabled = true;
            txtBackgroundImageFrontPage.Text = "";
            txtBackgroundImageFrontPage.Foreground = Brushes.Black;
            txtBackgroundColorFrontPage.Text = "";

            btnPlayFrontPage.IsEnabled = false;
            btnStopFrontPage.IsEnabled = false;
            btnMusicFrontPage.IsEnabled = true;
            txtMusicFrontPage.Text = "";
            txtMusicFrontPage.Foreground = Brushes.Black;
            lblStatusMusic.Content = "";
            hasMusicFrontPage = false;
            btnMusicFrontPage.Content = "...";

            StopMusic();
        }

        private void UpdateTaleGUI()
        {
            CleanTaleGUI();

            if (taleManager != null)
            {
                txtAuthor.Text = taleManager.Author;
                txtTitle.Text = taleManager.Title;
                txtLanguage.Text = taleManager.Language;
                txtFrontPage.Text = taleManager.Title;
                txtLicense.Text = taleManager.License;
                txtUrl.Text = taleManager.Url;
                txtDateOfCreation.Text = taleManager.DateOfCreation.ToString();
                //--music
                if (taleManager.Music != "")
                {
                    if (File.Exists(taleManager.Music))
                    {
                        btnPlayFrontPage.IsEnabled = true;
                        btnStopFrontPage.IsEnabled = true;

                        hasMusicFrontPage = true;

                        txtMusicFrontPage.Text = Utils.ChangeToRelativePath(taleManager.Music);
                        txtMusicFrontPage.Foreground = Brushes.Black;
                        btnMusicFrontPage.Content = "x";
                        imgBtnPlayPausePage.Source = Utils.GetBitmapFromUri("Iconos\\mplay.png", UriKind.Relative);

                        mediaPlayerTale.Open(new Uri(taleManager.Music));
                        timerMusic.Start();
                    }
                    else
                    {
                        hasMusicFrontPage = false;

                        btnPlayFrontPage.IsEnabled = false;
                        btnStopFrontPage.IsEnabled = false;

                        taleManager.Music = "";
                        txtMusicFrontPage.Text = "Música No encontrada";
                        txtMusicFrontPage.Foreground = Brushes.Red;
                    }
                }
                //-- Background
                String background = taleManager.Background;
                int tamBackground = background.Length;
                if (tamBackground > 0)
                {
                    if (Utils.isArchive(taleManager.Background))
                    {
                        String pathBackgroundPage = taleManager.Background;

                        imgFrontPageBackground.Source = Utils.GetBitmapFromUri(taleManager.Background, UriKind.Absolute);
                        if (imgFrontPageBackground.Source != null)
                        {
                            txtBackgroundImageFrontPage.Text = Utils.ChangeToRelativePath(imgFrontPageBackground.Source.ToString());
                            txtBackgroundImageFrontPage.Foreground = Brushes.Black;

                            rdbImageFrontPage.IsChecked = true;
                            rdbColorFrontPage.IsChecked = false;

                            btnBackgroundImageFrontPage.IsEnabled = true;
                            btnBackgroundColorFrontPage.IsEnabled = false;

                            imgFrontPageBackground.Stretch = Stretch.UniformToFill;
                            imgFrontPageBackground.HorizontalAlignment = HorizontalAlignment.Center;
                            imgFrontPageBackground.VerticalAlignment = VerticalAlignment.Center;
                        }
                        else
                        {
                            taleManager.Background = "";
                            rdbImageFrontPage.IsChecked = true;
                            txtBackgroundImageFrontPage.Text = "Imagen no encontrada";
                            txtBackgroundImageFrontPage.Foreground = Brushes.Red;
                        }
                    }

                    if (background.Contains("#"))
                    {
                        var bc = new BrushConverter();
                        grdFrontPage.Background = (Brush)bc.ConvertFrom(background);
                        txtBackgroundColorFrontPage.Text = background;

                        rdbImageFrontPage.IsChecked = false;
                        rdbColorFrontPage.IsChecked = true;

                        btnBackgroundImageFrontPage.IsEnabled = false;
                        btnBackgroundColorFrontPage.IsEnabled = true;
                    }
                }
            }
            lblStatusBarTitle.Content = "Cuento: " + txtTitle.Text; //txtTitle.Text
            lblStatusBarPages.Content = "";

        }

        private void CleanPictograms()
        {
            HideBorder();

            foreach (var t in textBoxesTextToRead)
            {
                t.Foreground = Brushes.Black;
                t.Text = "";
            }

            for (int i = 0; i < 10; i++)
            {
                String path = "/Iconos/none.png";
                images[i].Source = Utils.GetBitmapFromUri(path, UriKind.Relative);

                labelsWord[i].Content = "";
                labelsPic[i].Visibility = Visibility.Visible;
                labelsNumber[i].Background = Brushes.Transparent;
            }

        }

        private void CleanPageGUI()
        {
            rdbImagePage.IsChecked = true;
            rdbColorPage.IsChecked = false;
            btnBackgroundImagePage.IsEnabled = true;
            txtBackgroundColorPage.Text = "";
            txtBackgroundImagePage.Text = "";
            btnImgExist.IsEnabled = false;
            txtBackgroundImagePage.Foreground = Brushes.Black;

            imgPageBackground.Source = null;
            grdPage.Background = null;
            HideBorder();
            CleanSelected();

            btnMusicPage.IsEnabled = true;
            txtMusicPage.Text = "";
            txtMusicPage.Foreground = Brushes.Black;
            btnPlayPage.IsEnabled = false;
            btnStopPage.IsEnabled = false;
            //btnPausePage.IsEnabled = false;
            lblStatusMusicPage.Content = "";
            hasMusicPage = false;
            btnMusicPage.Content = "...";

            StopMusic();

            foreach (var t in textBoxesTextToRead)
            {
                t.Foreground = Brushes.Black;
                t.Text = "";
            }

            for (int i = 0; i < 10; i++)
            {
                String path = "/Iconos/none.png";
                images[i].Source = Utils.GetBitmapFromUri(path, UriKind.Relative);

                labelsWord[i].Content = "";
                labelsPic[i].Visibility = Visibility.Visible;
                labelsNumber[i].Background = Brushes.Transparent;
            }

            if (hasSentence)
            {
                txtSentence.Text = "";
            }


        }

        private void UpdatePageGUI()
        {

            CleanPageGUI();
            btnReproduce.IsEnabled = true;
            btnStop.IsEnabled = true;
            hasSentence = false;

            int numPage = taleManager.CurrentPageIndex + 1;
            TPage auxPage = taleManager.CurrentPage;

            lblPage.Content = "Página " + numPage;

            if (auxPage != null)
            {
                lblStatusBarPages.Content = "Número de páginas: " + taleManager.NumberOfPages;

                //-- music ---

                if (auxPage.Music != "")
                {
                    if (File.Exists(auxPage.Music))
                    {
                        btnPlayPage.IsEnabled = true;
                        btnStopPage.IsEnabled = true;
                        hasMusicPage = true;

                        txtMusicPage.Text = Utils.ChangeToRelativePath(auxPage.Music);
                        btnMusicPage.IsEnabled = true;
                        txtMusicPage.Foreground = Brushes.Black;
                        btnMusicPage.Content = "x";
                        imgBtnPlayPausePage.Source = Utils.GetBitmapFromUri("Iconos\\mplay.png", UriKind.Relative);

                        mediaPlayerTale.Open(new Uri(auxPage.Music));
                        timerMusic.Start();
                    }
                    else
                    {
                        btnPlayPage.IsEnabled = false;
                        btnStopPage.IsEnabled = false;

                        taleManager.CurrentPage.Music = "";
                        txtMusicPage.Text = "Música no encontrada";
                        txtMusicPage.Foreground = Brushes.Red;
                    }
                }

                //-- Background
                String background = auxPage.Background;
                int tamBackground = background.Length;
                if (tamBackground > 0)
                {
                    if (Utils.isArchive(background))
                    {
                        imgPageBackground.Source = Utils.GetBitmapFromUri(background, UriKind.Absolute);

                        if (imgPageBackground.Source != null)
                        {
                            txtBackgroundImagePage.Text = Utils.ChangeToRelativePath(imgPageBackground.Source.ToString());

                            rdbImagePage.IsChecked = true;
                            rdbColorPage.IsChecked = false;

                            btnBackgroundImagePage.IsEnabled = true;
                            btnBackgroundColorPage.IsEnabled = false;

                            txtBackgroundImagePage.Foreground = Brushes.Black;

                            imgPageBackground.Stretch = Stretch.UniformToFill;
                            imgPageBackground.HorizontalAlignment = HorizontalAlignment.Center;
                            imgPageBackground.VerticalAlignment = VerticalAlignment.Center;


                        }
                        else
                        {
                            taleManager.CurrentPage.Background = "";
                            txtBackgroundImagePage.Text = "Imagen no encontrada";
                            rdbImagePage.IsChecked = true;
                            txtBackgroundImagePage.Foreground = Brushes.Red;
                        }
                    }

                    if (background.Contains("#"))
                    {
                        var bc = new BrushConverter();
                        grdPage.Background = (Brush)bc.ConvertFrom(background);
                        txtBackgroundColorPage.Text = background;

                        rdbImagePage.IsChecked = false;
                        rdbColorPage.IsChecked = true;

                        btnBackgroundImagePage.IsEnabled = false;
                        btnBackgroundColorPage.IsEnabled = true;
                    }
                }

                //-- PICTOGRAMAS ----
                foreach (Pictogram pictogram in auxPage.Pictograms)
                {

                    int i = pictogram.Index;
                    String path = pictogram.ImageName;



                    if (pictogram.Word != "")
                    {

                        if (path != null && path != "")
                        {
                            images[i].Source = Utils.GetBitmapFromUri(path, UriKind.Absolute);
                            labelsNumber[i].Background = Brushes.White;
                            labelsPic[i].Visibility = Visibility.Hidden;
                        }

                        rdbImageExistPic.IsChecked = false;
                        rdbImageGaleryPic.IsChecked = true;

                        if (pictogram.Type == WordType.Ninguno)
                        {
                            EditBorder(borders[i], Brushes.DarkGray, 1);
                        }
                        else
                        {
                            EditBorder(borders[i], pictogram.getColorByType(pictogram.Type), 4);
                        }

                        labelsWord[i].Content = pictogram.TextToRead;
                    }
                    else
                    {
                        if (path != null && path != "")
                        {
                            if (isImageCut == true && isImageSquare == false)
                            {
                                BitmapImage bitmapImage = Utils.GetBitmapFromUri(path, UriKind.Absolute);
                                CroppedBitmap imageCropp = new CroppedBitmap(bitmapImage, new Int32Rect(0, 0, bitmapImage.PixelWidth, bitmapImage.PixelWidth));
                                images[i].Source = imageCropp;
                            }
                            else
                            {
                                images[i].Source = Utils.GetBitmapFromUri(path, UriKind.Absolute);
                            }

                            if (images[i].Source != null)
                            {
                                labelsPic[i].Visibility = Visibility.Hidden;
                                labelsWord[i].Content = pictogram.TextToRead;
                                if (pictogram.Type == WordType.Ninguno)
                                {
                                    EditBorder(borders[i], Brushes.DarkGray, 1);
                                }
                                else
                                {
                                    EditBorder(borders[i], pictogram.getColorByType(pictogram.Type), 4);
                                }

                                txtImgExist.Text = Utils.ChangeToRelativePath(pictogram.ImageName);
                                cmbImgExistPic.IsEnabled = true;
                                rdbImageExistPic.IsChecked = true;
                                rdbImageGaleryPic.IsChecked = false;
                                txtImgExist.Foreground = Brushes.Black;
                                labelsNumber[i].Background = Brushes.White;
                            }
                            else
                            {
                                pictogram.ImageName = "";
                                txtImgExist.Text = "Imagen no encontrada";
                                rdbImageExistPic.IsChecked = true;
                                txtImgExist.Foreground = Brushes.Red;
                            }
                        }
                    }

                    if (path == "")
                    {
                        EditBorder(borders[i], Brushes.DarkGray, 1);
                        String path2 = "/Iconos/none.png";
                        labelsPic[i].Visibility = Visibility.Visible;
                        images[i].Source = Utils.GetBitmapFromUri(path2, UriKind.Relative);

                        if (pictogram.TextToRead != "")
                        {
                            labelsWord[i].Content = pictogram.TextToRead;
                        }
                    }

                    /* if(pictogram.ImageName=="" && pictogram.Word=="")
                     {
                         EditBorder(borders[i], Brushes.DarkGray, 1);
                         String path2 = "/Iconos/none.png";
                         labelsPic[i].Visibility = Visibility.Visible;
                         images[i].Source = Utils.GetBitmapFromUri(path2, UriKind.Relative);
                     }*/

                }

                //-----reproducir
                GetReadingElement();

            }
        }

        private void StopMusic()
        {

            //synth.Dispose();
            timerMusic.Stop();
            //timerReproduce.Stop();

            mediaPlayerTale.Stop();
        }

        private void CleanPictogramGUI()
        {
            rdbImageExistPic.IsChecked = false;
            rdbImageGaleryPic.IsChecked = true;
            txtImgExist.Text = "";
            cmbImgExistPic.IsEnabled = false;
            cmbImgExistPic.SelectedItem = -1;
            cmbImgPic.Items.Clear();
            cmbImgPic.IsEnabled = false;
            txtTextToSearchPic.Text = "";
            cmbImgPic.Foreground = Brushes.Black;
            cmbImgPic.IsEditable = false;
            cmbImgExistPic.IsEnabled = false;

            txtTextToReadPic.Text = "";

            txtSoundPic.Text = "";
            btnPlayPictogram.IsEnabled = false;
            btnStopPictogram.IsEnabled = false;

            StopMusic();
            lblStatusSoundPictogram.Content = "";
            lblStatusMusicPage.Content = "";
            hasSoundPictogram = false;
            btnSoundPic.Content = "...";

            btnDeletePic.IsEnabled = false;

            btnDeleteImageGalery.IsEnabled = true;

            hasImgExist = false;
            btnImgExist.Content = "...";
        }

        private void UpdatePictogramGUI()
        {
            CleanPictogramGUI();

            int index = taleManager.CurrentPictogramIndex + 1;
            lblPic.Content = "Pictograma " + index;

            if (taleManager.PictogramExist())
            {
                if (taleManager.CurrentPictogram.Word != "")
                {
                    if (taleManager.CurrentPictogram.ImageName != "")
                    {
                        txtTextToSearchPic.Text = taleManager.CurrentPictogram.Word;
                        UpdateImageGalery(txtTextToSearchPic.Text);

                        btnDeletePic.IsEnabled = true;
                    }
                    else
                    {
                        txtTextToSearchPic.Text = taleManager.CurrentPictogram.Word;
                        btnDeletePic.IsEnabled = false;

                        cmbImgPic.IsEditable = true;
                        cmbImgPic.IsEnabled = false;
                        cmbImgPic.Foreground = Brushes.Red;
                        cmbImgPic.Text = "No hay resultados";
                    }
                }

                if (taleManager.CurrentPictogram.Word == "")
                {
                    if (taleManager.CurrentPictogram.ImageName != "")
                    {
                        rdbImageGaleryPic.IsChecked = false;
                        rdbImageExistPic.IsChecked = true;
                        cmbImgExistPic.IsEnabled = true;
                        btnDeletePic.IsEnabled = true;

                        cmbImgExistPic.SelectedItem = taleManager.CurrentPictogram.Type;
                        String path = Utils.ChangeToRelativePath(taleManager.CurrentPictogram.ImageName);
                        txtImgExist.Text = path;

                        hasImgExist = true;
                        btnImgExist.Content = "x";
                    }
                    else
                    {
                        hasImgExist = false;
                        btnImgExist.Content = "...";
                    }
                }

                if (taleManager.CurrentPictogram.Sound != "")
                {
                    if (File.Exists(taleManager.CurrentPictogram.Sound))
                    {
                        btnPlayPictogram.IsEnabled = true;
                        btnStopPictogram.IsEnabled = true;

                        btnDeletePic.IsEnabled = true;
                        hasSoundPictogram = true;

                        txtTextToReadPic.Text = taleManager.CurrentPictogram.TextToRead;
                        txtSoundPic.Text = Utils.ChangeToRelativePath(taleManager.CurrentPictogram.Sound);
                        txtSoundPic.Foreground = Brushes.Black;
                        btnSoundPic.Content = "x";
                        imgBtnPlayPausePage.Source = Utils.GetBitmapFromUri("Iconos\\mplay.png", UriKind.Relative);

                        mediaPlayerTale.Open(new Uri(taleManager.CurrentPictogram.Sound));
                        timerMusic.Start();
                    }
                    else
                    {
                        btnPlayPictogram.IsEnabled = false;
                        btnStopPictogram.IsEnabled = false;

                        hasSoundPictogram = false;

                        labelsWord[taleManager.CurrentPictogramIndex].Content = "";


                        taleManager.CurrentPictogram.TextToRead = "";
                        taleManager.CurrentPictogram.Sound = "";

                        txtSoundPic.Text = "Sonido no encontrado";
                        txtSoundPic.Foreground = Brushes.Red;
                    }
                }

                if (taleManager.CurrentPictogram.TextToRead != "")
                {
                    txtTextToReadPic.Text = taleManager.CurrentPictogram.TextToRead;
                    btnDeletePic.IsEnabled = true;
                }
            }
            else
            {
                btnDeletePic.IsEnabled = false;
                cmbImgPic.IsEnabled = false;
                txtTextToSearchPic.Text = "";
                txtTextToReadPic.Text = "";
            }
        }

        private void HideBorder()
        {
            int i = 0;
            foreach (Border b in borders)
            {
                EditBorder(borders[i], Brushes.DarkGray, 1);
                i++;
            }
        }

        private void EditBorder(Border border, Brush color, int borderSize)
        {
            border.BorderBrush = color;
            border.BorderThickness = new Thickness(borderSize, borderSize, borderSize, borderSize);
        }

        private void CleanSelected()
        {
            int i = 0;
            foreach (Grid g in grid)
            {
                grid[i].Background.Opacity = 0;
                i++;
            }
        }

        private string ChooseMusic(TextBox textBox, MediaPlayer mediaPlayer)
        {
            string path = "";
            OpenFileDialog file = new OpenFileDialog();

            file.Filter = "Fichero de audio (*.mp3, *.wav)|*.mp3;*.wav";

            if (file.ShowDialog() == true)
            {
                path = file.FileName;
                textBox.Text = file.SafeFileName;

                mediaPlayer.Open(new Uri(path));
            }
            timer2 = DateTime.Now;
            return path;
        }

        private string ChooseSound(TextBox textBox, MediaPlayer mediaPlayer)
        {
            string path = "";
            OpenFileDialog file = new OpenFileDialog();

            file.Filter = "Fichero de audio (*.wav)|*.wav";

            if (file.ShowDialog() == true)
            {
                path = file.FileName;
                textBox.Text = file.SafeFileName;

                mediaPlayer.Open(new Uri(path));
            }
            timer2 = DateTime.Now;
            return path;
        }

        private String ChooseImage(Image imageBackground, TextBox textbox)
        {
            String namePath = "";
            Image img = new Image();

            if (img.Source == null)
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.InitialDirectory = locationChooseImage;
                openFile.Filter = "Imagenes | *.jpg; *.gif; *.png; *.bmp";

                if (openFile.ShowDialog() == true)
                {
                    textbox.Text = openFile.SafeFileName;
                    namePath = openFile.FileName;

                    if (locationChooseImage == "")
                    {
                        int tamSafeFileName = openFile.SafeFileName.Length;
                        int tamFileName = openFile.FileName.Length;
                        locationChooseImage = namePath.Substring(0, tamFileName - tamSafeFileName);
                    }

                    img.Source = Utils.GetBitmapFromUri(openFile.FileName, UriKind.Absolute);
                    imageBackground.Source = img.Source;
                }
            }

            timer2 = DateTime.Now;
            return namePath;
        }

        private String ChooseImagePic(Image imageBackground, TextBox textbox)
        {
            String namePath = "";
            Image img = new Image();
            double width = 0;
            double height = 0;

            if (img.Source == null)
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.InitialDirectory = locationChooseImage;
                openFile.Filter = "Imagenes | *.jpg; *.gif; *.png; *.bmp";

                if (openFile.ShowDialog() == true)
                {
                    img.Source = Utils.GetBitmapFromUri(openFile.FileName, UriKind.Absolute);

                    width = img.Source.Width;
                    height = img.Source.Height;
                    namePath = openFile.FileName;
                    BitmapImage bitmapImage = (BitmapImage)img.Source;

                    if (locationChooseImage == "")
                    {
                        int tamSafeFileName = openFile.SafeFileName.Length;
                        int tamFileName = openFile.FileName.Length;
                        locationChooseImage = namePath.Substring(0, tamFileName - tamSafeFileName);
                    }

                    if (width != height)
                    {
                        if (height > width)
                        {
                            isImageSquare = false;
                            isImageCut = true;
                            CroppedBitmap imageCropp = new CroppedBitmap(bitmapImage, new Int32Rect(0, 0, bitmapImage.PixelWidth, bitmapImage.PixelWidth));
                            textbox.Text = openFile.SafeFileName;
                            imageBackground.Source = imageCropp;
                        }

                        else
                        {
                            isImageSquare = false;
                            isImageCut = false;
                            textbox.Text = openFile.SafeFileName;
                            imageBackground.Source = img.Source;
                        }
                    }
                    else
                    {
                        isImageSquare = true;
                        isImageCut = false;
                        textbox.Text = openFile.SafeFileName;
                        imageBackground.Source = img.Source;
                    }
                }
            }

            timer2 = DateTime.Now;
            return namePath;
        }

        private Boolean ChooseColor(Grid grdBackground, TextBox textBox)
        {
            Boolean changeColor = false;
            System.Windows.Forms.ColorDialog colorDialog1 = new System.Windows.Forms.ColorDialog();
            colorDialog1.CustomColors = colorPer;

            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var wpfColor = Color.FromArgb(colorDialog1.Color.A, colorDialog1.Color.R, colorDialog1.Color.G, colorDialog1.Color.B);
                var brush = new SolidColorBrush(wpfColor);

                colorPer = (int[])colorDialog1.CustomColors.Clone();

                grdBackground.Background = brush;
                textBox.Text = brush.ToString();
                changeColor = true;
            }
            else
            {
                changeColor = false;
            }
            return changeColor;
        }

        private void SelectedImage()
        {
            Word word;
            String img;

            word = Utils.WordByID((String)(cmbImgPic.SelectedItem), words);

            if (word != null)
            {
                img = word.NameImage;
                String path = img;

                #region guardar
                if (taleManager.PictogramExist())
                {
                    taleManager.SetPictogramType(word.Type);
                    taleManager.SetPictogramImageName(img);
                }
                else
                {
                    taleManager.CurrentPage.InsertPictogram(new Pictogram(taleManager.CurrentPictogramIndex));
                    taleManager.SetPictogramType(word.Type);
                    taleManager.SetPictogramImageName(img);
                }
                isChangedTale = true;
                #endregion guardar

                if (taleManager.CurrentPictogram != null)
                {
                    EditBorder(borders[taleManager.CurrentPictogramIndex], taleManager.CurrentPictogram.getColorByType(word.Type), 4);
                    images[taleManager.CurrentPictogramIndex].Source = Utils.GetBitmapFromUri(path, UriKind.Absolute);
                    labelsPic[taleManager.CurrentPictogramIndex].Visibility = Visibility.Hidden;
                    labelsNumber[taleManager.CurrentPictogramIndex].Background = Brushes.White;
                    btnDeletePic.IsEnabled = true;
                }

            }

        }

        private void UpdateImageGalery(String texto)
        {
            int ind = 1;
            String nombreImagenNumero = "";
            WordType tipo;
            List<Word> test;
            String uri = "";

            cmbImgPic.Items.Clear();
            cmbImgPic.IsEnabled = false;
            cmbImgPic.IsEditable = false;
            cmbImgPic.Foreground = Brushes.Black;
            words.Clear();

            test = dbManager.GetWords(texto);
            //test = Utils.SearchWords(texto);

            if (test == null)
            {
                test = new List<Word>();
            }

            if (test.Count >= 1)
            {
                foreach (Word s in test)
                {
                    String palabraUno;
                    palabraUno = s.ID;
                    nombreImagenNumero = s.NameImage;
                    tipo = s.Type;

                    if (nombreImagenNumero != "")
                    {
                        uri = Environment.CurrentDirectory + "/Galeria/" + nombreImagenNumero.Substring(0, 1) + "/" + nombreImagenNumero;
                    }

                    words.Add(new Word(palabraUno, texto, uri, tipo));

                    cmbImgPic.Items.Add(palabraUno);
                    cmbImgPic.IsEditable = false;
                    cmbImgPic.IsEnabled = true;
                    ind++;
                }
                test.Clear();
                btnDeletePic.IsEnabled = true;
            }

            if (taleManager.CurrentPictogram.Word != "")
            {
                btnDeleteImageGalery.IsEnabled = true;
            }
        }

        private void SearchImageGalery(String texto)
        {
            int ind = 1;
            String nameImage = "";
            WordType tipo;
            List<Word> test;
            String uri = "";

            cmbImgPic.Items.Clear();
            cmbImgPic.IsEnabled = true;
            cmbImgPic.IsEditable = true;
            cmbImgPic.Foreground = Brushes.Black;
            words.Clear();


            test = dbManager.GetWords(texto);
            //test = Utils.SearchWords(texto);

            if (test == null)
            {
                test = new List<Word>();
            }

            //if (test!=null)
            if (test.Count >= 1)
            {
                foreach (Word s in test)
                {
                    String palabraUno;
                    palabraUno = s.ID;
                    nameImage = s.NameImage;
                    tipo = s.Type;

                    if (nameImage != "")
                    {
                        uri = Environment.CurrentDirectory + "/Galeria/" + nameImage.Substring(0, 1) + "/" + nameImage;
                    }

                    words.Add(new Word(palabraUno, texto, uri, tipo));

                    cmbImgPic.Items.Add(palabraUno);
                    cmbImgPic.IsEditable = false;
                    cmbImgPic.IsEnabled = false;
                    ind++;
                }

                String path2 = words[0].NameImage;
                images[taleManager.CurrentPictogramIndex].Source = Utils.GetBitmapFromUri(path2, UriKind.Absolute);
                cmbImgPic.SelectedItem = cmbImgPic.Items.GetItemAt(0);
                labelsWord[taleManager.CurrentPictogramIndex].Content = texto;
                labelsNumber[taleManager.CurrentPictogramIndex].Background = Brushes.White;

                if (taleManager.PictogramExist())
                {
                    taleManager.SetPictogramTextToRead(txtTextToSearchPic.Text);

                    taleManager.SetPictogramWord(txtTextToSearchPic.Text);

                }
                else
                {
                    if (txtTextToSearchPic.Text != "")
                    {
                        taleManager.CurrentPage.InsertPictogram(new Pictogram(taleManager.CurrentPictogramIndex));
                        taleManager.SetPictogramWord(txtTextToSearchPic.Text);
                        taleManager.SetPictogramTextToRead(txtTextToSearchPic.Text);
                    }
                }
                isChangedTale = true;

                test.Clear();
                cmbImgPic.IsEnabled = true;
                btnDeletePic.IsEnabled = true;
            }
            else
            {
                EditBorder(borders[taleManager.CurrentPictogramIndex], Brushes.DarkGray, 1);

                String path2 = "/Iconos/none.png";
                images[taleManager.CurrentPictogramIndex].Source = Utils.GetBitmapFromUri(path2, UriKind.Relative);
                labelsPic[taleManager.CurrentPictogramIndex].Visibility = Visibility.Visible;
                labelsNumber[taleManager.CurrentPictogramIndex].Background = Brushes.Transparent;
                btnDeletePic.IsEnabled = false;

                cmbImgPic.IsEditable = true;
                cmbImgPic.IsEnabled = false;
                cmbImgPic.Foreground = Brushes.Red;
                cmbImgPic.Text = "No hay resultados";
            }
            if (txtTextToSearchPic.Text != "")
            {
                btnDeleteImageGalery.IsEnabled = true;
            }
        }

        private void ClicKPictogram()
        {
            words.Clear();
            cmbImgPic.IsEnabled = false;
            cmbImgPic.Items.Clear();
            CleanSelected();

            grid[taleManager.CurrentPictogramIndex].Background.Opacity = 0.1d;

            UpdatePictogramGUI();
            ShowPictogramGUI();
        }

        private void GetReadingElement()
        {
            foreach (Pictogram p in taleManager.CurrentPage.Pictograms)
            {
                if (p != null)
                {
                    if (p.TextToRead != "" && p.Sound == "")
                    {
                        textBoxesTextToRead[p.Index].Text = p.TextToRead;
                        textBoxesTextToRead[p.Index].Foreground = Brushes.Black;
                    }
                    else
                    {
                        if (p.Sound != "")
                        {
                            if (File.Exists(p.Sound))
                            {
                                textBoxesTextToRead[p.Index].Text = HttpUtility.HtmlDecode("&#9834;&#9834;").ToString();
                            }
                            else
                            {
                                textBoxesTextToRead[p.Index].Text = "Sonido no encontrado";
                                textBoxesTextToRead[p.Index].Foreground = Brushes.Red;
                            }
                        }
                        else
                        {
                            textBoxesTextToRead[p.Index].Foreground = Brushes.Black;
                            textBoxesTextToRead[p.Index].Text = "";
                        }
                    }
                }
            }
        }

        private void CompressFile(String location, String startPath, String zipPath, String filename)
        {
            DirectoryInfo dir = new DirectoryInfo(location);
            FileInfo[] files = dir.GetFiles(filename + ".tale", SearchOption.TopDirectoryOnly);

            if (files.Length > 0)
            {
                files[0].Delete();
                ZipFile.CreateFromDirectory(startPath, zipPath);

            }
            else
            {
                ZipFile.CreateFromDirectory(startPath, zipPath);
            }
        }

        private void ChangeToAbsolutePath(String location)
        {

            //cuento
            if (taleManager.Background != "" && Utils.isArchive(taleManager.Background))
            {
                taleManager.Background = location + "\\Imagenes\\0\\" + taleManager.Background;
            }
            if (taleManager.Music != "" && Utils.isArchive(taleManager.Music))
            {
                taleManager.Music = location + "\\Audios\\0\\" + taleManager.Music;
            }
            //páginas
            foreach (TPage page in taleManager.GetPages)
            {
                int numPage = page.Index + 1;
                if (page.Background != "" && Utils.isArchive(page.Background))
                {
                    page.Background = location + "\\Imagenes" + "\\" + numPage + "\\" + page.Background;
                }
                if (page.Music != "" && Utils.isArchive(page.Music))
                {
                    page.Music = location + "\\Audios\\" + numPage + "\\" + page.Music;
                }

                foreach (Pictogram picto in page.Pictograms)
                {
                    bool aux = Utils.isArchive(picto.ImageName);
                    if (picto.ImageName != "" && aux)
                    {
                        picto.ImageName = location + "\\Imagenes\\" + numPage + "\\" + picto.ImageName;
                    }
                    if (picto.Sound != "" && Utils.isArchive(picto.Sound))
                    {
                        picto.Sound = location + "\\Audios\\" + numPage + "\\" + picto.Sound;
                    }
                }
            }
        }

        private void ChangeNameFiles()
        {
            //cuento
            if (taleManager.Background != "" && Utils.isArchive(taleManager.Background))
            {
                taleManager.Background = Utils.ChangeToRelativePath(taleManager.Background);
                taleManager.Background = "bg" + "." + Utils.GetExtension(taleManager.Background);
            }
            if (taleManager.Music != "")
            {
                taleManager.Music = Utils.ChangeToRelativePath(taleManager.Music);
                taleManager.Music = "music" + "." + Utils.GetExtension(taleManager.Music);
            }

            //páginas
            foreach (TPage page in taleManager.GetPages)
            {
                if (page.Background != "" && Utils.isArchive(page.Background))
                {
                    page.Background = Utils.ChangeToRelativePath(page.Background);
                    page.Background = "bg" + "." + Utils.GetExtension(page.Background);
                }
                if (page.Music != "")
                {
                    page.Music = Utils.ChangeToRelativePath(page.Music);
                    page.Music = "music" + "." + Utils.GetExtension(page.Music);
                }

                foreach (Pictogram picto in page.Pictograms)
                {
                    int numPicto = picto.Index + 1;

                    if (picto.ImageName != "")
                    {
                        picto.ImageName = Utils.ChangeToRelativePath(picto.ImageName);
                        picto.ImageName = numPicto.ToString() + "." + Utils.GetExtension(picto.ImageName);
                    }
                    if (picto.Sound != "")
                    {
                        picto.Sound = Utils.ChangeToRelativePath(picto.Sound);
                        picto.Sound = numPicto.ToString() + "." + Utils.GetExtension(picto.Sound);
                    }
                }
            }
        }

        private void CopyArchives(String location)
        {
            String path = location;
            Utils.CreateDirectory(path);
            String pathImages = location + "\\Imagenes";
            String pathAudios = location + "\\Audios";
            String pathFrontPageImage = location + "\\Imagenes\\0";
            String pathFrontPageAudio = location + "\\Audios\\0";

            #region music
            if (taleManager.Music != "")
            {
                Utils.CreateDirectory(pathAudios);
                Utils.CreateDirectory(pathFrontPageAudio);
                FileInfo[] files = Utils.SeachFileDirectory(path, "music");

                if (files.Length > 0 && (!Utils.isArchive(taleManager.Music)))
                {
                    files[0].Delete();
                }
                else if (Utils.isArchive(taleManager.Background))
                {
                    Utils.CopyFiles(pathFrontPageAudio, taleManager.Music, "music");
                }
            }
            #endregion music

            #region back
            if (taleManager.Background != "")
            {
                Utils.CreateDirectory(pathImages);
                Utils.CreateDirectory(pathFrontPageImage);
                FileInfo[] files = Utils.SeachFileDirectory(path, "bg");

                if (files.Length > 0 && (!Utils.isArchive(taleManager.Background)))
                {
                    files[0].Delete();
                }
                else if (Utils.isArchive(taleManager.Background))
                {
                    Utils.CopyFiles(pathFrontPageImage, taleManager.Background, "bg");
                }
            }
            #endregion back

            #region page

            if (taleManager.NumberOfPages > 0)
            {
                foreach (TPage page in taleManager.GetPages)
                {
                    int numPage = page.Index + 1;
                    String pathPagesImage = location + "\\Imagenes\\" + numPage;
                    String pathPagesAudio = location + "\\Audios\\" + numPage;
                    if (page.Background != "")
                    {
                        Utils.CreateDirectory(pathImages);
                        Utils.CreateDirectory(pathPagesImage);
                        FileInfo[] files = Utils.SeachFileDirectory(pathPagesImage, "bg");
                        if (files.Length > 0 && (!Utils.isArchive(page.Background)))
                        {
                            files[0].Delete();
                        }
                        else if (Utils.isArchive(page.Background))
                        {
                            Utils.CopyFiles(pathPagesImage, page.Background, "bg");
                        }
                    }

                    if (page.Music != "")
                    {
                        Utils.CreateDirectory(pathAudios);
                        Utils.CreateDirectory(pathPagesAudio);
                        FileInfo[] files = Utils.SeachFileDirectory(pathPagesAudio, "music");
                        if (files.Length > 0 && (!Utils.isArchive(page.Music)))
                        {
                            files[0].Delete();
                        }
                        else if (Utils.isArchive(page.Music))
                        {
                            Utils.CopyFiles(pathPagesAudio, page.Music, "music");
                        }
                    }

                    #region picto
                    if (page.Pictograms.Count > 0)
                    {
                        foreach (Pictogram picto in page.Pictograms)
                        {
                            int numPicto = picto.Index + 1;
                            if (picto.ImageName != "")
                            {
                                Utils.CreateDirectory(pathImages);
                                Utils.CreateDirectory(pathPagesImage);
                                FileInfo[] files = Utils.SeachFileDirectory(pathPagesImage, numPicto.ToString());
                                if (files.Length > 0 && !Utils.isArchive(picto.ImageName))
                                {
                                    files[0].Delete();
                                }
                                else
                                {

                                    Utils.CopyFiles(pathPagesImage, picto.ImageName, numPicto.ToString());
                                }
                            }

                            if (picto.Sound != "")
                            {
                                Utils.CreateDirectory(pathAudios);
                                Utils.CreateDirectory(pathPagesAudio);
                                FileInfo[] files = Utils.SeachFileDirectory(pathPagesAudio, numPicto.ToString());
                                if (files.Length > 0 && !Utils.isArchive(picto.Sound))
                                {
                                    files[0].Delete();
                                }
                                else
                                {
                                    Utils.CopyFiles(pathPagesAudio, picto.Sound, numPicto.ToString());
                                }
                            }
                        }
                    }
                    #endregion picto
                }
            }
            #endregion page
        }





        private void TimerTick()
        {
            if (mediaPlayerTale.Source != null)
            {
                if (taleManager.IsFrontPage())
                {
                    lblStatusMusic.Content = String.Format("{0} / {1}", mediaPlayerTale.Position.ToString(@"mm\:ss"), mediaPlayerTale.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
                }

                if (taleManager.IsPage())
                {
                    lblStatusMusicPage.Content = String.Format("{0} / {1}", mediaPlayerTale.Position.ToString(@"mm\:ss"), mediaPlayerTale.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
                }

                if (taleManager.IsPictogram())
                {
                    lblStatusSoundPictogram.Content = String.Format("{0} / {1}", mediaPlayerTale.Position.ToString(@"mm\:ss"), mediaPlayerTale.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
                }
            }
            else
            {
                if (taleManager.IsFrontPage())
                {
                    lblStatusMusic.Content = "Archivo no seleccionado...";
                }

                if (taleManager.IsPage())
                {
                    lblStatusMusicPage.Content = "Archivo no seleccionado...";
                }

                if (taleManager.IsPictogram())
                {
                    lblStatusSoundPictogram.Content = "Archivo no seleccionado...";
                }
            }
        }

        private void ShowMenuGUI()
        {
            if (taleManager == null)
            {
                toolBarPages.Visibility = Visibility.Visible;
                menuCloseTale.IsEnabled = false;
                menuExportTale.IsEnabled = false;
                //menuExportTaleAndroid.IsEnabled = false;
                menuImportTale.IsEnabled = true;
                menuOpenTale.IsEnabled = true;
                menuSaveAsTale.IsEnabled = false;
                menuSaveTale.IsEnabled = false;
                btnOpenFileQuick.IsEnabled = true;
                imgOpenFileQuick.Source = Utils.GetBitmapFromUri("Iconos\\mopen.png", UriKind.Relative);
            }
            else
            {
                toolBarPages.Visibility = Visibility.Visible;
                btnPreview.IsEnabled = true;
                menuImportTale.IsEnabled = false;
                menuExportTale.IsEnabled = true;
                //menuExportTaleAndroid.IsEnabled = true;
                menuCloseTale.IsEnabled = true;
                menuSaveAsTale.IsEnabled = true;

                menuSaveTale.IsEnabled = true;
                btnQuickSave.IsEnabled = true;
                imgQuickSave.Source = Utils.GetBitmapFromUri("Iconos\\msave.png", UriKind.Relative);

                btnAddPage.IsEnabled = true;
                btnQuickAddPage.IsEnabled = true;
                imgQuickAddPage.Source = Utils.GetBitmapFromUri("Iconos\\madd.png", UriKind.Relative);

                if (taleManager.IsFrontPage())
                {
                    btnDeletePage.IsEnabled = false;
                    btnQuickDeletePage.IsEnabled = false;
                    imgQuickDeletePage.Source = Utils.GetBitmapFromUri("Iconos\\msubOff.png", UriKind.Relative);
                }
                else
                {
                    btnDeletePage.IsEnabled = true;
                    btnQuickDeletePage.IsEnabled = true;
                    imgQuickDeletePage.Source = Utils.GetBitmapFromUri("Iconos\\msub.png", UriKind.Relative);
                }

                if (taleManager.HasPage())
                {
                    btnDeletePage.IsEnabled = true;
                    btnQuickDeletePage.IsEnabled = true;
                    imgQuickDeletePage.Source = Utils.GetBitmapFromUri("Iconos\\msub.png", UriKind.Relative);
                }
            }

        }

        private void NewTale()
        {
            taleManager = new TaleManager();
            taleManager.Language = "Español";
            taleManager.DateOfCreation = DateTime.Now;

            taleManager.GoToFrontPage();
            UpdateGUI();
            ShowMenuGUI();

            isNewTale = true;
            isChangedTale = false;
            isImportTale = false;
            isOpenTale = false;
        }

        private void CloseTale()
        {
            isNewTale = false;
            isChangedTale = false;
            isImportTale = false;
            isOpenTale = false;

            StopMusic();
            mediaPlayerTale.Close();

            taleManager = null;

            lblStatusBarTitle.Content = "";
            lblStatusBarNotification.Content = "";
            lblStatusBarPages.Content = "Carga un cuento o crea uno nuevo";

            //--- esto hay que hacerlo si o si ---
            grdFrontPage.Background = null;
            grdPage.Background = null;
            //-------------------------------

            imgFrontPageBackground.Source = null;
            imgPageBackground.Source = null;

            ShowMenuGUI();
            UpdateGUI();

            if (Directory.Exists(locationSaveImport))
            {
                //Recursividad para borrar los ficheros y las carpetas hijas
                String[] chilFolder = Directory.GetDirectories(locationSaveImport);
                foreach (string pathFolder in chilFolder)
                {
                    try
                    {
                        if (Directory.Exists(pathFolder))
                        {
                            Directory.Delete(pathFolder, true);
                        }
                    }
                    catch { }
                }
            }
        }
        private void OpenFileTale()
        {
            LogStore lgOpen;
            String location = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Tale Editor\\";
            Utils.CreateDirectory(location);

            OpenFileDialog file = new OpenFileDialog();
            file.Title = "Abrir cuento";
            file.Filter = "Tproject Proyecto Tale|*.tproject";
            file.InitialDirectory = location;
            file.RestoreDirectory = true;

            if (file.ShowDialog() == true)
            {
                locationOpen = file.FileName;
                lgOpen = new LogStore(locationOpen);
                isOpenTale = true;
                isNewTale = false;
                isImportTale = false;
                isChangedTale = false;

                if (lgOpen != null)
                {
                    taleManager = new TaleManager();
                    taleManager = lgOpen.GetTale();
                    lgOpen.closeXML();
                    isChangedTale = false;
                }

                UpdateGUI();
                ShowTaleGUI();
                ShowMenuGUI();
            }

        }

        private void SaveTale()
        {
            String location = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Tale Editor\\";
            Utils.CreateDirectory(location);
            LogStore lgSave = new LogStore();
            XmlDocument docSave = null;
            XmlDocument docImport = null;

            if (taleManager != null)
            {
                if (isNewTale && !isImportTale)
                {
                    SaveFileDialog saveFile = new SaveFileDialog();
                    saveFile.Filter = "Tproject Proyecto Tale|*.tproject";
                    saveFile.Title = "Guardar cuento";
                    saveFile.FileName = "nuevoCuento";
                    saveFile.InitialDirectory = location;
                    saveFile.RestoreDirectory = true;

                    Nullable<bool> result = saveFile.ShowDialog();

                    if (result == true)
                    {
                        pathSave = saveFile.FileName;
                        if (pathSave != "")
                        {
                            isOpenTale = false;
                            isChangedTale = false;
                            isNewTale = false;
                            isImportTale = false;

                            docSave = lgSave.SaveTaleManagerXML(taleManager);

                            using (var writer = new StreamWriter(pathSave))
                            {
                                docSave.Save(writer);
                            }

                            lgSave.closeXML();

                            isChangedTale = false;
                            isSaveTale = true;
                        }
                    }
                }

                #region importado
                if (isImportTale && !isNewTale)
                {
                    isOpenTale = false;
                    isNewTale = false;
                    isImportTale = false;;

                    String dirTemp = locationSaveImport + "\\" + nameArchiveImport;

                    int tamNameArchiveImportWithExtension = nameArchiveImport.Length + 5;
                    int tamLocationOpenImport = locationOpenImport.Length;
                    String dirOpen = locationOpenImport.Substring(0, tamLocationOpenImport - 5);
                    String dir = locationOpenImport.Substring(0, tamLocationOpenImport - tamNameArchiveImportWithExtension);

                    if (Directory.Exists(dirTemp))
                    {
                        LogStore lgImport = new LogStore();

                        Utils.DeleteDirectory(dirOpen); //elimina el directorio si existe
                        Directory.Move(dirTemp, dirOpen);

                        ChangeNameFiles(); //lo paso a relativos

                        ChangeToAbsolutePath(dirOpen);

                        docImport = lgImport.SaveTaleManagerXML(taleManager);

                        using (var writer = new StreamWriter(dirOpen + "\\index.xml"))
                        {
                            docImport.Save(writer);
                        }

                        lgImport.closeXML();

                        isChangedTale = false;

                        if (File.Exists(dir + "\\" + nameArchiveImport + ".tproject"))
                        {
                            File.Delete(dir + "\\" + nameArchiveImport + ".tproject");
                        }

                        File.Copy(dirOpen + "\\index.xml", dir + "\\" + nameArchiveImport + ".tproject");
                    }

                }
                #endregion importado

                if (isOpenTale || isChangedTale)
                {
                    isOpenTale = false;
                    
                    isNewTale = false;
                    isImportTale = false;
                    if (File.Exists(locationOpen))
                    {
                        File.Delete(locationOpen);
                        docSave = lgSave.SaveTaleManagerXML(taleManager);
                        using (var writer = new StreamWriter(locationOpen))
                        {
                            docSave.Save(writer);
                        }

                        lgSave.closeXML();
                        isChangedTale = false;
                        //lblStatusBarNotification.Content = "cuento guardado";
                    }


                }

            }
        }

        private void PlayMusicTale(String path)
        {
            mediaPlayerTale.Open(new Uri(path));
            mediaPlayerTale.Play();

            timerMusic.Start();
        }
        #endregion funciones



        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if (speak != null)
            {
                speak.SpeakerStop();
            }
        }



        private void btnSentence_Click(object sender, RoutedEventArgs e)
        {
            if (!hasSentence)
            {
                getSentence(txtSentence.Text);
                hasSentence = true;
                btnSentence.Content = "x";
            }
            else
            {
                btnSentence.Content = "...";

                taleManager.RemovePictogramsPage(taleManager.CurrentPageIndex);
                CleanPictograms();

                txtSentence.Text = "";

                hasSentence = false;
            }

        }



        private void txtSentence_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (!hasSentence)
                {
                    getSentence(txtSentence.Text);
                    hasSentence = true;
                    btnSentence.Content = "x";
                }
                else
                {
                    btnSentence.Content = "...";

                    taleManager.RemovePictogramsPage(taleManager.CurrentPageIndex);
                    CleanPictograms();

                    txtSentence.Text = "";

                    hasSentence = false;
                }
            }

        }

        private void drawPicto(Word w, int posPicto)
        {
            if (posPicto >= 0 && posPicto < 10)
            {
                String uri = "";
                // word = "";

                String nombreImagenNumero = w.NameImage;

                Pictogram p = new Pictogram(posPicto);
                p.ImageName = nombreImagenNumero;
                p.Word = w.WordImage;
                p.TextToRead = w.WordImage;
                p.Type = w.Type;

                if (nombreImagenNumero != null && nombreImagenNumero != "")
                {
                    uri = Environment.CurrentDirectory + "/Galeria/" + nombreImagenNumero.Substring(0, 1) + "/" + nombreImagenNumero;
                    p.ImageName = uri;
                    images[posPicto].Source = Utils.GetBitmapFromUri(uri, UriKind.Absolute);
                    labelsPic[posPicto].Visibility = Visibility.Hidden;
                    labelsNumber[posPicto].Background = Brushes.White;
                }
                
                EditBorder(borders[posPicto], p.getColorByType(p.Type), 4);
                labelsWord[posPicto].Content = p.Word;


                taleManager.CurrentPage.InsertPictogram(p);
                isChangedTale = true;
            }
        }

        private void getSentence(String text)
        {

            String word = "";
            bool hasVowels;

            int posPicto = 0;

            CleanSelected();

            List<Word> words = dbManager.GetSentence(Utils.CutSentence(text)); // ESTO HACE LA BUSQUEDA NO?

            if (words != null)
            {
                if (words.Count > 0)
                {
                    foreach (Word w in words)
                    {
                        if (posPicto <= 10)
                        {
                            if (word != w.WordImage)
                            {
                                hasVowels = Utils.hasVowel(w.WordImage);

                                if (!hasVowels)
                                {
                                    drawPicto(w, posPicto);
                                    posPicto++;
                                }
                                else
                                {
                                    drawPicto(w, posPicto);
                                    posPicto++;
                                }
                            }

                            word = w.WordImage;
                        }
                        else
                        {
                            MessageBox.Show("La frase es demasiado larga");
                        }
                    }

                    UpdatePageGUI();
                    ShowPageGUI();

                    GetReadingElement();
                }
                else
                {
                    MessageBox.Show("No se encontraron resultados!");
                }
            }

        }

        private void menuExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool exitTale()
        {
            bool ret = false;
            if (taleManager != null)
            {
                if (isChangedTale)
                {
                    MessageBoxResult result = MessageBox.Show("¿Desea guardar cambios en el cuento actual antes de salir?", "Tale Editor", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        SaveTale();
                        ret = true;
                    }
                    if (result == MessageBoxResult.No)
                    {
                        ret = true;
                    }
                }
                else
                {
                    ret = true;
                }
            }
            else
            {
                ret = true;
            }

            return ret;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool really = exitTale();
            if (!really)
            {
                e.Cancel = true;
            }
        }

        private void btnDeleteImageGalery_Click(object sender, RoutedEventArgs e)
        {
            if (txtTextToSearchPic.Text != "")
            {
                if (taleManager.PictogramExist())
                {
                    taleManager.CurrentPictogram.Word = "";
                    taleManager.CurrentPictogram.ImageName = "";
                }


                clearImagePicto();
                cmbImgPic.IsEnabled = false;
                cmbImgPic.Items.Clear();
                txtTextToSearchPic.Text = "";

            }
        }

        private void clearImagePicto()
        {
            images[taleManager.CurrentPictogramIndex].Source = null;
            EditBorder(borders[taleManager.CurrentPictogramIndex], Brushes.DarkGray, 1);
            labelsNumber[taleManager.CurrentPictogramIndex].Background = Brushes.Transparent;
            labelsPic[taleManager.CurrentPictogramIndex].Visibility = Visibility.Visible;
        }

        private void txtTextToSearchPic_MouseMove(object sender, MouseEventArgs e)
        {
            txtTextToSearchPic.BorderBrush = Brushes.DarkGray;
        }

        private void txtTextToSearchPic_MouseLeave(object sender, MouseEventArgs e)
        {
            txtTextToSearchPic.BorderBrush = Brushes.DarkGray;
        }

        private void txtTextToSearchPic_MouseEnter(object sender, MouseEventArgs e)
        {
            txtTextToSearchPic.BorderBrush = Brushes.DarkGray;
        }

        private void txtTextToSearchPic_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtTextToSearchPic.BorderBrush = Brushes.DarkGray;
        }

        private void txtTextToSearchPic_MouseUp(object sender, MouseButtonEventArgs e)
        {
            txtTextToSearchPic.BorderBrush = Brushes.DarkGray;
            //btnDeleteImageGalery.BorderBrush = Brushes.Blue;
        }

        private void txtTextToSearchPic_GotFocus(object sender, RoutedEventArgs e)
        {
            //txtTextToSearchPic.BorderBrush = Brushes.DarkGray;
        }

        private void txtTextToSearchPic_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //txtTextToSearchPic.BorderBrush = Brushes.DarkGray;
        }

        /*private void menuExportTaleAndroid_Click(object sender, RoutedEventArgs e)
        {
            if (taleManager != null)
            {


                String pathSaveExportAndroid = "";
                String location = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Tale Editor Android\\";
                XmlDocument docExportAndroid = null;
                LogStore lgExportAndroid = new LogStore();

                Utils.CreateDirectory(location);

                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.InitialDirectory = location;
                saveFile.Filter = "Tale Tale|*.tale";
                saveFile.Title = "Exportar cuento para Android";
                saveFile.FileName = "nuevoCuento";
                saveFile.RestoreDirectory = true;

                Nullable<bool> result = saveFile.ShowDialog();

                if (result == true)
                {
                    if (saveFile.FileName != "")
                    {
                        try
                        {
                            pathSaveExportAndroid = saveFile.FileName;
                            String name = saveFile.SafeFileName;

                            int tam = pathSaveExportAndroid.Length - 5;
                            String nameDirectory = pathSaveExportAndroid.Substring(0, tam);
                            string zipPath = pathSaveExportAndroid;

                            CopyArchivesAndroid(nameDirectory);
                            ChangeNameFilesAndroid();

                            docExportAndroid = lgExportAndroid.SaveTaleManagerXML(taleManager, nameDirectory, "index.xml");

                            CompressFile(location, nameDirectory, zipPath, name);

                            Utils.DeleteDirectory(nameDirectory);

                            CloseTale();
                        }
                        catch (Exception)
                        { }

                        lgExportAndroid.closeXML();

                        isChangedTale = false;
                    }
                }
            }
        }*/


        /*private void ChangeToAbsolutePathAndroid()
        {
            //cuento
            if (taleManager.Background != "" && Utils.isArchive(taleManager.Background))
            {
                taleManager.Background = "img0_" + taleManager.Background;
            }
            if (taleManager.Music != "" && Utils.isArchive(taleManager.Music))
            {
                taleManager.Music = "music0_" + taleManager.Music;
            }
            //páginas
            foreach (TPage page in taleManager.GetPages)
            {
                int numPage = page.Index;
                if (page.Background != "" && Utils.isArchive(page.Background))
                {
                    page.Background = "img" + numPage + "_" + page.Background;
                }
                if (page.Music != "" && Utils.isArchive(page.Music))
                {
                    page.Music = "music" + numPage + "_" + page.Music;
                }

                foreach (Pictogram picto in page.Pictograms)
                {
                    if (picto.ImageName != "" && Utils.isArchive(picto.ImageName))
                    {
                        picto.ImageName = "img" + numPage + "_" + picto.ImageName;
                    }
                    if (picto.Sound != "" && Utils.isArchive(picto.Sound))
                    {
                        picto.Sound = "music" + numPage + "_" + picto.Sound;
                    }
                }
            }
        }*/


        /*private void ChangeNameFilesAndroid()
        {
            //cuento
            if (taleManager.Background != "" && Utils.isArchive(taleManager.Background))
            {
                taleManager.Background = Utils.ChangeToRelativePath(taleManager.Background);
                taleManager.Background = "img0_" + "bg" + "." + Utils.GetExtension(taleManager.Background);
            }
            if (taleManager.Music != "")
            {
                taleManager.Music = Utils.ChangeToRelativePath(taleManager.Music);
                taleManager.Music = "music0_" + "music" + "." + Utils.GetExtension(taleManager.Music);
            }

            //páginas
            foreach (TPage page in taleManager.GetPages)
            {
                int numPage = page.Index + 1;

                if (page.Background != "" && Utils.isArchive(page.Background))
                {
                    page.Background = Utils.ChangeToRelativePath(page.Background);
                    page.Background = "img" + numPage + "_" + "bg" + "." + Utils.GetExtension(page.Background);
                }
                if (page.Music != "")
                {
                    page.Music = Utils.ChangeToRelativePath(page.Music);
                    page.Music = "music" + numPage + "_" + "music" + "." + Utils.GetExtension(page.Music);
                }

                foreach (Pictogram picto in page.Pictograms)
                {
                    int numPicto = picto.Index + 1;

                    if (picto.ImageName != "")
                    {
                        picto.ImageName = Utils.ChangeToRelativePath(picto.ImageName);
                        picto.ImageName = "img" + numPage + "_" + numPicto.ToString() + "." + Utils.GetExtension(picto.ImageName);
                    }
                    if (picto.Sound != "")
                    {
                        picto.Sound = Utils.ChangeToRelativePath(picto.Sound);
                        picto.Sound = "music" + numPage + "_"  + numPicto.ToString() + "." + Utils.GetExtension(picto.Sound);
                    }
                }
            }
        }*/



        /*private void CopyArchivesAndroid(String location)
        {
            String path = location;
            Utils.CreateDirectory(path);
            String pathImages = location;
            String pathAudios = location;
            String pathFrontPageImage = location;
            String pathFrontPageAudio = location;

            #region music
            if (taleManager.Music != "")
            {
                Utils.CreateDirectory(pathAudios);
                Utils.CreateDirectory(pathFrontPageAudio);
                FileInfo[] files = Utils.SeachFileDirectory(path, "music0_" + "music");

                if (files.Length > 0 && (!Utils.isArchive(taleManager.Music)))
                {
                    files[0].Delete();
                }
                else if (Utils.isArchive(taleManager.Music))
                {
                    Utils.CopyFiles(pathFrontPageAudio, taleManager.Music, "music0_" + "music");
                }
            }
            #endregion music

            #region back
            if (taleManager.Background != "")
            {
                Utils.CreateDirectory(pathImages);
                Utils.CreateDirectory(pathFrontPageImage);
                FileInfo[] files = Utils.SeachFileDirectory(path, "img0_" + "bg");

                if (files.Length > 0 && (!Utils.isArchive(taleManager.Background)))
                {
                    files[0].Delete();
                }
                else if (Utils.isArchive(taleManager.Background))
                {
                    Utils.CopyFiles(pathFrontPageImage, taleManager.Background, "img0_" + "bg");
                }
            }
            #endregion back

            #region page

            if (taleManager.NumberOfPages > 0)
            {
                foreach (TPage page in taleManager.GetPages)
                {
                    int numPage = page.Index + 1;
                    String pathPagesImage = location;
                    String pathPagesAudio = location;
                    if (page.Background != "")
                    {
                        Utils.CreateDirectory(pathImages);
                        Utils.CreateDirectory(pathPagesImage);
                        FileInfo[] files = Utils.SeachFileDirectory(pathPagesImage, "img" + numPage+ "_" + "bg");
                        if (files.Length > 0 && (!Utils.isArchive(page.Background)))
                        {
                            files[0].Delete();
                        }
                        else if (Utils.isArchive(page.Background))
                        {
                            Utils.CopyFiles(pathPagesImage, page.Background, "img" + numPage + "_" + "bg");
                        }

                        
                    }

                    if (page.Music != "")
                    {
                        Utils.CreateDirectory(pathAudios);
                        Utils.CreateDirectory(pathPagesAudio);
                        FileInfo[] files = Utils.SeachFileDirectory(pathPagesAudio, "music" + numPage + "_" +  "music");
                        if (files.Length > 0 && (!Utils.isArchive(page.Music)))
                        {
                            files[0].Delete();
                        }
                        else if (Utils.isArchive(page.Music))
                        {
                            Utils.CopyFiles(pathPagesAudio, page.Music, "music" + numPage + "_" + "music");
                        }
                    }

                    #region picto
                    if (page.Pictograms.Count > 0)
                    {
                        foreach (Pictogram picto in page.Pictograms)
                        {
                            int numPicto = picto.Index + 1;
                            if (picto.ImageName != "")
                            {
                                Utils.CreateDirectory(pathImages);
                                Utils.CreateDirectory(pathPagesImage);
                                FileInfo[] files = Utils.SeachFileDirectory(pathPagesImage, "img" + numPage + "_" + numPicto.ToString());
                                if (files.Length > 0 && !Utils.isArchive(picto.ImageName))
                                {
                                    files[0].Delete();
                                }
                                else
                                {

                                    Utils.CopyFiles(pathPagesImage, picto.ImageName, "img" + numPage + "_" + numPicto.ToString());
                                }
                            }

                            if (picto.Sound != "")
                            {
                                Utils.CreateDirectory(pathAudios);
                                Utils.CreateDirectory(pathPagesAudio);
                                FileInfo[] files = Utils.SeachFileDirectory(pathPagesAudio, "music" + page.Index + "_" + numPicto.ToString());
                                if (files.Length > 0 && !Utils.isArchive(picto.Sound))
                                {
                                    files[0].Delete();
                                }
                                else
                                {
                                    Utils.CopyFiles(pathPagesAudio, picto.Sound, "music" + page.Index + "_" + numPicto.ToString());
                                }
                            }
                        }
                    }
                    #endregion picto
                }
            }
            #endregion page
        }*/


        /*private void btnFontFrontPage_Click(object sender, RoutedEventArgs e)
        {
            


        }

        private void ChooseFont(Label label)
        {
            
            System.Windows.Forms.FontDialog f = new System.Windows.Forms.FontDialog();
            f.ShowEffects = false;
            f.AllowScriptChange = false;
            f.ShowDialog();

            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //label.FontFamily = f.Font;
            }
            else
            {
                /*FontDialog objFontDialog = new FontDialog();
                objFontDialog.ShowDialog();
                Font objSelectedFont = objFontDialog.Font;
                FontFamilyConverter o = new FontFamilyConverter();
                textBox1.FontFamily = (System.Windows.Media.FontFamily)o.ConvertFromString(objSelectedFont.Name);*/
        //}

        //}

    }
}
