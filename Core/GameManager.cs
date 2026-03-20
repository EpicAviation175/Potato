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

        public GameManager()
        {
            activeScene = Scenes.GAME;
        }

        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _spriteBatch = new SpriteBatch(graphicsDevice);
            var playerTexture = content.Load<Texture2D>("potato");
            player = new Player(playerTexture, new Vector2(65, 330));
        }

        public void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                return;
            }

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
                    graphicsDevice.Clear(Color.CornflowerBlue);
                    break;
            }

            _spriteBatch.Begin();

            if (activeScene == Scenes.GAME && player != null)
            {
                _spriteBatch.Draw(player.texture, player.Rect, Color.White);
            }

            _spriteBatch.End();
        }

        public void SetScene(Scenes scene)
        {
            activeScene = scene;
        }

        public Scenes ActiveScene => activeScene;
    }
}