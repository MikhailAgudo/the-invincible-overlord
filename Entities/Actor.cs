using System;
using SadConsole;
using Entity = SadConsole.Entities.Entity;
using Microsoft.Xna.Framework;

namespace the_invincible_overlord.Entities {
    class Actor : Entity {
        private int _health;
        private int _maxHealth;

        public int Health {
            get => _health;
            set {
                _health = value;
            }
        }
        public int MaxHealth {
            get => _maxHealth;
            set {
                _maxHealth = value;
            }
        }

        protected Actor(Color foreground, Color background, int glyph, int width=1, int height=1)
            : base(width, height) {
            this.Animation.CurrentFrame[0].Foreground = foreground;
            this.Animation.CurrentFrame[0].Background = background;
            this.Animation.CurrentFrame[0].Glyph = glyph;
        }

        public bool MoveBy(Point positionChange) {
            // # the actor moves by positionChange tiles in any direction
            // # return true if actor was able to move, false if failed to move
            if(the_invincible_overlord.GameLoop.isTileWalkable(Position + positionChange)) {
                Position += positionChange;
                return true;
            }
            return false;
        }

        public bool MoveTo(Point newPosition) {
            // # the actor moves to newPosition location
            // # returns true if actor was able to move, false if failed to move
            Position = newPosition;
            return true;
        }

    }
}
