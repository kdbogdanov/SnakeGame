using System;
using Xunit;
using Server.Entities;
using Server.Exceptions;
using System.Collections.Generic;

namespace SnakeGame.Tests
{
    public class SnakeTests
    {
        [Fact]
        public void Should_IncreasesBody_WhenEats()
        {
            // When Tail.X == preTail.X
            Snake snake = new Snake(20, 20);
            var expected = new List<Cell>();
            foreach (var cell in snake.Body)
                expected.Add(cell);
            expected.Add(new Cell(snake.Tail.X, snake.Tail.Y - 1));

            snake.Eat();

            Assert.Equal(expected.Count, snake.Body.Count);
            Assert.Equal(expected, snake.Body);

            // When Tail.X != preTail.X
            snake.Body.Add(new Cell(snake.Tail.X - 1, snake.Tail.Y));
            expected.Add(new Cell(snake.Tail.X, snake.Tail.Y));
            expected.Add(new Cell(snake.Tail.X - 1, snake.Tail.Y));

            snake.Eat();

            Assert.Equal(expected.Count, snake.Body.Count);
            Assert.Equal(expected, snake.Body);
        }

        [Fact]
        public void Should_Intersects()
        {
            Snake snake = new Snake(20, 20);

            var cell = new Cell(snake.Head.X, snake.Head.Y);

            Assert.True(snake.Head == cell);


            cell = new Cell(snake.Head.X - 1, snake.Head.Y - 1);

            Assert.False(snake.Head == cell);
        }

        [Fact]
        public void Should_Move_WhenGetNewDirection()
        {
            Snake snake = new Snake(20, 20);
            snake.UpdateMoveDirection(Direction.Right);
            var expectedNewHead = new Cell(snake.Head.X + 1, snake.Head.Y);

            snake.Move();

            Assert.Equal(expectedNewHead, snake.Head);
        }

        [Fact]
        public void Should_ThrowsSnakeException_WhenAtedSelf()
        {
            Snake snake = new Snake(20, 20);
            for (int i = 0; i < 4; i++) // The minimum length of the snake to cross itself
                snake.Eat();
            snake.UpdateMoveDirection(Direction.Right);
            snake.Move();
            snake.UpdateMoveDirection(Direction.Bottom);
            snake.Move();
            snake.UpdateMoveDirection(Direction.Left);

            Exception ex = Assert.Throws<SnakeException>(() => snake.Move());

            Assert.Equal("The snake ate its own body!!!", ex.Message);
        }

    }
}
