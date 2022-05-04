using System;
using ScrumBoard;
using Xunit;


namespace ScrumBoardTest
{
    public class TaskTest
    {
        [Fact]
        public void Created_task_with_properties()
        {
            const string taskName = "test name";
            const string taskDescription = "test description";
            const int taskPriority = 999999;

            ITask task = new Task(taskName, taskDescription, taskPriority);

            Assert.True(task.Name == taskName);
            Assert.True(task.Description == taskDescription);
            Assert.True(task.Priority == taskPriority);
        }

        [Fact]
        public void Change_task_name()
        {
            const string newTaskName = "new test name";

            ITask task = new Task("test name", "test description", 999999);
            task.Rename(newTaskName);

            Assert.True(task.Name == newTaskName);
        }

        [Fact]
        public void Change_task_description()
        {
            const string newTaskDescription = "new test description";

            ITask task = new Task("test name", "test description", 999999);
            task.ChangeDescription(newTaskDescription);

            Assert.True(task.Description == newTaskDescription);
        }

        [Fact]
        public void Change_task_priority()
        {
            const int newTaskPriority = 0;

            ITask task = new Task("test name", "test description", 999999);
            task.ChangePriority(newTaskPriority);

            Assert.True(task.Priority == newTaskPriority);
        }
    }
}
