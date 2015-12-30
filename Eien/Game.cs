using Eien.Input;

namespace Eien.Framework
{
	class Game : App
	{
		protected override void Update()
		{
			if(Controllers[0].Pressed(Button.Cross))
			{
				Stop();
			}
		}

		protected override void Draw()
		{
		}
	}
}