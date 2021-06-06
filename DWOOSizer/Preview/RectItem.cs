using System;
using System.Drawing;

namespace DWOOSizer.Preview
{
	/// <summary>
	/// ��`�A�C�e��
	/// </summary>
	public class RectItem : ViewBaseItem
	{
		/// <summary>
		/// ��`
		/// </summary>
		protected Rectangle m_rect ;

		/// <summary>
		/// ��`�v���p�e�B
		/// </summary>
		public Rectangle Rect
		{
			get
			{
				return m_rect;
			}
			set
			{
				m_rect = value;
			}
		}

		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public RectItem()
		{
		}

		/// <summary>
		/// �y���쐬
		/// </summary>
		/// <returns></returns>
		protected virtual Pen CreatePen()
		{
			Pen pen = new Pen(Color.Black, 2 );
			pen.Brush = new SolidBrush( Color.Black );
			pen.Width = 2.0f;

			return pen;
		}

		/// <summary>
		/// �`��C�x���g
		/// </summary>
		/// <param name="gr"></param>
		public override void OnPaint(Graphics gr)
		{
			double height = ((double)m_rect.Height)*m_zoom;
			double width = ((double)m_rect.Width)*m_zoom;

			Rectangle rect = new Rectangle( m_rect.X,m_rect.Y,(int)width,(int)height );

			//�y�����쐬
			Pen pen = CreatePen();

			gr.DrawRectangle( pen, rect );

			base.OnPaint (gr);
		}
	}
}
