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
        private static Tiles.TileBase[] _tiles;
        private const int _roomWidth = 10;
        private const int _roomHeight = 20;

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
            CreateWalls();
            CreateFloors();
            //Console startingConsole = new Console(Width, Height);
            Console startingConsole = new ScrollingConsole(Width, 
                Height, 
                Global.FontDefault, 
                new Rectangle(0, 0, Width, Height),
                _tiles);
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

        private static void CreateFloors() {
            for (int x = 0; x < _roomWidth; x++) {
                for (int y = 0; y < _roomHeight; y++) {
                    _tiles[y * Width + x] = new Tiles.TileFloor();
                }
            }
        }

        private static void CreateWalls() {
            _tiles = new Tiles.TileBase[Width * Height];

            for(int i = 0; i < _tiles.Length; i++) {
                _tiles[i] = new Tiles.TileWall();
            }
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