using System;
using System.Text;
using SadConsole;
using GoRogue.DiceNotation;
using Microsoft.Xna.Framework;

namespace the_invincible_overlord.Commands {
    class CommandManager {
        // # Has all the generic actions for actors: combat, movement, etc.
        // # Stores actor's last move
        private Point _lastMoveActorPoint;
        private Entities.Actor _lastMoveActor;

        public CommandManager() {

        }

        public bool MoveActorBy(Entities.Actor actor, Point position) {
            _lastMoveActor = actor;
            _lastMoveActorPoint = position;
            return actor.MoveBy(position);
        }

        public bool RedoMoveActorBy() {
            // # Redoes the last move of an actor
            // # Make sure an actor is available to redo
            if (_lastMoveActor != null) {
                return _lastMoveActor.MoveBy(_lastMoveActorPoint);
            }
            return false;
        }

        public bool UndoMoveActorBy() {
            // # Make sure there is an actor to undo
            if (_lastMoveActor != null) {
                // # Reverse the directions of the last move
                _lastMoveActorPoint = new Point(-_lastMoveActorPoint.X, -_lastMoveActorPoint.Y);

                if (_lastMoveActor.MoveBy(_lastMoveActorPoint)) {
                    _lastMoveActorPoint = new Point(0, 0);
                    return true;
                }
                else {
                    _lastMoveActorPoint = new Point(0, 0);
                    return false;
                }
            }
            return false;
        }

        public void Attack(Entities.Actor attacker, Entities.Actor defender) {
            // # Create two messages that describe the outcome of the attack and defense
            StringBuilder attackMessage = new StringBuilder();
            StringBuilder defenseMessage = new StringBuilder();

            // # Count the amount of damage done and the number of successful blocks
            int hits = ResolveAttack(attacker, defender, attackMessage);
            int blocks = ResolveDefense(defender, hits, attackMessage, defenseMessage);

            // # Display the outcome in the message log
            GameLoop.UIManager.MessageLog.Add(attackMessage.ToString());
            if (!string.IsNullOrWhiteSpace(defenseMessage.ToString())) {
                GameLoop.UIManager.MessageLog.Add(defenseMessage.ToString());
            }

            int damage = hits - blocks;

            // # Defender then takes damage
            ResolveDamage(defender, damage);
        }

        private static int ResolveAttack(Entities.Actor attacker, Entities.Actor defender, StringBuilder attackMessage) {
            int hits = 0;
            attackMessage.AppendFormat("{0} attacks {1}, ", attacker.Name, defender.Name);

            for (int dice = 0; dice < attacker.Attack; dice++) {
                int diceOutcome = Dice.Roll("1d100");
                if (diceOutcome <= attacker.AttackChance) {
                    hits++;
                }
            }

            return hits;
        }

        private static int ResolveDefense(Entities.Actor defender, int hits, StringBuilder attackMessage, StringBuilder defenseMessage) {
            int blocks = 0;
            if (hits > 0) {
                attackMessage.AppendFormat("scoring {0} hits.", hits);
                defenseMessage.AppendFormat("{0} defends and rolls: ", defender.Name);

                for (int dice = 0; dice < defender.Defense; dice++) {
                    int diceOutcome = Dice.Roll("1d100");
                    if (diceOutcome <= defender.DefenseChance) {
                        blocks++;
                    }
                }
                defenseMessage.AppendFormat("resulting in {0} blocks.", blocks);
            }
            else {
                attackMessage.Append("and misses completely!");
            }

            return blocks;
        }

        private static void ResolveDamage(Entities.Actor defender, int damage) {
            if(damage > 0) {
                defender.Health -= damage;
                GameLoop.UIManager.MessageLog.Add($"{defender.Name} was hit for {damage} damage.");
                if (defender.Health <= 0) {
                    ResolveDeath(defender);
                }
            }
            else {
                GameLoop.UIManager.MessageLog.Add($"{defender.Name} blocked all damage!");
            }
        }

        private static void ResolveDeath(Entities.Actor defender) {
            GameLoop.World.CurrentMap.Remove(defender);
            if (defender is Entities.Player) {
                GameLoop.UIManager.MessageLog.Add($"{defender.Name} was killed.");
            }
            else if (defender is Entities.Monster) {
                GameLoop.UIManager.MessageLog.Add($"{defender.Name} died and dropped {defender.Gold} gold coins.");
            }
        }
    }
}
