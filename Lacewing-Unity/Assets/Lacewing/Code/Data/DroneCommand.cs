using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Lacewing.Data
{
	public readonly struct DroneCommand
	{
		[DataMember] public readonly PositionVector velocity;
		[DataMember] public readonly OrientationVector orientation;

		[JsonConstructor]
		public DroneCommand(PositionVector velocity, OrientationVector orientation)
		{
			this.velocity = velocity;
			this.orientation = orientation;
		}
	}
}