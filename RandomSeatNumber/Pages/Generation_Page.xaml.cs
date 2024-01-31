using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Numerics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RandomSeatNumber.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Generation_Page : Page
    {
        public string GeneratedNumber {  get; set; }

        public Generation_Page()
        {
            GeneratedNumber = "TTTEST";
            this.InitializeComponent();

        }

        private void TestBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
