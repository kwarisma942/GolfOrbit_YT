using System;
using UnityEngine;

[AttributeUsage (AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class ShowOnlyAttribute: PropertyAttribute
{

	public readonly bool onlyRuntime = false;

	public ShowOnlyAttribute ()
	{
		this.onlyRuntime = false;
    }

	public ShowOnlyAttribute ( bool isOnlyRuntime )
	{
		this.onlyRuntime = isOnlyRuntime;
    }

}
