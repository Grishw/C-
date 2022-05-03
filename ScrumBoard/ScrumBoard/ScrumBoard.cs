using System;
using System.Collections.Generic;
using System.Linq;

namespace ScrumBoard
{
    public class ScrumBoard
    {
        private const int MaxColumnsCount = 10;
        private const int ColumnMinOrderValue = 0;
        private string _name = "ScrumBoardBaseName";
        public string Name
        {
            get => _name;
            init => _name = value;
        }

        private List<IColumn> _columns;


        public ScrumBoard(string name)
        {
            Name = name;
            _columns = new List<IColumn>();
        }

        private void UpdateColumnsPriorityInRange(int rangeStart, int rangeEnd, int increaseValue)
        {
            for (int i = rangeStart + 1; i < rangeEnd; i++)
            {
                _columns[i].ChengeOrder(_columns[i].Order + increaseValue);
            }
        }


        public bool AddColumn(IColumn column)
        {
            if (_columns.Count> MaxColumnsCount)
            {
                return false;
            }

            if (column.Order > _columns.Count)
            {
                column.ChengeOrder(_columns.Count);
                _columns.Add(column);
                return true;
            }

            switch (column.Order)
            {
                case <= ColumnMinOrderValue:
                    column.ChengeOrder(ColumnMinOrderValue);
                    _columns.Insert(column.Order, column);
                    UpdateColumnsPriorityInRange(column.Order, _columns.Count, 1);
                    return true;

                default:
                    _columns.Insert(column.Order, column);
                    UpdateColumnsPriorityInRange(column.Order, _columns.Count, 1);
                    return true;
            }
        }

        public bool AddColumn()
        {

            if (_columns.Count > MaxColumnsCount)
            {
                return false;
            }

            IColumn column = new Column(name: $"column {_columns.Count}", order: _columns.Count);
            _columns.Add(column);
            return true;
        }

        public List<IColumn> GetColumnList()
        {
            return _columns;
        }

        public IColumn GetColumn(int order)
        {
            return _columns.Find(element => element.Order == order);
        }

        public IColumn GetColumn(string name)
        {
            return _columns.Find(element => element.Name == name);
        }

        public IColumn GetColumn(IColumn column)
        {
            return _columns.Find(element => element.Name == column.Name);
        }

        public void DeleteColumn(IColumn column)
        {
            int columnToRemoveOrder = column.Order - 1;
            _columns.Remove(column);
            UpdateColumnsPriorityInRange(columnToRemoveOrder, _columns.Count, -1);
        }

        public void MoveColumn(IColumn columnToMove, int newOrder)
        {
            if(newOrder < 0)
            {
                newOrder = 0;
            }

            if (newOrder >= _columns.Count)
            {
                newOrder = _columns.Count;
            }

            int orderColumnToMuve = columnToMove.Order;
            columnToMove.ChengeOrder(newOrder);

             DeleteColumn(columnToMove);
             AddColumn(columnToMove);
        }

        public void AddTask(ITask task)
        {
            if(!_columns.Any())
            {
                this.AddColumn();
            }
            _columns[ColumnMinOrderValue].AddTask(task);
        }


        public void MoveTaskOverColumn(ITask taskToMove, int newPrior, IColumn columnTo, IColumn columnFrom)
        {
            columnFrom.DeleteTask(taskToMove);
            columnTo.AddTask(taskToMove.Name, taskToMove.Description, newPrior);
        }


    }
}
