﻿using System;
using SadConsole.Components;
using Microsoft.Xna.Framework;

namespace the_invincible_overlord {
    class World {
        // # All game state data is saved here
        // # Also creates and processes generators for mapgen

        // map creation and storage stuff
        private int _mapWidth = 100;
        private int _mapHeight = 100;
        private Tiles.TileBase[]_mapTiles;
        private int _maxRooms = 100;
        private int _minRoomSize = 4;
        private int _maxRoomSize = 15;
        public Map CurrentMap { get; set; }

        // Player data
        public Entities.Player Player { get; set; }

        // # New game world and stores it in public
        public World() {
            // # Build the map
            CreateMap();

            // # Put the player in it
            CreatePlayer();
        }

        private void CreateMap() {
            _mapTiles = new Tiles.TileBase[_mapWidth * _mapHeight];
            CurrentMap = new Map(_mapWidth, _mapHeight);
            MapGenerator mapGen = new MapGenerator();
            CurrentMap = mapGen.GenerateMap(_mapWidth, _mapHeight, _maxRooms, _minRoomSize, _maxRoomSize);
        }

        private void CreatePlayer() {
            Player = new Entities.Player(Color.Yellow, Color.Transparent);
            Player.Position = new Point(5, 5);

            // # Put in a component that allows viewport stuff to work
            Player.Components.Add(new EntityViewSyncComponent());
        }
    }
}
