using System;
using Microsoft.Xna.Framework;

namespace the_invincible_overlord {
    class Map {
        // # does all stuff with tile data, and is relatively clean and simple
        Tiles.TileBase[] _tiles;
        private int _width;
        private int _height;

        public Tiles.TileBase[] MapTiles {
            get => _tiles;
            set {
                _tiles = value;
            }
        }
        public int Width {
            get => _width;
            set {
                _width = value;
            }
        }
        public int Height {
            get => _height;
            set {
                _height = value;
            }
        }

        public Map(int width, int height) {
            _width = width;
            _height = height;
            MapTiles = new Tiles.TileBase[width * height];
        }

        public bool IsTileWalkable(Point location) {
            if (location.X < 0 || location.Y < 0 || location.X >= Width || location.Y >= Height) {
                return false;
            }

            return !_tiles[location.Y * Width + location.X].IsBlockingMove;
        }
    }
}
