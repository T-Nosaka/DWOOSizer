/*

	バイリニア管理

*/

#pragma once

#include "OResizeMem.h"
#include "ODPixelMem.h"
#include "OPlaneLeafRgb24.h"

class DLLCLASSIMPEXP OBiLinearMem : public OResizeMem
{
protected:

	class OResourceInfo
	{
	public:
		//乗数、2進数、拡大率、ソース画像の計算範囲、値域、座標, 横フラグ
		OResourceInfo( long multiplier2m, long magnification, ODPixelMem** pPixel, DWORD region, DWORD dwValue, bool flg )
		{
			//乗数決定
			long multiplier = 1L << multiplier2m ;

			//まず、ソース画像のポイントを算出
			point_real= (long)( dwValue << (multiplier2m << 1 ) ) / magnification;

			unsigned long point = point_real - point_real%multiplier ;
			point_pp = point >> multiplier2m ;

			distance = point_real - point;

			for( unsigned short gCnt=0; gCnt<4; gCnt++ )
			{
				if( flg == true )
					nearValue[gCnt] = pPixel[gCnt]->GetSrcPointX( (long)(point_pp) ) - 1;
				else
					nearValue[gCnt] = pPixel[gCnt]->GetSrcPointY( (long)(point_pp) ) - 1;

				//ここで、描画の端っこの場合を考慮すること。
				nearValue[gCnt] = ( nearValue[gCnt] < 0 ) ? 0:nearValue[gCnt];
				nearValue[gCnt] = ( (DWORD)nearValue[gCnt] >= region ) ? region-1:nearValue[gCnt];
			}
		}

		//元画像論理位置(実数)
		unsigned long point_real;

		//元画像中心点位置
		unsigned long point_pp;

		//元画像論理位置と元画像中心点位置の距離
		unsigned long distance;

		//近接4つの座標
		LONG nearValue[4];
	};


	static long Multiplier;	//乗数、おもみ精度

	static long Multiplier2m;	//乗数、2進数よりで

	//ソース画像の計算範囲
	ODPixelMem** m_pPixel;

	void Init();

public:
	OBiLinearMem() ;
	~OBiLinearMem() ;

	OBiLinearMem( const OBiLinearMem& instance )
	{
		Init();

		((OResizeMem&)*this) = instance;
	}

	OBiLinearMem( const OImageProcess& instance )
	{
		Init();

		((OImageProcess&)*this) = instance;
	}

	OBiLinearMem( const OResizeMem& instance )
	{
		Init();

		((OResizeMem&)*this) = instance;
	}

	void operator= ( const OBiLinearMem& instance );

	///
	// 画素数変換
	virtual bool Resize( OImageProcess& imgmem, DWORD dwWidthPrm, DWORD dwHeightPrm );
};


