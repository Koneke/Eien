using SFML.Graphics;
using SFML.System;

namespace Eien.Rendering
{
	class DrawCall
	{
		Sprite internalSprite;

		private Vector2f position;
		private Color color;
		private IntRect source;

		public DrawCall(Sprite sprite)
		{
			internalSprite = new Sprite(sprite);

			position = new Vector2f(0, 0);
			color = SFML.Graphics.Color.White;
		}

		public DrawCall SetPosition(Vector2f position)
		{
			this.position = position;
			return this;
		}

		public DrawCall SetColor(Color color)
		{
			this.color = color;
			return this;
		}

		public DrawCall SetSource(IntRect source)
		{
			this.source = source;
			return this;
		}

		private bool SourceIsEmpty()
		{
			return source.Width + source.Height == 0;
		}

		public void Draw(RenderWindow window)
		{
			internalSprite.Position = position;
			internalSprite.Color = color;

			if(!SourceIsEmpty())
			{
				internalSprite.TextureRect = source;
			}

			window.Draw(internalSprite);
		}
	}
}