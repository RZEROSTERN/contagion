using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Contagion
{
    public class Animation
    {
        public string name;
        public List<int> animationOrder = new List<int>();
        public int speed;

        public Animation()
        {

        }

        public Animation(string inputName, int inputSpeed, List<int> inputAnimationOrder)
        {
            name = inputName;
            speed = inputSpeed;
            animationOrder = inputAnimationOrder;
        }
    }

    public class AnimationSet
    {
        public int width;
        public int height;
        public int gridX;
        public int gridY;
        public List<Animation> animationList = new List<Animation>();

        public AnimationSet()
        {

        }

        public AnimationSet(int inputWidth, int inputHeight, int inputGridX, int inputGridY)
        {
            width = inputWidth;
            height = inputHeight;
            gridX = inputGridX;
            gridY = inputGridY;
        }
    }

    public class AnimationData
    {
        public AnimationSet animation { get; set; }
        public string texturePath { get; set; }
    }
}
