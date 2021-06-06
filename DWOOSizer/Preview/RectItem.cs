using System;
using System.Drawing;

namespace DWOOSizer.Preview
{
	/// <summary>
	/// 矩形アイテム
	/// </summary>
	public class RectItem : ViewBaseItem
	{
		/// <summary>
		/// 矩形
		/// </summary>
		protected Rectangle m_rect ;

		/// <summary>
		/// 矩形プロパティ
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
		/// コンストラクタ
		/// </summary>
		public RectItem()
		{
		}

		/// <summary>
		/// ペン作成
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
		/// 描画イベント
		/// </summary>
		/// <param name="gr"></param>
		public override void OnPaint(Graphics gr)
		{
			double height = ((double)m_rect.Height)*m_zoom;
			double width = ((double)m_rect.Width)*m_zoom;

			Rectangle rect = new Rectangle( m_rect.X,m_rect.Y,(int)width,(int)height );

			//ペンを作成
			Pen pen = CreatePen();

			gr.DrawRectangle( pen, rect );

			base.OnPaint (gr);
		}
	}
}
