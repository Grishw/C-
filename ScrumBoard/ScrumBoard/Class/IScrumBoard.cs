using System;
using System.Collections.Generic;

namespace ScrumBoard
{
    public interface IScrumBoard
    {
        string Name { get; }

        bool AddColumn(IColumn column);
        bool AddColumn();
        List<IColumn> GetColumnList();
        IColumn GetColumn(int order);
        IColumn GetColumn(string name);
        IColumn GetColumn(IColumn column);
        void DeleteColumn(IColumn column);
        void MoveColumn(IColumn columnToMove, int newOrder);
        void AddTask(ITask task);
        void MoveTaskOverColumn(ITask taskToMove, int newPrior, IColumn columnTo, IColumn columnFrom);

    }
}
