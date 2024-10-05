using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomSeatNumber.Animations
{
    class TextAnimations
    {
        public static async Task RandomTextNumberAsync(TextBlock textBlock ,string tgtNum, int freq, int delay)
        {
            var random = new Random();
            for (int i = 0; i < freq; i++)
            {
                textBlock.Text = random.Next(0, int.Parse(tgtNum)+1)
                                 .ToString();
                await Task.Delay(delay);
            }
            textBlock.Text = tgtNum;
        }

        public static async Task RandomTextNumberPerCharAsync(TextBlock textBlock, string tgtNum, int freq, int delay)
        {
            textBlock.Text = "";
            int charLength = tgtNum.Length;
            var random = new Random();
            string randNum;

            for (int i = 0; i < freq; i++)
            {
                for (int j = charLength; j > 0; j--)
                {
                    randNum = random.Next(1, 10).ToString();
                    textBlock.Text += randNum;
                }

                int finDelay = delay/* + (int)(2 * Math.Log2(i + 1) * delay)*/;
                await Task.Delay(finDelay);

                textBlock.Text = "";
            }

            textBlock.Text = tgtNum;
        }

        public static void OderedTextNumber(int tgtNum) 
        { 
            
        }
    }
}
