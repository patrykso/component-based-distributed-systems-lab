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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AxWMPLib;
using WMPLib;

namespace HostingAxInWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void grid1_Loaded(object sender, RoutedEventArgs e)
        {
            Window_Loaded(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host =
                new System.Windows.Forms.Integration.WindowsFormsHost();

            // Create the ActiveX control.
            // WMPLib.AxWindowsMediaPlayer axWmp = new AxWmpLib.AxWindowsMediaPlayer();
            //WMPLib.IWMPPlayerApplication axWmp = new WMPLib.IWMPPlayerApplication();
            AxWindowsMediaPlayer axWmp = new AxWindowsMediaPlayer();

            // Assign the ActiveX control as the host control's child.
            host.Child = axWmp;

            // Add the interop host control to the Grid
            // control's collection of child controls.
            this.grid1.Children.Add(host);

            // Play a .wav file with the ActiveX control.
            axWmp.URL = @"D:\191711\Lab3\7\Ring01.wav";
        }
    }
}
