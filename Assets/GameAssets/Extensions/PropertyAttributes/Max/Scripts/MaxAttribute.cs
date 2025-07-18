using System;
using UnityEngine;

[AttributeUsage (AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class MaxAttribute: PropertyAttribute
{
	
	public readonly float max;

	public MaxAttribute ( float max )
	{
		this.max = max;
    }

}
