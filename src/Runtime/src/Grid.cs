using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace flexington.Grid
{
    /// <summary>
    /// Represents a grid of elements of type T.
    /// </summary>
    /// <typeparam name="T">The type of elements in the grid.</typeparam>
    public class Grid<T>
    {
        public Vector2Int GridSize => _gridSize;
        private Vector2Int _gridSize;
        private Vector2 _cellSize;
        private Vector2 _origin;
        private T[,] _grid;
        private GridVisualiser<T> _visualiser;

        /// <summary>
        /// Initializes a new instance of the <see cref="GridBase{T}"/> class with the specified grid size, cell size and origin.
        /// </summary>
        /// <param name="gridSize">The size of the grid.</param>
        /// <param name="cellSize">The size of each cell.</param>
        /// <param name="origin">The origin of the grid.</param>
        public Grid(Vector2Int gridSize, Vector2 cellSize = default, Vector2 origin = default, GridVisualiser<T> visualiser = null)
        {
            _gridSize = gridSize;
            _cellSize = cellSize;
            _origin = origin;
            _grid = new T[gridSize.x, gridSize.y];
            _visualiser = visualiser;
        }

        /// <summary>
        /// Sets the value of the grid at the specified position.
        /// If the position is out of range, a error is logged and the value is ignored.
        /// </summary>
        /// <param name="gridPosition">The position to set the value at.</param>
        /// <param name="value">The value to set.</param>
        public void SetValue(Vector2Int gridPosition, T value)
        {
            if (gridPosition.x < 0 || gridPosition.x > _gridSize.x || gridPosition.y < 0 || gridPosition.y > _gridSize.y)
            {
                Debug.LogError($"Position {gridPosition} is out of range for grid of size {_gridSize}.");
                return;
            }

            _grid[gridPosition.x, gridPosition.y] = value;
        }

        /// <summary>
        /// Sets the value of the grid cell at the given world position to the specified value.
        /// </summary>
        /// <param name="worldPosition">The world position of the cell to set the value of.</param>
        /// <param name="value">The value to set the cell to.</param>
        public void SetValue(Vector2 worldPosition, T value)
        {
            Vector2Int gridPosition = WorldToGrid(worldPosition);
            SetValue(gridPosition, value);
        }

        public void SetValues(T[,] values)
        {
            if (values.GetLength(0) != _gridSize.x || values.GetLength(1) != _gridSize.y)
            {
                Debug.LogError($"Grid size of {values.GetLength(0)}x{values.GetLength(1)} does not match grid size of {_gridSize.x}x{_gridSize.y}.");
                return;
            }

            _grid = values;
        }

        /// <summary>
        /// Gets the value at the specified grid position.
        /// </summary>
        /// <typeparam name="T">The type of value stored in the grid.</typeparam>
        /// <param name="gridPosition">The position of the value in the grid.</param>
        /// <returns>The value at the specified position, or the default value of type T if the position is out of range.</returns>
        public T GetValue(Vector2Int gridPosition)
        {
            if (gridPosition.x < 0 || gridPosition.x > _gridSize.x || gridPosition.y < 0 || gridPosition.y > _gridSize.y)
            {
                Debug.LogError($"Position {gridPosition} is out of range for grid of size {_gridSize}.");
                return default(T);
            }

            return _grid[gridPosition.x, gridPosition.y];
        }

        /// <summary>
        /// Gets the value at the specified world position.
        /// </summary>
        /// <typeparam name="T">The type of value stored in the grid.</typeparam>
        /// <param name="worldPosition">The position of the value in the world.</param>
        /// <returns>The value at the specified position, or the default value of type T if the position is out of range.</returns>
        public T GetValue(Vector2 worldPosition)
        {
            Vector2Int gridPosition = WorldToGrid(worldPosition);
            return GetValue(gridPosition);
        }

        public T[,] GetValues()
        {
            return _grid;
        }

        public void Visualise(GridVisualiser<T> visualiser, Transform parent = null)
        {
            visualiser.Visualise(_grid, parent, _cellSize, _origin);
        }

        public void Visualise(Transform parent = null)
        {
            if (_visualiser == null) _visualiser = new DefaultGridVisualiser<T>();
            _visualiser.Visualise(_grid, parent, _cellSize, _origin);
        }

        private Vector2Int WorldToGrid(Vector2 worldPosition)
        {
            Vector2Int gridPosition = new Vector2Int();
            gridPosition.x = Mathf.FloorToInt((worldPosition.x - _origin.x) / _cellSize.x);
            gridPosition.y = Mathf.FloorToInt((worldPosition.y - _origin.y) / _cellSize.y);
            return gridPosition;
        }
    }
}