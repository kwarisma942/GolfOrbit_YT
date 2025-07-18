using UnityEngine;
using Pinpin.Types;

namespace Pinpin.Helpers
{

	public static class GeometryHelper
	{

		private static Vector2 m_rotateVectorBuffer = new Vector2();
		public static Vector2 Rotate ( Vector2 v, float theta )
		{
			float	radians = theta * Mathf.Deg2Rad;
         	float	sin = Mathf.Sin(radians);
         	float	cos = Mathf.Cos(radians);
         	float	tx = v.x;
        	float	ty = v.y;

			m_rotateVectorBuffer.x = cos * tx - sin * ty;
			m_rotateVectorBuffer.y = sin * tx + cos * ty;
			return (m_rotateVectorBuffer);
		}

	}

}