using Eien.Input;
using Eien.Framework;
using Eien.Content;
using SFML.Audio;
using System;
using SFML.System;

namespace Eien.Game
{
	class Game : App
	{
		Character character;
		Sound s;
		Sound b;
		Sound c;

		protected override void Initialise()
		{
			Window.SetFramerateLimit(60);

			character = new Character()
				.SetAtlas(AtlasLoader.AtlasFromFile("data/atlas/test.json"))
				.StartAnimation("foo");

			SoundBuffer sa = new SoundBuffer("data/testdata/sound/impact/impact1.wav");
			s = new Sound(sa);

			SoundBuffer sb = new SoundBuffer("data/testdata/sound/impact/impact2.wav");
			b = new Sound(sb);

			SoundBuffer sc = new SoundBuffer("data/testdata/sound/impact/impact3.wav");
			c = new Sound(sc);
		}

		void Play(Sound x)
		{
			x.Pitch = 2 + (float)Random.Variance(0.5);
			x.Attenuation = 0;
			x.Position = new Vector3f(
				(float)Random.Variance(2),
				0,
				1
			);
			x.Play();
		}

		protected override void Update()
		{
			if(Controllers[0].Pressed(Button.Cross))
			{
				Stop();
			}

			if(Controllers[0].Pressed(Button.Square))
			{
				Play(s);
			}

			if(Controllers[0].Pressed(Button.Circle))
			{
				Play(b);
				character.StartAnimation("foo");
			}

			if(Controllers[0].Pressed(Button.Triangle))
			{
				Play(c);
				character.StartAnimation("bar");
			}
		}

		protected override void Draw()
		{
			character.Draw(Window);
		}
	}
}