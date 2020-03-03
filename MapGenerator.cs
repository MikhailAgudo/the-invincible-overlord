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
    }
}
