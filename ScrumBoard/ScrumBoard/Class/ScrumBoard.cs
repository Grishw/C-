// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;

namespace ScrumBoard
{
    public class Board : IBoard
    {
        private const int MaxColumnsCount = 10;
        private const int ColumnMinOrderValue = 0;
        private string _name = "ScrumBoard Base Name";
        public string Name
        {
            get => _name;
            init => _name = value;
        }

        private List<IColumn> _columns;


        public Board(string name)
        {
            if(name != "")
            {
                Name = name;
            }
            _columns = new List<IColumn>();
        }

        private void UpdateColumnsPriorityInRange(int rangeStart, int rangeEnd, int increaseValue)
        {
            for (int i = rangeStart + 1; i < rangeEnd; i++)
            {
                _columns[i].ChangeOrder(_columns[i].Order + increaseValue);
            }
        }

        private IColumn AddTaskListToColumnTaskListWithNewPriority(IColumn column, List<ITask> taskList, int newPriority)
        {
            int counter = 0;
            foreach (ITask task in taskList)
            {
                task.ChangePriority(newPriority + counter);
                column.AddTask(task);
                counter += 1;
            }
            return column;
        }

        private List<IColumn> GetListClone(List<IColumn> columnList)
        {
            List<IColumn> newColumnList = new List<IColumn>();
            IColumn newColumn = new Column("", 0);

            foreach (IColumn column in columnList)
            {
                newColumn.Rename(column.Name);
                newColumn.ChangeOrder(column.Order);
                AddTaskListToColumnTaskListWithNewPriority(newColumn, column.GetTaskList(), 0);
                newColumnList.Add(newColumn);
                newColumn.Clear();
            }
            return newColumnList;
        }

        public bool AddColumn(IColumn inColumn)
        {
            IColumn column = (IColumn)inColumn.Clone();
            if (_columns.Count> MaxColumnsCount)
            {
                return false;
            }

            if (column.Order > _columns.Count)
            {
                column.ChangeOrder(_columns.Count);
                _columns.Add(column);
                return true;
            }

            switch (column.Order)
            {
                case <= ColumnMinOrderValue:
                    column.ChangeOrder(ColumnMinOrderValue);
                    _columns.Insert(column.Order, column);
                    UpdateColumnsPriorityInRange(column.Order, _columns.Count, 1);
                    return true;

                default:
                    _columns.Insert(column.Order, column);
                    UpdateColumnsPriorityInRange(column.Order, _columns.Count, 1);
                    return true;
            }
        }

        public bool AddColumn(string name)
        {

            if (_columns.Count > MaxColumnsCount)
            {
                return false;
            }

            IColumn column = new Column(name: name, order: _columns.Count);
            _columns.Add(column);
            return true;
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
            return GetListClone(_columns);
        }

        public IColumn GetColumn(int order)
        {
            return _columns.Find(element => element.Order == order) != null
                ? (IColumn)_columns.Find(element => element.Order == order).Clone()
                : null;
        }

        public IColumn GetColumn(string name)
        {
            return _columns.Find(element => element.Name == name) != null
                ? (IColumn)_columns.Find(element => element.Name == name).Clone()
                : null;
        }

        public IColumn GetColumn(IColumn column)
        {
            return _columns.Find(element => element.Name == column.Name && element.Order == column.Order) != null
                ? (IColumn)_columns.Find(element => element.Name == column.Name && element.Order == column.Order).Clone()
                : null;
        }

        public void DeleteColumn(IColumn column)
        {
            if(column != null)
            {
                int columnToRemoveOrder = column.Order - 1;
                _columns.RemoveAll(element => (element.Name == column.Name && element.Order == column.Order));
                UpdateColumnsPriorityInRange(columnToRemoveOrder, _columns.Count, -1);
            }
            
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
            columnToMove.ChangeOrder(newOrder);

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

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
