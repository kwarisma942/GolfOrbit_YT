using System;
using UnityEngine;

[AttributeUsage (AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class MinAttribute: PropertyAttribute
{
	public readonly float min;

	public MinAttribute ( float min )
	{
        this.min = min;
    }

}
