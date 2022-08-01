using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisjointedSets
{
    internal class Table
    {
        private Table parent;
        public Table(int numberOfRows)
        {
            NumberOfRows = numberOfRows;
            parent = this;
            Rank = 0;
        }

        public int NumberOfRows { get; set; }
        public int Rank { get; set; }

        public void SetParent(Table parent)
        {
            this.parent = parent;
        }

        private Table GetParent(Table table)
        {
            if(table != table.parent)
            {
                table.parent = GetParent(table.parent);
            }

            return table.parent;
        }

        public Table GetParent()
        {
            return GetParent(this);
        }
    }
}
