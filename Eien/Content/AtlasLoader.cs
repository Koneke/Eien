using Eien.Graphics;
using Newtonsoft.Json;
using SFML.System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Eien.Content
{
	class AtlasLoader
	{
		private class JsonFrame
		{
			public uint X;
			public uint Y;
			public int Duration;
		}

		private class JsonAnimation
		{
			public string Name;
			public List<JsonFrame> Frames;
		}

		public static Atlas FromFile(string filepath)
		{
			AtlasLoader loader = JsonConvert.DeserializeObject<AtlasLoader>(
				File.ReadAllText(filepath));

			Atlas atlas = new Atlas(loader.FilePath);

			AtlasSlicer slicer = new AtlasSlicer(loader.FilePath);
			List<AtlasSlice> slices = slicer.Slice();

			foreach(JsonAnimation jsonAnimation in loader.Animations)
			{
				Animation animation = new Animation(jsonAnimation.Name);

				foreach(JsonFrame frame in jsonAnimation.Frames)
				{
					AtlasSlice slice = slices.FirstOrDefault(
						s => s.Coordinate == new Vector2u(frame.X, frame.Y)
					);

					if(slice == null)
					{
						throw new System.FormatException();
					}

					animation.AddFrame(slice.Rectangle, frame.Duration);
				}

				atlas.AddAnimation(animation);
			}

			return atlas;
		}

		[JsonProperty("FilePath")]
		string FilePath;

		[JsonProperty("Animations")]
		List<JsonAnimation> Animations;
	}
}
