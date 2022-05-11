using System;
using Xunit;
using ScrumBoard;
using System.Collections.Generic;
using System.Linq;

namespace ScrumBoardTest
{
    public class ScrumBoardTest
    {
        [Fact]
        public void Create_scrumboard()
        {
            const string board_1_Name = "board 1";
            const string board_2_Name = "";
            const string board_2_NameWaited = "ScrumBoard Base Name";

            IBoard board_1 = new Board(board_1_Name);
            IBoard board_2 = new Board(board_2_Name);

            Assert.Equal(board_1_Name, board_1.Name);
            Assert.Empty(board_1.GetColumnList());
            Assert.True(board_1_Name != board_2.Name);
            Assert.True(board_2_NameWaited == board_2.Name);
            Assert.Empty(board_2.GetColumnList());
        }

        [Fact]
        public void Add_column_to_board_like_standart_add()
        {
            const string boardName = "board 1";
            const string columnNameWaited = "column 0";
            const int columnOrderWaited = 0;
            IBoard board = new Board(boardName);

            board.AddColumn();
            IColumn columnGettedFromBoard = board.GetColumn(columnNameWaited);

            Assert.True(columnGettedFromBoard != null);
            Assert.True(columnGettedFromBoard.Name == columnNameWaited);
            Assert.True(columnGettedFromBoard.Order == columnOrderWaited);
            Assert.True(!columnGettedFromBoard.GetTaskList().Any());
        }

        [Fact]
        public void Add_column_to_board_like_name()
        {
            const string boardName = "board 1";
            const string columnName = "column 1";
            const int columnOrderWaited = 0;
            IBoard board = new Board(boardName);

            board.AddColumn(columnName);
            IColumn columnGettedFromBoard = board.GetColumn(columnName);

            Assert.True(columnGettedFromBoard != null);
            Assert.True(columnGettedFromBoard.Name == columnName);
            Assert.True(columnGettedFromBoard.Order == columnOrderWaited);
            Assert.True(!columnGettedFromBoard.GetTaskList().Any());
        }

        [Fact]
        public void Add_column_to_board_like_column()
        {
            const string boardName = "board 1";
            const string columnName = "column 1";
            const int columnOrder = 9999999;
            const int columnOrderWaited = 0;
            IBoard board = new Board(boardName);
            IColumn column = new Column(columnName, columnOrder);

            board.AddColumn(column);
            IColumn columnGettedFromBoard = board.GetColumn(columnName);

            Assert.True(board.GetColumn(columnName) != null);
            Assert.True(columnGettedFromBoard.Name == columnName);
            Assert.True(columnGettedFromBoard.Order == columnOrderWaited);
            Assert.True(!columnGettedFromBoard.GetTaskList().Any());
        }

        [Fact]
        public void Get_column_list_from_board()
        {
            const string boardName = "board 1";
            const int columnOrderWaited = 0;
            IColumn column = new Column("test name", 999999);
            IBoard board = new Board(boardName);
            board.AddColumn(column);
            List<IColumn> columnListFromBoardWaited = new List<IColumn>();
            column.ChangeOrder(columnOrderWaited);
            columnListFromBoardWaited.Add(column);

            List<IColumn> columnListFromBoard = board.GetColumnList();

            Assert.True(columnListFromBoard.Count == columnListFromBoardWaited.Count);
            Assert.True(columnListFromBoard[0].Name == columnListFromBoardWaited[0].Name);
            Assert.True(columnListFromBoard[0].Order == columnListFromBoardWaited[0].Order);
        }

        [Fact]
        public void Check_the_task_data_mutation_by_getted_column_list()
        {
            IBoard board = new Board("board 1");
            IColumn column = new Column("test name", 999999);
            List<IColumn> columnListFromBoardWaited = new List<IColumn>();
            columnListFromBoardWaited.Add(new Column(column.Name, 0));
            board.AddColumn(column);
            const int newColumnOrder = 120000;

            List <IColumn> columnListFromBoard = board.GetColumnList();
            columnListFromBoard[0].ChangeOrder(newColumnOrder);

            Assert.True(board.GetColumnList().Count == columnListFromBoardWaited.Count);
            Assert.True(board.GetColumnList()[0].Name == columnListFromBoardWaited[0].Name);
            Assert.True(board.GetColumnList()[0].Order == columnListFromBoardWaited[0].Order);

        }

        [Fact]
        public void Get_column_from_board_by_column_order()
        {
            IBoard board = new Board("board 1");
            IColumn column = new Column("test name", 999999);
            board.AddColumn(column);
            const int columnOrderForGet_1 = 0;
            const int columnOrderForGet_2 = 2;//out of board column list

            IColumn columnGettedFromBoard_1 = board.GetColumn(columnOrderForGet_1);
            IColumn columnGettedFromBoard_2 = board.GetColumn(columnOrderForGet_2);

            Assert.NotNull(columnGettedFromBoard_1);
            Assert.True(columnGettedFromBoard_1.Name == board.GetColumnList()[columnOrderForGet_1].Name);
            Assert.True(columnGettedFromBoard_1.Order == board.GetColumnList()[columnOrderForGet_1].Order);
            Assert.True(columnGettedFromBoard_2 == null);
        }

        [Fact]
        public void Get_column_from_board_by_column_name()
        {
            IBoard board = new Board("board 1");
            IColumn column = new Column("test name", 999999);
            board.AddColumn(column);
            string columnNameForGet_1 = column.Name;
            string columnNamerForGet_2 = "name from outside of board column list";//out of board column list

            IColumn columnGettedFromBoard_1 = board.GetColumn(columnNameForGet_1);
            IColumn columnGettedFromBoard_2 = board.GetColumn(columnNamerForGet_2);

            Assert.NotNull(columnGettedFromBoard_1);
            Assert.True(columnGettedFromBoard_1.Name == board.GetColumnList()[columnGettedFromBoard_1.Order].Name);
            Assert.True(columnGettedFromBoard_1.Order == board.GetColumnList()[columnGettedFromBoard_1.Order].Order);
            Assert.True(columnGettedFromBoard_2 == null);
        }

        [Fact]
        public void Get_column_from_board_by_column_like_column()
        {
            IBoard board = new Board("board 1");
            IColumn column = new Column("test name", 999999);
            board.AddColumn(column);
            IColumn columnForGet_1 = new Column("test name", 0); ;
            IColumn columnrForGet_2 = new Column("test name", 999999); ;//out of board column list

            IColumn columnGettedFromBoard_1 = board.GetColumn(columnForGet_1);
            IColumn columnGettedFromBoard_2 = board.GetColumn(columnrForGet_2);

            Assert.NotNull(columnGettedFromBoard_1);
            Assert.True(columnGettedFromBoard_1.Name == board.GetColumnList()[columnGettedFromBoard_1.Order].Name);
            Assert.True(columnGettedFromBoard_1.Order == board.GetColumnList()[columnGettedFromBoard_1.Order].Order);
            Assert.True(columnGettedFromBoard_2 == null);
        }

        [Fact]
        public void Check_the_column_data_mutation_by_getted_column()
        {
            IBoard board = new Board("board 1");
            IColumn column = new Column("test name", 999999);
            board.AddColumn(column);
            const int columnOrderForGet_1 = 0;

            IColumn columnGettedFromBoard_1 = board.GetColumn(columnOrderForGet_1);
            columnGettedFromBoard_1.ChangeOrder(9999999);
            IColumn columnFromBoardAfterChangeGettedEarlierColumn = board.GetColumn(columnOrderForGet_1);

            Assert.True(columnGettedFromBoard_1.Name == columnFromBoardAfterChangeGettedEarlierColumn.Name);
            Assert.True(columnGettedFromBoard_1.Order != columnFromBoardAfterChangeGettedEarlierColumn.Order);
        }

        [Fact]
        public void Delete_column()
        {
            IBoard board = new Board("board 1");
            IColumn column = new Column("test name", 999999);
            board.AddColumn(column);
            IColumn columnForDelete_1 = new Column("test name", 0); ;
            IColumn columnForDelete_2 = new Column("test name", 999999); ;//out of board column list

            board.DeleteColumn(columnForDelete_2);
            Assert.True(board.GetColumnList().Any());

            board.DeleteColumn(columnForDelete_1);
            Assert.True(!board.GetColumnList().Any());
        }

        [Fact]
        public void Add_some_column_to_board()
        {
            IBoard board = new Board("board 1");
            IColumn column_1 = new Column("test name 1", 0);
            IColumn column_2 = new Column("test name 2", 2);
            IColumn column_3 = new Column("test name 3", 0);
            board.AddColumn(column_1);
            board.AddColumn(column_2);
            board.AddColumn(column_3);

            List<IColumn> columnListFromBoard = board.GetColumnList();

            Assert.True(columnListFromBoard.Any() != false);
            Assert.True(columnListFromBoard.Count == 3);
            Assert.True(board.GetColumn(column_1.Name).Order == 1);
            Assert.True(board.GetColumn(column_2.Name).Order == 2);
            Assert.True(board.GetColumn(column_3.Name).Order == 0);
        }

        [Fact]
        public void Delete_one_of_column_from_board()
        {
            Board board = new Board("board 1");
            IColumn column_1 = new Column("test name 1", 0);
            IColumn column_2 = new Column("test name 2", 2);
            IColumn column_3 = new Column("test name 3", 0);
            board.AddColumn(column_1);
            board.AddColumn(column_2);
            board.AddColumn(column_3);

            board.DeleteColumn(board.GetColumn(column_1.Name));
            List<IColumn> columnListFromBoard = board.GetColumnList();

            Assert.True(columnListFromBoard.Any() != false);
            Assert.True(columnListFromBoard.Count == 2);
            Assert.True(board.GetColumn(column_1.Name) == null);
            Assert.True(board.GetColumn(column_2.Name).Order == 1);
            Assert.True(board.GetColumn(column_3.Name).Order == 0);


            board.DeleteColumn(board.GetColumn(column_1.Name));
            columnListFromBoard = board.GetColumnList();

            Assert.True(columnListFromBoard.Any() != false);
            Assert.True(columnListFromBoard.Count == 2);
            Assert.True(board.GetColumn(column_1.Name) == null);
            Assert.True(board.GetColumn(column_2.Name).Order == 1);
            Assert.True(board.GetColumn(column_3.Name).Order == 0);
        }



    }
}
