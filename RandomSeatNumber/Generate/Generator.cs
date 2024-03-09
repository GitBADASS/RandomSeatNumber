using System.Collections.Generic;

namespace RandomSeatNumber.Generate
{
    class Generator
    {
        /// <summary>
        /// 将初始的区加工为装载数组的集合
        /// </summary>
        /// <param name="distracts">初始区</param>
        /// <returns></returns>
        public static List<int[]> CreateSeatTableByDistracts(params InitialDistract[] distracts)
        {
            List<int[]> Result = new();

            // 通过行列限制将数组初步生成
            foreach (var d in distracts)
            {
                int[] resultArrays;
                // 每一行
                for (int i = 1; i <= d.RowLimition; i++)
                {
                    // 每一列
                    for (int i1 = 1; i1 <= d.ColumnLimition; i1++)
                    {
                        resultArrays = new int[3]; // 在每次循环时创建新的数组

                        resultArrays[0] = d.ID;
                        resultArrays[1] = i;
                        resultArrays[2] = i1;

                        Result.Add(resultArrays);
                    }
                }
            }

            // TODO *移除黑名单* 不可用！！！
            // 最外循环：获取单个区
            foreach (var d in distracts)
            {
                // 当前操作域：单个区

                // 遍历该区黑名单座号集合
                // 逐个获取区黑名单集合中的单个项（b 是黑名单座号，d.BlackList 是整个集合）
                if (d.BlackList != null)
                {
                    foreach (var b in d.BlackList)
                    {
                        // 当前操作域：单个黑名单座号
                        // 为原始黑名单座号缀上区号
                        int[] item = { d.ID, b[0], b[1] };

                        // 移除这个座号
                        Result.Remove(item);

                        // 完成，（若 黑名单座号集合 未遍历完全则）进行下一个黑名单座号的移除
                    } 
                }

                // 完成，（若未遍历完全则）转到下一个区进行 移除黑名单 操作
            }

            //TODO 添加白名单

            return Result;
        }

        /*private List<int[]> ProcessIntitialDistract(InitialDistract distract)
        {


            return null;
        }*/
    }
}
