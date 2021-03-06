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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SoundEffect song;
        SoundEffectInstance songInstance;

        public List<GameObject> objects = new List<GameObject>();
        public Map map = new Map();

        GameHUD gameHUD = new GameHUD();
        Editor editor;

        enum GameState
        {
            MainMenu,
            Playing,
            Paused
        }

        GameState currentGameState = GameState.MainMenu;

        public Game1() //This is the constructor, this function is called whenever the game class is created.
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Resolution.Init(ref graphics);
            Resolution.SetVirtualResolution(1280, 704);
            Resolution.SetResolution(1280, 720, false);
        }

        /// <summary>
        /// This function is automatically called when the game launches to initialize any non-graphic variables.
        /// </summary>
        protected override void Initialize()
        {
#if DEBUG
            editor = new Editor(this);
            editor.Show();
#endif

            base.Initialize();
            Camera.Initialize();
            Global.Initialize(this);
        }

        /// <summary>
        /// Automatically called when your game launches to load any game assets (graphics, audio etc.)
        /// </summary>
        protected override void LoadContent()
        {
            if (currentGameState == GameState.Playing)
            {
#if DEBUG
                editor.LoadTextures(Content);
#endif
            }
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            if(currentGameState == GameState.Playing)
            {
                song = Content.Load<SoundEffect>("Audio\\lv1loop");

                if (songInstance == null)
                    songInstance = song.CreateInstance();

                map.Load(Content);
                gameHUD.Load(Content);
                LoadLevel("test.cslf");
            }
        }

        /// <summary>
        /// Called each frame to update the game. Games usually runs 60 frames per second.
        /// Each frame the Update function will run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            if (currentGameState == GameState.Playing)
            {
#if DEBUG
                editor.Update(objects, map);
#endif
            }

            Input.Update();

            UpdateObjects();
            map.Update(objects);
            UpdateCamera();

            if (Input.KeyPressed(Keys.Escape) == true)
                Exit();
            else if(Input.KeyPressed(Keys.Enter) == true)
            {
                currentGameState = GameState.Playing;
                LoadContent();
                UpdateMusic();
            }
            

            //Update the things FNA handles for us underneath the hood:
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game is ready to draw to the screen, it's also called each frame.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            //This will clear what's on the screen each frame, if we don't clear the screen will look like a mess:
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Resolution.BeginDraw();
            
            if(currentGameState == GameState.Playing)
            {
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Camera.GetTransformMatrix());
#if DEBUG
                editor.Draw(spriteBatch);
#endif
                DrawObjects();
                map.DrawWalls(spriteBatch);
                spriteBatch.End();

                gameHUD.Draw(spriteBatch);
                //Draw the things FNA handles for us underneath the hood:
                base.Draw(gameTime);
            }
        }

        public void LoadLevel(string filename)
        {
            Global.levelName = filename;

            LevelData levelData = XmlHelper.Load("Content\\Levels\\" + filename);

            map.walls = levelData.walls;
            map.decor = levelData.decor;
            objects = levelData.objects;

            map.LoadMap(Content);
            LoadObjects();
        }

        private void UpdateMusic()
        {
            if (songInstance.State != SoundState.Playing)
            {
                songInstance.IsLooped = true;
                songInstance.Play();
            }
        }

        public void LoadObjects()
        {
            for(int i = 0; i < objects.Count; i++)
            {
                objects[i].Initialize();
                objects[i].Load(Content);
            }
        }

        public void UpdateObjects()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Update(objects, map);
            }
        }

        public void DrawObjects()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Draw(spriteBatch);
            }

            for (int i = 0; i < map.decor.Count; i++)
            {
                map.decor[i].Draw(spriteBatch);
            }
        }

        private void UpdateCamera()
        {
            if (objects.Count == 0)
                return;

            Camera.Update(objects[0].position);
        }
    }
}
