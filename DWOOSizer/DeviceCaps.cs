using System;
using System.Runtime.InteropServices;

namespace DWOOSizer
{
	/// <summary>
	/// �v�����^�\�͎擾���C�u����
	/// </summary>
	public class DeviceCaps
	{

		[DllImport("gdi32")] public static extern IntPtr CreateIC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpdvmInit );
		[DllImport("gdi32")] public static extern bool DeleteDC(IntPtr hdc);
		[DllImport("gdi32")] public static extern int GetDeviceCaps(IntPtr hdc, int index);

		public enum GetDeviceCapsFunction
		{
			HORZRES = 8,				//���ۂ̃X�N���[���̕��i������̈�j
			VERTRES = 10,				//���ۂ̃X�N���[���̍���
			PHYSICALWIDTH = 110,		//�����I��(���p���T�C�Y�j
			PHYSICALHEIGHT = 111,		//�����I����
			PHYSICALOFFSETX = 112,		//����\�ȍ������̃}�[�W��
			PHYSICALOFFSETY = 113		//����\�ȏ�����̃}�[�W��
		};

	}
}
