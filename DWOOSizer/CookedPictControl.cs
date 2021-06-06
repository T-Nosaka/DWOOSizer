using System;
using System.Drawing;
using System.Drawing.Drawing2D;

using LanzNet;

namespace DWOOSizer
{
	/// <summary>
	/// �摜�����Ǘ��R���g���[��
	/// </summary>
	public class CookedPictControl : ResizePictControl
	{
		#region �R���|�[�l���g �f�U�C�i�Ő������ꂽ�R�[�h 
		/// <summary>
		/// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
		/// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// MainPicture
			// 
			this.MainPicture.Name = "MainPicture";
			this.MainPicture.Size = new System.Drawing.Size(152, 152);
			// 
			// CookedPictControl
			// 
			this.Name = "CookedPictControl";
			this.Size = new System.Drawing.Size(152, 152);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// �摜�����N���X
		/// </summary>
		protected ColorHue m_colorhue = new ColorHue();

		/// <summary>
		/// �ʓx�v���p�e�B
		/// </summary>
		public int Saturation
		{
			get
			{
				return m_colorhue.Saturation;
			}
			set
			{
				m_colorhue.Saturation = value;
			}
		}

		/// <summary>
		/// �P�x�v���p�e�B
		/// </summary>
		public int Lightness
		{
			get
			{
				return m_colorhue.Lightness;
			}
			set
			{
				m_colorhue.Lightness = value;
			}
		}

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public CookedPictControl()
		{
		}

		/// <summary>
		/// �摜����
		/// </summary>
		public Bitmap Cooked( Bitmap srcbitmap )
		{
			//����摜���쐬
			using( BitmapUn unbitmapDataSrc = new BitmapUn( srcbitmap ) )
			{
				//�󂯎��摜���쐬
				using( BitmapUn unbitmapDst = new BitmapUn( srcbitmap.Width, srcbitmap.Height ) )
				{
					//�摜����
					m_colorhue.CookedBitmapRgb24( unbitmapDataSrc.BitmapData, unbitmapDst.BitmapData );

					//���摜�ɔ��f
					return unbitmapDst.Unlock();
				}
			}
		}
	}
}
