using System;
using SadConsole;
using GoRogue;
using SadConsole.Components;
using Microsoft.Xna.Framework;

namespace the_invincible_overlord.Entities {
    public abstract class Entity : SadConsole.Entities.Entity, GoRogue.IHasID {
        // # Extends SadConsole's Entity class by adding an ID to it with GoRogue
        public uint ID { get; private set; } // stores the entity's unique identification number

        protected Entity(Color foreground, Color background, int glyph, int width=1, int height=1) : base(width,height) {
            this.Animation.CurrentFrame[0].Foreground = foreground;
            this.Animation.CurrentFrame[0].Background = background;
            this.Animation.CurrentFrame[0].Glyph = glyph;

            // Create a new unique identifier for the entity
            ID = Map.IDGenerator.UseID();

            // Ensure that the entity position/offset is tracked by scrollingconsoles
            Components.Add(new EntityViewSyncComponent());
        }
    }
}
