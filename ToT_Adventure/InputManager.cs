using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ToT_Adventure
{
    public class InputManager
    {
        KeyboardState prevKeyState, keyState;
        GamePadState prevButtonState, buttonState;
        MouseState prevMouseState, mouseState;
        readonly PlayerIndex playerIndex = PlayerIndex.One;

        public KeyboardState PrevKeyState
        {
            get { return prevKeyState; }
            set { prevKeyState = value; }
        }
        public KeyboardState KeyState
        {
            get { return KeyState; }
            set { KeyState = value; }
        }

        public InputManager(PlayerIndex _playerIndex = PlayerIndex.One)
        {
            playerIndex = _playerIndex;
        }

        public void Update()
        {
            prevKeyState = keyState;
            keyState = Keyboard.GetState();
            prevButtonState = buttonState;
            buttonState = GamePad.GetState(playerIndex);
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();
        }

        public Vector2 MousePosition()
        {
            return new Vector2(mouseState.Position.X, mouseState.Position.Y);
        }

        public Rectangle MouseRect()
        {
            return new Rectangle((int)MousePosition().X - 1, (int)MousePosition().Y - 1, 2, 2);
        }

        public bool MouseDown()
        {
            return (mouseState.LeftButton == ButtonState.Pressed);
        }

        public bool MouseClick()
        {
            return (prevMouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released);
        }

        public bool KeyPressed(Keys key)
        {
            if (keyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key))
                return true;
            return false;
        }

        public bool KeyPressed(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (keyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key))
                    return true;
            }
            return false;
        }

        public bool ButtonPressed(Buttons button)
        {
            if (buttonState.IsButtonDown(button) && prevButtonState.IsButtonUp(button))
                return true;
            return false;
        }

        public bool ButtonPressed(params Buttons[] buttons)
        {
            foreach (Buttons button in buttons)
            {
                if (buttonState.IsButtonDown(button) && prevButtonState.IsButtonUp(button))
                    return true;
            }
            return false;
        }


        public bool KeyReleased(Keys key)
        {
            if (keyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key))
                return true;
            return false;
        }

        public bool KeyReleased(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (keyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key))
                    return true;
            }
            return false;
        }

        public bool KeyDown(Keys key)
        {
            if (keyState.IsKeyDown(key))
                return true;
            return false;

        }
        public bool ButtonDown(Buttons button)
        {
            if (buttonState.IsButtonDown(button))
                return true;
            return false;
        }

        public bool KeyDown(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (keyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }

        public bool ButtonDown(params Buttons[] buttons)
        {
            foreach (Buttons button in buttons)
            {
                if (buttonState.IsButtonDown(button))
                    return true;
            }
            return false;
        }
    }
}
