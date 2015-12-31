using Eien.Input;
using Eien.Framework;
using Eien.Graphics;

namespace Eien.Game
{
	class Game : App
	{
		Character character;

		protected override void Initialise()
		{
			Window.SetFramerateLimit(60);
			character = new Character().SetTexture("data/tex/spritesheet.png");
			AtlasSlicer s = new AtlasSlicer();
			s.Slice();
		}

		protected override void Update()
		{
			if(Controllers[0].Pressed(Button.Cross))
			{
				Stop();
			}

			if(Controllers[0].Pressed(Button.Circle))
			{
				character.StartAnimation("blue");
			}

			if(Controllers[0].Pressed(Button.Square))
			{
				character.StartAnimation("red");
			}
		}

		protected override void Draw()
		{
			character.Draw(Window);
		}
	}
}