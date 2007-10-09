using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RagadesCubeWin.Input
{
    class RealMouseState
    {


        // Keeps state of button
        struct ButtonState
        {
            public bool Tapped, Pressed, WasPressed;
        };

        #region Vars

        ButtonState leftbutton;                 // left button of the mouse
        ButtonState rightbutton;                // right button of the mouse
        Vector2 prevPosition;                   // keeps previous position
        Vector2 curPosition;                    // gets new posisiton
        Vector2 hovered;                        // holds distance traveled

        #endregion

        /// <summary>
        /// Assumes all button states are released
        /// </summary>
        public RealMouseState()
        {
            // just assume everything is unpressed
            leftbutton.Pressed = false;
            leftbutton.Tapped = false;
            leftbutton.WasPressed = false;

            rightbutton.Pressed = false;
            rightbutton.Tapped = false;
            rightbutton.WasPressed = false;
            
        }

        /// <summary>
        /// Tests to see if a particular mouse button is pressed
        /// </summary>
        /// <param name="mbt">Which common mouse button</param>
        /// <returns>True if the button is pressed</returns>
        public bool IsPressed(Input.Types.MouseInput mbt)
        {
            switch (mbt)
            {
                    // left button
                case Input.Types.MouseInput.LeftButton:
                return leftbutton.Pressed;
                    // right button
                case Input.Types.MouseInput.RightButton:
                return rightbutton.Pressed;
                    // unknown button
                default:
                return false;
            };
            
        }

        /// <summary>
        /// Tests to see if a particular mouse button has been clicked
        /// </summary>
        /// <param name="mbt"></param>
        /// <returns></returns>
        public bool IsTapped(Input.Types.MouseInput mbt)
        {
            switch (mbt)
            {
                    // left button
                case Input.Types.MouseInput.LeftButton:
                    return leftbutton.Tapped;
                    // right button
                case Input.Types.MouseInput.RightButton:
                    return rightbutton.Tapped;
                    // unknown button
                default:
                    return false;
            };
        }

        /// <summary>
        /// Returns the hovered distance
        /// </summary>
        /// <returns>Distance traveled</returns>
        public Vector2 GetHover()
        {
            return hovered;
        }

        /// <summary>
        /// Returns the current position
        /// </summary>
        /// <returns>Current Position</returns>
        public Vector2 GetPosition()
        {
            return curPosition;
        }

        /// <summary>
        /// Take the current mousestate to update the realstate
        /// </summary>
        /// <param name="ms">Current mouse state</param>
        public void MouseState(MouseState ms)
        {
            // update the buttons and hover distance
            #region LeftButton
            // left button
            if (ms.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                leftbutton.Pressed = true;

                if (leftbutton.WasPressed)
                {
                    leftbutton.WasPressed = true;
                    leftbutton.Tapped = false;

                }
                else
                {
                    leftbutton.WasPressed = true;
                    leftbutton.Tapped = true;
                }
            }
            else
            {
                leftbutton.Pressed = false;
                leftbutton.Tapped = false;
                leftbutton.WasPressed = false;
            }
            #endregion

            #region RightButton
            // right button
            if (ms.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                rightbutton.Pressed = true;

                if (rightbutton.WasPressed)
                {
                    rightbutton.WasPressed = true;
                    rightbutton.Tapped = false;
                }
                else
                {
                    rightbutton.WasPressed = true;
                    rightbutton.Tapped = true;
                }
            }
            else
            {
                rightbutton.Pressed = false;
                rightbutton.Tapped = false;
                rightbutton.WasPressed = false;
            }
            #endregion

            #region Hovered,Dragged

            prevPosition = curPosition;
            curPosition = new Vector2((float)ms.X, (float)ms.Y); 
            hovered = new Vector2(prevPosition.X - curPosition.X,
                                    prevPosition.Y - curPosition.Y);
               
       
            #endregion
        }
    }
}
