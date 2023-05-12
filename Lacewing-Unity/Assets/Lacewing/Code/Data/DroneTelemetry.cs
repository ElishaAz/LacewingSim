using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Lacewing.Data
{
	public readonly struct DroneTelemetry
	{
		[DataMember] public readonly PositionVector position;
		[DataMember] public readonly OrientationVector orientation;
		[DataMember] public readonly IReadOnlyDictionary<string, float> lidars;

		[JsonConstructor]
		public DroneTelemetry(PositionVector position, OrientationVector orientation,
			IReadOnlyDictionary<string, float> lidars)
		{
			this.position = position;
			this.orientation = orientation;
			this.lidars = lidars;
		}
	}
}