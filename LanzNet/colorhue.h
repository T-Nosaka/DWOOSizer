/*++

	�F���ϊ��Ǘ�
	.Net ���b�s���O�N���X

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
		long m_SetSaturation;		//�ʓx
		long m_SetLightness;		//���x

	public:

		ColorHue() {
		}

		~ColorHue() {
		}

		///
		// �ʓx�v���p�e�B
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
		// ���x�v���p�e�B
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
		// HLS�ϊ��ɂ��F�X�ȋZ
		///
		void CookedBitmapRgb24( BitmapData^ srcbitmap, BitmapData^ destbitmap )
		{
			ORgbBmpObj oRgbData ;
			oRgbData.SetImage( srcbitmap->Width, srcbitmap->Height, srcbitmap->Stride, (LPBYTE)(HANDLE)(srcbitmap->Scan0.ToPointer()) ) ;

			//HLS�ϊ��������쐬
			OCookedMem cookedmem ;
			cookedmem.SetBitmapRgb24( oRgbData );

			//�����ݒ�
			cookedmem.SetLightness() = m_SetLightness;
			cookedmem.SetSaturation() = m_SetSaturation;

			cookedmem.Function();

			//���������ɂāA��������ԋp����B
			ORgbBmpObj oDstRgbData;
			cookedmem.GetBitmapRgb24( oDstRgbData );

			destbitmap->Width = oDstRgbData.Width();
			destbitmap->Height = oDstRgbData.Height();
			destbitmap->Stride = oDstRgbData.RowByte();

			//�r�b�g�}�b�v���u���b�N�R�s�[
			memcpy( destbitmap->Scan0.ToPointer(), oDstRgbData.Image(), oDstRgbData.GetMemorySize() );
		}
	};
}
