using System;
using System.Linq;
using System.Collections.Generic;

namespace ScrumBoard
{
    public class Column : IColumn
    {
        public string Name { get; private set; }
        public int Order { get; private set; }
        private List<ITask> _tasks;

        public const int TaskMinPriorityValue = 0;

        public Column(string name, int order)
        {
            Name = name;
            Order = order;
            _tasks = new List<ITask>();
        }


        public void Rename(string name)
        {
            Name = name;
        }

        public void ChangeOrder(int order)
        {
            Order = order;
        }


        private void UpdateTasksPriorityInRange(int rangeStart, int rangeEnd, int increaseValue)
        {
            for (int i = rangeStart + 1; i < rangeEnd; i++)
            {
                _tasks[i].ChangePriority(_tasks[i].Priority + increaseValue);
            }
        }

        private List<ITask> GetListClone(List<ITask> taskList)
        {
            return taskList.Select(elenemt => (ITask)elenemt.Clone()).ToList();
        }

        public Boolean AddTask(ITask inTask)
        {
            ITask task = (Task)inTask.Clone();
            if (!_tasks.Any())
            {
                task.ChangePriority(TaskMinPriorityValue);
                _tasks.Add(task);
                return true;
            }

            if (task.Priority  >= _tasks.Count)
            {
                task.ChangePriority(_tasks.Count);
            }

            switch (task.Priority)
            {
                case <= TaskMinPriorityValue:
                    task.ChangePriority(TaskMinPriorityValue);
                    _tasks.Insert(task.Priority, task);
                    UpdateTasksPriorityInRange(task.Priority, _tasks.Count, 1);
                    return true;

                default:
                    _tasks.Insert(task.Priority, task);
                    UpdateTasksPriorityInRange(task.Priority, _tasks.Count, 1);
                    return true;
            }
        }

        public Boolean AddTask(string name, string description, int priority)
        {
            ITask task = new Task(name: name, description: description, priority: priority);
            bool result = this.AddTask(task);
            return result;
        }

        public Boolean AddTask(string name, string description)
        {
            ITask task = new Task(name: name, description: description, priority: _tasks.Count);
            _tasks.Add(task);
            return true;
        }

        public Boolean AddTask(string name)
        {
            ITask task = new Task(name: name, description: "common description", priority: _tasks.Count);
            _tasks.Add(task);
            return true;
        }

        public Boolean AddTask()
        {
            ITask task = new Task(name: $"task {_tasks.Count}", description: "common description", priority: _tasks.Count);
            _tasks.Add(task);
            return true;
        }

        public List<ITask> GetTaskList()
        {
            return GetListClone(_tasks);
        }

        public ITask GetTask(int priority)
        {
            return _tasks.Find(element => element.Priority == priority) != null ? (Task)_tasks.Find(element => element.Priority == priority).Clone() : null;
        }

        public ITask GetTask(string name)
        {
            return _tasks.Find(element => element.Name == name) != null ? (Task)_tasks.Find(element => element.Name == name).Clone() : null;
        }

        public ITask GetTask(ITask task)
        {
            return _tasks.Find(element => element == task) != null ? (Task)_tasks.Find(element => element.Name == task.Name).Clone() : null;
        }

        public void DeleteTask(ITask task)
        {
            int taskToRemovePriority = task.Priority - 1;
            _tasks.Remove(task);
            UpdateTasksPriorityInRange(taskToRemovePriority, _tasks.Count, -1);
        }

        public void MoveTask(ITask taskToMove, int newPrior)
        {
            if (newPrior < TaskMinPriorityValue)
            {
                newPrior = TaskMinPriorityValue;
            }

            if (newPrior >= _tasks.Count)
            {
                newPrior = _tasks.Count;
            }

            int orderColumnToMuve = taskToMove.Priority;
            taskToMove.ChangePriority(newPrior);

            DeleteTask(taskToMove);
            AddTask(taskToMove);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public void Clear()
        {
            _tasks.Clear();
        }

    }
}
