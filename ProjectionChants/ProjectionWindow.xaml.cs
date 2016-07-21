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

namespace ProjectionChants
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class ProjectionWindow : Window
    {
        public ProjectionWindow()
        {
            InitializeComponent();
        }

        public void setProjectedText(string refrain, string refrain_tab, int refrain_tab_size, int refrain_size, string couplet, string couplet_tab, int couplet_tab_size)
        {
            RtbHelper.loadRtb(rtbRefrain, refrain);
            RtbHelper.loadRtb(rtbRefrainTab, refrain_tab);
            columnRefrainTab.Width = new GridLength(refrain_tab_size);
            rowRefrain.Height = new GridLength(refrain_size);
            RtbHelper.loadRtb(rtbCouplet, couplet);
            RtbHelper.loadRtb(rtbCoupletTab, couplet_tab);
            columnCoupletTab.Width = new GridLength(couplet_tab_size);
        }

        public void setProjectionObject(Object o)
        {
            viewProjection.Child = (UIElement)o;
        }

        public void setRefrainScrollOffsets(double vOffset, double hOffset)
        {
            rtbRefrain.ScrollToVerticalOffset(vOffset);
            rtbRefrain.ScrollToHorizontalOffset(hOffset);
        }

        public void setCoupletScrollOffsets(double vOffset, double hOffset)
        {
            rtbCouplet.ScrollToVerticalOffset(vOffset);
            rtbCouplet.ScrollToHorizontalOffset(hOffset);
        }

        private void Projection_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        public void setBlack()
        {
            whiteRectangle.Visibility = System.Windows.Visibility.Hidden;

            if (blackRectangle.Visibility == System.Windows.Visibility.Visible)
            {
                blackRectangle.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                blackRectangle.Visibility = System.Windows.Visibility.Visible;
              //  blackRectangle.Height = Projection.Height;
               // blackRectangle.Width = Projection.Width;
            }

        }

        public void setWhite()
        {
            blackRectangle.Visibility = System.Windows.Visibility.Hidden;

            if (whiteRectangle.Visibility == System.Windows.Visibility.Visible)
            {
                whiteRectangle.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                whiteRectangle.Visibility = System.Windows.Visibility.Visible;
            }
        }

        
    }
}
