using System;
namespace ScrumBoard
{
    public class Task
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }

        public Task(string name, string description, int priority)
        {
            Name = name;
            Description = description;
            Priority = priority;
        }
    }
}
