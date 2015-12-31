using Eien.Input;
using Eien.Framework;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace Eien.Game
{
	class Slicer
	{
		static Color magicTopLeft = new Color(255, 0, 0);
		static Color magicBottomRight = new Color(0, 0, 255);

		Image i;

		private Vector2u SearchBottomRight(Vector2u topLeft)
		{
			// Start search for next corner one pixel to the right of
			// our start.
			for(uint x = topLeft.X + 1; x < i.Size.X; x++)
			{
				// If we find the topleft of the next frame,
				// start looking downwards.
				if(i.GetPixel(x, topLeft.Y) == magicTopLeft)
				{
					for(uint y = topLeft.Y + 1; y < i.Size.Y; y++)
					{
						if(i.GetPixel(x, y) == magicBottomRight)
						{
							return new Vector2u(x, y);
						}
					}
				}
			}

			throw new System.FormatException("Malformatted spritesheet.");
		}

		private void Slice()
		{
			Texture t = new Texture("data/tex/spritesheet.png");
			Image i = t.CopyToImage();

			List<IntRect> foundFrames = new List<IntRect>();

			Vector2u current = new Vector2u(0, 0);

			while(current.Y < i.Size.Y)
			{
				while(current.X < i.Size.X)
				{
					if(i.GetPixel(current.X, current.Y) != magicTopLeft)
					{
						throw new System.FormatException("Something is weird.");
					}

					Vector2u bottomRight = SearchBottomRight(current);

					IntRect ir = new IntRect(
						(int)current.X,
						(int)current.Y,
						(int)(bottomRight.X - current.X),
						(int)(bottomRight.Y - current.Y));

					foundFrames.Add(ir);
				}

				current.X = 0;
				//current.Y = 
			}

			if(i.GetPixel(0, 0) != magicTopLeft)
			{
				throw new System.FormatException("Malformatted spritesheet.");
			}
		}
	}

	class Game : App
	{
		Character character;

		protected override void Initialise()
		{
			Window.SetFramerateLimit(60);
			character = new Character().SetTexture("data/tex/spritesheet.png");
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