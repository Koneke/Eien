using SFML.Window;

namespace Eien.Input
{
	enum Axis
	{
		LeftX,
		LeftY,
		LeftTrigger,
		RightX,
		RightY,
		RightTrigger
	}

	interface IControllerAxis
	{
		float GetValue(Controller controller);
	}

	class StandardAxis : IControllerAxis
	{
		uint axisId;
		float deadZone;
		bool inverted;
		// Curve?

		public StandardAxis(uint axisId, bool inverted = false, float deadZone = 0f)
		{
			this.axisId = axisId;
			this.inverted = inverted;
			this.deadZone = deadZone;
		}

		public float GetValue(Controller controller)
		{
			float position = Joystick.GetAxisPosition(controller.id, (Joystick.Axis)axisId);

			// Apply deadzoning.
			if(System.Math.Abs(position) < deadZone)
			{
				position = 0;
			}

			return position * (inverted ? -1 : 1);
		}
	}

	//class ButtonAxis : IControllerAxis
}