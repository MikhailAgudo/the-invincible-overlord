using System;
using SadConsole;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace the_invincible_overlord {
    class GameLoop {
        public const int Width = 80;
        public const int Height = 25;
        //static Engine MainEngine = new Engine(Width, Height);
        private static Entities.Player player;

        public static Map GameMap;
        private static int _mapWidth = 100;
        private static int _mapHeight = 100;
        private static int _maxRooms = 500;
        private static int _minRoomSize = 4;
        private static int _maxRoomSize = 15;

        static void Main() {
            // Setup the engine and create the main window.
            SadConsole.Game.Create(Width, Height);

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;

            SadConsole.Game.OnUpdate = Update;

            // Start the game.
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        private static void Update(GameTime time) {
            // Called each logic update
            CheckKeyboard();
        }

        private static void Init() {
            // # Initialize new game map
            GameMap = new Map(_mapWidth, _mapHeight);

            // # Instantiate a new generator
            MapGenerator mapGen = new MapGenerator();
            GameMap = mapGen.GenerateMap(_mapWidth, _mapHeight, _maxRooms, _minRoomSize, _maxRoomSize);

            //Console startingConsole = new Console(Width, Height);
            Console startingConsole = new ScrollingConsole(GameMap.Width, 
                GameMap.Height, 
                Global.FontDefault, 
                new Rectangle(0, 0, Width, Height),
                GameMap.MapTiles);
            SadConsole.Global.CurrentScreen = startingConsole;
            CreatePlayer();
            startingConsole.Children.Add(player);
        }

        // # Old INit
        //private static void Init() {
        //    MainEngine.Main();
        //    SadConsole.Global.CurrentScreen = MainEngine.GetMap(0);
        //    Global.CurrentScreen.IsFocused = true;
        //}

        private static void CreatePlayer() {
            player = new Entities.Player(Color.Yellow, Color.Transparent);
            player.Position = new Point(5, 5);
        }

        private static void CheckKeyboard() {
            // F5 to make fullscreen
            if (SadConsole.Global.KeyboardState.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.F5)) {
                SadConsole.Settings.ToggleFullScreen();
            }
            
            if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.W)) {
                player.MoveBy(new Point(0, -1));
            }
            else if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.S)) {
                player.MoveBy(new Point(0, 1));
            }
            else if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.A)) {
                player.MoveBy(new Point(-1, 0));
            }
            else if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.D)) {
                player.MoveBy(new Point(1, 0));
            }
        }
    }
}