/*
	バイキュービック法管理
*/

#include <windows.h>

#include "OBiCubicMem.h"

void OBiCubicMem::Init()
{
	m_pPixel = new ODPixelMem*[16];

	int cnt = 0;
	//第一次近傍
	//第二次近傍

	m_pPixel[cnt++] = new ODPixelMem( -1, -1 );
	m_pPixel[cnt++] = new ODPixelMem( -1, 0 );
	m_pPixel[cnt++] = new ODPixelMem( -1, 1 );
	m_pPixel[cnt++] = new ODPixelMem( -1, 2 );

	m_pPixel[cnt++] = new ODPixelMem( 0, -1 );
	m_pPixel[cnt++] = new ODPixelMem( 0, 0 );
	m_pPixel[cnt++] = new ODPixelMem( 0, 1 );
	m_pPixel[cnt++] = new ODPixelMem( 0, 2 );

	m_pPixel[cnt++] = new ODPixelMem( 1, -1 );
	m_pPixel[cnt++] = new ODPixelMem( 1, 0 );
	m_pPixel[cnt++] = new ODPixelMem( 1, 1 );
	m_pPixel[cnt++] = new ODPixelMem( 1, 2 );

	m_pPixel[cnt++] = new ODPixelMem( 2, -1 );
	m_pPixel[cnt++] = new ODPixelMem( 2, 0 );
	m_pPixel[cnt++] = new ODPixelMem( 2, 1 );
	m_pPixel[cnt++] = new ODPixelMem( 2, 2 );

	//重みリスト初期化
	m_pDignityList = NULL;

}

OBiCubicMem::OBiCubicMem()
{
	Multiplier = 100L;

	Multiplier2m = 8L;

	Init();
}


OBiCubicMem::~OBiCubicMem()
{
	if ( m_pPixel )
	{
		for( int cnt = 0; cnt< 16; cnt++ )
			delete m_pPixel[cnt];

		delete [] m_pPixel ;
	}

	if ( m_pDignityList )
	{
		delete m_pDignityList;
		m_pDignityList = NULL;
	}
}


void OBiCubicMem::operator= ( const OBiCubicMem& instance )
{
	for( int cnt = 0; cnt< 16; cnt++ )
		m_pPixel[cnt] = instance.m_pPixel[cnt];

	((ORgbIfMem&)*this) = (ORgbIfMem&)instance;

}



#ifndef _DEBUG
#pragma optimize("t",on)
#endif

void OBiCubicMem::DignityList()
{
	if ( m_pDignityList )
	{
		delete [] m_pDignityList;
		m_pDignityList = NULL;
	}

	long cnt;
	long max = 2L << Multiplier2m;

	m_pDignityList = new long[max];

	for( cnt=0; cnt<max; cnt++ )
	{
		m_pDignityList[cnt] = (long)(Dignity((double)cnt/(double)Multiplier) * (double)Multiplier) ;
	}

}

bool OBiCubicMem::Resize( OImageProcess& imgmem, DWORD dwWidthPrm, DWORD dwHeightPrm )
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
	auto lMagnification_v = (unsigned long )(((double)effectWidth)/((double)dwWidth) * (double)Multiplier) ;
	auto lMagnification_h = (unsigned long )(((double)effectHeight)/((double)dwHeight) * (double)Multiplier) ;

	//重みのリスト作成
	DignityList();

	//計算に必要な値をここで、やっておく
	auto pXResource = new OResourceInfo*[ effectWidth ];
	for( unsigned long lCnt=0; lCnt<effectWidth; lCnt++ )
	{
		pXResource[lCnt] = new OResourceInfo( this, Multiplier2m, lMagnification_v, m_pPixel, dwWidth-1, lCnt, true );
	}
	auto pYResource = new OResourceInfo*[ effectHeight ];
	for( unsigned long lCnt=0; lCnt<effectHeight; lCnt++ )
	{
		pYResource[lCnt] = new OResourceInfo( this, Multiplier2m, lMagnification_h, m_pPixel, dwHeight-1, lCnt, false );
	}

	//スタック変数集合
	unsigned long distance_x;

	long lResult1;
	long lResult2;
	long lResult3;

	long lResultLuminosity1[4] ;
	long lResultLuminosity2[4] ;
	long lResultLuminosity3[4] ;

	WORD wLine ;
	WORD wFuncV ;
	unsigned short gCnt ;

	OPlaneLeafRgb24::iterator itSrcFirst;

	//結果に対して全走査し、ピクセル変換
	DWORD dwX, dwY;
	for( dwY= 0 ; dwY< effectHeight; dwY++ )
	{
		for( dwX= 0; dwX< effectWidth; dwX++ )
		{
			//結果輝度
			lResult1 = 0L;
			lResult2 = 0L;
			lResult3 = 0L;

			//内積結果格納変数 全色
			lResultLuminosity1[0] = 0;
			lResultLuminosity1[1] = 0;
			lResultLuminosity1[2] = 0;
			lResultLuminosity1[3] = 0;
			lResultLuminosity2[0] = 0;
			lResultLuminosity2[1] = 0;
			lResultLuminosity2[2] = 0;
			lResultLuminosity2[3] = 0;
			lResultLuminosity3[0] = 0;
			lResultLuminosity3[1] = 0;
			lResultLuminosity3[2] = 0;
			lResultLuminosity3[3] = 0;

			for( gCnt=0; gCnt<16; gCnt++ )
			{
				//重み
				wLine = (WORD)(gCnt >> 2);
				wFuncV = (WORD)(gCnt & 0x03);

				itSrcFirst = pRed->begin() + pXResource[dwX]->nearValue[gCnt] + pRed->Width() * pYResource[dwY]->nearValue[gCnt] ;
				lResultLuminosity1[ wLine ] += pYResource[dwY]->resultValue[wFuncV] * (unsigned long)(*itSrcFirst);

				itSrcFirst = pGreen->begin() + pXResource[dwX]->nearValue[gCnt] + pGreen->Width() * pYResource[dwY]->nearValue[gCnt] ;
				lResultLuminosity2[ wLine ] += pYResource[dwY]->resultValue[wFuncV] * (unsigned long)(*itSrcFirst);

				itSrcFirst = pBlue->begin() + pXResource[dwX]->nearValue[gCnt] + pBlue->Width() * pYResource[dwY]->nearValue[gCnt] ;
				lResultLuminosity3[ wLine ] += pYResource[dwY]->resultValue[wFuncV] * (unsigned long)(*itSrcFirst);
			}

			distance_x = pXResource[dwX]->distance;

			lResult1 = (
				pXResource[dwX]->resultValue[0] * ( lResultLuminosity1[0] >> Multiplier2m ) +
				pXResource[dwX]->resultValue[1] * ( lResultLuminosity1[1] >> Multiplier2m ) +
				pXResource[dwX]->resultValue[2] * ( lResultLuminosity1[2] >> Multiplier2m ) +
				pXResource[dwX]->resultValue[3] * ( lResultLuminosity1[3] >> Multiplier2m ) ) >> Multiplier2m ;

			lResult2 = (
				pXResource[dwX]->resultValue[0] * ( lResultLuminosity2[0] >> Multiplier2m ) +
				pXResource[dwX]->resultValue[1] * ( lResultLuminosity2[1] >> Multiplier2m ) +
				pXResource[dwX]->resultValue[2] * ( lResultLuminosity2[2] >> Multiplier2m ) +
				pXResource[dwX]->resultValue[3] * ( lResultLuminosity2[3] >> Multiplier2m ) ) >> Multiplier2m ;

			lResult3 = (
				pXResource[dwX]->resultValue[0] * ( lResultLuminosity3[0] >> Multiplier2m ) +
				pXResource[dwX]->resultValue[1] * ( lResultLuminosity3[1] >> Multiplier2m ) +
				pXResource[dwX]->resultValue[2] * ( lResultLuminosity3[2] >> Multiplier2m ) +
				pXResource[dwX]->resultValue[3] * ( lResultLuminosity3[3] >> Multiplier2m ) ) >> Multiplier2m ;

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
	//プレーンを追加
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

