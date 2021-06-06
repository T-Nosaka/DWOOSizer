/*

	バイリニア法管理

*/

#include <windows.h>

#include "OBiLinearMem.h"

long OBiLinearMem::Multiplier = 100L;

long OBiLinearMem::Multiplier2m = 9L;

void OBiLinearMem::Init()
{
	m_pPixel = new ODPixelMem*[4];

	int cnt = 0;
	//第一次近傍

	m_pPixel[cnt++] = new ODPixelMem( 1, 1 );
	m_pPixel[cnt++] = new ODPixelMem( 0, 1 );
	m_pPixel[cnt++] = new ODPixelMem( 1, 0 );
	m_pPixel[cnt++] = new ODPixelMem( 0, 0 );
}

OBiLinearMem::OBiLinearMem()
{
	Init();
}


OBiLinearMem::~OBiLinearMem()
{
	if ( m_pPixel )
	{
		for( int cnt = 0; cnt< 4; cnt++ )
			delete m_pPixel[cnt];

		delete [] m_pPixel ;
	}
}


void OBiLinearMem::operator= ( const OBiLinearMem& instance )
{
	for( int cnt = 0; cnt< 4; cnt++ )
		m_pPixel[cnt] = instance.m_pPixel[cnt];

	((ORgbIfMem&)*this) = (ORgbIfMem&)instance;

}


bool OBiLinearMem::Resize( OImageProcess& imgmem, DWORD dwWidthPrm, DWORD dwHeightPrm )
{
	//ターゲットを24BitRGBとする
	auto pRed = (OPlaneLeafRgb24*)GetPlane( OPlaneLeafRgb24::REDNAME );
	auto pGreen = (OPlaneLeafRgb24*)GetPlane( OPlaneLeafRgb24::GREENNAME );
	auto pBlue = (OPlaneLeafRgb24*)GetPlane( OPlaneLeafRgb24::BLUENAME );

	if ( pRed == NULL ||
		 pGreen == NULL ||
		 pBlue == NULL )
		return false;

	//乗数決定
	Multiplier = 1L << Multiplier2m ;

	//画像サイズ取得
	auto dwWidth = GetWidth();
	auto dwHeight = GetHeight();

	//最終結果サイズ
	auto effectWidth = dwWidthPrm;
	auto effectHeight = dwHeightPrm;

	//結果用プレーン
	auto pLanRed = new OPlaneLeafRgb24( effectWidth, effectHeight, OPlaneLeafRgb24::REDNAME );
	auto pLanGreen = new OPlaneLeafRgb24( effectWidth, effectHeight, OPlaneLeafRgb24::GREENNAME );
	auto pLanBlue = new OPlaneLeafRgb24( effectWidth, effectHeight, OPlaneLeafRgb24::BLUENAME );

	//変換結果格納反復子
	auto itDestR = pLanRed->begin();
	auto itDestG = pLanGreen->begin();
	auto itDestB = pLanBlue->begin();

	//拡縮率
	auto Magnification_v = ((double)effectWidth)/((double)dwWidth);
	auto lMagnification_v = (long )( Magnification_v* (double)Multiplier) ;
	auto Magnification_h = ((double)effectHeight)/((double)dwHeight);
	auto lMagnification_h = (long )( Magnification_h* (double)Multiplier) ;

	//計算に必要な値をここで、やっておく
	auto pXResource = new OResourceInfo*[ effectWidth ];
	for( unsigned long lCnt=0; lCnt<effectWidth; lCnt++ )
	{
		pXResource[lCnt] = new OResourceInfo( Multiplier2m, lMagnification_v, m_pPixel, dwWidth-1, lCnt, true );
	}
	auto pYResource = new OResourceInfo*[ effectHeight ];
	for( unsigned long lCnt=0; lCnt<effectHeight; lCnt++ )
	{
		pYResource[lCnt] = new OResourceInfo( Multiplier2m, lMagnification_h, m_pPixel, dwHeight-1, lCnt, false );
	}

	unsigned long distance_x;
	unsigned long distance_y;
	unsigned long distance_calc[4];

	//結果輝度
	long lResult1;
	long lResult2;
	long lResult3;

	OPlaneLeafRgb24::RGB24 pPixelR[4];
	OPlaneLeafRgb24::RGB24 pPixelG[4];
	OPlaneLeafRgb24::RGB24 pPixelB[4];

	OPlaneLeafRgb24::iterator itSrcFirst;

	//結果に対して全走査し、ピクセル変換
	DWORD dwX, dwY;
	for( dwY= 0 ; dwY< effectHeight; dwY++ )
	{
		for( dwX= 0; dwX< effectWidth; dwX++ )
		{
			distance_x = pXResource[dwX]->distance;
			distance_y = pYResource[dwY]->distance;
			distance_calc[0] = distance_x * distance_y;
			distance_calc[1] = ((Multiplier) - distance_x) * distance_y;
			distance_calc[2] = distance_x * ((Multiplier) - distance_y);
			distance_calc[3] = ((Multiplier) - distance_x) * ((Multiplier) - distance_y);

			//第一次近傍計算
			itSrcFirst = pRed->begin() + pXResource[dwX]->nearValue[0] + pRed->Width() * pYResource[dwY]->nearValue[0] ;
			pPixelR[ 0 ] = *itSrcFirst;
			itSrcFirst = pRed->begin() + pXResource[dwX]->nearValue[1] + pRed->Width() * pYResource[dwY]->nearValue[1] ;
			pPixelR[ 1 ] = *itSrcFirst;
			itSrcFirst = pRed->begin() + pXResource[dwX]->nearValue[2] + pRed->Width() * pYResource[dwY]->nearValue[2] ;
			pPixelR[ 2 ] = *itSrcFirst;
			itSrcFirst = pRed->begin() + pXResource[dwX]->nearValue[3] + pRed->Width() * pYResource[dwY]->nearValue[3] ;
			pPixelR[ 3 ] = *itSrcFirst;

			itSrcFirst = pGreen->begin() + pXResource[dwX]->nearValue[0] + pGreen->Width() * pYResource[dwY]->nearValue[0] ;
			pPixelG[ 0 ] = *itSrcFirst;
			itSrcFirst = pGreen->begin() + pXResource[dwX]->nearValue[1] + pGreen->Width() * pYResource[dwY]->nearValue[1] ;
			pPixelG[ 1 ] = *itSrcFirst;
			itSrcFirst = pGreen->begin() + pXResource[dwX]->nearValue[2] + pGreen->Width() * pYResource[dwY]->nearValue[2] ;
			pPixelG[ 2 ] = *itSrcFirst;
			itSrcFirst = pGreen->begin() + pXResource[dwX]->nearValue[3] + pGreen->Width() * pYResource[dwY]->nearValue[3] ;
			pPixelG[ 3 ] = *itSrcFirst;

			itSrcFirst = pBlue->begin() + pXResource[dwX]->nearValue[0] + pBlue->Width() * pYResource[dwY]->nearValue[0] ;
			pPixelB[ 0 ] = *itSrcFirst;
			itSrcFirst = pBlue->begin() + pXResource[dwX]->nearValue[1] + pBlue->Width() * pYResource[dwY]->nearValue[1] ;
			pPixelB[ 1 ] = *itSrcFirst;
			itSrcFirst = pBlue->begin() + pXResource[dwX]->nearValue[2] + pBlue->Width() * pYResource[dwY]->nearValue[2] ;
			pPixelB[ 2 ] = *itSrcFirst;
			itSrcFirst = pBlue->begin() + pXResource[dwX]->nearValue[3] + pBlue->Width() * pYResource[dwY]->nearValue[3] ;
			pPixelB[ 3 ] = *itSrcFirst;

			//値反映 バイリニア処理
			lResult1 = ( ((pPixelR[0]) ) * distance_calc[0]
						+ ((pPixelR[1]) ) * distance_calc[1]
						+ ((pPixelR[2]) ) * distance_calc[2]
						+ ((pPixelR[3]) ) * distance_calc[3] )
						>> ( (Multiplier2m << 1 ) );

			lResult2 = ( ((pPixelG[0]) ) * distance_calc[0]
						+ ((pPixelG[1]) ) * distance_calc[1]
						+ ((pPixelG[2]) ) * distance_calc[2]
						+ ((pPixelG[3]) ) * distance_calc[3] )
						>> ( (Multiplier2m << 1 ) );

			lResult3 = ( ((pPixelB[0]) ) * distance_calc[0]
						+ ((pPixelB[1]) ) * distance_calc[1]
						+ ((pPixelB[2]) ) * distance_calc[2]
						+ ((pPixelB[3]) ) * distance_calc[3] )
						>> ( (Multiplier2m << 1 ) );

			//値反映
			*( itDestR ) = ( lResult1 > 255 ) ? 255:( lResult1< 0 ) ? (BYTE)0:(BYTE)lResult1 ;
			*( itDestG ) = ( lResult2 > 255 ) ? 255:( lResult2< 0 ) ? (BYTE)0:(BYTE)lResult2 ;
			*( itDestB ) = ( lResult3 > 255 ) ? 255:( lResult3< 0 ) ? (BYTE)0:(BYTE)lResult3 ;

			itDestR++;
			itDestG++;
			itDestB++;
		}
	}

	//結果を反映(元画像は、維持する)
	RemovePlane( OPlaneLeafRgb24::REDNAME, false );
	RemovePlane( OPlaneLeafRgb24::GREENNAME, false);
	RemovePlane( OPlaneLeafRgb24::BLUENAME, false);
	m_planelist.push_back( pLanRed );
	m_planelist.push_back( pLanGreen );
	m_planelist.push_back( pLanBlue );

	//計算に使った情報を破棄
	if ( pXResource )
	{
		for( unsigned long lCnt=0; lCnt<effectWidth; lCnt++ )
			delete pXResource[lCnt];

		delete [] pXResource ;
	}
	if ( pYResource )
	{
		for( unsigned long lCnt=0; lCnt<effectHeight; lCnt++ )
			delete pYResource[lCnt];

		delete [] pYResource ;
	}

	return true;
}


