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
        private string generatedNumber;
        public string GeneratedNumber
        {
            get
            {
                return generatedNumber;
            }

            set
            {
                generatedNumber = value;

                if(NumberTextBlock != null) 
                {
                    NumberTextBlock.Text = generatedNumber;
                }
            }
        }

        public Generation_Page()
        {
            GeneratedNumber = "???";
            this.InitializeComponent();

        }

        private void Generate_Btn_Click(object sender, RoutedEventArgs e)
        {
            GeneratedNumber = "SCSS";
        }
    }
}
