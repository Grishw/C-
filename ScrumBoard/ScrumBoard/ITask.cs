using System;
namespace ScrumBoard
{
    public interface ITask
    {
        string Name { get;}
        string Description { get;}
        int Priority { get;}

        void Rename(string name);
        void ChengeDescription(string description);
        void ChengePriority(int priority);
    }
}
