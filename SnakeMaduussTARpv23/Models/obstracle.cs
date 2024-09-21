using System;
using System.Collections.Generic;

namespace SnakeMaduussTARpv23.Models
{
    internal class Obstacle
    {
        List<Point> obstacleList;

        public Obstacle(int xStart, int yStart, int width, int height)
        {
            obstacleList = new List<Point>();  // Change List<Figure> to List<Point>

            // Add horizontal and vertical edges of the obstacle.
            for (int x = xStart; x < xStart + width; x++)
            {
                obstacleList.Add(new Point(x, yStart, '─'));                    // Top border
                obstacleList.Add(new Point(x, yStart + height - 1, '─'));       // Bottom border
            }

            for (int y = yStart; y < yStart + height; y++)
            {
                obstacleList.Add(new Point(xStart, y, '│'));                    // Left border
                obstacleList.Add(new Point(xStart + width - 1, y, '│'));        // Right border
            }

            // Add corner points
            obstacleList.Add(new Point(xStart, yStart, '┌'));                    // Top-left corner
            obstacleList.Add(new Point(xStart + width - 1, yStart, '┐'));        // Top-right corner
            obstacleList.Add(new Point(xStart, yStart + height - 1, '└'));       // Bottom-left corner
            obstacleList.Add(new Point(xStart + width - 1, yStart + height - 1, '┘')); // Bottom-right corner
        }

        internal bool IsHit(Figure figure)
        {
            foreach (var obstacle in obstacleList)
            {
                if (figure.IsHit(obstacle))  // Check if Figure hits a Point.
                {
                    return true;
                }
            }
            return false;
        }

        public void Draw()
        {
            foreach (var obstacle in obstacleList)
            {
                obstacle.Draw();  // Draw each Point.
            }
        }
    }
}
