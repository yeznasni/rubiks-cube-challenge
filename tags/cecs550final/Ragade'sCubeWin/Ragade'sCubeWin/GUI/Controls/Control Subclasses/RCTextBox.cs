using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace RagadesCubeWin.GUI
{
    internal class RCTextBox : RCControl
    {
        [placeHolder]
        [needsXML]
        internal RCTextBox(
            float width,
            float height,
            int screenWidth,
            int screenHeight)
            : base(
            width,
            height,
            screenWidth,
            screenHeight
            )
        {
        }


    }
}
