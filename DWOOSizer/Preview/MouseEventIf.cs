using System;
using System.Windows.Forms;

namespace DWOOSizer.Preview
{
	/// <summary>
	/// マウスイベントインターフェース
	/// </summary>
	public interface MouseEventIf
	{
		/// <summary>
		/// マウスダウン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnMouseDown( MouseEventArgs e);

		/// <summary>
		/// マウスアップ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnMouseUp( MouseEventArgs e);

		/// <summary>
		/// マウスムーブ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnMouseMove( MouseEventArgs e);

		/// <summary>
		/// イベント排他
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		bool IsEventExclusive( MouseEventArgs e );

		/// <summary>
		/// 再描画の必要
		/// </summary>
		/// <returns></returns>
		bool IsInvalidate();
	}
}
