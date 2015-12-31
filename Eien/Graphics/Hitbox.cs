using SFML.Graphics;

namespace Eien.Game
{
	public class Hitbox
	{
		public enum Type
		{
			Hitbox,
			Hurtbox
		}

		IntRect Rectangle;
		Type HitboxType;

		public Hitbox(IntRect rectangle, Type hitboxType)
		{
			Rectangle = rectangle;
			HitboxType = hitboxType;
		}
	}
}