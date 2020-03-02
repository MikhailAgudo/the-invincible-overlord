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
    class Entity {
        public Point Position;
        Cell Char;
        bool Player;

        public Entity(int x, int y, char Char, Color CharColor, bool Player = false) {
            this.Position = new Point(x, y);
            this.Char = new Cell(CharColor, Colors.Transparent, Char);
            this.Player = Player;
        }

        public void Move(Point destination) {
            this.Position += destination;
        }

        public Cell GetCell() {
            return this.Char;
        }

        public bool GetPlayer() {
            return this.Player;
        }

        public Point GetPosition() {
            return this.Position;
        }
    }
}
