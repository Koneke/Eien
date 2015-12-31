using Eien.Input;
using Eien.Framework;
using Eien.Graphics;
using Eien.Content;

namespace Eien.Game
{
	class Game : App
	{
		Character character;

		protected override void Initialise()
		{
			Window.SetFramerateLimit(60);

			character = new Character()
				.SetAtlas(AtlasLoader.FromFile("data/atlases/test.json"))
				.StartAnimation("foo");
		}

		protected override void Update()
		{
			if(Controllers[0].Pressed(Button.Cross))
			{
				Stop();
			}

			if(Controllers[0].Pressed(Button.Circle))
			{
				character.StartAnimation("foo");
			}

			if(Controllers[0].Pressed(Button.Square))
			{
				character.StartAnimation("bar");
			}
		}

		protected override void Draw()
		{
			character.Draw(Window);
		}
	}
}