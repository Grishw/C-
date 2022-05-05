using System;
using System.Collections.Generic;

namespace ScrumBoard
{
    public interface IColumn
    {
        string Name { get;}
        int Order { get;}

        void Rename(string name);
        void ChangeOrder(int order);
        Boolean AddTask(ITask task);
        Boolean AddTask(string name, string description, int priority);
        Boolean AddTask(string name, string description);
        Boolean AddTask(string name);
        Boolean AddTask();
        List<ITask> GetTaskList();
        ITask GetTask(int priority);
        ITask GetTask(string name);
        ITask GetTask(ITask task);
        void DeleteTask(ITask task);
        void MoveTask(ITask taskToMove, int newPrior);
        object Clone();
        void Clear();
    }
}
