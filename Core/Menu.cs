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

        private Vector2 textMiddlePoint;
        private Vector2 textPosition;

        SpriteFont font;

        public Menu(GameManager gameManager, Game1 game)
        {
            this.gameManager = gameManager;
            this.game = game;
        }

        public void LoadContent(ContentManager content, GraphicsDevice graphics)
        {
            playButtonTexture = content.Load<Texture2D>("playbutton");
            playButton = new Button(playButtonTexture, new Vector2((gameManager.screenWidth/2)-110, gameManager.screenHeight/2));

            exitButtonTexture = content.Load<Texture2D>("exitbutton");
            exitButton = new Button(exitButtonTexture, new Vector2((gameManager.screenWidth/2)+10, gameManager.screenHeight/2));

            font = game.Content.Load<SpriteFont>("pixellated");
            textMiddlePoint = font.MeasureString("Potato") / 2;
            textPosition = new Vector2(((gameManager.screenWidth / 2)+25) - textMiddlePoint.X, ((gameManager.screenHeight / 2)-75) - textMiddlePoint.Y);
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
            if (font != null)
            {
                spriteBatch.DrawString(font, "Potato", textPosition, Color.Black, 0f, textMiddlePoint, 5.0f, SpriteEffects.None, 0.5f);
            }

            if (playButton.mouseOverlap())
            {
                spriteBatch.Draw(playButtonTexture, playButton.rect2, Color.Black);
            }
            else
            {
                spriteBatch.Draw(playButtonTexture, playButton.rect, Color.Gray);
            }

            if (exitButton.mouseOverlap())
            {
                spriteBatch.Draw(exitButtonTexture, exitButton.rect2, Color.Black);
            }
            else
            {
                spriteBatch.Draw(exitButtonTexture, exitButton.rect, Color.Black);
            }
        }
    }
}
