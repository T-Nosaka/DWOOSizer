using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace DWOOSizer.Preview
{
	/// <summary>
	/// 印刷プレビューコントロール
	/// </summary>
	public class PrintPreviewControl : PreviewControl
	{
		/// <summary>
		/// 紙サイズ
		/// </summary>
		protected PaperSizeItem m_paperitem = new PaperSizeItem();

		/// <summary>
		/// 画像イメージ
		/// </summary>
		protected PreviewItem m_imgitem = new PreviewItem();

		/// <summary>
		/// 紙拡大率
		/// </summary>
		protected double m_zoom = 0.0;

		/// <summary>
		/// 画像拡大率
		/// </summary>
		protected double m_imgzoom = 1.0;

		/// <summary>
		/// 印刷画像
		/// </summary>
		protected Bitmap m_bitmap = new Bitmap(1,1);

		/// <summary>
		/// 印刷画像プロパティ
		/// </summary>
		public Bitmap Bitmap
		{
			get
			{
				return m_bitmap;
			}
			set
			{
				m_imgitem.Bitmap = value;

				m_bitmap = value;
			}
		}

		/// <summary>
		/// 画像拡大率プロパティ
		/// </summary>
		public double ImageZoom
		{
			get
			{
				return m_imgzoom;
			}
			set
			{
				m_imgzoom = value;
			}
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PrintPreviewControl()
		{
			//初期状態作成

			//印刷対象
			AddItem( m_imgitem );

			//紙サイズ登録
			AddItem( m_paperitem );

			//イベント設定
			m_imgitem.DropEvent += new EventHandler( OnDropImage );

			//ダブルバッファ設定
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
		}

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				Bitmap = null;
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// 紙サイズセット
		/// </summary>
		public void SetPaperSize( PageSettings pagesetting, bool bLandscape )
		{
			m_paperitem.PageSettings = pagesetting;
			m_paperitem.LandScape = bLandscape;

			Rectangle areasize = new Rectangle( 0,0, Width, Height );

			//サイズ調整を行い拡大率を取得
			m_zoom = m_paperitem.SizeAdjust( pagesetting.PrinterSettings.CreateMeasurementGraphics(), areasize );

			//画像拡大率をセット
			m_imgitem.Zoom = m_zoom * m_imgzoom;
		}

		/// <summary>
		/// 画像位置設定
		/// </summary>
		/// <param name="pos"></param>
		public void SetImagePoint( Point pos )
		{
			int pos_x = (int)(((double)pos.X) * m_zoom);
			int pos_y = (int)(((double)pos.Y) * m_zoom);

			//画像拡大率をセット
			m_imgitem.Zoom = m_zoom * m_imgzoom;

			m_imgitem.Rect = new Rectangle( m_paperitem.StartPoint.X + pos_x, m_paperitem.StartPoint.Y + pos_y, Bitmap.Width, Bitmap.Height );

			this.Invalidate();
		}

		/// <summary>
		/// 画像位置取得
		/// </summary>
		/// <returns></returns>
		public Point GetImagePoint()
		{
			double dY = (double)(m_imgitem.Rect.Y - m_paperitem.StartPoint.Y);
			double dX = (double)(m_imgitem.Rect.X - m_paperitem.StartPoint.X);

			dY = dY / m_zoom;
			dX = dX / m_zoom;

			return new Point( (int)dX, (int) dY );
		}

		/// <summary>
		/// ドロップイベント
		/// </summary>
		public event EventHandler ImageDropEvent;

		/// <summary>
		/// 印刷画像のドロップイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="arg"></param>
		protected void OnDropImage( object sender, EventArgs arg )
		{
			if( ImageDropEvent != null )
			{
				ImageDropEvent( this, arg );
			}
		}

	}
}
