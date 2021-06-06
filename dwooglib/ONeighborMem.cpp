/*

	ニアレストネイバ管理

*/

#include <windows.h>

#include "ONeighborMem.h"

#include "OPlaneLeafRgb24.h"

bool ONeighborMem::Resize( OImageProcess& imgmem, DWORD dwWidthPrm, DWORD dwHeightPrm )
{
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

	//最終結果サイズ
	DWORD effectWidth = (DWORD)dwWidthPrm;
	DWORD effectHeight = (DWORD)dwHeightPrm;
	//拡縮率
	double dMagnificationH = ((double)effectWidth)/((double)dwWidth);
	double dMagnificationV = ((double)effectHeight)/((double)dwHeight);

	//クリップされた結果サイズ
	RECTL effectRect;
	effectRect.top = 0;
	effectRect.left = 0;
	effectRect.right = effectWidth-1;
	effectRect.bottom = effectHeight-1;

	//結果用プレーン
	OPlaneLeafRgb24* pLanRed = new OPlaneLeafRgb24( effectWidth, effectHeight, OPlaneLeafRgb24::REDNAME );
	OPlaneLeafRgb24* pLanGreen = new OPlaneLeafRgb24( effectWidth, effectHeight, OPlaneLeafRgb24::GREENNAME );
	OPlaneLeafRgb24* pLanBlue = new OPlaneLeafRgb24( effectWidth, effectHeight, OPlaneLeafRgb24::BLUENAME );

	//変換結果格納反復子
	OPlaneLeafRgb24::iterator itDestR = pLanRed->begin();
	OPlaneLeafRgb24::iterator itDestG = pLanGreen->begin();
	OPlaneLeafRgb24::iterator itDestB = pLanBlue->begin();

	//結果に対して全走査し、ピクセル変換
	DWORD dwX, dwY;
	for( dwY= effectRect.top ; dwY<= (DWORD)effectRect.bottom; dwY++ )
	{
		for( dwX= effectRect.left; dwX<= (DWORD)effectRect.right; dwX++ )
		{
			//まず、ソース画像のポイントを算出
			double pointx_real= (double)dwX / dMagnificationH;
			double pointy_real= (double)dwY / dMagnificationV;
			DWORD pointx = (DWORD)( pointx_real+0.5 );	//だいたい四捨五入して、ポイントの絶対値を算出
			DWORD pointy = (DWORD)( pointy_real+0.5 );

			//ここで、描画の端っこの場合を考慮すること。
			pointx = ( pointx < 0 ) ? pointx=0:pointx;
			pointx = ( (DWORD)pointx >= dwWidth ) ? pointx=dwWidth-1:pointx;
			pointy = ( pointy < 0 ) ? pointy=0:pointy;
			pointy = ( (DWORD)pointy >= dwHeight ) ? pointy=dwHeight-1:pointy;

			OPlaneLeafRgb24::iterator itSrcFirstR = pRed->begin() + pointx + pRed->Width() * pointy ;
			OPlaneLeafRgb24::iterator itSrcFirstG = pGreen->begin() + pointx + pGreen->Width() * pointy ;
			OPlaneLeafRgb24::iterator itSrcFirstB = pBlue->begin() + pointx + pBlue->Width() * pointy ;

			//値反映
			*( itDestR ) = ( *itSrcFirstR > 255 ) ? 255:( *itSrcFirstR< 0 ) ? (BYTE)0:(BYTE)*itSrcFirstR ;
			*( itDestG ) = ( *itSrcFirstG > 255 ) ? 255:( *itSrcFirstG< 0 ) ? (BYTE)0:(BYTE)*itSrcFirstG ;
			*( itDestB ) = ( *itSrcFirstB > 255 ) ? 255:( *itSrcFirstB< 0 ) ? (BYTE)0:(BYTE)*itSrcFirstB ;

			itDestR++;
			itDestG++;
			itDestB++;
		}
	}

	//結果を反映(元画像は、維持する)
	RemovePlane(OPlaneLeafRgb24::REDNAME, false);
	RemovePlane(OPlaneLeafRgb24::GREENNAME, false);
	RemovePlane(OPlaneLeafRgb24::BLUENAME, false);
	m_planelist.push_back(pLanRed);
	m_planelist.push_back(pLanGreen);
	m_planelist.push_back(pLanBlue);

	return true;
}




