using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace the_invincible_overlord {
    class MapGenerator {
        // based on tunnelling room generation algorithm
        // from RogueSharp tutorial
        // https://roguesharp.wordpress.com/2016/03/26/roguesharp-v3-tutorial-simple-room-generation/
        
        public MapGenerator() {
            // empty
        }

        Map _map; // temporarily store the map currently being worked on

        public Map GenerateMap(int mapWidth, int mapHeight, int maxRooms, int minRoomSize, int maxRoomSize) {
            // This RogueSharp-based function uses the "Tunneling" algorithm
            // Which puts rooms all over the map and tunnels connections
            // # Initiate the map
            _map = new Map(mapWidth, mapHeight);

            // # Initiate RNG
            Random randNum = new Random();

            // # Make a list containing all the rooms
            List<Rectangle> Rooms = new List<Rectangle>();

            // # Iterate over max rooms
            for (int i = 0; i < maxRooms; i++) {
                // # Figure out the room's size
                int newRoomWidth = randNum.Next(minRoomSize, maxRoomSize);
                int newRoomHeight = randNum.Next(minRoomSize, maxRoomSize);

                // # Figure out the room's coordinates
                int newRoomX = randNum.Next(0, mapWidth - newRoomWidth - 1);
                int newRoomY = randNum.Next(0, mapHeight - newRoomHeight - 1);

                // # Make a new rectangle which is the room, with XNA's Rectangle
                Rectangle newRoom = new Rectangle(newRoomX, newRoomY, newRoomWidth, newRoomHeight);

                // # See if the room intersects with any of the added rooms in the list
                bool newRoomIntersects = Rooms.Any(room => newRoom.Intersects(room));

                if (!newRoomIntersects) {
                    // # If it doesn't intersect, add it in
                    Rooms.Add(newRoom);
                }
            }

            // # A method that puts in walls everywhere
            FloodWalls();

            foreach (Rectangle room in Rooms) {
                // # Iterate over the room list and use a method to put floors on it
                CreateRoom(room);
            }

            return _map;
        }

        private void CreateRoom(Rectangle room) {
            for (int x = room.Left + 1; x < room.Right; x++) {
                for (int y = room.Top + 1; y < room.Bottom; y++) {
                    CreateFloor(new Point(x, y));
                }
            }

            List<Point> perimeter = GetBorderCellLocations(room);
            foreach(Point location in perimeter) {
                CreateWall(location);
            }
        }

        private void CreateFloor(Point location) {
            _map.MapTiles[location.ToIndex(_map.Width)] = new Tiles.TileFloor();
        }

        private void CreateWall(Point location) {
            _map.MapTiles[location.ToIndex(_map.Width)] = new Tiles.TileWall();
        }

        private void FloodWalls() {
            for (int i = 0; i < _map.MapTiles.Length; i++) {
                _map.MapTiles[i] = new Tiles.TileWall();
            }
        }

        private List<Point> GetBorderCellLocations(Rectangle room) {
            int xMin = room.Left;
            int xMax = room.Right;
            int yMin = room.Top;
            int yMax = room.Bottom;

            // # Put some lines from each of the 4 points in the room, basically
            List<Point> borderCells = GetTileLocationsAlongLine(xMin, yMin, xMax, yMin).ToList();
            borderCells.AddRange(GetTileLocationsAlongLine(xMin, yMin, xMin, yMax));
            borderCells.AddRange(GetTileLocationsAlongLine(xMin, yMax, xMax, yMax));
            borderCells.AddRange(GetTileLocationsAlongLine(xMax, yMin, xMax, yMax));

            return borderCells;
        }

        public IEnumerable<Point> GetTileLocationsAlongLine(int xOrigin, int yOrigin, int xDestination, int yDestination) {
            // # Prevent the line from overflowing boundaries of the map
            xOrigin = ClampX(xOrigin);
            yOrigin = ClampY(yOrigin);
            xDestination = ClampX(xDestination);
            yDestination = ClampY(yDestination);

            int dx = Math.Abs(xDestination - xOrigin);
            int dy = Math.Abs(yDestination - yOrigin);

            int sx = xOrigin < xDestination ? 1 : -1;
            int sy = yOrigin < yDestination ? 1 : -1;
            int err = dx - dy;

            while(true) {
                yield return new Point(xOrigin, yOrigin);
                if (xOrigin == xDestination && yOrigin == yDestination) {
                    break;
                }
                int e2 = 2 * err;
                if (e2 > -dy) {
                    err = err - dy;
                    xOrigin = xOrigin + sx;
                }
                if (e2 < dx) {
                    err = err + dx;
                    yOrigin = yOrigin + sy;
                }
            }
        }

        private int ClampX(int x) {
            if (x < 0)
                x = 0;
            else if (x > _map.Width - 1)
                x = _map.Width - 1;
            return x;
            // OR using ternary conditional operators: return (x < 0) ? 0 : (x > _map.Width - 1) ? _map.Width - 1 : x;
        }

        // sets Y coordinate between top and bottom edges of map
        // to prevent any out-of-bounds errors
        private int ClampY(int y) {
            if (y < 0)
                y = 0;
            else if (y > _map.Height - 1)
                y = _map.Height - 1;
            return y;
            // OR using ternary conditional operators: return (y < 0) ? 0 : (y > _map.Height - 1) ? _map.Height - 1 : y;
        }
    }
}
