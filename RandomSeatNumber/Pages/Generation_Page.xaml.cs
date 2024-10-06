using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using RandomSeatNumber.Animations;
using RandomSeatNumber.Generate;
using RandomSeatNumber.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        // 生成所需参数集合（暂用硬编码
        //***************
        private int _generationFreq;
        public int GenerationFreq
        {
            set
            {
                _generationFreq = value;
                GenerationOptionsHelper.GenerationFreq = value;
                NumberOfGenerateTime_Input.Value = value;
            }
            get { return _generationFreq; }
        }

        private bool _isRepeatable;
        public bool IsRepeatable {
            set 
            {
                _isRepeatable = value;
                GenerationOptionsHelper.IsRepeatable = value;
                IsRepeatableCheckBox.IsChecked = value;
            }
            get { return _isRepeatable; }
        }

        // 生成的座位号
        private string generatedRes;
        public string GeneratedRes;
        public ObservableCollection<string> GeneratedResList = new();

        // 座位号随机生成器
        public RandomGenerator generator;

        public Generation_Page()
        {
            this.InitializeComponent();

            Loaded += GenerationPageOnLauched;
        }

        private async void Generate_Btn_ClickAsync(object sender, RoutedEventArgs e)
        {
            var tgtBtn = (Button)sender;

            RSNGenerate();
            // 如果生成频率大于一，那就对按钮实行触发冷却，防止过多生成产生意外
            if (GenerationFreq > 1)
            {
                tgtBtn.IsEnabled = false;
                await Task.Delay(500);
                tgtBtn.IsEnabled = true;
            }
        }

        private void NumberOfGenerateTime_Input_LostFocus(object sender, RoutedEventArgs e)
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
        private void GenerationPageOnLauched(object sender, RoutedEventArgs e)
        {
            // 按照本地设置分配参数值
            IsRepeatable = GenerationOptionsHelper.IsRepeatable;
            GenerationFreq = GenerationOptionsHelper.GenerationFreq;

            List<int[]> test;
            List<int[]> bl = new();
            bl.Add(new int[] { 7, 2 });
            bl.Add(new int[] { 7, 1 });
            InitialDistract dis1 = new(1, 6, 3, null, null);
            InitialDistract dis2 = new(2, 6, 3, null, null);
            InitialDistract dis3 = new(3, 7, 3, null, bl);

            test = SeatNumberTranslator.CreateSeatTableByDistracts(dis1, dis2, dis3);
            foreach (var i in test)
            {
                Debug.WriteLine($"第 {i} 位为 {i[0]} 组 {i[1]} 行 {i[2]} 列");
            }

            generator = new(test);
        }

        // 生成座位号的函数
        private void RSNGenerate()
        {
            int[] resArr;
            string res = "";
            for (int i = 0; i < GenerationFreq; i++) {
                if(IsRepeatable)
                {
                    resArr = generator.GetDistinctRandomSeatNumber();
                    Debug.WriteLine($"第 {i} 次无重生成，容器剩余 {generator.Content.Count} 个内容");
                }
                else
                {
                    resArr = generator.GetRandomSeatNumber();
                }
                res = resArr[0].ToString() +
                          resArr[1].ToString() +
                          resArr[2].ToString();
                GeneratedResList.Add(res);
            }

            GeneratedRes = res;
            // 暂用硬编码，日后整改
            _ = TextAnimations.RandomTextNumberPerCharAsync(GnrtResTextBlock, GeneratedRes, 10, 10);
        }

        private void IsRepeatableCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            IsRepeatable = false;
            generator.RestoreContent();
        }
    }
}
