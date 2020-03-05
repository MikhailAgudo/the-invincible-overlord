using System;
using SadConsole;
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
    }
}
