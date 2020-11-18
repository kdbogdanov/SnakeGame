namespace Server.Entities
{
    /// <summary>
    /// Class, which represents cell on game field with two coordinates: X and Y
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        public Cell()
        {
            X = 0;
            Y = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>The x.</value>
        public int X
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>The y.</value>
        public int Y
        {
            get;
            set;
        }

        public override bool Equals(object obj)
        {
            return obj is Cell cell &&
                   X == cell.X &&
                   Y == cell.Y;
        }

        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="cell1">The cell1.</param>
        /// <param name="cell2">The cell2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Cell cell1, Cell cell2) =>
            cell1.X == cell2.X && cell1.Y == cell2.Y;

        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="c1">The c1.</param>
        /// <param name="c2">The c2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Cell c1, Cell c2) => !(c1 == c2);
    }
}
