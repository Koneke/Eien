using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eien.Input
{
	class ButtonEventArgs : EventArgs
	{
		Button button;

		public ButtonEventArgs(Button button)
		{
			this.button = button;
		}
	}

	class Controller
	{
		public static event EventHandler ButtonPressed;
		public static event EventHandler ButtonReleased;

		public static Controller AutoDetectController(uint id)
		{
			return new Controller(id, ControllerMap.X360);
		}

		public uint id;
		ControllerState state;
		ControllerState lastState;

		ControllerMap map;

		public Controller(uint id, ControllerMap map)
		{
			this.id = id;
			this.map = map;

			// Initialise, should be rewritten asap by the app.
			state = lastState = GetState();
		}

		public string Name
		{
			get
			{
				return Joystick.GetIdentification(id).Name;
			}
		}

		public void Update(bool frameFinal)
		{
			if(!frameFinal)
			{
				state = GetState();
			}
			else
			{
				lastState = state;
			}

			if(ButtonPressed != null)
			{
				foreach(Button button in map.Buttons.Where(Pressed))
				{
					ButtonPressed(this, new ButtonEventArgs(button));
				}
			}

			if(ButtonReleased != null)
			{
				foreach(Button button in map.Buttons.Where(Released))
				{
					ButtonReleased(this, new ButtonEventArgs(button));
				}
			}
		}

		public float GetAxis(Axis axis)
		{
			return state.GetAxis(axis);
		}

		public bool IsDown(Button button)
		{
			return state.IsDown(button);
		}

		public bool IsUp(Button button)
		{
			return !IsDown(button);
		}

		public bool Pressed(Button button)
		{
			return state.IsDown(button) && !lastState.IsDown(button);
		}

		public bool Released(Button button)
		{
			return state.IsUp(button) && !lastState.IsUp(button);
		}

		public ControllerState GetState()
		{
			return new ControllerState(this, map);
		}
	}

	class ControllerState
	{
		Dictionary<Button, bool> buttons;
		Dictionary<Axis, float> axises;

		public ControllerState(Controller controller, ControllerMap map)
		{
			buttons = new Dictionary<Button, bool>();
			axises = new Dictionary<Axis, float>();

			foreach(Axis axis in map.Axises)
			{
				axises[axis] = map.GetAxis(controller, axis);
			}

			foreach(Button button in map.Buttons)
			{
				buttons[button] = map.IsDown(controller, button);
			}
		}

		public float GetAxis(Axis axis)
		{
			return axises[axis];
		}

		public bool IsDown(Button button)
		{
			return buttons[button];
		}

		public bool IsUp(Button button)
		{
			return !IsDown(button);
		}
	}
}