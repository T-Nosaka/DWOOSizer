using System;
using System.Runtime.InteropServices;

namespace DWOOSizer
{
	/// <summary>
	/// プリンタ能力取得ライブラリ
	/// </summary>
	public class DeviceCaps
	{

		[DllImport("gdi32")] public static extern IntPtr CreateIC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpdvmInit );
		[DllImport("gdi32")] public static extern bool DeleteDC(IntPtr hdc);
		[DllImport("gdi32")] public static extern int GetDeviceCaps(IntPtr hdc, int index);

		public enum GetDeviceCapsFunction
		{
			HORZRES = 8,				//実際のスクリーンの幅（実印刷領域）
			VERTRES = 10,				//実際のスクリーンの高さ
			PHYSICALWIDTH = 110,		//物理的幅(実用紙サイズ）
			PHYSICALHEIGHT = 111,		//物理的高さ
			PHYSICALOFFSETX = 112,		//印刷可能な左方向のマージン
			PHYSICALOFFSETY = 113		//印刷可能な上方向のマージン
		};

	}
}
