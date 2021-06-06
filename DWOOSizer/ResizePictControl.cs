using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using LanzNet;


namespace DWOOSizer
{
	/// <summary>
	/// 画像補間コントロール
	/// </summary>
	public partial class ResizePictControl : System.Windows.Forms.UserControl
	{
		/// <summary>
		/// サイズ調整クラス
		/// </summary>
		protected OLanczLngMemNet m_lanznet = null;

		/// <summary>
		/// 補間タイプ
		/// </summary>
		protected int m_effecttype = 0;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ResizePictControl()
        {
            // この呼び出しは、Windows.Forms フォーム デザイナで必要です。
            InitializeComponent();

			this.Disposed += (owner,args) => 
			{
				if (m_lanznet != null)
				{
					m_lanznet.DestroyMemory();
					m_lanznet = null;
				}
			};
        }

		/// <summary>
		/// 補間タイプ設定
		/// </summary>
		public int EffectType
		{
			get
			{
				return m_effecttype;
			}
			set
			{
				m_effecttype = value;
				if( m_lanznet != null )
				{
					m_lanznet.EffectType( value );

					ReSizeImage( (uint)this.Width, (uint)this.Height );
				}
			}
		}

		/// <summary>
		/// 画像Ｉ／Ｆ
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual Bitmap Bitmap
		{
			get
			{
				return MainPicture.Image as Bitmap;
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
				MainPicture.Image = value;

				//イメージをアンマネージへ登録する。
				using ( BitmapUn srcbitmap = new BitmapUn( value ) )
				{
					if( m_lanznet != null )
					{
						m_lanznet.DestroyMemory();
						m_lanznet = null;
					}

					m_lanznet = new OLanczLngMemNet();
					m_lanznet.EffectType( m_effecttype );
					m_lanznet.SetBitmapRgb24( srcbitmap.BitmapData );
				}
			}
		}

		/// <summary>
		/// ピクチャコントロールプロパティ
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public PictureBox Picture
		{
			get
			{
				return MainPicture;
			}
		}

		/// <summary>
		/// イメージのサイズを変更
		/// </summary>
		public virtual void ReSizeImage( uint dwWidth, uint dwHeight )
		{
			if( m_lanznet == null || dwWidth <= 0 ||  dwHeight <= 0 )
				return ;

			lock( this )
			{
				//画面表示サイズへ調整する。
				m_lanznet.Resize( dwWidth , dwHeight );

				//受け取り画像を作成
				BitmapUn dstbitmap = new BitmapUn( (int)dwWidth, (int)dwHeight );
				//リサイズ後、画像メモリコピー
				m_lanznet.GetBitmapRgb24( dstbitmap.BitmapData );
				//ピクチャーへ設定
				MainPicture.Image = dstbitmap.Unlock();
			}
		}

	}
}
