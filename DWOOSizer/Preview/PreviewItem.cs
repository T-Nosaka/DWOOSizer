using System;
using System.Drawing;
using System.Windows.Forms;

namespace DWOOSizer.Preview
{
	/// <summary>
	/// プレビューアイテム
	/// </summary>
	public class PreviewItem : VirtualPictItem, MouseEventIf
	{
		/// <summary>
		/// ドラッグ中フラグ
		/// </summary>
		protected bool m_drag = false;

		/// <summary>
		/// ドラッグ開始位置
		/// </summary>
		protected Point m_dragpoint ;

		/// <summary>
		/// ドロップイベント
		/// </summary>
		public event EventHandler DropEvent;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PreviewItem()
		{
		}

		#region MouseEventIf メンバ

		/// <summary>
		/// マウスダウン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void OnMouseDown( MouseEventArgs e)
		{
			int height = (int)(((double)m_rect.Height)*m_zoom);
			int width = (int)(((double)m_rect.Width)*m_zoom);

			if( Rect.Left <= e.X && e.X <= Rect.Left + width &&
				Rect.Top <= e.Y && e.Y <= Rect.Top + height )
			{
				m_drag = true;
				m_dragpoint = new Point( e.X - Rect.X, e.Y - Rect.Y );
			}
		}

		/// <summary>
		/// マウスアップ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void OnMouseUp( MouseEventArgs e)
		{
			if( m_drag == true )
			{
				m_drag = false;

				if( DropEvent != null )
				{
					DropEvent( this, null );
				}
			}
		}

		/// <summary>
		/// マウスムーブ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void OnMouseMove( MouseEventArgs e)
		{
			if( m_drag == true )
			{
				int ix = e.X - m_dragpoint.X;
				int iy = e.Y - m_dragpoint.Y;

				Rect = new Rectangle( ix , iy, Rect.Width, Rect.Height );
			}
		}

		/// <summary>
		/// イベント排他
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		public bool IsEventExclusive( MouseEventArgs e )
		{
			if( m_drag == true )
				return true;

			return false;
		}

		/// <summary>
		/// 再描画の必要
		/// </summary>
		/// <returns></returns>
		public bool IsInvalidate()
		{
			if( m_drag == true )
				return true;

			return false;
		}

		#endregion
	}
}
