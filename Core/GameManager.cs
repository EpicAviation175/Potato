using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Potato
{
    public enum Scenes
    {
        MENU,
        SETTINGS,
        GAME
    }

    public class GameManager
    {
        private Scenes activeScene;
        private SpriteBatch _spriteBatch;
        private Player player;
        private Texture2D spikeTexture;

        List<Spike> spikes = new List<Spike>();
        List<Spike> killSpikes = new List<Spike>();
        static Random rand = new Random();
        float timeWindow = rand.Next(1,3);
        float spawnTimer = 0f;

        public GameManager()
        {
            activeScene = Scenes.GAME;
        }

        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _spriteBatch = new SpriteBatch(graphicsDevice);

            var playerTexture = content.Load<Texture2D>("potato");
            player = new Player(playerTexture, new Vector2(65, 330));

            spikeTexture = content.Load<Texture2D>("spike");

        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            spawnTimer += deltaTime;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                return;
            }

            //update scenes
            switch (activeScene)
            {
                case Scenes.MENU:
                    // _menu.Update();
                    break;
                case Scenes.SETTINGS:
                    // settings update logic
                    break;
                case Scenes.GAME:
                    player?.Update();

                    foreach (Spike s in spikes)
                    {
                        s.Update();

                        if (player.Rect.Intersects(s.Rect))
                        {
                            ResetGame();
                            SetScene(Scenes.MENU);
                        }
                    }

                    if (spawnTimer >= timeWindow)
                    {
                        SpawnSpike();
                        spawnTimer = 0f;
                        timeWindow = rand.Next(1, 3);
                    }
                    KillSpike();
                    break;
            }
        }

        public void Draw(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.Beige);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            switch (activeScene)
            {
                case Scenes.GAME:
                    if (player != null)
                    {
                        foreach (Spike spike in spikes)
                        {
                            _spriteBatch.Draw(spike.texture, spike.Rect, Color.Beige);
                        }

                        _spriteBatch.Draw(player.texture, player.Rect, Color.White);
                    }
                    break;
                case Scenes.MENU:
                    // menu drawing
                    break;
                case Scenes.SETTINGS:
                    // settings drawing (null atm)
                    break;
            }
            _spriteBatch.End();
        }

        public void SetScene(Scenes scene)
        {
            activeScene = scene;
        }
        
        public Scenes ActiveScene => activeScene;

        public void SpawnSpike()
        {
            Spike newspike = new Spike(spikeTexture, new Vector2(800, 330));
            spikes.Add(newspike);
        }

        public void KillSpike()
        {
            foreach (Spike spike in spikes)
            {
                if (spike.position.X <= -100) 
                {
                    killSpikes.Add(spike);
                }
            }
            if (killSpikes != null)
            {
                foreach (Spike spike in killSpikes)
                {
                    spikes.Remove(spike);
                }
                killSpikes.Clear();
            }
        }

        public void ResetGame()
        {
            killSpikes.Clear();
            spikes.Clear();
            spawnTimer = 0;
            timeWindow = rand.Next(1,3);
        }
    }
}