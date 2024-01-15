using UnityEngine;

namespace flexington.Grid
{
    /// <summary>
    /// Interface for visualising a grid of elements of type T.
    /// </summary>
    /// <typeparam name="T">The type of elements in the grid.</typeparam>
    public class DefaultGridVisualiser<T> : GridVisualiser<T>
    {
        private TextMesh[,] _text;

        /// <summary>
        /// Visualizes the given grid.
        /// </summary>
        /// <param name="grid">The grid to visualize.</param>
        public override void Visualise(T[,] grid, Transform parent = null, Vector2 cellSize = default, Vector2 origin = default)
        {
            var gridSize = new Vector2Int(grid.GetLength(0), grid.GetLength(1));
            if (_text == null) _text = new TextMesh[gridSize.x, gridSize.y];
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    if (grid[x, y] == null) continue;

                    if (_text[x, y] == null)
                    {
                        _text[x, y] = CreateWorldText(grid[x, y].ToString(), parent, new Vector3(x, y) * cellSize + origin + cellSize * .5f);
                    }
                    else
                    {
                        _text[x, y].text = grid[x, y].ToString();
                    }
                }
            }
        }
    }
}