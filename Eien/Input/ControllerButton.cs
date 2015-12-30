using SFML.Window;
using System;

namespace Eien.Input
{
	enum Button
	{
		Cross,
		Circle,
		Square,
		Triangle,
		LeftBumper,
		LeftTrigger,
		RightBumper,
		RightTrigger,
		Share,
		Options
	}

	interface IControllerButton
	{
		bool IsDown(Controller controller);
		bool IsUp(Controller controller);
	}

	class StandardButton : IControllerButton
	{
		uint buttonId;

		public StandardButton(uint buttonId)
		{
			this.buttonId = buttonId;
		}

		public bool IsDown(Controller controller)
		{
			return Joystick.IsButtonPressed(controller.id, buttonId);
		}

		public bool IsUp(Controller controller)
		{
			return !IsDown(controller);
		}
	}

	class AxisButton : IControllerButton
	{
		public enum Type
		{
			ActiveAbove,
			ActiveBelow
		}

		uint axisId;
		Type type;
		float deadZone;

		public AxisButton(uint axisId, Type type, float deadZone)
		{
			this.axisId = axisId;
			this.type = type;
			this.deadZone = deadZone;
		}

		public bool IsDown(Controller controller)
		{
			float position = Joystick.GetAxisPosition(controller.id, (Joystick.Axis)axisId);

			switch(type)
			{
				case Type.ActiveAbove:
					return position > deadZone;
				case Type.ActiveBelow:
					return position < deadZone;
				default:
					throw new Exception();
			}
		}

		public bool IsUp(Controller controller)
		{
			return !IsDown(controller);
		}
	}
}