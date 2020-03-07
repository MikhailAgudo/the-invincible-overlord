using System;
using SadConsole;
using SadConsole.Controls;
using Microsoft.Xna.Framework;

namespace the_invincible_overlord.UI {
    class UIManager : ContainerConsole {
        // # Creates, holds and destroys all consoles used
        // # makes consoles very easily addressable
        public ScrollingConsole MapConsole;
        public Window MapWindow;
        public MessageLogWindow MessageLog;

        public UIManager() {
            // # Must be set to true, because this console will be the console we need to see
            IsVisible = true;
            IsFocused = true;

            // # This is the only screen that SadConsole will process
            Parent = SadConsole.Global.CurrentScreen;
        }

        public override void Update(TimeSpan timeElapsed) {
            // # override means we are extending the original method by adding some of our own stuff
            // # UIManager inherits from ConsoleContainer, which eventually inherits from SadConsole.ScreenObject
            // # which is where Update(TimeSpan time) lives

            // # Triggered before every single frame update
            // # Where all the logic is before Draw is called

            // Called each logic update
            CheckKeyboard();
            base.Update(timeElapsed);
        }

        public void Init() {
            CreateConsoles();

            MessageLog = new MessageLogWindow(GameLoop.GameWidth, GameLoop.GameHeight / 2, "Message Log");
            Children.Add(MessageLog);
            MessageLog.Show();
            MessageLog.Position = new Point(0, GameLoop.GameHeight / 2);

            MessageLog.Add("Testing 123");
            MessageLog.Add("Testing 1123123312");
            MessageLog.Add("Testing 69");
            MessageLog.Add("Testing 420");
            MessageLog.Add("Testing 420 blazeit");
            MessageLog.Add("Testing 123");
            MessageLog.Add("Testing 1123123312");
            MessageLog.Add("Testing 69");
            MessageLog.Add("Testing 420");
            MessageLog.Add("Testing 420 blazeit");
            MessageLog.Add("Testing 123");
            MessageLog.Add("Testing 1123123312");
            MessageLog.Add("Testing 69");
            MessageLog.Add("Testing 420");
            MessageLog.Add("Testing 420 blazeit");
            MessageLog.Add("Testing 123");
            MessageLog.Add("Testing 1123123312");
            MessageLog.Add("Testing 69");
            MessageLog.Add("Testing 420");
            MessageLog.Add("Testing 420 blazeit");
            MessageLog.Add("Testing 123");
            MessageLog.Add("Testing 1123123312");
            MessageLog.Add("Testing 69");
            MessageLog.Add("Testing 420");
            MessageLog.Add("Testing 420 blazeit");

            // # Load the map into the MapConsole
            LoadMap(GameLoop.World.CurrentMap);

            // # With the MapConsole ready, build the window
            CreateMapWindow(GameLoop.GameWidth / 2, GameLoop.GameHeight / 2, "Game Map");
            UseMouse = true;

            // # Start the game with camera on player
            CenterOnActor(GameLoop.World.Player);
        }

        public void CreateConsoles() {
            MapConsole = new ScrollingConsole(GameLoop.GameWidth, GameLoop.GameHeight);
        }

        public void CreateMapWindow(int width, int height, string title) {
            MapWindow = new Window(width, height);
            MapWindow.CanDrag = true;

            // # Make the console short enough to show the window title and borders, then position it away from borders
            int mapConsoleWidth = width - 2;
            int mapConsoleHeight = height - 2;

            // # Resize the map console's viewport to fit inside the window's borders
            MapConsole.ViewPort = new Rectangle(0, 0, mapConsoleWidth, mapConsoleHeight);

            // # Reposition the map console so it doesn't overlap with the left/top edges
            MapConsole.Position = new Point(1, 1);

            // # Close window button
            Button closeButton = new Button(3, 1);
            closeButton.Position = new Point(0, 0);
            closeButton.Text = "[X]";

            // # Add closeButton to the window's list of UI elements
            MapWindow.Add(closeButton);

            // # Center the title text at the top of the window
            MapWindow.Title = title.Align(HorizontalAlignment.Center, mapConsoleWidth);

            // # Add the map viewer to the window
            MapWindow.Children.Add(MapConsole);

            // # The MapWindow becomes the child console of UIManager
            Children.Add(MapWindow);

            // # Add the player to the MapConsole's render list
            MapConsole.Children.Add(GameLoop.World.Player);

            // # Without this, the window will never be visible on screen
            MapWindow.Show();
        }

        private void LoadMap(Map map) {
            // # Load the map's tiles into the console
            MapConsole = new ScrollingConsole(GameLoop.World.CurrentMap.Width,
                GameLoop.World.CurrentMap.Height,
                Global.FontDefault,
                new Rectangle(0, 0, GameLoop.GameWidth, GameLoop.GameHeight),
                map.MapTiles
                );

            // # Then sync all of the map's entities
            SyncMapEntities(map);
        }

        private void SyncMapEntities(Map map) {
            // # Add the entire list of entities found in World.CurrentMap's Entities SpatialMap to the MapConsole, so they can be onscreen
            // # Remove all entities from the console first
            MapConsole.Children.Clear();

            // # Then put all of the entities in bulk
            foreach(Entities.Entity entity in map.Entities.Items) {
                // # the entity in SpatialMap is an Item, and ISpatial Maps doesn't care what's in it they're just called Items
                MapConsole.Children.Add(entity);
            }

            // # This portion means: If a monster is killed, it needs to disappear from both Map and MapConsole
            // ## The SpatialMap event tells the program to update MapConsole every time there is an entity removed (and added) in the map
            // # "Subscribe"s to the Entities ItemAdded listener, so we can keep our MapConsole entities in sync
            map.Entities.ItemAdded += OnMapEntityAdded;

            // # "Subscribe"s to the Entities ItemRemoved listener, so we can keep our MapConsole entities in sync
            map.Entities.ItemRemoved += OnMapEntityRemoved;
        }

        public void OnMapEntityRemoved(object sender, GoRogue.ItemEventArgs<Entities.Entity> args) {
            MapConsole.Children.Remove(args.Item);
        }

        public void OnMapEntityAdded(object sender, GoRogue.ItemEventArgs<Entities.Entity> args) {
            MapConsole.Children.Add(args.Item);
        }

        private void CheckKeyboard() {
            // # Looks at SadConsole's global keyboard state
            // F5 to make fullscreen
            if (SadConsole.Global.KeyboardState.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.F5)) {
                SadConsole.Settings.ToggleFullScreen();
            }

            // Undo command
            if (SadConsole.Global.KeyboardState.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.Z)) {
                GameLoop.CommandManager.UndoMoveActorBy();
                CenterOnActor(GameLoop.World.Player);
            }

            // Repeat last command
            if (SadConsole.Global.KeyboardState.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.X)) {
                GameLoop.CommandManager.RedoMoveActorBy();
                CenterOnActor(GameLoop.World.Player);
            }

            if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.W)) {
                GameLoop.CommandManager.MoveActorBy(GameLoop.World.Player, new Point(0, -1));
                CenterOnActor(GameLoop.World.Player);
            }
            else if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.S)) {
                GameLoop.CommandManager.MoveActorBy(GameLoop.World.Player, new Point(0, 1));
                CenterOnActor(GameLoop.World.Player);
            }
            else if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.A)) {
                GameLoop.CommandManager.MoveActorBy(GameLoop.World.Player, new Point(-1, 0));
                CenterOnActor(GameLoop.World.Player);
            }
            else if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.D)) {
                GameLoop.CommandManager.MoveActorBy(GameLoop.World.Player, new Point(1, 0));
                CenterOnActor(GameLoop.World.Player);
            }
        }

        public void CenterOnActor(Entities.Actor actor) {
            // Centers the viewport/screen on the actor
            MapConsole.CenterViewPortOnPoint(actor.Position);
        }
    }
}
