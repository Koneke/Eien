using Eien.Game;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace Eien.Graphics
{
	public class HitboxParser
	{
		Image image;

		// Hit/hurtbox colours.
		static Color originColor = new Color(255, 0, 255);
		static Color boxStartColor = new Color(0, 0, 255);
		static Color hitColor = new Color(255, 0, 0);
		static Color hurtColor = new Color(0, 255, 0);

		public HitboxParser(string filepath)
		{
			image = new Image(filepath);
		}

		private Hitbox ParseBox(Vector2u boxStart, Vector2u relativeZero)
		{
			Vector2u current = boxStart;

			current.X += 1;
			Color pixelColor = image.GetPixel(current.X, current.Y);
			Color boxColor = pixelColor;

			while(pixelColor == boxColor)
			{
				current.X += 1;
				pixelColor = image.GetPixel(current.X, current.Y);
			}

			// Go back to where we last were ok.
			current.X -= 1;
			pixelColor = image.GetPixel(current.X, current.Y);

			while(pixelColor == boxColor)
			{
				current.Y += 1;
				pixelColor = image.GetPixel(current.X, current.Y);
			}

			// We want to end up at the +1, +1 relative to the
			// bottom right pixel of the hitbox.
			current.X += 1;

			IntRect rectangle = new IntRect(
				(int)(boxStart.X - relativeZero.X),
				(int)(boxStart.Y - relativeZero.Y),
				(int)(current.X - boxStart.X),
				(int)(current.Y - boxStart.Y));

			Hitbox.Type type = pixelColor == hitColor
				? Hitbox.Type.Hitbox
				: Hitbox.Type.Hurtbox;

			return new Hitbox(rectangle, type);
		}

		public Frame ProcessFrame(Frame frame)
		{
			// Look up corresponding area on our hitboximage to the slice given,
			// parse hitboxes and origin on it, create the frame with this information
			// and the one sent in with the slice.

			List<Hitbox> hitboxes = new List<Hitbox>();

			Vector2u relativeZero = new Vector2u((uint)frame.Source.Left, (uint)frame.Source.Top);

			for(uint x = relativeZero.X, w = 0; w < frame.Source.Width; x++, w++)
			{
				for(uint y = relativeZero.Y, h = 0; h < frame.Source.Height; y++, h++)
				{
					Color pixelColor = image.GetPixel(x, y);

					if(pixelColor == originColor)
					{
						frame.Origin = new Vector2u(x, y) - relativeZero;
					}
					else if(pixelColor == boxStartColor)
					{
						hitboxes.Add(ParseBox(new Vector2u(x, y), relativeZero));
					}
				}
			}

			// Make our hitboxes relative to our origin.
			foreach(Hitbox hitbox in hitboxes)
			{
				hitbox.Rectangle.Left -= (int)frame.Origin.X;
				hitbox.Rectangle.Top -= (int)frame.Origin.Y;
			}

			frame.Hitboxes = hitboxes;
			return frame;
		}
	}
}