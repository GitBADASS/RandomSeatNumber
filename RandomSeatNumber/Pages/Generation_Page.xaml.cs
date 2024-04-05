using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using RandomSeatNumber.Generate;
using System;
using System.Collections.Generic;

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

        public List<int[]> SeatTable; // 临时内容

        public List<int[]> DistinctSeatTable; // 临时内容

        Random Random = new Random(); // 临时内容

        private int currentGenerateTime = 1;

        private int newGenerateTime;

        private string intitialHistory;

        private string[] historyInArrays;

        private int generatedTimes;

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
            GeneratedNumber = "请点击”生成“按钮";
            this.InitializeComponent();

            /*
             * === 下方为临时内容 ===
             * 暂时采取硬编码，往后需要更改
             */

            // 第一区
            // 删除座号考虑一种可以直接删除整排或整列的方法
            List<int[]> BlackList_1_2 = new List<int[]>
            {
                new int[] { 7, 1 },
                new int[] { 7, 2 },
                new int[] { 7, 3 },
            };

            List<int[]> BlackList_3 = new List<int[]>
            {
                new int[] { 7, 1 },
                new int[] { 7, 2 }
            };

            InitialDistract distract1 = new InitialDistract(
                1, 7, 3, null, BlackList_1_2);

            // 第二区
            InitialDistract distract2 = new InitialDistract(
                2, 6, 3, null, BlackList_1_2);

            // 第三区
            InitialDistract distract3 = new InitialDistract(
                3, 6, 3, null, BlackList_3);

            // 生成初始座次表
            SeatTable = Generator.CreateSeatTableByDistracts(distract1, distract2, distract3);
            DistinctSeatTable = SeatTable;

            intitialHistory = HistoryOfGenerated.Text;
        }

        private void Generate_Btn_Click(object sender, RoutedEventArgs e)
        {
            /*
             * === 下方为临时内容 ===
             * 暂时采取硬编码，往后需要更改
             */

            // 定义结果字符串
            string Result = "";
            string ResultOfHistory = "";

            if(HistoryOfGenerated.Text == intitialHistory)
            {
                HistoryOfGenerated.Text = "";
            }

            if(generatedTimes > 100)
            {
                generatedTimes = 0;
                // TODO: 生成历史会直接清空，考虑把最先的几个挤出去而不是清空（可能的实现方法：除去头 currentGenerateTime 个内容。
                // 或者换用 ListView 表示而不是 TextBlock。
                historyInArrays = HistoryOfGenerated.Text.Split('\n');
                for (global::System.Int32 i = currentGenerateTime - 1; i < historyInArrays.Length; i++)
                {
                    ResultOfHistory += historyInArrays[i] + "\n";
                }
                HistoryOfGenerated.Text = ResultOfHistory;
            }

            // 对结果字符串进行相关操作
            // 如果生成次数不等于 1，循环搭建多次生成结果
            if ((bool)!Distinct.IsChecked)
            {
                if (currentGenerateTime != 1)
                {
                    // 搭建多次生成结果字符串
                    for (int i = 1; i <= currentGenerateTime; i++)
                    {
                        if (i != currentGenerateTime)
                        {
                            Result += TransformToString(GetRandomSeatNumber()) + "\n";
                        }
                        else
                        {
                            Result += TransformToString(GetRandomSeatNumber());
                        }
                    }
                }
                else
                {
                    // 如果生成次数为 1，只需生成一次
                    Result = TransformToString(GetRandomSeatNumber());
                } 
            }
            else
            {
                if (currentGenerateTime != 1)
                {
                    // 搭建多次生成结果字符串
                    for (int i = 1; i <= currentGenerateTime; i++)
                    {
                        if (i != currentGenerateTime)
                        {
                            Result += TransformToString(GetDistinctRandomSeatNumber()) + "\n";
                        }
                        else
                        {
                            Result += TransformToString(GetDistinctRandomSeatNumber());
                        }
                    }
                }
                else
                {
                    // 如果生成次数为 1，只需生成一次
                    Result = TransformToString(GetDistinctRandomSeatNumber());
                }
            }

            HistoryOfGenerated.Text += Result + "\n";
            generatedTimes += currentGenerateTime;
            // 将文本覆盖到显示结果的控件
            NumberTextBlock.Text = Result;
        }

        private int[] GetRandomSeatNumber()
        {
            // 随机抽取序数
            int RandomOrder = (int)Random.NextInt64(0, SeatTable.Count);
            int[] RandomSeatNumber = SeatTable[RandomOrder];
            return RandomSeatNumber;
        }
        
        private int[] GetDistinctRandomSeatNumber()
        {
            if(DistinctSeatTable.Count <= 0)
            {
                DistinctSeatTable.AddRange(SeatTable);
            }
            // 随机抽取序数
            int RandomOrder = (int)Random.NextInt64(0, DistinctSeatTable.Count);
            int[] RandomSeatNumber = DistinctSeatTable[RandomOrder];
            DistinctSeatTable.RemoveAll(e => e == RandomSeatNumber);
            return RandomSeatNumber;
        }

        private string TransformToString(int[] RandomSeatNumber)
        {
            // 转为字符串
            string DistrictNumber = RandomSeatNumber[0].ToString();
            string RowNumber = RandomSeatNumber[1].ToString();
            string ColumnNumber = RandomSeatNumber[2].ToString();

            return DistrictNumber + ", " + RowNumber + ", " + ColumnNumber;
        }

        // 应用用户输入的生成次数

        private void NumberOfGenerateTime_Input_LostFocus(object sender, RoutedEventArgs e)
        {
            // 将生成结果转为 int，并赋值给 newGenerateTime
            bool result = int.TryParse(NumberOfGenerateTime_Input.Text, out newGenerateTime);

            // 如果符合“全部为数字字符且该数字的值不超过 100”的条件
            if (result && newGenerateTime > 0 && newGenerateTime <= 100)
            {
                // 应用数值
                currentGenerateTime = newGenerateTime;

                // 通知用户修改成功
                ShowNotification("成功修改", 
                                 "你已经成功修改生成次数为 " + currentGenerateTime + " 次", 
                                 InfoBarSeverity.Success);
            }
            // 如果不符合条件
            else
            {
                // newGenerateTime 被赋值为 0，currentGenerateTime 不受影响

                // 可能是因为用户输入了非数字字符
                if (!result)
                {
                    ShowNotification("无效数字", "你输入的内容可能包含非阿拉伯数字字符", InfoBarSeverity.Warning);
                }
                // 如果通过了上面的判断，则进行对数字数值问题的排查
                // 可能是数值过大
                else if (newGenerateTime > 100 || newGenerateTime < 0)
                {
                    ShowNotification("极端数值", "你输入的数值可能并不在允许的范围内，请输入 1~100 内的数字", InfoBarSeverity.Warning);
                }
            }
        }

        private void ShowNotification(string title, string message, InfoBarSeverity severity)
        {
            if(InforBar.IsOpen)
            {
                InforBar.IsOpen = false;
            }

            InforBar.Title = title;
            InforBar.Message = message;
            InforBar.Severity = severity;

            InforBar.IsOpen = true;
        }
    }
}
