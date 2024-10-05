using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomSeatNumber.Generate
{
    public class RandomGenerator
    {
        // 样本空间
        public List<int[]> Content;
        // 样本空间原始副本
        readonly List<int[]> CopyOfContent;
        // 随机对象
        readonly Random randomer;

        public RandomGenerator(List<int[]> content)
        {
            Content = new();
            if(content != null)
            {
                // 输入内容给样本空间
                Content.AddRange(content);

                // 存储原始样本空间的副本
                CopyOfContent = new();
                CopyOfContent.AddRange(Content);

                // 初始化随机对象
                randomer = new();
            }

        }

        // 获取随机数
        public int[] GetRandomSeatNumber()
        {
            // 得到一个随机数，后续会作为索引
            var randomNumber = randomer.Next(0, Content.Count);

            // 随机数作为索引返回座号数组
            // 抽取样本拷贝内容，因为样本元素可能不齐全
            return CopyOfContent[randomNumber]; 
        }

        // 获取不重复的随机数
        public int[] GetDistinctRandomSeatNumber()
        {
            // 率先检查内容是否为空，若为空自动恢复
            if (Content.Count == 0)
            {
                RestoreContent();
            }
            // 得到一个随机数，后续会作为索引
            var randomNumber = randomer.Next(0, Content.Count);

            // 获取结果
            int[] res = Content[randomNumber];
            // 将结果从内容中移除
            Content.RemoveAt(randomNumber);

            return res;
        }

        // 恢复样本空间
        public void RestoreContent()
        {
            Content.AddRange(CopyOfContent);
        }
    }
}
