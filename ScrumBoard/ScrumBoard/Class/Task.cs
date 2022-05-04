using System;
namespace ScrumBoard
{
    public class Task : ITask
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Priority { get; private set; }

        public Task(string name, string description, int priority)
        {
            Name = name;
            Description = description;
            Priority = priority;
        }

        public void Rename(string name)
        {
            Name = name;
        }

        public void ChangeDescription(string description)
        {
            Description = description;
        }

        public void ChangePriority(int priority)
        {
            Priority = priority;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

    }
}
