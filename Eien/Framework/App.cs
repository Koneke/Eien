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
		protected List<Controller> Controllers;

		VideoMode videoMode;
		RenderWindow window;
		ContextSettings settings;

		protected abstract void Update();
		protected abstract void Draw();

		public App()
		{
			settings = new ContextSettings();
			videoMode = new VideoMode(1280, 720); // Default.
		}

		public void Start()
		{
			CreateWindow();
			SetupPadSupport();

			while(window.IsOpen)
			{
				EngineUpdate();
				EngineDraw();
			}
		}

		public void Stop()
		{
		}

		private void EngineUpdate()
		{
			Joystick.Update();

			foreach(Controller c in Controllers)
			{
				c.Update(frameFinal: false);
			}

			Update();

			foreach(Controller c in Controllers)
			{
				c.Update(frameFinal: true);
			}
		}

		private void EngineDraw()
		{
			window.Clear();
			window.DispatchEvents();
			Draw();
			window.Display(); 
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

			window.JoystickConnected += JoystickConnected;
			window.JoystickDisconnected += JoystickDisconnected;
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
			if(window != null)
			{
				window.Close();
				window = null;
			}

			window = new RenderWindow(videoMode, "Eien", Styles.Close | Styles.Titlebar, settings);
			window.Closed += WindowClosed;
		}

		public void Resize(uint width, uint height)
		{
			videoMode = new VideoMode(width, height);
			CreateWindow();
		}

		private void WindowClosed(object sender, EventArgs e)
		{
			window.Close();
		}
	}
}