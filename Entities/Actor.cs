using System;
using SadConsole;
using Entity = SadConsole.Entities.Entity;
using Microsoft.Xna.Framework;

namespace the_invincible_overlord.Entities {
    public abstract class Actor : Entities.Entity {
        private int _health;
        private int _maxHealth;
        private int _attack;
        private int _attackChance;
        private int _defense;
        private int _defenseChance;
        private int _gold;

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
        public int Attack {
            get => _attack;
            set {
                _attack = value;
            }
        }
        public int AttackChance {
            get => _attackChance;
            set {
                _attackChance = value;
            }
        }
        public int Defense {
            get => _defense;
            set {
                _defense = value;
            }
        }
        public int DefenseChance {
            get => _defenseChance;
            set {
                _defenseChance = value;
            }
        }
        public int Gold {
            get => _gold;
            set {
                _gold = value;
            }
        }

        protected Actor(Color foreground, Color background, int glyph, int width=1, int height=1)
            : base(foreground, background, glyph, width, height) {
            this.Animation.CurrentFrame[0].Foreground = foreground;
            this.Animation.CurrentFrame[0].Background = background;
            this.Animation.CurrentFrame[0].Glyph = glyph;
        }

        public bool MoveBy(Point positionChange) {
            // # the actor moves by positionChange tiles in any direction
            // # return true if actor was able to move, false if failed to move
            if(GameLoop.World.CurrentMap.IsTileWalkable(Position + positionChange)) {
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
