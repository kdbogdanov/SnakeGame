using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Server.Exceptions;

namespace Server.Entities
{
    /// <summary>
    /// Class representing snake on game board.
    /// </summary>
    public class Snake
    {
        /// <summary>
        /// The start length of snake.
        /// </summary>
        public const int StartLength = 2;

        /// <summary>
        /// Gets the body of snake(list of Cells).
        /// </summary>
        /// <value>The body of the snake.</value>
        public List<Cell> Body
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the length of the snake.
        /// </summary>
        /// <value>The length of the snake.</value>
        [JsonIgnore]
        public int Length => Body.Count;

        /// <summary>
        /// Gets the tail of the snake.
        /// </summary>
        /// <value>The tail of the snake.</value>
        [JsonIgnore]
        public Cell Tail => Body.Last();

        /// <summary>
        /// Gets the head of the snake.
        /// </summary>
        /// <value>The head of the snake.</value>
        [JsonIgnore]
        public Cell Head => Body.First();

        /// <summary>
        /// Gets the move direction.
        /// </summary>
        /// <value>The move direction.</value>
        [JsonIgnore]
        public Direction MoveDirection
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the previous move direction.
        /// </summary>
        /// <value>The previous move direction.</value>
        [JsonIgnore]
        public Direction PrevMoveDirection
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Snake"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public Snake(int width, int height)
        {
            Body = new List<Cell>
            {
                new Cell(width / 2, height / 2),
                new Cell(width / 2, height / 2 - 1)
            };
            MoveDirection = Direction.Top;
            PrevMoveDirection = MoveDirection;
        }

        /// <summary>
        /// Eats the food on game board.
        /// </summary>
        public void Eat()
        {
            var preTail = Body[Body.Count - 2];
            Cell newSnakeCell;

            if (Tail.X == preTail.X)
                newSnakeCell = new Cell(Tail.X, 2 * Tail.Y - preTail.Y);
            else
                newSnakeCell = new Cell(2 * Tail.X - preTail.X, Tail.Y);

            Body.Add(newSnakeCell);
        }

        /// <summary>
        /// Updates the move direction.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public void UpdateMoveDirection(Direction direction) =>
            MoveDirection = direction;

        /// <summary>
        /// Moves the snake on game board.
        /// </summary>
        /// <exception cref="SnakeException">The snake ate its own body!!!</exception>
        public void Move()
        {
            if (PrevMoveDirection + 2 == MoveDirection || MoveDirection + 2 == PrevMoveDirection)
                MoveDirection = PrevMoveDirection;

            Cell newHead = MoveDirection switch
            {
                Direction.Left => new Cell(Head.X - 1, Head.Y),
                Direction.Bottom => new Cell(Head.X, Head.Y - 1),
                Direction.Right => new Cell(Head.X + 1, Head.Y),
                Direction.Top => new Cell(Head.X, Head.Y + 1),
                _ => throw new ArgumentException("Unknown move command."),
            };
            Body.Insert(0, newHead);
            Body.Remove(Body.Last());

            // Checks if snake's head intersects with her body
            for (int i = 1; i < Length; i++)
                if (Body[i].X == Head.X && Body[i].Y == Head.Y)
                    throw new SnakeException("The snake ate its own body!!!");

            PrevMoveDirection = MoveDirection;
        }
    }
}
