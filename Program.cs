using System;
using SadConsole;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace the_invincible_overlord {
    class Program {
        public const int Width = 80;
        public const int Height = 50;
        static Engine MainEngine = new Engine(Width, Height);

        static void Main() {
            // Setup the engine and create the main window.
            SadConsole.Game.Create(Width, Height);

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;

            // Start the game.
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        private static void Init() {
            MainEngine.Main();
            SadConsole.Global.CurrentScreen = MainEngine.GetMap(0);
            Global.CurrentScreen.IsFocused = true;
        }

        //private static void Init() {
        //    // Any startup code for your game. We will use an example console for now
        //    var startingConsole = SadConsole.Global.CurrentScreen;
        //    startingConsole.FillWithRandomGarbage();
        //    startingConsole.Fill(new Rectangle(3, 3, 27, 5), null, Color.Black, 0, SpriteEffects.None);
        //    startingConsole.Print(6, 5, "Hello from SadConsole", ColorAnsi.CyanBright);
        //}
    }
}