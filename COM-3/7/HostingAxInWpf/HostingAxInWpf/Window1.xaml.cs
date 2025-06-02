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
using AxAcroPDFLib;

namespace HostingAxInWpf
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void grid2_Loaded(object sender, RoutedEventArgs e)
        {
            Window_Loaded(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host =
                new System.Windows.Forms.Integration.WindowsFormsHost();

            // Create the ActiveX control.
            AxAcroPDF axPDF = new AxAcroPDF();

            // Assign the ActiveX control as the host control's child.
            host.Child = axPDF;

            // Add the interop host control to the Grid
            // control's collection of child controls.
            this.grid2.Children.Add(host);

            // Play a .wav file with the ActiveX control.
            axPDF.LoadFile("D:\\191711\\Lab3\\7\\Get_Started_With_Smallpdf");
        }
    }
}
