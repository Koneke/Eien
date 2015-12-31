using Eien.Rendering;
using SFML.Graphics;

namespace Eien.Game
{
	class Character
	{
		private SpriteSheet SpriteSheet;

		public Character()
		{
			SpriteSheet = new SpriteSheet("data/tex/spritesheet.png")
				.AddAnimation("red", new Animation()
					.AddFrame(new IntRect(64, 0, 64, 64), 1))
				.AddAnimation("blue", new Animation()
					.AddFrame(new IntRect(0, 0, 64, 64), 3))
				.StartAnimation("red");
		}

		public void Draw(RenderWindow window)
		{
			SpriteSheet.Draw(window);
		}

		public Character SetTexture(string filepath)
		{
			SpriteSheet.SetTexture(filepath);
			return this;
		}

		public Character StartAnimation(string key)
		{
			SpriteSheet.StartAnimation(key);
			return this;
		}
	}
}