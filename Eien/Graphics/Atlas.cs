﻿using Eien.Game;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using System.Linq;

namespace Eien.Graphics
{
	public class Frame
	{
		public IntRect Source;
		public Vector2u Origin;
		public List<Hitbox> Hitboxes;
		public int Duration;

		public Frame(IntRect source, int duration)
		{
			Source = source;
			Duration = duration;
		}
	}

	public class Animation
	{
		public string Name;
		public List<Frame> Frames;
		public bool Looping;

		public int Duration
		{
			get
			{
				return Frames.Sum(f => f.Duration);
			}
		}

		public Animation(string name)
		{
			Name = name;
			Frames = new List<Frame>();
		}

		public Animation AddFrame(IntRect source, int duration = 1)
		{
			if(duration == 0)
			{
				throw new System.ArgumentException("Duration cannot be zero.");
			}

			AddFrame(new Frame(source, duration));
			return this;
		}

		public Animation AddFrame(Frame frame)
		{
			Frames.Add(frame);
			return this;
		}

		public Animation SetLooping(bool looping)
		{
			Looping = looping;
			return this;
		}
	}

	public class Atlas
	{
		public SFML.Graphics.Sprite SfmlSprite;
		public Dictionary<string, Animation> Animations;
		public string CurrentAnimation;

		private int startFrame;
		private int currentFrame
		{
			get
			{
				int frame = Framework.App.FramesSinceStart - startFrame;

				if(Animations[CurrentAnimation].Looping)
				{
					frame %= Animations[CurrentAnimation].Duration;
				}

				return frame;
			}
		}

		public bool AnimationFinished
		{
			get
			{
				if(Animations[CurrentAnimation].Looping)
				{
					return false;
				}

				return currentFrame >= Animations[CurrentAnimation].Duration;
			}
		}

		public IntRect CurrentFrame
		{
			get
			{
				if(CurrentAnimation != null)
				{
					int sum = 0;
					foreach(Frame frame in Animations[CurrentAnimation].Frames)
					{
						sum += frame.Duration;
						if(currentFrame < sum)
						{
							return frame.Source;
						}
					}

					return Animations[CurrentAnimation].Frames.Last().Source;
				}

				return new IntRect();
			}
		}

		public Atlas(string filepath)
		{
			SfmlSprite = new Sprite(new Texture(filepath));
			Animations = new Dictionary<string, Animation>();
			CurrentAnimation = null;
		}

		public Atlas AddAnimation(Animation animation)
		{
			Animations.Add(animation.Name, animation);
			return this;
		}

		public Atlas StartAnimation(string key)
		{
			CurrentAnimation = key;
			startFrame = Framework.App.FramesSinceStart;
			return this;
		}

		public Atlas SetTexture(string filepath)
		{
			SfmlSprite.Texture = new Texture(filepath);
			return this;
		}

		public void Draw(RenderWindow window)
		{
			new DrawCall(SfmlSprite)
				.SetSource(CurrentFrame)
				.Draw(window);
		}
	}
}