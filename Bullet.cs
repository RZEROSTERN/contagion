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
    public class Bullet : GameObject
    {
        const float speed = 12f;
        Character owner;

        int destroyTimer;
        const int maxTimer = 180;

        public Bullet()
        {
            active = false;
        }

        public override void Load(ContentManager content)
        {
            image = TextureLoader.Load("bullet", content);
            base.Load(content);
        }

        public override void Update(List<GameObject> objects, Map map)
        {
            if (active == false)
                return;

            position += direction * speed;

            CheckCollisions(objects, map);

            destroyTimer--;

            if (destroyTimer <= 0 && active == true)
                Destroy();

            base.Update(objects, map);
        }

        private void CheckCollisions(List<GameObject> objects, Map map)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].active == true && objects[i] != owner && objects[i].CheckCollision(BoundingBox) == true)
                {
                    Destroy();
                    objects[i].BulletResponse();
                    return;
                }
            }

            if (map.CheckCollision(BoundingBox) != Rectangle.Empty)
                Destroy();
        }

        public void Destroy()
        {
            active = false;
        }

        public void Fire(Character inputOwner, Vector2 inputPosition, Vector2 inputDirection)
        {
            owner = inputOwner;
            position = inputPosition;
            direction = inputDirection;
            active = true;
            destroyTimer = maxTimer;
        }
    }
}
