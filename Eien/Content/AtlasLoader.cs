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
		[JsonProperty("FilePath")]
		string FilePath;

		[JsonProperty("HitboxPath")]
		string HitboxPath;

		[JsonProperty("Animations")]
		List<JsonAnimation> Animations;

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
			public bool Looping;
		}

		public static Atlas AtlasFromLoader(AtlasLoader loader)
		{
			Atlas atlas = new Atlas(loader.FilePath);

			AtlasSlicer slicer = new AtlasSlicer(loader.FilePath);
			List<AtlasSlicer.Slice> slices = slicer.SliceImage();

			foreach(JsonAnimation jsonAnimation in loader.Animations)
			{
				Animation animation = new Animation(jsonAnimation.Name);

				foreach(JsonFrame loadedFrame in jsonAnimation.Frames)
				{
					AtlasSlicer.Slice slice = slices.FirstOrDefault(
						s => s.Coordinate == new Vector2u(loadedFrame.X, loadedFrame.Y));

					if(slice == null)
					{
						throw new System.FormatException();
					}

					Frame frame = new Frame(slice.Rectangle, loadedFrame.Duration);

					new HitboxParser(loader.HitboxPath)
						.ProcessFrame(frame);

					animation.AddFrame(frame);
				}

				if(jsonAnimation.Looping)
				{
					animation.Looping = true;
				}

				atlas.AddAnimation(animation);
			}

			return atlas;
		}

		public static Atlas AtlasFromFile(string filepath)
		{
			AtlasLoader loader = JsonConvert.DeserializeObject<AtlasLoader>(
				File.ReadAllText(filepath));

			return AtlasFromLoader(loader);
		}
	}
}