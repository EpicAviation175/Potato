using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Potato
{
    public class Menu
    {
        private static Texture2D playButtonTexture;
        private Button playButton;
        private Texture2D exitButtonTexture;
        public Button exitButton;
        private GameManager gameManager;
        private Game1 game;
        public bool exit;

        public Menu(GameManager gameManager, Game1 game)
        {
            this.gameManager = gameManager;
            this.game = game;
        }

        public void LoadContent(ContentManager content, GraphicsDevice graphics)
        {
            playButtonTexture = content.Load<Texture2D>("playbutton");
            playButton = new Button(playButtonTexture, new Vector2(280, 200));

            exitButtonTexture = content.Load<Texture2D>("exitbutton");
            exitButton = new Button(exitButtonTexture, new Vector2(400, 200));
        }

        public void Update(GameTime gameTime)
        {
            if (playButton.isPressed())
            {
                gameManager.SetScene(Scenes.GAME);
            }

            if (exitButton.isPressed())
            {
                game.Exit();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playButtonTexture, playButton.rect, Color.Black);
            spriteBatch.Draw(exitButtonTexture, exitButton.rect, Color.Black);
        }
    }
}