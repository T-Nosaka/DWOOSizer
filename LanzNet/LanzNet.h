// LanzNet.h

#pragma once

using namespace System;
using namespace System::Drawing::Imaging;
using namespace System::Runtime::InteropServices;

namespace LanzNet
{
	public ref class OLanczLngMemNet
	{
	public:
		OLanczLngMemNet()
		{
			//初期動作 ランチョス
			m_Impl = new OLanczLngMem();

			m_reserve = new ORgbIfMem();
		}

		~OLanczLngMemNet()
		{
			DestroyMemory();
		}

		///
		// 解放
		///
		void DestroyMemory()
		{
			if( m_Impl != NULL )
			{
				delete m_Impl;
				m_Impl = NULL;
			}
			if( m_reserve != NULL )
			{
				delete m_reserve;
				m_reserve =NULL;
			}
		}

		void EffectType( int type )
		{
			OResizeMem* effectobj = NULL;

			switch( type )
			{
			case 0:
				effectobj = new OLanczLngMem();
				break;
			case 1:
				effectobj = new ONeighborMem();
				break;
			case 2:
				effectobj = new OBiLinearMem();
				break;
			case 3:
				effectobj = new OBiCubicMem();
				break;
			}

			//画像を交換
			*effectobj = *m_Impl;
			delete m_Impl;
			m_Impl = effectobj;
		}

		///
		// 初期ビットマップイメージ設定
		///
		bool SetBitmapRgb24( BitmapData^ bitmap )
		{
			ORgbBmpObj oRgbData ;
			oRgbData.AttachImage( bitmap->Width, bitmap->Height, bitmap->Stride, (LPBYTE)(HANDLE)(bitmap->Scan0.ToPointer()) ) ;

			//読み込んだ画像を保持
			m_reserve->SetBitmapRgb24( oRgbData );

			//マネージよりのメモリの為、解放しない。
			oRgbData.DettachImage();

			return true;
		}

		///
		// ビットマップイメージ取得
		// リサイズ後
		///
		bool GetBitmapRgb24( BitmapData^ bitmap )
		{
			ORgbBmpObj oDstRgbData ;

			m_Impl->GetBitmapRgb24( oDstRgbData, (LPBYTE)bitmap->Scan0.ToPointer() );

			bitmap->Width = oDstRgbData.Width();
			bitmap->Height = oDstRgbData.Height();
			bitmap->Stride = oDstRgbData.RowByte();

			//マネージよりのメモリの為、解放しない。
			oDstRgbData.DettachImage();

			return true;
		}

		///
		// ビットマップをトリムする。
		// リザーブしいてる元データからデータを作成する。
		///
		void TrimBitmapRgb24( BitmapData^ bitmap, DWORD left, DWORD width, DWORD top, DWORD height )
		{
			ORgbBmpObj oDstRgbData ;

			ORgbIfMem* trimh = (ORgbIfMem* )m_reserve->GetPerpendicularArea( left, left+width );
			ORgbIfMem* trimw = (ORgbIfMem* )trimh->GetLine( top, top+height );

			trimw->GetBitmapRgb24( oDstRgbData );

			bitmap->Width = oDstRgbData.Width();
			bitmap->Height = oDstRgbData.Height();
			bitmap->Stride = oDstRgbData.RowByte();
			//ビットマップをブロックコピー
			oDstRgbData.ImageCopy( bitmap->Scan0.ToPointer() );

			delete trimh;
			delete trimw;
		}

		///
		// 画像サイズ変換
		///
		bool Resize( unsigned long dwWidthPrm, unsigned long dwHeightPrm  )
		{
			try
			{
				//元画像をリンク
				m_Impl->GetSrcImage( *m_reserve );

				m_Impl->Resize( *m_reserve, dwWidthPrm, dwHeightPrm );

				return true;
			}
			catch( Exception^ )
			{
				return false;
			}
		}

	private:
		//ランチョスオブジェクト アンマネージ
		OResizeMem* m_Impl;

		//元画像
		ORgbIfMem* m_reserve;
	};
}
