using UnityEngine;

namespace Pinpin.Helpers
{

	public static class ColorHelper
	{
	
		/// <summary>
		/// Gets the brightness.
		/// </summary>
		/// <returns>The brightness between 0 (dark) and 1 (light).</returns>
		/// <param name="color">Color.</param>
		public static float GetBrightness ( Color color )
		{
			return (Mathf.Sqrt(color.r * color.r * .241f + color.g * color.g * .691f + color.b * color.b * .068f));
		}

		/// <summary>
		/// Gets the brightness.
		/// </summary>
		/// <returns>The brightness between 0 (dark) and 1 (light).</returns>
		/// <param name="color">Color.</param>
		public static float GetBrightness ( Color32 color )
		{
			return (Mathf.Sqrt(color.r * color.r * .241f + color.g * color.g * .691f + color.b * color.b * .068f) / 255f);
		}

		/// <summary>
		/// Gets the dominant color of a texture.
		/// </summary>
		/// <returns>The dominant color.</returns>
		/// <param name="texture">Texture.</param>
		public static Color GetDominantColor ( Texture2D texture )
		{
			int width = texture.width;
			int	height = texture.height;
			int	pixels = width * height;

			float r = 0;
			float g = 0;
			float b = 0;

			for ( int x = 0 ; x < width ; x++ )
			{
				for ( int y = 0 ; y < height ; y++ )
				{
					Color rgb = texture.GetPixel(x, y);
					r += rgb.r;
					g += rgb.g;
					b += rgb.b;		
				}
			}
			return ( new Color (r / pixels, g / pixels, b / pixels) );
		}

		/// <summary>
		/// Gets the complementary of a color
		/// </summary>
		/// <returns>The complementary color.</returns>
		/// <param name="color">Color.</param>
		public static Color GetComplementaryColor ( Color color )
		{
			return ( new Color(1f - color.r, 1f - color.g, 1f - color.b) );		
		}

		/// <summary>
		/// Gets the complementary of a color
		/// </summary>
		/// <returns>The complementary color.</returns>
		/// <param name="color">Color.</param>
		public static Color32 GetComplementaryColor ( Color32 color )
		{
			return ( new Color(255 - color.r, 255 - color.g, 255 - color.b) );		
		}

	}

}