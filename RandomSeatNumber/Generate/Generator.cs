using System.Collections.Generic;

namespace RandomSeatNumber.Generate
{
    internal class Generator
    {
        /// <summary>
        /// 将初始的区加工为装载数组的集合
        /// </summary>
        /// <param name="distracts">传入初始区</param>
        /// <returns></returns>
        public static List<int[]> CreateSeatTableByDistracts(params InitialDistract[] distracts)
        {
            List<int[]> Result = null;
            int[] item = new int[2];

            // 通过行列限制将数组初步生成
            foreach (var d in distracts)
            {
                // 每一行
                for (int i = 0; i <= d.RowLimition; i++)
                {
                    // 每一列
                    for (int i1 = 0; i1 <= d.ColumnLimition; i1++)
                    {
                        item[0] = i;
                        item[1] = i1;

                        Result.Add(item);
                    }
                }
            }

            return Result;
        }

        /*private List<int[]> ProcessIntitialDistract(InitialDistract distract)
        {


            return null;
        }*/
    }
}
