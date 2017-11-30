using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ScreenManagerLibrary
{
    public class ScreenManager
    {
        private readonly List<Screen> _screens = new List<Screen>();
        private bool _started;
        private Screen _previous;

        private Screen _activeScreen;

        public int ScreenCount => _screens.Count;

        public void AddScreen(Screen screen)
        {
            foreach (Screen scr in _screens)
            {
                if (scr.Name == screen.Name)
                {
                    return;
                }
            }
            _screens.Add(screen);
        }

        public Screen GetScreen(int index)
        {
            return _screens[index];
        }

        public void GotoScreen(string name)
        {
            foreach (Screen screen in _screens)
            {
                if (screen.Name == name)
                {
                    // Shutsdown previous screen
                    _previous = _activeScreen;
                    _activeScreen?.Shutdown();

                    // Initializes New Screen
                    _activeScreen = screen;
                    //if (_started) _activeScreen.Initialize();

                    return;
                }
            }
        }

        public void GoBack()
        {
            if (_previous != null)
            {
                GotoScreen(_previous.Name);
            }
        }

        public void Initialize()
        {
            _started = true;
            _activeScreen?.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            if (_started == false) return;

            _activeScreen?.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            if (_started == false) return;

            _activeScreen?.Draw(gameTime);
        }
    }
}