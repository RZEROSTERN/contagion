using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Contagion
{
    public class GameHUD
    {
        SpriteFont font;

        public void Load(ContentManager content)
        {
            font = content.Load<SpriteFont>("Fonts\\gamer");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Resolution.getTransformationMatrix());
            spriteBatch.DrawString(font, "Score: " + Hero.score.ToString(), new Vector2(spriteBatch.GraphicsDevice.Viewport.Width - 90, 0), Color.White);
            spriteBatch.DrawString(font, "Lives: " + Hero.lives.ToString(), new Vector2(10, 0), Color.White);
            spriteBatch.End();
        }
    }
}
