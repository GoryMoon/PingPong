using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PingPong
{
    public class MathHelper
    {

        static public float remapY(float y)
        {
            return remap(y, Resolution.Viewport.Y, Resolution.Viewport.Height, 0, Resolution.WindowSize.Y);
        }

        static public float remapX(float x)
        {
            return remap(x, Resolution.Viewport.X, Resolution.Viewport.X + Resolution.Viewport.Width, 0, Resolution.WindowSize.X);
        }

        static public float remap(float x, float f1, float f2, float t1, float t2)
        {
            return ((x - f1) / (f2 - f1)) * (t2 - t1) + t1;
        }

    }
}
