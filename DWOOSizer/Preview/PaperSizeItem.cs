using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;

namespace DWOOSizer.Preview
{
	/// <summary>
	/// 紙サイズアイテム
	/// </summary>
	public class PaperSizeItem : RectItem
	{
		/// <summary>
		/// 紙設定
		/// </summary>
		protected PageSettings m_pagesetting;
 
		/// <summary>
		/// 縦横
		/// </summary>
		protected bool m_landscape;

		/// <summary>
		/// 始点(描画座標)
		/// </summary>
		protected Point m_startpoint;

		/// <summary>
		/// 始点プロパティ
		/// </summary>
		public Point StartPoint
		{
			get
			{
				return m_startpoint;
			}
			set
			{
				m_startpoint = value;
			}
		}

		/// <summary>
		/// 紙サイズプロパティ
		/// </summary>
		public PageSettings PageSettings
		{
			get
			{
				return m_pagesetting;
			}
			set
			{
				m_pagesetting = value;
			}
		}

		/// <summary>
		/// 縦横プロパティ
		/// </summary>
		public bool LandScape
		{
			get
			{
				return m_landscape;
			}
			set
			{
				m_landscape = value;
			}
		}

		/// <summary>
		/// マージンサイズ
		/// </summary>
		protected Rectangle m_marginrect ;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PaperSizeItem()
		{
		}

		/// <summary>
		/// ペン作成
		/// </summary>
		/// <returns></returns>
		protected override Pen CreatePen()
		{
			Pen pen = new Pen(Color.Black, 2 );
			pen.Brush = new SolidBrush( Color.Black );
			pen.Width = 1.0f;

			return pen;
		}

		/// <summary>
		/// 描画イベント
		/// </summary>
		/// <param name="gr"></param>
		public override void OnPaint(Graphics gr)
		{
			double height = ((double)m_marginrect.Height)*m_zoom;
			double width = ((double)m_marginrect.Width)*m_zoom;

			double top = ((double)m_marginrect.Y)*m_zoom;
			double left = ((double)m_marginrect.X)*m_zoom;

			Rectangle rect = new Rectangle( (int)((double)m_rect.X + left), (int)((double)m_rect.Y + top),(int)width,(int)height );

			//ペンを作成
			Pen pen = new Pen(Color.Gray, 2 );
			pen.Brush = new LinearGradientBrush( new Point(0,0), new Point(2,2) , Color.White, Color.Black);
			pen.Width = 1.0f;

			gr.DrawRectangle( pen, rect );

			base.OnPaint (gr);
		}

		/// <summary>
		/// 自身サイズ調整
		/// </summary>
		public double SizeAdjust( Graphics gr, Rectangle areasize )
		{
			//実印刷ドット取得
			int iHorzres = 0;
			int iVertres = 0;
			int iMarginLeft = 1;
			int iMarginTop = 1;
			int iCapHorzres = 1;
			int iCapVertres = 1;

			IntPtr hdc = IntPtr.Zero;
			try
			{
				//物理印刷領域取得
				hdc = gr.GetHdc();

				if( hdc != IntPtr.Zero )
				{
					//実領域ドット数
					iHorzres = DeviceCaps.GetDeviceCaps( hdc, (int)DeviceCaps.GetDeviceCapsFunction.PHYSICALWIDTH );
					iVertres = DeviceCaps.GetDeviceCaps( hdc, (int)DeviceCaps.GetDeviceCapsFunction.PHYSICALHEIGHT );

					//余白位置
					//左上マージントップ
					iMarginLeft = DeviceCaps.GetDeviceCaps( hdc, (int)DeviceCaps.GetDeviceCapsFunction.PHYSICALOFFSETX );
					iMarginTop = DeviceCaps.GetDeviceCaps( hdc, (int)DeviceCaps.GetDeviceCapsFunction.PHYSICALOFFSETY );

					//印字可能領域
					iCapHorzres = DeviceCaps.GetDeviceCaps( hdc, (int)DeviceCaps.GetDeviceCapsFunction.HORZRES );
					iCapVertres = DeviceCaps.GetDeviceCaps( hdc, (int)DeviceCaps.GetDeviceCapsFunction.VERTRES );
				}
			}
			finally
			{
				gr.ReleaseHdc(hdc);
			}

			//マージン設定
			m_marginrect = new Rectangle( iMarginLeft, iMarginTop, iCapHorzres, iCapVertres);

			double paper_h = (double)iVertres;
			double paper_w = (double)iHorzres;
			double area_h = (double)areasize.Height;
			double area_w = (double)areasize.Width;

			double insize_h = area_h - area_h/10;
			double insize_w = area_w - area_w/10;

			//紙のイメージサイズ
			int rectWidth = (int)(insize_h/paper_h*paper_w);
			int rectHeight = (int)(insize_h);

			if( rectWidth > insize_w )
			{
				rectWidth = (int)(insize_w);
				rectHeight = (int)(insize_w/paper_w*paper_h);
			}

			//センタリング
			m_rect.X = (int)(area_w - rectWidth) / 2;
			m_rect.Y = (int)(area_h - rectHeight) / 2;

			//紙設定
			m_rect.Width = (int)paper_w;
			m_rect.Height = (int)paper_h;

			//拡大率
			m_zoom = ((double)rectWidth)/paper_w;

			//始点描画座標設定
			m_startpoint = new Point( m_rect.X, m_rect.Y );

			return m_zoom;
		}
	}
}
