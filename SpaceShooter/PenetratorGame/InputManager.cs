using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace PenetratorGame
{
    // TODO: support mouse and gamepad
    public static class InputManager
    {
        #region Action Enumeration

        /// <summary>
        /// The actions that are possible within the game.
        /// </summary>
        public enum Action
        {
            ResetCamera,
            MoveCharacterUp,
            MoveCharacterDown,
            MoveCharacterLeft,
            MoveCharacterRight,
            ZoomIn,
            ZoomOut,
            RotateLeft,
            RotateRight,
            TotalActionCount
        }

        /// <summary>
        /// Readable names of each action.
        /// </summary>
        private static readonly string[] ActionNames =
            {
                "Reset Camera",
                "Move Character - Up",
                "Move Character - Down",
                "Move Character - Left",
                "Move Character - Right",
                "Zoom In",
                "Zoom Out",
                "Rotate Left",
                "Rotate Right"
            };

        /// <summary>
        /// Returns the readable name of the given action.
        /// </summary>
        public static string GetActionName(Action action)
        {
            int index = (int)action;

            if ((index < 0) || (index > ActionNames.Length))
            {
                throw new ArgumentException("action");
            }

            return ActionNames[index];
        }

        #endregion

        #region Support Types

        /// <summary>
        /// A combination of gamepad and keyboard keys mapped to a particular action.
        /// </summary>
        public class ActionMap
        {
            /// <summary>
            /// List of GamePad controls to be mapped to a given action.
            /// </summary>
            //public List<GamePadButtons> gamePadButtons = new List<GamePadButtons>();

            /// <summary>
            /// List of Keyboard controls to be mapped to a given action.
            /// </summary>
            public List<Keys> KeyboardKeys = new List<Keys>();
        }

        #endregion

        #region Constants

        /// <summary>
        /// The value of an analog control that reads as a "pressed button".
        /// </summary>
        private const float AnalogLimit = 0.5f;

        #endregion

        #region Keyboard Data

        /// <summary>
        /// The state of the keyboard as of the last update.
        /// </summary>
        private static KeyboardState _currentKeyboardState;

        /// <summary>
        /// The state of the keyboard as of the last update.
        /// </summary>
        public static KeyboardState CurrentKeyboardState => _currentKeyboardState;

        /// <summary>
        /// The state of the keyboard as of the previous update.
        /// </summary>
        private static KeyboardState _previousKeyboardState;

        /// <summary>
        /// Check if a key is pressed.
        /// </summary>
        public static bool IsKeyPressed(Keys key) => _currentKeyboardState.IsKeyDown(key);

        /// <summary>
        /// Check if a key was just pressed in the most recent update.
        /// </summary>
        public static bool IsKeyTriggered(Keys key)
        {
            return _currentKeyboardState.IsKeyDown(key) && !_previousKeyboardState.IsKeyDown(key);
        }

        #endregion

        #region Action Mapping

        /// <summary>
        /// The action mappings for the game.
        /// </summary>
        private static ActionMap[] _actionMaps;

        public static ActionMap[] ActionMaps => _actionMaps;

        /// <summary>
        /// Reset the action maps to their default values.
        /// </summary>
        private static void ResetActionMaps()
        {
            _actionMaps = new ActionMap[(int)Action.TotalActionCount];

            _actionMaps[(int)Action.ResetCamera] = new ActionMap();
            _actionMaps[(int)Action.ResetCamera].KeyboardKeys.Add(Keys.Home);

            _actionMaps[(int)Action.MoveCharacterUp] = new ActionMap();
            _actionMaps[(int)Action.MoveCharacterUp].KeyboardKeys.Add(Keys.Up);

            _actionMaps[(int)Action.MoveCharacterDown] = new ActionMap();
            _actionMaps[(int)Action.MoveCharacterDown].KeyboardKeys.Add(Keys.Down);

            _actionMaps[(int)Action.MoveCharacterLeft] = new ActionMap();
            _actionMaps[(int)Action.MoveCharacterLeft].KeyboardKeys.Add(Keys.Left);

            _actionMaps[(int)Action.MoveCharacterRight] = new ActionMap();
            _actionMaps[(int)Action.MoveCharacterRight].KeyboardKeys.Add(Keys.Right);

            _actionMaps[(int)Action.ZoomIn] = new ActionMap();
            _actionMaps[(int)Action.ZoomIn].KeyboardKeys.Add(Keys.PageUp);

            _actionMaps[(int)Action.ZoomOut] = new ActionMap();
            _actionMaps[(int)Action.ZoomOut].KeyboardKeys.Add(Keys.PageDown);

            _actionMaps[(int)Action.RotateLeft] = new ActionMap();
            _actionMaps[(int)Action.RotateLeft].KeyboardKeys.Add(Keys.Insert);

            _actionMaps[(int)Action.RotateRight] = new ActionMap();
            _actionMaps[(int)Action.RotateRight].KeyboardKeys.Add(Keys.Delete);
        }

        /// <summary>
        /// Check if an action has been pressed.
        /// </summary>
        public static bool IsActionPressed(Action action)
        {
            return IsActionMapPressed(_actionMaps[(int)action]);
        }


        /// <summary>
        /// Check if an action was just performed in the most recent update.
        /// </summary>
        public static bool IsActionTriggered(Action action)
        {
            return IsActionMapTriggered(_actionMaps[(int)action]);
        }

        /// <summary>
        /// Check if an action map has been pressed.
        /// </summary>
        private static bool IsActionMapPressed(ActionMap actionMap)
        {
            return actionMap.KeyboardKeys.Any(IsKeyPressed);
        }

        /// <summary>
        /// Check if an action map has been triggered this frame.
        /// </summary>
        private static bool IsActionMapTriggered(ActionMap actionMap)
        {
            return actionMap.KeyboardKeys.Any(IsKeyTriggered);
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes the default control keys for all actions.
        /// </summary>
        public static void Initialize()
        {
            ResetActionMaps();
        }

        #endregion

        #region Updating

        /// <summary>
        /// Updates the keyboard and gamepad control states.
        /// </summary>
        public static void Update()
        {
            // update the keyboard state
            _previousKeyboardState = _currentKeyboardState;
            _currentKeyboardState = Keyboard.GetState();
        }

        #endregion

    }
}