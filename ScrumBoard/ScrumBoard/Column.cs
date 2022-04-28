using System;
using System.Linq;
using System.Collections.Generic;

namespace ScrumBoard
{
    public class Column
    {
        public string Name { get; set; }
        public int Order { get; set; }
        private List<Task> _tasks;


        public Column(string name)
        {
            Name = name;
            _tasks = new List<Task>();
        }

        private void UpdateTasksPriorityInRange(int rangeStart, int rangeEnd)
        {
            for (int i = rangeStart + 1; i < rangeEnd; i++)
            {
                _tasks[i].Priority = _tasks[i].Priority + 1;
            }
        }

        public Boolean AddTask(Task task)
        {
            if (_tasks == null)
            {
                task.Priority = 0;
                _tasks.Add(task);
                return true;
            }

            if (task.Priority  >= _tasks.Count)
            {
                task.Priority = _tasks.Count + 1;
            }

            switch (task.Priority)
            {
                case <= 0:
                    task.Priority = 0;
                    _tasks.Insert(task.Priority, task);
                    UpdateTasksPriorityInRange(task.Priority, _tasks.Count);
                    return true;
                    break;

                default:
                    _tasks.Insert(task.Priority, task);
                    UpdateTasksPriorityInRange(task.Priority, _tasks.Count);
                    return true;
                    break;
            }

            return false;
        }

        


    }
}
