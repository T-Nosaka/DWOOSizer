using System;
using System.Drawing;

namespace DWOOSizer.Preview
{
	/// <summary>
	/// 描画アイテム
	/// </summary>
	public class ViewBaseItem
	{
		/// <summary>
		/// イメージ拡大率
		/// </summary>
		protected double m_zoom = 0.0;

		/// <summary>
		/// イメージ拡大率プロパティ
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
		/// コンストラクタ
		/// </summary>
		public ViewBaseItem()
		{
		}

		/// <summary>
		/// 描画
		/// </summary>
		/// <param name="gr"></param>
		public virtual void OnPaint( Graphics gr )
		{
		}
	}
}
