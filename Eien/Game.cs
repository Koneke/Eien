using Eien.Input;

namespace Eien.Framework
{
	class Game : App
	{
		protected override void Update()
		{
			if(Controllers[0].Pressed(Button.Cross))
			{
				Resize(640, 480);
			}
		}

		protected override void Draw()
		{
		}
	}
}