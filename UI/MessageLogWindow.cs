using System;
using System.Collections.Generic;
using SadConsole;
using SadConsole.Themes;
using Microsoft.Xna.Framework;

namespace the_invincible_overlord.UI {
    class MessageLogWindow : Window {
        // # Scrollable window that displays messages, using FIFO
        // max lines in a message log
        private static readonly int _maxLines = 100;

        // # First line added is first line removed
        private readonly Queue<string> _lines;

        // # Displays the active messages
        private SadConsole.ScrollingConsole _messageConsole;
        // # The scrollbar for the console
        private SadConsole.Controls.ScrollBar _messageScrollBar;

        // # Tracker for the scrollbar's current position
        private int _scrollBarCurrentPosition;

        // # Thickness of the window border
        private int _windowBorderThickness = 2;

        // # Create new window with title centered
        public MessageLogWindow(int width, int height, string title) : base(width, height) {
            // # Ensure window is correct color then initialize or set some values
            Theme.FillStyle.Background = DefaultBackground;
            _lines = new Queue<string>();
            CanDrag = true;
            Title = title.Align(HorizontalAlignment.Center, Width);

            // # Add the message console, reposition, and add it to the window
            _messageConsole = new SadConsole.ScrollingConsole(width - _windowBorderThickness, _maxLines);
            _messageConsole.Position = new Point(1, 1);
            _messageConsole.ViewPort = new Rectangle(0, 0, width - 1, height - _windowBorderThickness);

            // # Creates the scrollbar to attach to an event handler, then add it to the Window
            _messageScrollBar = new SadConsole.Controls.ScrollBar(SadConsole.Orientation.Vertical, height - _windowBorderThickness);
            _messageScrollBar.Position = new Point(_messageConsole.Width + 1, _messageConsole.Position.X);
            _messageScrollBar.IsEnabled = false;
            _messageScrollBar.ValueChanged += MessageScrollBar_ValueChanged;
            Add(_messageScrollBar);

            // # The mouse input
            UseMouse = true;

            Children.Add(_messageConsole);
        }

        public override void Draw(TimeSpan drawTime) {
            // # Draws the window
            base.Draw(drawTime);
        }

        public override void Update(TimeSpan time) {
            // # Custom update method that allows a vertical scroller
            base.Update(time);

            // # Ensure the scrollbar tracks the current position of the _messageConsole
            if (_messageConsole.TimesShiftedUp != 0 
                | _messageConsole.Cursor.Position.Y >= _messageConsole.ViewPort.Height + _scrollBarCurrentPosition) {
                // # Enable scrollbar if messagelog has been filled up with enough text
                _messageScrollBar.IsEnabled = true;

                // # Make sure we never scroll the entire size of the buffer
                if (_scrollBarCurrentPosition < _messageConsole.Height - _messageConsole.ViewPort.Height) {
                    // # Record how much we scroll up to enable how far back we see
                    // # Ternary Operator alert
                    _scrollBarCurrentPosition += _messageConsole.TimesShiftedUp != 0 ? _messageConsole.TimesShiftedUp : 1;
                }

                // # Determines the max vertical position
                _messageScrollBar.Maximum = _scrollBarCurrentPosition - _windowBorderThickness;

                // # Follow the cursor since we move the render area in the event
                _messageScrollBar.Value = _scrollBarCurrentPosition;

                // # Reset the shift amount
                _messageConsole.TimesShiftedUp = 0;
            }
        }

        public void Add(string message) {
            _lines.Enqueue(message);

            // # If the queue exceeds _maxLines, dequeue
            if (_lines.Count > _maxLines) {
                _lines.Dequeue();
            }

            // # Move the cursor to the last line and print the message
            _messageConsole.Cursor.Position = new Point(1, _lines.Count);
            _messageConsole.Cursor.Print(message + "\n");
        }

        void MessageScrollBar_ValueChanged(object sender, EventArgs e) {
            // # Controls the position of the messagelog viewport
            // # based on scrollbar position using an event handler
            // # Basically, every time the scroll bar's value changes, the viewport position is updated
            _messageConsole.ViewPort = new Rectangle(0, 
                _messageScrollBar.Value + _windowBorderThickness, 
                _messageConsole.Width, 
                _messageConsole.ViewPort.Height);
        }
    }
}
