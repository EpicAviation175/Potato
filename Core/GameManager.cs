using System;
using System.Collections.Generic;
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
        private Menu _menu;
        private bool gameEnd;
        private Game1 game;

        public int screenWidth;
        public int screenHeight;

        List<Spike> spikes = new List<Spike>();
        List<Spike> killSpikes = new List<Spike>();
        static Random rand = new Random();
        float timeWindow = rand.Next(1,3);
        float spawnTimer = 0f;

        public GameManager(Game1 game)
        {
            this.game = game;
            activeScene = Scenes.MENU;
            _menu = new Menu(this, game);
        }

        private float groundY;

        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            screenWidth = graphicsDevice.Viewport.Width;
            screenHeight = graphicsDevice.Viewport.Height;
            groundY = screenHeight - 150;

            _spriteBatch = new SpriteBatch(graphicsDevice);

            _menu.LoadContent(content, graphicsDevice);

            var playerTexture = content.Load<Texture2D>("potato");
            player = new Player(playerTexture, new Vector2(screenWidth * 0.08f, groundY), groundY);

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

            switch (activeScene)
            {
                case Scenes.MENU:
                    _menu.Update(gameTime);
                    break;
                case Scenes.SETTINGS:
                    // settings update logic
                    break;
                case Scenes.GAME:
                    player?.Update();

                    if (gameEnd)
                    {
                        killSpikes.Clear();
                        spikes.Clear();
                        spawnTimer = 0;
                        timeWindow = rand.Next(1,3);
                        player.position.X = screenWidth * 0.08f;
                        player.position.Y = groundY;
                        gameEnd = false;
                    }

                    foreach (Spike s in spikes)
                    {
                        s.Update();

                        if (player.hitbox.Intersects(s.hitbox))
                        {
                            SetScene(Scenes.MENU);
                            gameEnd = true;
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
                    _menu.Draw(_spriteBatch);
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
            float y = groundY + (player.texture.Height - spikeTexture.Height);
            Spike newspike = new Spike(spikeTexture, new Vector2(screenWidth + 40, y));
            spikes.Add(newspike);
        }

        public void KillSpike()
        {
            foreach (Spike spike in spikes)
            {
                if (spike.position.X <= -spike.texture.Width)
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
    }
}