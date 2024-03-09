using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using RandomSeatNumber.Generate;
using System;
using System.Collections.Generic;
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
            /*
             * === �·�Ϊ��ʱ���� ===
             * ��ʱ��ȡӲ���룬������Ҫ����
             */

            // ��һ��
            List<int[]> BlackList = new List<int[]>
            {
                new int[] { 7, 1 },
                new int[] { 7, 2 }
            };
            InitialDistract distract1 = new InitialDistract(
                1, 7, 3, null, BlackList);

            // �ڶ���
            InitialDistract distract2 = new InitialDistract(
                2, 6, 3, null, null);

            // ������
            InitialDistract distract3 = new InitialDistract(
                3, 6, 3, null, null);

            // ���ɳ�ʼ���α�
            var SeatTable = Generator.CreateSeatTableByDistracts(distract1, distract2, distract3);

            // �����ȡ����
            Random Random = new Random();
            int RandomOrder = (int) Random.NextInt64(1, SeatTable.Count);
            int[] RandomSeatNumber = SeatTable[RandomOrder];

            // תΪ�ַ���
            string DistrictNumber = RandomSeatNumber[0].ToString();
            string RowNumber = RandomSeatNumber[1].ToString();
            string ColumnNumber = RandomSeatNumber[2].ToString();

            GeneratedNumber = DistrictNumber + ", "  + RowNumber + ", " + ColumnNumber;
        }

        private void MultiplyGenerate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
