using System;
using System.Drawing;

namespace DWOOSizer.Preview
{
	/// <summary>
	/// �`��A�C�e��
	/// </summary>
	public class ViewBaseItem
	{
		/// <summary>
		/// �C���[�W�g�嗦
		/// </summary>
		protected double m_zoom = 0.0;

		/// <summary>
		/// �C���[�W�g�嗦�v���p�e�B
		/// </summary>
		public virtual double Zoom
		{
			get
			{
				return m_zoom;
			}
			set
			{
				m_zoom = value;
			}
		}

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public ViewBaseItem()
		{
		}

		/// <summary>
		/// �`��
		/// </summary>
		/// <param name="gr"></param>
		public virtual void OnPaint( Graphics gr )
		{
		}
	}
}
