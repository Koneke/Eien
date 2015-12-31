using Eien.Input;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eien.Framework
{
	abstract class App
	{
		protected RenderWindow Window;
		protected Random Random;
		protected List<Controller> Controllers;
		protected Color ClearColor;

		VideoMode videoMode;
		ContextSettings settings;

		public static int FramesSinceStart;

		protected abstract void Initialise();
		protected abstract void Update();
		protected abstract void Draw();

		public App()
		{
			Random = new Random();
			settings = new ContextSettings();

			// Defaults.
			ClearColor = Color.Cyan;
			videoMode = new VideoMode(1280, 720);
		}

		public void Start()
		{
			CreateWindow();
			SetupPadSupport();

			Initialise();

			while(Window.IsOpen)
			{
				EngineUpdate();
				EngineDraw();
			}
		}

		public void Stop()
		{
			Window.Close();
		}

		private void EngineUpdate()
		{
			FramesSinceStart++;
			Joystick.Update();

			foreach(Controller controller in Controllers)
			{
				controller.Update(frameFinal: false);
			}

			Update();

			foreach(Controller c in Controllers)
			{
				c.Update(frameFinal: true);
			}
		}

		private void EngineDraw()
		{
			Window.Clear(ClearColor);
			Draw();
			Window.DispatchEvents();
			Window.Display(); 
		}

		private void SetupPadSupport()
		{
			ControllerMap.InitialiseStandardMappings();
			Joystick.Update();

			Controllers = new List<Controller>();

			for(uint i = 0; i < Joystick.Count; i++)
			{
				if(Joystick.IsConnected(i))
				{
					JoystickConnected(null, i);
				}
			}

			Window.JoystickConnected += JoystickConnected;
			Window.JoystickDisconnected += JoystickDisconnected;
		}

		private void JoystickConnected(object sender, uint id)
		{
			Controllers.Add(Controller.AutoDetectController(id));
		}

		private void JoystickConnected(object sender, JoystickConnectEventArgs e)
		{
			JoystickConnected(sender, e.JoystickId);
		}

		private void JoystickDisconnected(object sender, JoystickConnectEventArgs e)
		{
			Controllers.Remove(Controllers.FirstOrDefault(c => c.id == e.JoystickId));
		}

		private void CreateWindow()
		{
			if(Window != null)
			{
				Window.Close();
				Window = null;
			}

			Window = new RenderWindow(videoMode, "Eien", Styles.Close | Styles.Titlebar, settings);
			Window.Closed += WindowClosed;
		}

		public void Resize(uint width, uint height)
		{
			videoMode = new VideoMode(width, height);
			CreateWindow();
		}

		private void WindowClosed(object sender, EventArgs e)
		{
			Stop();
		}
	}
}