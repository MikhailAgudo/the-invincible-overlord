using System;
using System.Collections.Generic;
using SadConsole;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Colors = Microsoft.Xna.Framework.Color;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace the_invincible_overlord {
    class Engine {
        List<Console> ConsoleList;
        List<Components.Map> MapList;
        int StandardWidth;
        int StandardHeight;

        public Engine(int width, int height) {
            this.ConsoleList = new List<Console>();
            this.MapList = new List<Components.Map>();
            this.StandardWidth = width;
            this.StandardHeight = height;
        }

        public void Main() {
            int ScreenWidth = StandardWidth;
            int ScreenHeight = StandardHeight;

            Components.Map console = new Components.Map();
            MapList.Add(console);

            //Console console = new Console(ScreenWidth, ScreenHeight);
            //BGFGTest(console);
            //ConsoleList.Add(console);
        }

        public Console GetConsole(int consoleNum) {
            if (ConsoleList[consoleNum] != null) {
                return ConsoleList[consoleNum];
            }
            return null;
        }

        public Console GetConsole(Console targetConsole) {
            if (targetConsole != null) {
                for (int i = 0; i < ConsoleList.Count; i++) {
                    if (ConsoleList[i] == targetConsole) {
                        return ConsoleList[i];
                    }
                }
            }
            return null;
        }

        public Components.Map GetMap(int mapNum) {
            if (MapList[mapNum] != null) {
                return MapList[mapNum];
            }
            return null;
        }

        public Components.Map GetMap(Components.Map targetMap) {
            if (targetMap != null) {
                for (int i = 0; i < MapList.Count; i++) {
                    if (ConsoleList[i] == targetMap) {
                        return MapList[i];
                    }
                }
            }
            return null;
        }

        private void BGFGTest(Console console) {
            if (console.Width == StandardWidth && console.Height == StandardHeight) {
                //console.SetBackground(0, 0, Colors.White);
                console.SetForeground(0, 0, Colors.Blue);
                console.SetGlyph(0, 0, '@');
            }
        }
    }
}
