using System;
using System.Collections.Generic;
using SadConsole;
using Console = SadConsole.Console;
using Directions = SadConsole.Directions;
using SadConsole.Input;
using Microsoft.Xna.Framework;
using Colors = Microsoft.Xna.Framework.Color;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace the_invincible_overlord.Components {
    class Map : ContainerConsole {
        List<Entity> EntityList;
        public Console MapConsole;
        public Cell PlayerGlyph;

        private Point _playerPosition;
        public Point PlayerPosition {
            get => _playerPosition;
            private set {
                MapConsole.Clear(_playerPosition.X, _playerPosition.Y);
                _playerPosition = value;
                PlayerGlyph.CopyAppearanceTo(MapConsole[_playerPosition.X, _playerPosition.Y]);
            }
        }

        public Map(int width = 0, int height = 0) {
            if (width <= 0 && height <= 0) {
                width = Global.RenderWidth / Global.FontDefault.Size.X;
                height = Global.RenderHeight / Global.FontDefault.Size.Y;
            }

            // # Setup the map
            this.MapConsole = new Console(width, height);
            this.MapConsole.Parent = this;

            // # Setup the player
            this.PlayerGlyph = new Cell(Colors.White, Color.Transparent, '@');
            PlayerPosition = new Point(4, 4);
            PlayerGlyph.CopyAppearanceTo(MapConsole[PlayerPosition.X, PlayerPosition.Y]);
        }

        public override bool ProcessKeyboard(Keyboard info) {
            Point newPlayerPosition = PlayerPosition;

            // # WASD for movement, separate horizontal and vertical to allow diagonal movement
            if (info.IsKeyPressed(Keys.W)) {
                newPlayerPosition += Directions.North;
            } else if (info.IsKeyPressed(Keys.S)) {
                newPlayerPosition += Directions.South;
            } else if (info.IsKeyPressed(Keys.A)) {
                newPlayerPosition += Directions.West;
            } else if (info.IsKeyPressed(Keys.D)) {
                newPlayerPosition += Directions.East;
            }

            // # If values are different, change PlayerPosition's
            if (newPlayerPosition != PlayerPosition) {
                PlayerPosition = newPlayerPosition;
                return true;
            }

            return false;
        }
    }
}
