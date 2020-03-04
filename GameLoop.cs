using System;
using SadConsole;
using SadConsole.Components;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace the_invincible_overlord {
    class GameLoop {
        public const int GameWidth = 80;
        public const int GameHeight = 25;

        // # The manager stuff
        public static UI.UIManager UIManager;
        public static World World;

        static void Main() {
            // Setup the engine and create the main window.
            SadConsole.Game.Create(GameWidth, GameHeight);

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;

            SadConsole.Game.OnUpdate = Update;

            // Start the game.
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        private static void Update(GameTime time) {

        }

        private static void Init() {
            // # Instantiate the UIManager
            UIManager = new UI.UIManager();

            // # Make a new World
            World = new World();

            UIManager.Init();

        }
    }
}