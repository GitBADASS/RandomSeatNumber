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

        Random Random = new Random(); // 临时内容
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
            List<int[]> BlackList = new List<int[]>
            {
                new int[] { 7, 1 },
                new int[] { 7, 2 }
            };
            InitialDistract distract1 = new InitialDistract(
                1, 7, 3, null, BlackList);

            // 第二区
            InitialDistract distract2 = new InitialDistract(
                2, 6, 3, null, null);

            // 第三区
            InitialDistract distract3 = new InitialDistract(
                3, 6, 3, null, null);

            // 生成初始座次表
            SeatTable = Generator.CreateSeatTableByDistracts(distract1, distract2, distract3);

        }

        private void Generate_Btn_Click(object sender, RoutedEventArgs e)
        {
            /*
             * === 下方为临时内容 ===
             * 暂时采取硬编码，往后需要更改
             */

            // 随机抽取序数
            int[] RandomSeatNumber = GetRandomSeatNumber();

            // 转为字符串
            GeneratedNumber = TransformToString(RandomSeatNumber);
        }

        private void MultiplyGenerate_Click(object sender, RoutedEventArgs e)
        {
            string Result = "";
            MultipleNumber.Text = Result;

            for (int i = 1; i <= 5; i++)
            {
                if(i != 5)
                {
                    Result += TransformToString(GetRandomSeatNumber()) + " 与 ";
                }
                else
                {
                    Result += TransformToString(GetRandomSeatNumber());
                }
            }

            MultipleNumber.Text = Result;
        }

        private int[] GetRandomSeatNumber()
        {
            // 随机抽取序数
            int RandomOrder = (int)Random.NextInt64(0, SeatTable.Count);
            int[] RandomSeatNumber = SeatTable[RandomOrder];
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
    }
}
