using CommunityToolkit.WinUI.UI.Controls;
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
using Windows.ApplicationModel.DataTransfer;

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
        public string GeneratedRes; // 最近生成结果
        public ObservableCollection<string> GeneratedResList = new(); // 生成结果集合

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

            private void NumberOfGenerateTime_Input_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {
            if(sender.Value >= 50)
            {
                GnrtFreqTeachingTip.IsOpen = true;
                Debug.WriteLine($"生成次数数值过大！错误值为 {sender.Value}");
                sender.Value = 50;
            } else if (sender.Value <= 0)
            {
                GnrtFreqTeachingTip.IsOpen = true;
                Debug.WriteLine($"生成次数数值过小！错误值为 {sender.Value}");
                sender.Value = 1;
            }
            else
            {
                GnrtFreqTeachingTip.IsOpen = false;
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

            // 页面加载后再给 `NumberOfGenerateTime_Input` 分配值更改函数
            // 防止程序一运行，在其值尚为初始值（0）时就激发提示
            NumberOfGenerateTime_Input.ValueChanged += NumberOfGenerateTime_Input_ValueChanged;

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

        private async void ListClearButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            if(GeneratedResList.Count == 0) {  return; }

            ContentDialog clearConfirmDialog = new()
            {
                // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
                XamlRoot = this.XamlRoot,
                Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style,
                Title = "清空确认",
                PrimaryButtonText = "清空",
                CloseButtonText = "取消",
                DefaultButton = ContentDialogButton.Primary,
                Content = new TextBlock
                {
                    Text = "确定要将当前生成结果的集合全部清空吗？未保存的数据或将丢失。"
                }
            };

            var result = await clearConfirmDialog.ShowAsync();

            if(result == ContentDialogResult.Primary)
            {
                if (IsRepeatable)
                {
                    generator.RestoreContent();
                }

                GeneratedResList.Clear();
            }        

        }

        private void ListCopyButtonClicked(object sender, RoutedEventArgs e)
        {
            if(GeneratedResList.Count == 0) { return; }

            var package = new DataPackage();
            string resList = "";
            foreach (var i in GeneratedResList) 
            { 
                resList += i + "\n";
            }
            package.SetText(resList);
            Clipboard.SetContent(package);

        }

        private void ResCopyButtonClicked(object sender, RoutedEventArgs e)
        {
            if(GeneratedRes == null) { return; }
            var package = new DataPackage();
            package.SetText(GeneratedRes);
            Clipboard.SetContent(package);
        }
    }
}
