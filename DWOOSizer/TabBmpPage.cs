using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace DWOOSizer
{
	/// <summary>
	/// �^�u�y�[�W�摜
	/// </summary>
	public class TabBmpPage : TabPage, IDisposable
	{
		/// <summary>
		/// �\�����r�b�g�}�b�v
		/// </summary>
		protected BitmapStock m_bitmap = new BitmapStock();

		/// <summary>
		/// �t�@�C�����@�������́A���̑��
		/// </summary>
		protected string m_filename = string.Empty;

		/// <summary>
		/// �t�@�C�����v���p�e�B
		/// </summary>
		public string Filename
		{
			get
			{
				return m_filename;
			}
			set
			{
				m_filename = value;
			}
		}

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public TabBmpPage()
		{
		}

		/// <summary>
		/// �f�X�g���N�^
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			m_bitmap.Dispose();

			base.Dispose (disposing);
		}

		/// <summary>
		/// �\�����r�b�g�}�b�v�v���p�e�B
		/// </summary>
		public Bitmap SrcBitmap
		{
			get
			{
				return m_bitmap.Bitmap;
			}
			set
			{
				m_bitmap.Bitmap = value;
			}
		}

		/// <summary>
		/// 90�x��]
		/// </summary>
		public void Rotate( int pos )
		{
			Bitmap bitmap = SrcBitmap;

			RotateFlipType fliptype = RotateFlipType.Rotate90FlipNone;
			switch( pos )
			{
				case 0:
					fliptype = RotateFlipType.Rotate90FlipNone;
					break;
				case 1:
					fliptype = RotateFlipType.Rotate180FlipNone;
					break;
				case 2:
					fliptype = RotateFlipType.Rotate270FlipNone;
					break;
			}

			bitmap.RotateFlip( fliptype );

			SrcBitmap = bitmap;
		}
	}
}
