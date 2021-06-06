using System;
using System.Drawing;

using LanzNet;

namespace DWOOSizer.Preview
{
	/// <summary>
	/// 仮想画像アイテム
	/// </summary>
	public class VirtualPictItem : RectItem
	{
		/// <summary>
		/// サイズ調整クラス
		/// </summary>
		protected OLanczLngMemNet m_lanznet ;

		/// <summary>
		/// 印刷画像
		/// </summary>
		protected Bitmap m_bitmap = null;

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
				if( value == null )
				{
					//クリアの場合、確保済みメモリも破棄
					if( m_lanznet != null )
					{
						m_lanznet.DestroyMemory();
						m_lanznet = null;
					}

					return ;
				}

				//ビットマップのアタッチ
				m_bitmap = value;

				//イメージをアンマネージへ登録する。
				using ( BitmapUn srcbitmap = new BitmapUn( value ) )
				{
					if( m_lanznet != null )
					{
						m_lanznet.DestroyMemory();
						m_lanznet = null;
					}

					m_lanznet = new OLanczLngMemNet();
					m_lanznet.EffectType( 0 );
					m_lanznet.SetBitmapRgb24( srcbitmap.BitmapData );
				}
			}
		}
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public VirtualPictItem()
		{
		}

		/// <summary>
		/// ペン作成
		/// </summary>
		/// <returns></returns>
		protected override Pen CreatePen()
		{
			Pen pen = new Pen(Color.FromArgb(128, Color.Black), 1 );
			pen.Brush = new SolidBrush( Color.Black );
			pen.Width = 1.0f;

			return pen;
		}

		/// <summary>
		/// 描画イベント
		/// </summary>
		/// <param name="gr"></param>
		public override void OnPaint(System.Drawing.Graphics gr)
		{
			double height = ((double)m_rect.Height)*m_zoom;
			double width = ((double)m_rect.Width)*m_zoom;

			if( m_bitmap != null )
			{
				//画面表示サイズへ調整する。
				m_lanznet.Resize( (uint)width , (uint)height );

				//受け取り画像を作成
				BitmapUn unbitmapDst = new BitmapUn( (int)width, (int)height );

				//リサイズ後、画像メモリコピー
				m_lanznet.GetBitmapRgb24( unbitmapDst.BitmapData );

				//画像貼り付け
				gr.DrawImage( unbitmapDst.Unlock(), m_rect.X,m_rect.Y,(int)width,(int)height );
			}

			//先に枠を書く
			base.OnPaint (gr);
		}
	}
}
