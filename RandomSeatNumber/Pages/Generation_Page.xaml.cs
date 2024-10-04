using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using RandomSeatNumber.Generate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RandomSeatNumber.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Generation_Page : Page
    {
        //***************
        // 生成所需参数集合
        //***************
        private int GenerationFreq = 1;


        // 生成的座位号
        private string generatedRes;

        public string GeneratedRes;

        // 座位号随机生成器
        public RandomGenerator generator;

        public Generation_Page()
        {
            GeneratedRes = "请点击”生成“按钮";
            this.InitializeComponent();
        }

        private async void Generate_Btn_ClickAsync(object sender, RoutedEventArgs e)
        {
            var tgtBtn = (Button)sender;

            // 如果生成频率大于一，那就对按钮实行触发冷却，防止过多生成产生意外
            if(GenerationFreq > 1)
            {
                tgtBtn.IsEnabled = false;
                await Task.Delay(1000);
                tgtBtn.IsEnabled = true;
            }
            

        }

        private void NumberOfGenerateTime_Input_LostFocus(object sender, RoutedEventArgs e)
        {
        }

        private void ClearHistory_Click(object sender, RoutedEventArgs e)
        {
        }

        private void NumberOfGenerateTime_Input_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {
            if(sender.Value >= 50)
            {
                sender.Value = 50;
            }

            if (sender.Value <= 0)
            {
                sender.Value = 1;
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
