using System;
using System.Collections.Generic;

namespace SnakeMaduussTARpv23.Models
{
    internal class Obstacle
    {
        private List<Point> obstaclePoints;

        public Obstacle(int xStart, int yStart, int width, int height)
        {
            obstaclePoints = new List<Point>();
            for (int x = xStart; x < xStart + width; x++)
            {
                obstaclePoints.Add(new Point(x, yStart, '─'));
                obstaclePoints.Add(new Point(x, yStart + height - 1, '─'));
            }
            for (int y = yStart; y < yStart + height; y++)
            {
                obstaclePoints.Add(new Point(xStart, y, '│'));
                obstaclePoints.Add(new Point(xStart + width - 1, y, '│'));
            }
            obstaclePoints.Add(new Point(xStart, yStart, '┌'));
            obstaclePoints.Add(new Point(xStart + width - 1, yStart, '┐'));
            obstaclePoints.Add(new Point(xStart, yStart + height - 1, '└'));
            obstaclePoints.Add(new Point(xStart + width - 1, yStart + height - 1, '┘'));
        }

        public void Draw()
        {
            foreach (Point p in obstaclePoints)
            {
                p.Draw();
            }
        }
    }
}
