using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;

using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;
using System.Collections.Generic;
using System.Windows.Media.Animation;

namespace ProjectionChants
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserDataContext udc;
        private DataBaseManagement data;
        private bool isLoading;
        private int selectedLine;
        private ProjectionWindow projection;
        private DataTable allData;
        private bool isProjectionSync = false;

        public MainWindow()
        {
            InitializeComponent();
            data = new DataBaseManagement();
            udc = (UserDataContext)this.DataContext;

            // initialize comboTheme
            foreach (Theme t in Enum.GetValues(typeof(Theme)))
            {
                ComboBoxItem cbItem = new ComboBoxItem();
                cbItem.Content = t.ToString();
                comboTheme.Items.Add(cbItem);
            }
            transformCommandScale.ScaleY = 0;
        }

        public bool MaximizeToSecondaryMonitor(Window window)
        {

            var secondaryScreen = System.Windows.Forms.Screen.AllScreens.Where(s => !s.Primary).FirstOrDefault();

            if (secondaryScreen != null)
            {
                if (!window.IsLoaded) window.WindowStartupLocation = WindowStartupLocation.Manual;
                window.WindowStyle = System.Windows.WindowStyle.None;
                window.ResizeMode = System.Windows.ResizeMode.NoResize;
                var workingArea = secondaryScreen.WorkingArea;
                window.Left = workingArea.Left;
                window.Top = workingArea.Top;
                window.Width = workingArea.Width;
                window.Height = workingArea.Height;
                if (window.IsLoaded) window.WindowState = WindowState.Maximized;
                return true;
            }
            else
            {
                //MessageBox.Show("Aucun projecteur détecté", "", MessageBoxButton.OK, MessageBoxImage.Error);
                window.WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
                window.ResizeMode = System.Windows.ResizeMode.CanResize;
                return false;
            }
        }

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            projection = new ProjectionWindow();
            projection.Owner = this;
            if (MaximizeToSecondaryMonitor(projection))
            {
                projection.Show();
            }
            else
            {
                menuStopProjection.IsChecked = true;
            }
            _fontFamily.ItemsSource = System.Windows.Media.Fonts.SystemFontFamilies;
            _fontSize.ItemsSource = FontSizes;

            udc.isTabVisible = true;
            loadList();
            resetView();
            textNumero.Focus();
        }

        private void loadList()
        {
            isLoading = true;
            allData = data.LoadSongs();
            gridChants.ItemsSource = allData.DefaultView;
            gridChants.Columns[1].SortDirection = ListSortDirection.Ascending;
            ICollectionView dataView = CollectionViewSource.GetDefaultView(gridChants.ItemsSource);
            dataView.SortDescriptions.Clear();
            dataView.SortDescriptions.Add(new SortDescription("nom", ListSortDirection.Ascending));
            dataView.Refresh();
            gridChants.SelectedIndex = 0;
            this.Title = Properties.Resources.appTitle + " (" + data.getDataFileName() + ")";
            Storyboard closeCommands = (Storyboard)FindResource("closeCommands");
            closeCommands.Begin(this);
            isLoading = false;
        }

        private void gridChants_SelectedCellsChanged(object sender, Microsoft.Windows.Controls.SelectedCellsChangedEventArgs e)
        {
            if (isLoading || udc.isFiltering) return;
            
            if (e.AddedCells.Count > 0)
            {
                selectedLine = gridChants.SelectedIndex;
                loadRow((DataRowView)e.AddedCells[0].Item);
            }
        }

        private void loadRow(DataRowView dvr)
        {
            udc.Id = dvr.Row.Field<int>("id");
            udc.Titre = dvr.Row.Field<string>("nom");
            udc.Theme = dvr.Row.Field<string>("theme");
            udc.Cle = dvr.Row.Field<string>("cle");
            udc.Numero = dvr.Row.Field<string>("numero");
            udc.Refrain = dvr.Row.Field<string>("refrain");
            RtbHelper.loadRtb(rtbRefrain, udc.Refrain);
            udc.RefrainSize = dvr.Row.Field<int>("ref_size");
            udc.RefrainTab = dvr.Row.Field<string>("tab");
            RtbHelper.loadRtb(rtbRefrainTab, udc.RefrainTab);
            udc.RefrainTabSize = dvr.Row.Field<int>("tab_size");
            udc.NbCouplet = dvr.Row.Field<int>("nb_couplet");
            udc.Couplets = loadCouplets(udc.Id);
            // limite à 1 couplet
            if (udc.Couplets.Count == 0)
            {
                udc.Couplets.Add(1, new Couplet(1));
            }
            Couplet c = udc.Couplets[1];
            udc.Couplet = c.Text;
            RtbHelper.loadRtb(rtbCouplet, udc.Couplet);
            udc.CoupletTab = c.Tab;
            RtbHelper.loadRtb(rtbCoupletTab, udc.CoupletTab);
            udc.CoupletTabSize = c.TabSise;
            isProjectionSync = false;
        }

        private SortedList<int, Couplet> loadCouplets(int songId)
        {
            DataTable dtCouplet = data.LoadVerse(songId);
            DataTableReader dtr = dtCouplet.CreateDataReader();
            SortedList<int, Couplet> listCouplets = new SortedList<int, Couplet>();
            while (dtr.Read())
            {
                Couplet c = new Couplet(dtr.GetInt32(dtr.GetOrdinal("couplet_id")), dtr.GetInt32(dtr.GetOrdinal("id")));
                c.Text = dtr.GetString(dtr.GetOrdinal("couplet"));
                c.Tab = dtr.GetString(dtr.GetOrdinal("tab"));
                c.TabSise = dtr.GetInt32(dtr.GetOrdinal("tab_size"));
                listCouplets.Add(c.Numero, c);
            }
            return listCouplets;
        }

        private void ajouterChant_Click(object sender, RoutedEventArgs e)
        {
            // passage en mode nouveau
            udc.modeEdition = true;
            udc.modeNouveau = true;
            //transformCommandScale.ScaleY = 1;
            udc.Id = 0;
            udc.Numero = "";
            udc.Titre = "";
            udc.Theme = "";
            udc.Cle = "";
            udc.Refrain = "";
            udc.RefrainTab = "";
            udc.NbCouplet = 1;
            udc.Couplet = "";
            udc.CoupletTab = "";
            udc.RefrainSize = 300;
            udc.RefrainTabSize = 150;
            udc.CoupletTabSize = 150;
            udc.Couplets = new SortedList<int, Couplet>();
            udc.Couplets.Add(1, new Couplet(1));
            RtbHelper.loadRtb(rtbRefrain, "");
            RtbHelper.loadRtb(rtbRefrainTab, "");
            RtbHelper.loadRtb(rtbCouplet, "");
            RtbHelper.loadRtb(rtbCoupletTab, "");
        }

        private void modifierChant_Click(object sender, RoutedEventArgs e)
        {
            // passage en mode édition
            udc.modeEdition = true;
            //transformCommandScale.ScaleY = 1;

        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            udc.Refrain = RtbHelper.saveRtb(rtbRefrain);
            udc.RefrainTab = RtbHelper.saveRtb(rtbRefrainTab);
            udc.Couplet = RtbHelper.saveRtb(rtbCouplet);
            udc.CoupletTab = RtbHelper.saveRtb(rtbCoupletTab);

            try
            {

                if (udc.Refrain.Length > 4000)
                {
                    MessageBox.Show("Le refrain est trop long pour être enregistré");
                    return;
                }

                if (udc.Couplet.Length > 4000)
                {
                    MessageBox.Show("Le couplet est trop long pour être enregistré");
                    return;
                }



                // couplets
                udc.Couplets[1].Text = udc.Couplet;
                udc.Couplets[1].Tab = udc.CoupletTab;
                udc.Couplets[1].TabSise = udc.CoupletTabSize;

                if (udc.modeNouveau)
                {
                    // insert
                    data.InsertSong(udc.Titre, udc.Theme, udc.Cle, udc.Numero, udc.Refrain, udc.RefrainSize, udc.RefrainTab, udc.RefrainTabSize, udc.Couplets);
                    loadList();
                }
                else if (udc.modeEdition)
                {
                    // update
                    data.UpdateSong(udc.Titre, udc.Theme, udc.Cle, udc.Numero, udc.Refrain, udc.RefrainSize, udc.RefrainTab, udc.RefrainTabSize, udc.Couplets, udc.Id);
                    loadList();
                }
            }
            finally
            {
                resetView();
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            resetView();
        }

        private void resetView()
        {
            // passage en mode normal
            udc.modeEdition = false;
            udc.modeNouveau = false;
            udc.isFiltering = false;
            gridChants.ItemsSource = allData.DefaultView;
            if (selectedLine >= 0)
            {
                gridChants.SelectedIndex = selectedLine;
                loadRow((DataRowView)gridChants.SelectedItem);
            }
            gridChants.Focus();
            
        }

        /*
        private void mainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            // test projection hotkeys
           


            if (!udc.modeEdition && !udc.modeNouveau && !udc.isFiltering)
            {
                if (e.Key == Key.N) {
                    projection.setBlack();
                }
                else if (e.Key == Key.B)
                {
                    projection.setWhite();
                }
            }
        }
        */

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            // suppression
            if (MessageBox.Show("Vraiment supprimer ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                // delete
                data.DeleteSong(udc.Id);
                loadList();
                resetView();
            }
        }

        private void textBoxSelectionOnGotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void comboTheme_GotFocus(object sender, RoutedEventArgs e)
        {
            ((ComboBox)sender).IsDropDownOpen = true;
        }

        private void mainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            //projection.Close();
        }

        private void buttonProjection_Click(object sender, RoutedEventArgs e)
        {
            projection.setProjectedText(udc.Refrain, udc.RefrainTab, udc.isTabVisible ? udc.RefrainTabSize : 0, udc.RefrainSize, udc.Couplet, udc.CoupletTab, udc.isTabVisible ? udc.CoupletTabSize : 0);
            isProjectionSync = true;
        }

        private Object Clone(Object o)
        {
            string xamlCode = XamlWriter.Save(o);
            return XamlReader.Load(new XmlTextReader(new StringReader(xamlCode)));
        }

        private void gridChants_Sorting(object sender, Microsoft.Windows.Controls.DataGridSortingEventArgs e)
        {
            
        }

        private void menuStopProjection_Checked(object sender, RoutedEventArgs e)
        {
            projection.Hide();
        }

        private void menuStopProjection_Unchecked(object sender, RoutedEventArgs e)
        {
            projection.Show();
        }

        public double[] FontSizes
        {
            get
            {
                return new double[] { 
		            3.0, 4.0, 5.0, 6.0, 6.5, 7.0, 7.5, 8.0, 8.5, 9.0, 9.5, 
		            10.0, 10.5, 11.0, 11.5, 12.0, 12.5, 13.0, 13.5, 14.0, 15.0,
		            16.0, 17.0, 18.0, 19.0, 20.0, 22.0, 24.0, 26.0, 28.0, 30.0,
		            32.0, 34.0, 36.0, 38.0, 40.0, 44.0, 48.0, 52.0, 56.0, 60.0, 64.0, 68.0, 72.0, 76.0,
		            80.0, 88.0, 96.0, 104.0, 112.0, 120.0, 128.0, 136.0, 144.0
		            };
            }
        }

        private void rtb_GotFocus(object sender, RoutedEventArgs e)
        {
            udc.SelectedRtb = (RichTextBox)sender;
        }

        private void rtb_SelectionChanged(object sender, RoutedEventArgs e)
        {
            UpdateVisualState((RichTextBox)sender);
        }
        
        private void FontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;
            FontFamily editValue = (FontFamily)e.AddedItems[0];
            ApplyPropertyValueToSelectedText(udc.SelectedRtb, TextElement.FontFamilyProperty, editValue);
        }

        private void FonSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
                ApplyPropertyValueToSelectedText(udc.SelectedRtb, TextElement.FontSizeProperty, e.AddedItems[0]);
        }

        private void UpdateVisualState(RichTextBox rtb)
        {
            UpdateToggleButtonState(rtb);
            UpdateSelectionListType(rtb);
            UpdateSelectedFontFamily(rtb);
            UpdateSelectedFontSize(rtb);
        }

        private void UpdateToggleButtonState(RichTextBox rtb)
        {
            UpdateItemCheckedState(rtb, _btnBold, TextElement.FontWeightProperty, FontWeights.Bold);
            UpdateItemCheckedState(rtb, _btnItalic, TextElement.FontStyleProperty, FontStyles.Italic);
            UpdateItemCheckedState(rtb, _btnUnderline, Inline.TextDecorationsProperty, TextDecorations.Underline);

            UpdateItemCheckedState(rtb, _btnAlignLeft, Paragraph.TextAlignmentProperty, TextAlignment.Left);
            UpdateItemCheckedState(rtb, _btnAlignCenter, Paragraph.TextAlignmentProperty, TextAlignment.Center);
            UpdateItemCheckedState(rtb, _btnAlignRight, Paragraph.TextAlignmentProperty, TextAlignment.Right);
        }

        void UpdateItemCheckedState(RichTextBox rtb, ToggleButton button, DependencyProperty formattingProperty, object expectedValue)
        {
            object currentValue = rtb.Selection.GetPropertyValue(formattingProperty);
            button.IsChecked = (currentValue == DependencyProperty.UnsetValue) ? false : currentValue != null && currentValue.Equals(expectedValue);
        }

        private void UpdateSelectionListType(RichTextBox rtb)
        {
            Paragraph startParagraph = rtb.Selection.Start.Paragraph;
            Paragraph endParagraph = rtb.Selection.End.Paragraph;
            if (startParagraph != null && endParagraph != null && (startParagraph.Parent is ListItem) && (endParagraph.Parent is ListItem) && object.ReferenceEquals(((ListItem)startParagraph.Parent).List, ((ListItem)endParagraph.Parent).List))
            {
                TextMarkerStyle markerStyle = ((ListItem)startParagraph.Parent).List.MarkerStyle;
                if (markerStyle == TextMarkerStyle.Disc) //bullets
                {
                    _btnBullets.IsChecked = true;
                }
                else if (markerStyle == TextMarkerStyle.Decimal) //numbers
                {
                    _btnNumbers.IsChecked = true;
                }
            }
            else
            {
                _btnBullets.IsChecked = false;
                _btnNumbers.IsChecked = false;
            }
        }

        private void UpdateSelectedFontFamily(RichTextBox rtb)
        {
            object value = rtb.Selection.GetPropertyValue(TextElement.FontFamilyProperty);
            FontFamily currentFontFamily = (FontFamily)((value == DependencyProperty.UnsetValue) ? null : value);
            if (currentFontFamily != null)
            {
                _fontFamily.SelectedItem = currentFontFamily;
            }
        }

        private void UpdateSelectedFontSize(RichTextBox rtb)
        {
            object value = rtb.Selection.GetPropertyValue(TextElement.FontSizeProperty);
            _fontSize.SelectedValue = (value == DependencyProperty.UnsetValue) ? null : value;
        }


        void ApplyPropertyValueToSelectedText(RichTextBox rtb, DependencyProperty formattingProperty, object value)
        {
            if (value == null)
                return;

            rtb.Selection.ApplyPropertyValue(formattingProperty, value);
        }

        private void rtbRefrain_Loaded(object sender, RoutedEventArgs e)
        {
            var scrollViewer = VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(rtbRefrain, 0), 0) as ScrollViewer;
            scrollViewer.ScrollChanged += (scroller, eScroller) =>
            {
                if (isProjectionSync)
                {
                    projection.setRefrainScrollOffsets(rtbRefrain.VerticalOffset, rtbRefrain.HorizontalOffset);
                }
            };
        }

        private void rtbCouplet_Loaded(object sender, RoutedEventArgs e)
        {
            var scrollViewer = VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(rtbCouplet, 0), 0) as ScrollViewer;
            scrollViewer.ScrollChanged += (scroller, eScroller) =>
            {
                if (isProjectionSync)
                {
                    projection.setCoupletScrollOffsets(rtbCouplet.VerticalOffset, rtbCouplet.HorizontalOffset);
                }
            };
        }


        /*  
         * fonctions de filtrage 
         */
         
        private void filterList(string rowName, string filterValue)
        {
            var results = from myRow in allData.AsEnumerable() where myRow.Field<string>(rowName).Contains(filterValue) select myRow;
            DataTable dt = results.CopyToDataTable<DataRow>();
            gridChants.ItemsSource = dt.DefaultView;
        }

       
        private void textNumero_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            handleFilter("numero", textNumero.Text);
        }

        private SortedList<string, string> columnFilters = new SortedList<string, string>();
        private void handleFilter(string columnName, string filterValue)
        {
            if (!udc.modeEdition)
            {
                if ((columnName == null) || (columnName.Length == 0)) return;
                if (columnName == "*")
                {
                    // réinitialiser le filtrage
                    foreach (string c in columnFilters.Keys) { columnFilters[c] = ""; }
                    resetView();
                }
                else
                {
                    columnFilters[columnName] = filterValue;
                }


            }
        }


        /* impression de la liste
         */

        private void menuPrintList_Click(object sender, RoutedEventArgs e)
        {
            PrintListWindow plw = new PrintListWindow(allData);
            plw.ShowDialog();
        }

        private void menuChooseList_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".sdf";
            dlg.Filter = "Liste de chants (.sdf)|*.sdf";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                data.setDataFileName(dlg.FileName);
                loadList();
                resetView();
            }
        }

       
    }
    

    public sealed class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var flag = false;
            if (value is bool)
            {
                flag = (bool)value;
            }
            else if (value is bool?)
            {
                var nullable = (bool?)value;
                flag = nullable.GetValueOrDefault();
            }
            if (parameter != null)
            {
                if (bool.Parse((string)parameter))
                {
                    flag = !flag;
                }
            }
            return (flag ? Visibility.Visible : Visibility.Collapsed);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var back = ((value is Visibility) && (((Visibility)value) == Visibility.Visible));
            if (parameter != null)
            {
                if ((bool)parameter)
                {
                    back = !back;
                }
            }
            return back;
        }
    }

    public sealed class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var flag = (bool)value;
            return !flag;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var flag = (bool)value;
            return !flag;
        }
    }
}
