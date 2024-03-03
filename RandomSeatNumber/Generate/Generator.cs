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

            return Result;
        }

        /*private List<int[]> ProcessIntitialDistract(InitialDistract distract)
        {


            return null;
        }*/
    }
}
