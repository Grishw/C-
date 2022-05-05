using System;
using ScrumBoard;
using System.Linq;
using System.Collections.Generic;
using Xunit;

namespace ScrumBoardTest
{
    public class ColumnTest
    {
        [Fact]
        public void Create_column_with_properties()
        {
            const string columnName = "test name";
            const int columnOrder = 999999;

            IColumn column = new Column(columnName, columnOrder);

            Assert.True(column.Name == columnName);
            Assert.True(column.Order == columnOrder);
        }

        [Fact]
        public void Created_column_does_not_have_tasks()
        {
            IColumn column = new Column("test name", 999999);

            List<ITask> taskListFromNewColumn = column.GetTaskList();

            Assert.True(taskListFromNewColumn.Any() == false);
        }

        [Fact]
        public void Change_column_name()
        {
            const string newColumnName = "new column name";

            IColumn column = new Column("test name", 999999);
            column.Rename(newColumnName);

            Assert.True(column.Name == newColumnName);
        }

        [Fact]
        public void Change_column_order()
        {
            const int newColumnOrder = 0;

            IColumn column = new Column("test name", 999999);
            column.ChangeOrder(newColumnOrder);

            Assert.True(column.Order == newColumnOrder);
        }

        [Fact]
        public void Add_one_task_to_column_like_task()
        {
            ITask task = new Task("test name", "test description", 999999);
            IColumn column = new Column("test name", 999999);
            const int taskPriorityFromList = 0;

            column.AddTask(task);
            List<ITask> taskListFromColumn = column.GetTaskList();

            Assert.True(taskListFromColumn.Any() != false);
            Assert.True(taskListFromColumn.Count == 1);

            Assert.True(taskListFromColumn[0].Name == task.Name);
            Assert.True(taskListFromColumn[0].Description == task.Description);
            Assert.True(taskListFromColumn[0].Priority != task.Priority);
            Assert.True(taskListFromColumn[0].Priority == taskPriorityFromList);
        }

        [Fact]
        public void Add_one_task_to_column_like_name_description_priority()
        {
            const string taskName = "test name";
            const string taskDescription = "test description";
            const int taskPriority = 999999;
            const int taskPriorityWaited = 0;
            IColumn column = new Column("test name", 999999);

            column.AddTask(taskName, taskDescription);
            List<ITask> taskListFromColumn = column.GetTaskList();

            Assert.True(taskListFromColumn.Any() != false);
            Assert.True(taskListFromColumn.Count == 1);

            Assert.True(taskListFromColumn[0].Name == taskName);
            Assert.True(taskListFromColumn[0].Description == taskDescription);
            Assert.True(taskListFromColumn[0].Priority != taskPriority);
            Assert.True(taskListFromColumn[0].Priority == taskPriorityWaited);
        }

        [Fact]
        public void Add_one_task_to_column_like_name_description()
        {
            const string taskName = "test name";
            const string taskDescription = "test description";
            const int taskPriorityWaited = 0;
            IColumn column = new Column("test name", 999999);

            column.AddTask(taskName, taskDescription);
            List<ITask> taskListFromColumn = column.GetTaskList();

            Assert.True(taskListFromColumn.Any() != false);
            Assert.True(taskListFromColumn.Count == 1);

            Assert.True(taskListFromColumn[0].Name == taskName);
            Assert.True(taskListFromColumn[0].Description == taskDescription);
            Assert.True(taskListFromColumn[0].Priority == taskPriorityWaited);
        }

        [Fact]
        public void Add_one_task_to_column_like_name()
        {
            const string taskName = "test name";
            const string taskDescriptionWaited = "common description";
            const int taskPriorityWaited = 0;
            IColumn column = new Column("test name", 999999);

            column.AddTask(taskName);
            List<ITask> taskListFromColumn = column.GetTaskList();

            Assert.True(taskListFromColumn.Any() != false);
            Assert.True(taskListFromColumn.Count == 1);

            Assert.True(taskListFromColumn[0].Name == taskName);
            Assert.True(taskListFromColumn[0].Description == taskDescriptionWaited);
            Assert.True(taskListFromColumn[0].Priority == taskPriorityWaited);
        }

        [Fact]
        public void Add_one_task_to_column_like_standart_add()
        {
            const string taskNameWaited = "task 0";
            const string taskDescriptionWaited = "common description";
            const int taskPriorityWaited = 0;
            IColumn column = new Column("test name", 999999);

            column.AddTask();
            List<ITask> taskListFromColumn = column.GetTaskList();

            Assert.True(taskListFromColumn.Any() != false);
            Assert.True(taskListFromColumn.Count == 1);

            Assert.True(taskListFromColumn[0].Name == taskNameWaited);
            Assert.True(taskListFromColumn[0].Description == taskDescriptionWaited);
            Assert.True(taskListFromColumn[0].Priority == taskPriorityWaited);
        }

        [Fact]
        public void Get_task_list_from_column()
        {
            IColumn column = new Column("test name", 999999);
            ITask task = new Task("test name", "test description", 999999);
            List<ITask> taskListFromColumnWaited = new List<ITask>();
            taskListFromColumnWaited.Add(new Task("test name", "test description", 0));
            column.AddTask(task);

            List<ITask> taskListFromColumn = column.GetTaskList();

            Assert.True(taskListFromColumn.Count == taskListFromColumnWaited.Count);
            Assert.True(taskListFromColumn[0].Name == taskListFromColumnWaited[0].Name);
            Assert.True(taskListFromColumn[0].Description == taskListFromColumnWaited[0].Description);
            Assert.True(taskListFromColumn[0].Priority == taskListFromColumnWaited[0].Priority);
        }

        [Fact]
        public void Check_the_task_data_mutation_by_getted_column_list()
        {
            IColumn column = new Column("test name", 999999);
            ITask task = new Task("test name", "test description", 999999);
            List<ITask> taskListFromColumnWaited = new List<ITask>();
            taskListFromColumnWaited.Add(new Task("test name", "test description", 0));
            column.AddTask(task);
            const int newPriority = 120000;

            List<ITask> taskListFromColumn = column.GetTaskList();
            taskListFromColumn[0].ChangePriority(newPriority);

            Assert.True(column.GetTaskList().Count == taskListFromColumnWaited.Count);
            Assert.True(column.GetTaskList()[0].Name == taskListFromColumnWaited[0].Name);
            Assert.True(column.GetTaskList()[0].Description == taskListFromColumnWaited[0].Description);
            Assert.True(column.GetTaskList()[0].Priority == taskListFromColumnWaited[0].Priority);

        }

        [Fact]
        public void Get_task_from_column_by_task_priority()
        {
            IColumn column = new Column("test name", 999999);
            column.AddTask(new Task("test name", "test description", 9999999));
            const int taskPriorityForGet_1 = 0;
            const int taskPriorityForGet_2 = 2;

            ITask taskGettedFromColumn_1 = column.GetTask(taskPriorityForGet_1);

            Assert.NotNull(taskGettedFromColumn_1);
            Assert.True(taskGettedFromColumn_1.Name == column.GetTaskList()[taskPriorityForGet_1].Name);
            Assert.True(taskGettedFromColumn_1.Description == column.GetTaskList()[taskPriorityForGet_1].Description);
            Assert.True(taskGettedFromColumn_1.Priority == column.GetTaskList()[taskPriorityForGet_1].Priority);

            ITask taskGettedFromColumn_2 = column.GetTask(taskPriorityForGet_2);

            Assert.True(taskGettedFromColumn_2 == null);
        }

        [Fact]
        public void Get_task_from_column_by_task_name()
        {
            IColumn column = new Column("test name", 999999);
            column.AddTask(new Task("test name", "test description", 9999999));
            const string taskNameForGet_1 = "test name";
            const string taskNameForGet_2 = "name out of list in column name";

            ITask taskGettedFromColumn_1 = column.GetTask(taskNameForGet_1);

            Assert.NotNull(taskGettedFromColumn_1);
            
            ITask taskGettedFromColumn_2 = column.GetTask(taskNameForGet_2);

            Assert.True(taskGettedFromColumn_2 == null);
        }

        [Fact]
        public void Get_task_from_column_by_task()
        {
            IColumn column = new Column("test name", 999999);
            ITask task = new Task("test name", "test description", 9999999);
            column.AddTask(task);
            ITask taskForGet_1 = new Task("test name", "test description", 0);
            ITask taskForGet_2 = (Task)task.Clone();
            

            ITask taskGettedFromColumn_1 = column.GetTask(taskForGet_1);

            Assert.NotNull(taskGettedFromColumn_1);

            ITask taskGettedFromColumn_2 = column.GetTask(taskForGet_2);

            Assert.True(taskGettedFromColumn_2 == null);
        }

        [Fact]
        public void Check_the_task_data_mutation_by_getted_task()
        {
            IColumn column = new Column("test name", 999999);
            column.AddTask(new Task("test name", "test description", 9999999));
            const int taskPriorityForGet_1 = 0;

            ITask taskGettedFromColumn_1 = column.GetTask(taskPriorityForGet_1);
            taskGettedFromColumn_1.ChangePriority(9999999);
            ITask taskFromColumnAfterChangeGettedEarlierTask = column.GetTask(taskPriorityForGet_1);

            Assert.True(taskGettedFromColumn_1 != taskFromColumnAfterChangeGettedEarlierTask);
            Assert.True(taskGettedFromColumn_1.Name == taskFromColumnAfterChangeGettedEarlierTask.Name);
            Assert.True(taskGettedFromColumn_1.Description == taskFromColumnAfterChangeGettedEarlierTask.Description);
            Assert.True(taskGettedFromColumn_1.Priority != taskFromColumnAfterChangeGettedEarlierTask.Priority);
        }

        [Fact]
        public void Delete_task()
        {
            IColumn column = new Column("test name", 999999);
            ITask task = new Task("test name", "test description", 0);
            column.AddTask(task);

            column.DeleteTask(task);

            Assert.True(column.GetTaskList().Any() == false);
        }

    }
}
