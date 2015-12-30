using System.Collections.Generic;

namespace Eien.Input
{
	class ControllerMap
	{
		public static ControllerMap X360;

		public static void InitialiseStandardMappings()
		{
			ControllerMap map = new ControllerMap();

			map.AddAxis(Axis.LeftX, new StandardAxis(0));
			map.AddAxis(Axis.LeftY, new StandardAxis(1, true));

			map.AddAxis(Axis.RightX, new StandardAxis(4));
			map.AddAxis(Axis.RightY, new StandardAxis(3, true));

			map.AddButton(Button.Cross, new StandardButton(0));
			map.AddButton(Button.Circle, new StandardButton(1));
			map.AddButton(Button.Square, new StandardButton(2));
			map.AddButton(Button.Triangle, new StandardButton(3));

			map.AddButton(Button.LeftBumper, new StandardButton(4));
			map.AddButton(Button.RightBumper, new StandardButton(5));
			map.AddButton(Button.LeftTrigger, new AxisButton(2, AxisButton.Type.ActiveAbove, 50f));
			map.AddButton(Button.RightTrigger, new AxisButton(2, AxisButton.Type.ActiveBelow, -50f));

			map.AddButton(Button.Share, new StandardButton(6));
			map.AddButton(Button.Options, new StandardButton(7));

			X360 = map;
		}

		public List<Button> Buttons;
		public List<Axis> Axises;
		private Dictionary<Button, IControllerButton> ButtonMappings;
		private Dictionary<Axis, IControllerAxis> AxisMappings;

		public ControllerMap()
		{
			Buttons = new List<Button>();
			Axises = new List<Axis>();
			ButtonMappings = new Dictionary<Button, IControllerButton>();
			AxisMappings = new Dictionary<Axis, IControllerAxis>();
		}

		public void AddButton(Button button, IControllerButton mapping)
		{
			Buttons.Add(button);
			ButtonMappings[button] = mapping;
		}

		public void RemoveButton(Button button)
		{
			Buttons.Remove(button);
			ButtonMappings.Remove(button);
		}

		public void AddAxis(Axis axis, IControllerAxis mapping)
		{
			Axises.Add(axis);
			AxisMappings[axis] = mapping;
		}

		public void RemoveAxis(Axis axis)
		{
			Axises.Remove(axis);
			AxisMappings.Remove(axis);
		}

		public float GetAxis(Controller controller, Axis axis)
		{
			return AxisMappings[axis].GetValue(controller);
		}

		public bool IsDown(Controller controller, Button button)
		{
			return ButtonMappings[button].IsDown(controller);
		}
	}
}
