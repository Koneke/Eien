using System;
using System.Collections.Generic;
using System.Linq;
using Eien.Framework;
using Eien.Input;
using SFML.Graphics;

namespace Eien.TestApps
{
	class ButtonTest : App
	{
		protected override void Initialise()
		{
		}

		protected override void Update()
		{
			Dictionary<Button, Color> buttonTests = new Dictionary<Button, Color>() {
				{ Button.Cross, Color.Cyan },
				{ Button.Circle, Color.Green },
				{ Button.Square, Color.Red },
				{ Button.Triangle, Color.Yellow },
				{ Button.LeftTrigger, Color.White },
				{ Button.RightTrigger, Color.Black },
				{ Button.LeftBumper, Color.Magenta },
				{ Button.RightBumper, Color.Blue },
				{ Button.Share, new Color(100, 100, 100) },
				{ Button.Options, new Color(200, 200, 200) }
			};

			foreach(Button button in Enum.GetValues(typeof(Button)).Cast<Button>())
			{
				if(buttonTests.ContainsKey(button))
				{
					if(Controllers[0].IsDown(button))
					{
						ClearColor = buttonTests[button];
					}
				}
			}
		}

		protected override void Draw()
		{
		}
	}
}