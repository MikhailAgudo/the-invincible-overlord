using System;
using System.Linq;
using GoRogue;
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

        public GoRogue.MultiSpatialMap<Entities.Entity> Entities; // # Keeps track of all the entities in the map
        public static GoRogue.IDGenerator IDGenerator = new GoRogue.IDGenerator(); // # IDGenerator that all entities can access

        public Map(int width, int height) {
            // # Builds a map based on the width and height
            _width = width;
            _height = height;
            MapTiles = new Tiles.TileBase[width * height];
            this.Entities = new GoRogue.MultiSpatialMap<Entities.Entity>();
        }

        public bool IsTileWalkable(Point location) {
            // # Checks if the actor tried to walk off the map or into a non-walkable tile
            if (location.X < 0 || location.Y < 0 || location.X >= Width || location.Y >= Height) {
                return false;
            }

            return !_tiles[location.Y * Width + location.X].IsBlockingMove;
        }

        public T GetEntityAt<T>(Point location) where T : Entities.Entity {
            // # Checks if a certain type of entity is at a location in the manager's list of entities
            // # and if it is, return that entity
            // # T is a placeholder variable for the type of object the entity is i.e. it can be anything
            // ## T is called a Generic
            // # The reason for this is, what if we had a Hero class, Monster class and Player class?
            // ## We'd need to create functions for all 3 types
            // ## Meanwhile, T allows us to use any of those types with only 1 method. Epic
            return Entities.GetItems(location).OfType<T>().FirstOrDefault();
        }

        public void Remove(Entities.Entity entity) {
            // # Removes an entity from the MultiSpatialMap
            // # Remove from SpatialMap
            Entities.Remove(entity);

            // # Link the entity's Moved event to a new handler
            entity.Moved -= OnEntityMoved;
        }

        public void Add(Entities.Entity entity) {
            // # Adds an entity, basically just the reverse of Remove
            Entities.Add(entity, entity.Position);

            entity.Moved += OnEntityMoved;
        }

        public void OnEntityMoved(object sender, Entities.Entity.EntityMovedEventArgs args) {
            Entities.Move(args.Entity as Entities.Entity, args.Entity.Position);
        }
    }
}
