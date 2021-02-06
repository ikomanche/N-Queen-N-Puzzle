/*1600002148
 İlker Kılınçarslan*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightQueen
{
    class Queen
    {
        private int row;
        private int column;

        public Queen(int row,int column)
        {
            this.row = row;
            this.column = column;
        }

        public void Move()
        {
            row++;
        }

        public bool ifConflict(Queen q)
        {
            if (row == q.getRow() || column == q.getColumn())
                return true;
            else if (Math.Abs(column - q.getColumn()) == Math.Abs(row - q.getRow()))
                return true;
            return false;
        }

        public int getRow()
        {
            return row;
        }

        public int getColumn()
        {
            return column;
        }
    }
}
