/*++

	Windows
	汎用

	ランチョス画像メモリ管理

--*/


#include <windows.h>

#include "OLanczLngMem.h"

const long OLanczLngMem::FIXFLOATSHIFT = 1000000;

OLanczLngMem::~OLanczLngMem( )
{
	for( auto it = m_HorizonWeight.begin(); it!=m_HorizonWeight.end(); it++ )
	{
		delete *it;
	}

	for( auto it = m_VerticalWeight.begin(); it!=m_VerticalWeight.end(); it++ )
	{
		delete *it;
	}
}


bool OLanczLngMem::Resize( OImageProcess& imgmem, DWORD effectWidth, DWORD effectHeight )
{
	//ターゲットを24BitRGBとする
	auto pRed = (OPlaneLeafRgb24*)GetPlane( OPlaneLeafRgb24::REDNAME );
	auto pGreen = (OPlaneLeafRgb24*)GetPlane( OPlaneLeafRgb24::GREENNAME );
	auto pBlue = (OPlaneLeafRgb24*)GetPlane( OPlaneLeafRgb24::BLUENAME );

	if ( pRed == NULL ||
		 pGreen == NULL ||
		 pBlue == NULL )
		return false;

	//画像サイズ取得
	DWORD dwWidth = GetWidth();
	DWORD dwHeight = GetHeight();

	//結果用プレーン
	auto pLanRed = new OPlaneLeafRgb24( effectWidth, effectHeight, OPlaneLeafRgb24::REDNAME );
	auto pLanGreen = new OPlaneLeafRgb24( effectWidth, effectHeight, OPlaneLeafRgb24::GREENNAME );
	auto pLanBlue = new OPlaneLeafRgb24( effectWidth, effectHeight, OPlaneLeafRgb24::BLUENAME );

	//水平重み
	CreateWeights( m_HorizonWeight, effectWidth, dwWidth );
	//垂直重み
	CreateWeights( m_VerticalWeight, effectHeight, dwHeight );

	//変換
	SizeDown( *pRed, *pLanRed );
	SizeDown( *pGreen, *pLanGreen );
	SizeDown( *pBlue, *pLanBlue );

	//結果を反映(元画像は、維持する)
	RemovePlane( OPlaneLeafRgb24::REDNAME, false );
	RemovePlane( OPlaneLeafRgb24::GREENNAME, false );
	RemovePlane( OPlaneLeafRgb24::BLUENAME, false );
	m_planelist.push_back( pLanRed );
	m_planelist.push_back( pLanGreen );
	m_planelist.push_back( pLanBlue );

	return true;
}


bool OLanczLngMem::CreateWeights( std::vector< Weight<long>* >& vecTarget, DWORD effectLength, DWORD dwSrcLength, DWORD dwDestLength )
{
	//画像サイズ取得
	DWORD dwLength = dwSrcLength;

	//拡縮率
	double dMagnification = ((double)effectLength)/((double)dwLength);

	//タップ幅取得
	long lTap;
	lTap = (dMagnification < 1.0) ? 6.0/dMagnification:6.0;
	double* pLanczos3Val = new double[lTap];
	long* pLindex = new long[lTap];
	double dLanczos3ValSum;

	int iCnt;
	int dwTapCnt;

	for each (auto vec in vecTarget)
		delete vec;
	vecTarget.clear();

	for( iCnt=0; iCnt<effectLength; iCnt++ )
	{
		//まず、ソース画像のポイントを算出
		long dwFirstPosition = (long)( ((double)(iCnt-3)+0.5) / dMagnification );	//ポイントの値を算出

		dLanczos3ValSum = 0;

		double dDistance;

		//距離より重み算出
		for( dwTapCnt=0; dwTapCnt<lTap; dwTapCnt++ )
		{
			//縮小
			if (dMagnification < 1.0)
			{
				dDistance =
					((double)iCnt) -
					((double)(dwFirstPosition+dwTapCnt) * dMagnification) ;

				//端は、隔たせる。
				if ( (dwFirstPosition+dwTapCnt) < 0 )
				{
					pLindex[dwTapCnt] = 0;
				}
				else
				if ( (dwFirstPosition+dwTapCnt) >= dwLength )
				{
					pLindex[dwTapCnt] = dwLength-1;
				}
				else
				{
					pLindex[dwTapCnt] = dwFirstPosition+dwTapCnt;
				}
			}
			else
			{
				double dCenter = ((double)iCnt+0.5)/dMagnification;
				double dTapPoint = ((double)dwFirstPosition*dMagnification+dwTapCnt)/dMagnification;

				//拡大
				dDistance = dCenter - dTapPoint;

				//端は、隔たせる。
				if ( dTapPoint < 0.0 )
				{
					pLindex[dwTapCnt] = 0;
				}
				else
				if ( dTapPoint >= dwLength )
				{
					pLindex[dwTapCnt] = dwLength-1;
				}
				else
				{
					pLindex[dwTapCnt] = dTapPoint;
				}
			}

			//ランチョス窓関数
			pLanczos3Val[dwTapCnt] = Lanczos3Weight( dDistance );

			//指数算出の為の集計
			dLanczos3ValSum += pLanczos3Val[dwTapCnt];
		}

		auto weight = new Weight<long>( pLindex[lTap-1] - pLindex[0]+1, pLindex[0] );

		//0 Div
		dLanczos3ValSum = ( dLanczos3ValSum == 0.0 ) ? 0.000000001:dLanczos3ValSum;

		//重みより原画からの採用率を算出
		for( dwTapCnt=0; dwTapCnt<lTap; dwTapCnt++ )
		{
			//指数算出
			double dFactor = (pLanczos3Val[dwTapCnt] / dLanczos3ValSum) * FIXFLOATSHIFT;
			weight->Data( pLindex[dwTapCnt] - pLindex[0] ) += dFactor ;
		}

		vecTarget.push_back(weight);
	}

	delete [] pLanczos3Val;
	delete [] pLindex;

	return true;
}

void OLanczLngMem::SizeDown( OPlaneLeafRgb24& SrcLeaf, OPlaneLeafRgb24& DestLeaf )
{
	//全ピクセル変換、RGB全色サイズは、きめうち処理とする。　気をつけよ

	//中間結果用プレーン
	auto pTempLeaf = new OPlaneLeafRgb24( DestLeaf.Width(), SrcLeaf.Height(), "Temp" );

	int dwHeightCnt;
	int dwWidthCnt;
	long dLanz = 0.0;
	int iTap ;	//重みカウンタ

	int dwHeightUpper;
	int dwWidthUpper;
	int iTapUpper;

	//水平中間変換結果格納反復子
	auto itDestFirst = pTempLeaf->begin();

	dwHeightUpper = pTempLeaf->Height();
	dwWidthUpper = pTempLeaf->Width();

	auto itSrc = SrcLeaf.begin();

	Weight<long>* itWeight;
	int iSrcWidth = SrcLeaf.Width();

	//水平変換
	for( dwHeightCnt=0; dwHeightCnt<dwHeightUpper; dwHeightCnt++ )
	{
		for( dwWidthCnt=0; dwWidthCnt<dwWidthUpper; dwWidthCnt++ )
		{
			itWeight = m_HorizonWeight[dwWidthCnt];

			OPlaneLeafRgb24::iterator itSrcFirst = itSrc + itWeight->m_dwSrcPosition + iSrcWidth * dwHeightCnt ;

			//重みの積算
			dLanz = 0;

			iTapUpper = itWeight->m_lCnt;

			//tap分の重みとソース画像を積算し、結果ピクセルを取得
			for( iTap=0; iTap< iTapUpper; iTap++ )
			{
				auto srcdata = (long)*itSrcFirst;
				auto target = itWeight->m_pdData[iTap];
				dLanz += (srcdata* target) ;
				itSrcFirst++;
			}

			dLanz /= FIXFLOATSHIFT;

			//値域内へ修正
			*itDestFirst = ( dLanz < DestLeaf.MinValue() ) ? DestLeaf.MinValue():( dLanz >= DestLeaf.MaxValue() ) ? DestLeaf.MaxValue():dLanz;

			itDestFirst++;
		}
	}

	dwHeightUpper = DestLeaf.Height();
	dwWidthUpper = DestLeaf.Width();

	auto itTmp = pTempLeaf->begin();
	auto itDestBegin = DestLeaf.begin();

	int iTempWidth = pTempLeaf->Width();

	//垂直変換
	for( dwWidthCnt=0; dwWidthCnt<dwWidthUpper; dwWidthCnt++ )
	{
		for( dwHeightCnt=0; dwHeightCnt<dwHeightUpper; dwHeightCnt++ )
		{
			itWeight = m_VerticalWeight[dwHeightCnt];

			OPlaneLeafRgb24::iterator itSrcFirst = itTmp + dwWidthCnt + iTempWidth * itWeight->m_dwSrcPosition  ;

			//重みの積算
			dLanz = 0;

			iTapUpper = itWeight->m_lCnt;

			//tap分の重みとソース画像を積算し、結果ピクセルを取得
			for( iTap=0; iTap< iTapUpper; iTap++ )
			{
				auto srcdata = (long)*itSrcFirst;
				auto target = itWeight->m_pdData[iTap];
				dLanz += (srcdata * target );
				itSrcFirst += iTempWidth;
			}

			dLanz /= FIXFLOATSHIFT;

			//値域内へ修正
			itDestFirst = itDestBegin + dwWidthCnt + DestLeaf.Width() * dwHeightCnt ;
			*itDestFirst = ( dLanz < DestLeaf.MinValue() ) ? DestLeaf.MinValue():( dLanz >= DestLeaf.MaxValue() ) ? DestLeaf.MaxValue():dLanz;
		}
	}

	delete pTempLeaf;
}


