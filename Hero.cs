﻿using System;
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
    public class Hero : FireCharacter
    {
        public static int score;
        public static int lives;

        public Hero()
        {

        }

        public Hero(Vector2 inputPosition)
        {
            position = inputPosition;
        }

        public override void Initialize()
        {
            score = 0;
            lives = 3;
            base.Initialize();
        }

        public override void Load(ContentManager content)
        {
            image = TextureLoader.Load("spritesheet", content);

            LoadAnimation("ShyBoy.anm", content);
            ChangeAnimation(Animations.IdleLeft);

            base.Load(content);

            boundingBoxOffset.X = 0; boundingBoxOffset.Y = 0; boundingBoxWidth = animationSet.width; boundingBoxHeight = animationSet.height;
        }

        public override void Update(List<GameObject> objects, Map map)
        {
            CheckInput(objects, map);
            base.Update(objects, map);
        }

        private void CheckInput(List<GameObject> objects, Map map)
        {
            if(Character.applyGravity == false)
            {
                if (Input.IsKeyDown(Keys.D) == true)
                {
                    MoveRight();
                }
                else if (Input.IsKeyDown(Keys.A) == true)
                {
                    MoveLeft();
                }

                if (Input.IsKeyDown(Keys.S) == true)
                {
                    MoveDown();
                }
                else if (Input.IsKeyDown(Keys.W) == true)
                {
                    MoveUp();
                }
            } else {
                if (Input.IsKeyDown(Keys.D) == true)
                {
                    MoveRight();
                }
                else if (Input.IsKeyDown(Keys.A) == true)
                {
                    MoveLeft();
                }

                if (Input.IsKeyDown(Keys.Space) == true)
                {
                    Jump(map);
                }
            }

            if (Input.KeyPressed(Keys.J))
                Fire();
            
        }

        protected override void UpdateAnimations()
        {
            if (currentAnimation == null)
                return;

            base.UpdateAnimations();

            if(velocity != Vector2.Zero || jumping == true)
            {
                if (direction.X < 0 && AnimationIsNot(Animations.RunLeft))
                    ChangeAnimation(Animations.RunLeft);
                else if (direction.X > 0 && AnimationIsNot(Animations.RunRight))
                    ChangeAnimation(Animations.RunRight);
            }
            else if(velocity == Vector2.Zero || jumping == false)
            {
                if (direction.X < 0 && AnimationIsNot(Animations.IdleLeft))
                    ChangeAnimation(Animations.IdleLeft);
                else if (direction.X > 0 && AnimationIsNot(Animations.IdleRight))
                    ChangeAnimation(Animations.IdleRight);
            }
        }
    }
}
