using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomSeatNumber.Generate
{
    class InitialSeatTable
    {
        public string ID { get; set; }

        public List<InitialDistract> Content { get; set; }

        public InitialSeatTable(string ID, List<InitialDistract> content)
        {
            #region Assignment
            this.ID = ID;
            Content = content;
            #endregion
        }

    }
}
