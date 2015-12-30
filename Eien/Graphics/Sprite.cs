using SFML.Graphics;
using SFML.System;

namespace OrczNet.Graphics
{
	class Sprite
	{
		SFML.Graphics.Sprite internalSprite;

		public Sprite(string filepath)
		{
			internalSprite = new SFML.Graphics.Sprite(new Texture(filepath));
		}

		public void Draw(RenderWindow window, Vector2f position)
		{
			Draw(window, position, Color.White);
		}

		public void Draw(RenderWindow window, Vector2f position, Color color)
		{
			internalSprite.Position = position;
			internalSprite.Color = color;
			window.Draw(internalSprite);
		}
	}
}