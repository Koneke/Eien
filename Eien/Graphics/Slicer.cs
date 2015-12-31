using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace Eien.Graphics
{
	class AtlasSlice
	{
		public IntRect Rectangle;
		public Vector2u Coordinate;

		public AtlasSlice(IntRect rectangle, Vector2u coordinate)
		{
			Rectangle = rectangle;
			Coordinate = coordinate;
		}
	}

	class AtlasSlicer
	{
		Image image;
		static Color magicColor = new Color(255, 0, 0);

		public AtlasSlicer(string filepath)
		{
			image = new Image(filepath);
		}

		private Vector2u SearchBottomRight(Vector2u start)
		{
			for(uint x = start.X + 1; x < image.Size.X; x++)
			{
				// If we find the topleft of the next frame,
				// or the end of the sheet,
				// start looking downwards.
				if(image.GetPixel(x, start.Y) == magicColor || x == image.Size.X - 1)
				{
					for(uint y = start.Y + 1; y < image.Size.Y; y++)
					{
						if(image.GetPixel(x, y) == magicColor)
						{
							return new Vector2u(x, y);
						}
					}
				}
			}

			throw new System.FormatException("Malformatted spritesheet.");
		}

		public List<AtlasSlice> Slice()
		{
			List<AtlasSlice> slices = new List<AtlasSlice>();
			Vector2u current = new Vector2u(0, 0);
			Vector2u coordinates = new Vector2u(0, 0);

			while(current.Y < image.Size.Y)
			{
				while(current.X < image.Size.X - 1)
				{
					Vector2u bottomRight = SearchBottomRight(current);

					IntRect rectangle = new IntRect(
						(int)current.X + 1,
						(int)current.Y + 1,
						(int)(bottomRight.X - current.X) - 1,
						(int)(bottomRight.Y - current.Y) - 1);

					slices.Add(new AtlasSlice(rectangle, coordinates));

					current.X = bottomRight.X;
					coordinates.X++;
				}

				current.X = 0;
				current.Y += 1;
				coordinates.X = 0;
				coordinates.Y++;

				while(current.Y < image.Size.Y)
				{
					if(image.GetPixel(current.X, current.Y) != magicColor)
					{
						current.Y++;
					}
					else
					{
						break;
					}
				}
			}

			return slices;
		}
	}
}
