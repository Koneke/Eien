using Eien.Framework;
using Eien.Input;
using SFML.Graphics;

namespace Eien.TestApps
{
	class AxisTest : App
	{
		float stickPercentage(bool left, bool x)
		{
			Axis axis = left
				? (x ? Axis.LeftX : Axis.LeftY)
				: (x ? Axis.RightX : Axis.RightY);

			return ((100 + Controllers[0].GetAxis(axis)) / 200f);
		}

		protected override void Update()
		{
			byte lx = (byte)(120f * stickPercentage(true, true));
			byte ly = (byte)(120f * stickPercentage(true, false));
			byte rx = (byte)(120f * stickPercentage(false, true));
			byte ry = (byte)(120f * stickPercentage(false, false));

			ClearColor = new Color((byte)(lx + rx), 0, (byte)(ly + ry));
		}

		protected override void Draw()
		{
		}
	}
}
