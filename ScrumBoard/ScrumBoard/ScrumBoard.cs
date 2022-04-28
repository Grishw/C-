using System;
using System.Collections.Generic;
using System.Linq;

namespace ScrumBoard
{
    public class ScrumBoard
    {
        private const int MaxColumnsCount = 10;
        private string _name = "ScrumBoardBaseName";
        public string Name
        {
            get => _name;
            init => _name = value;
        }

        private List<Column> _columns = new(MaxColumnsCount);


        public ScrumBoard(string name)
        {
            Name = name;
        }


        public bool AddColumn(Column column)
        {
            if (_columns.Count > MaxColumnsCount)
            {
                return false;
            }

            if (column.Order > _columns.Count+1)
            {
                
                return true;
            }

            _columns.Add(column);
            return true;
        }

        public void MoveColumn()
        {

        }

        public void AddTask()
        {

        }

        public void MoveTask()
        {

        }


    }
}
