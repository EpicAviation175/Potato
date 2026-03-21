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
        private Spike spike;
        private Texture2D spikeTexture;

        List<Spike> spikes = new List<Spike>();
        List<Spike> killSpikes = new List<Spike>();
        static Random rand = new Random();
        float timeWindow = rand.Next(3,5);
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
                    // TODO: menu update logic
                    break;
                case Scenes.SETTINGS:
                    // TODO: settings update logic
                    break;
                case Scenes.GAME:
                    player?.Update();

                    foreach (Spike s in spikes)
                    {
                        s.Update();
                    }

                    if (spawnTimer >= timeWindow)
                    {
                        spawnSpike();
                        spawnTimer = 0f;
                        timeWindow = rand.Next(3, 5);
                    }
                    killSpike();
                    break;
            }
        }

        public void Draw(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            switch (activeScene)
            {
                case Scenes.MENU:
                    break;
                case Scenes.SETTINGS:
                    graphicsDevice.Clear(Color.Beige);
                    break;
                case Scenes.GAME:
                    graphicsDevice.Clear(Color.Beige);
                    break;
            }

            _spriteBatch.Begin();

            if (activeScene == Scenes.GAME && player != null)
            {
                _spriteBatch.Draw(player.texture, player.Rect, Color.White);

                foreach (Spike spike in spikes)
                {
                    _spriteBatch.Draw(spike.texture, spike.Rect, Color.Beige);
                }
            }
            _spriteBatch.End();

        }

        public void SetScene(Scenes scene)
        {
            activeScene = scene;
        }

        public Scenes ActiveScene => activeScene;

        public void spawnSpike()
        {
            Spike newspike = new Spike(spikeTexture, new Vector2(500, 330));
            spikes.Add(newspike);
            Debug.WriteLine("Spawned spike");
        }

        public void killSpike()
        {
            foreach (Spike spike in spikes)
            {
                if (spike.position.X <= 0) 
                {
                    killSpikes.Add(spike);
                }
            }
            foreach (Spike spike in killSpikes)
            {
                spikes.Remove(spike);
                killSpikes.Clear();
            }
        }
    }
}