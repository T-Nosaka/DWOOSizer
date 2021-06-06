/*++

	色相変換管理
	.Net ラッピングクラス

--*/

#pragma once

using namespace System;
using namespace System::Drawing::Imaging;
using namespace System::Runtime::InteropServices;

namespace LanzNet
{
	public ref class ColorHue
	{
	protected:
		long m_SetSaturation;		//彩度
		long m_SetLightness;		//明度

	public:

		ColorHue() {
		}

		~ColorHue() {
		}

		///
		// 彩度プロパティ
		///
		property long Saturation
		{
			long get() {
				return m_SetSaturation;
			}
			void set(long val) {
				m_SetSaturation = val;
			}
		}

		///
		// 明度プロパティ
		///
		property long Lightness
		{
			long get() {
				return m_SetLightness;
			}
			void set(long val) {
				m_SetLightness = val;
			}
		}

		///
		// HLS変換による色々な技
		///
		void CookedBitmapRgb24( BitmapData^ srcbitmap, BitmapData^ destbitmap )
		{
			ORgbBmpObj oRgbData ;
			oRgbData.SetImage( srcbitmap->Width, srcbitmap->Height, srcbitmap->Stride, (LPBYTE)(HANDLE)(srcbitmap->Scan0.ToPointer()) ) ;

			//HLS変換メモリ作成
			OCookedMem cookedmem ;
			cookedmem.SetBitmapRgb24( oRgbData );

			//属性設定
			cookedmem.SetLightness() = m_SetLightness;
			cookedmem.SetSaturation() = m_SetSaturation;

			cookedmem.Function();

			//処理完了にて、メモリを返却する。
			ORgbBmpObj oDstRgbData;
			cookedmem.GetBitmapRgb24( oDstRgbData );

			destbitmap->Width = oDstRgbData.Width();
			destbitmap->Height = oDstRgbData.Height();
			destbitmap->Stride = oDstRgbData.RowByte();

			//ビットマップをブロックコピー
			memcpy( destbitmap->Scan0.ToPointer(), oDstRgbData.Image(), oDstRgbData.GetMemorySize() );
		}
	};
}
