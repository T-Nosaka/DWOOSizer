/*++

	HLS変換 I/F 色相変換管理
--*/


#include <windows.h>

#include "OCookedMem.h"
#include "OColorHue.h"
#include "OPlaneLeafRgb24.h"

//HLS変換
bool OCookedMem::Function()
{
	//HLS変換クラス作成
	OColorHue hue ;

	hue.SetLightness() = SetLightness();
	hue.SetSaturation() = SetSaturation();

	//ターゲットを24BitRGBとする
	OPlaneLeafRgb24* pRed = (OPlaneLeafRgb24*)GetPlane( OPlaneLeafRgb24::REDNAME );
	OPlaneLeafRgb24* pGreen = (OPlaneLeafRgb24*)GetPlane( OPlaneLeafRgb24::GREENNAME );
	OPlaneLeafRgb24* pBlue = (OPlaneLeafRgb24*)GetPlane( OPlaneLeafRgb24::BLUENAME );

	if ( pRed == NULL ||
		 pGreen == NULL ||
		 pBlue == NULL )
		return false;

	//画像サイズ取得
	DWORD dwWidth = GetWidth();
	DWORD dwHeight = GetHeight();

	//結果用プレーン
	OPlaneLeafRgb24* pLanRed = new OPlaneLeafRgb24( dwWidth, dwHeight, OPlaneLeafRgb24::REDNAME );
	OPlaneLeafRgb24* pLanGreen = new OPlaneLeafRgb24( dwWidth, dwHeight, OPlaneLeafRgb24::GREENNAME );
	OPlaneLeafRgb24* pLanBlue = new OPlaneLeafRgb24( dwWidth, dwHeight, OPlaneLeafRgb24::BLUENAME );

	//変換結果格納反復子
	OPlaneLeafRgb24::iterator itDestR = pLanRed->begin();
	OPlaneLeafRgb24::iterator itDestG = pLanGreen->begin();
	OPlaneLeafRgb24::iterator itDestB = pLanBlue->begin();

	//結果に対して全走査し、ピクセル変換
	DWORD dwX, dwY;
	for( dwY= 0 ; dwY< dwHeight; dwY++ )
	{
		for( dwX= 0; dwX< dwWidth; dwX++ )
		{
			OPlaneLeafRgb24::iterator itSrcFirstR = pRed->begin() + dwX + pRed->Width() * dwY ;
			OPlaneLeafRgb24::iterator itSrcFirstG = pGreen->begin() + dwX + pGreen->Width() * dwY ;
			OPlaneLeafRgb24::iterator itSrcFirstB = pBlue->begin() + dwX + pBlue->Width() * dwY ;

			hue.inR() = *itSrcFirstR ;
			hue.inG() = *itSrcFirstG ;
			hue.inB() = *itSrcFirstB ;

			//HLS変換
			hue.RGBtoHLS();

			//RGB変換
			hue.HLStoRGB();

			//値反映
			*( itDestR ) = hue.outR() ;
			*( itDestG ) = hue.outG() ;
			*( itDestB ) = hue.outB() ;

			itDestR++;
			itDestG++;
			itDestB++;
		}
	}

	//結果を反映
	RemovePlane( OPlaneLeafRgb24::REDNAME );
	RemovePlane( OPlaneLeafRgb24::GREENNAME );
	RemovePlane( OPlaneLeafRgb24::BLUENAME );

	//プレーンを追加
	m_planelist.push_back( pLanRed );
	m_planelist.push_back( pLanGreen );
	m_planelist.push_back( pLanBlue );

	return true;
}


