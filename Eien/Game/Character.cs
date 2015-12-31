using Eien.Graphics;
using SFML.Graphics;

namespace Eien.Game
{
	class Character
	{
		private Atlas Atlas;

		public Character()
		{
		}

		public void Draw(RenderWindow window)
		{
			Atlas.Draw(window);
		}

		public Character SetAtlas(Atlas atlas)
		{
			Atlas = atlas;
			return this;
		}

		public Character SetTexture(string filepath)
		{
			Atlas.SetTexture(filepath);
			return this;
		}

		public Character StartAnimation(string key)
		{
			Atlas.StartAnimation(key);
			return this;
		}
	}
}