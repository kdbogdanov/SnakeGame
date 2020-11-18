using System;
using System.Collections.Generic;
using System.Threading;
using Server.Entities;
using Server.Exceptions;

namespace Server.Models
{
    /// <summary>
    /// Class GameBoard.
    /// </summary>
    public class GameBoard
    {
        /// <summary>
        /// Async. thread
        /// </summary>
        private readonly Timer _snakeUpdater;

        /// <summary>
        /// The _random
        /// </summary>
        private readonly static Random _random = new Random();

        /// <summary>
        /// Creates the new food.
        /// </summary>
        private void CreateNewFood()
        {
            Cell food;
            do
                food = new Cell(_random.Next(GameBoardSize.Width),
                    _random.Next(GameBoardSize.Height));
            while (Snake.Body.Contains(food) || Food.Contains(food));
            Food.Add(food);
        }

        /// <summary>
        /// Gets the turn number.
        /// </summary>
        /// <value>The turn number.</value>
        public int TurnNumber { get; private set; }

        /// <summary>
        /// Getter/local setter for time for turn in ms
        /// </summary>
        /// <value>Time for turn in ms</value>
        public long TimeUntilNextTurnMilliseconds { get; private set; }

        /// <summary>
        /// Snake obj that moves on board
        /// </summary>
        /// <value>The snake.</value>
        public Snake Snake { get; private set; }

        /// <summary>
        /// Gets the size of the game board.
        /// </summary>
        /// <value>The size of the game board.</value>
        public GameBoardSize GameBoardSize { get; private set; }

        /// <summary>
        /// Food created by FoodCreator
        /// </summary>
        /// <value>The food.</value>
        public HashSet<Cell> Food { get; private set; }

        /// <summary>
        /// Game board's constructor
        /// </summary>
        /// <param name="width">board's width</param>
        /// <param name="height">board's height</param>
        /// <param name="timeUntilNextTurnMilliseconds">Time given for snake rotate in ms</param>
        /// <exception cref="ArgumentException">Wrong game board parameters</exception>
        public GameBoard(int width, int height, long timeUntilNextTurnMilliseconds, bool flag)
        {
            if (width < 2 || height < 2 || height < Snake.StartLength)
                throw new ArgumentException("Wrong game board parameters");

            GameBoardSize = new GameBoardSize
            {
                Width = width,
                Height = height
            };
            TimeUntilNextTurnMilliseconds = timeUntilNextTurnMilliseconds;
            Restart();
            if (flag)
            {
                var timerCallback = new TimerCallback(Update);
                _snakeUpdater = new Timer(timerCallback, null, 0, timeUntilNextTurnMilliseconds);
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="GameBoard"/> class.
        /// </summary>
        ~GameBoard() => _snakeUpdater.Dispose();

        /// <summary>
        /// Updates the direction.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public void UpdateDirection(Direction direction) =>
            Snake.UpdateMoveDirection(direction);

        /// <summary>
        /// Restarts the gameBoard, resets TurnNumber, Snake, Food.
        /// </summary>
        public void Restart()
        {
            TurnNumber = 0;
            Snake = new Snake(GameBoardSize.Width, GameBoardSize.Height);
            Food = new HashSet<Cell>();
            CreateNewFood();
        }

        /// <summary>
        /// Checks if snake the intersects with borders.
        /// </summary>
        /// <returns><c>true</c> if intersects, <c>false</c> otherwise.</returns>
        public bool SnakeIntersectsWithBorders()
        {
            if (Snake.Head.X > GameBoardSize.Width - 1
                || Snake.Head.Y > GameBoardSize.Height - 1
                || Snake.Head.X < 0 || Snake.Head.Y < 0)
                return true;
            return false;
        }

        /// <summary>
        /// Updates game board (Snake moves, number of turns increments and etc.)
        /// </summary>
        /// <param name="obj">just need for async. threading</param>
        /// <exception cref="SnakeException"></exception>
        public void Update(object obj)
        {
            try
            {
                TurnNumber++;
                if (TurnNumber % 5 == 0)
                    CreateNewFood();
                Snake.Move();

                if (Food.Contains(Snake.Head))
                {
                    Snake.Eat();
                    Food.Remove(Snake.Head);
                    CreateNewFood();
                }

                if (SnakeIntersectsWithBorders())
                    throw new SnakeException("The snake hit the wall:(");
            }
            catch (SnakeException)
            {
                Restart();
            }
        }
    }
}
