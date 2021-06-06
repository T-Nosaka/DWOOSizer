/*

	バイキュービック管理

*/

#pragma once

#include "OResizeMem.h"

#include "ODPixelMem.h"

#include "OPlaneLeafRgb24.h"

class DLLCLASSIMPEXP OBiCubicMem : public OResizeMem
{
protected:

	class OResourceInfo
	{
	protected:
		OBiCubicMem* m_parent;
	public:
		//自身、乗数、2進数、拡大率、ソース画像の計算範囲、値域、座標, 横フラグ
		OResourceInfo( OBiCubicMem* parent ,long multiplier2m, long magnification, ODPixelMem** pPixel, DWORD region, DWORD dwValue, bool flg )
		{
			m_parent = parent;

			//乗数決定
			long multiplier = 1L << multiplier2m ;

			//まず、ソース画像のポイントを算出
			point_real= (long)( dwValue << (multiplier2m << 1 ) ) / magnification;

			unsigned long point = point_real - point_real%multiplier ;
			point_pp = point >> multiplier2m ;

			distance = point_real - point;

			for( unsigned short gCnt=0; gCnt<16; gCnt++ )
			{
				if( flg == true )
					nearValue[gCnt] = pPixel[gCnt]->GetSrcPointX( (long)(point_pp) ) - 1;
				else
					nearValue[gCnt] = pPixel[gCnt]->GetSrcPointY( (long)(point_pp) ) - 1;

				//ここで、描画の端っこの場合を考慮すること。
				nearValue[gCnt] = ( nearValue[gCnt] < 0 ) ? 0:nearValue[gCnt];
				nearValue[gCnt] = ( (DWORD)nearValue[gCnt] >= region ) ? region-1:nearValue[gCnt];

			}

			//バイキュービック値
			resultValue[0] = parent->Dignity( (1L << multiplier2m) + (long)distance );
			resultValue[1] = parent->Dignity( (long)distance ); 
			resultValue[2] = parent->Dignity( (1L << multiplier2m) - (long)distance ); 
			resultValue[3] = parent->Dignity( (2L << multiplier2m) - (long)distance ); 
		}

		//元画像論理位置(実数)
		unsigned long point_real;

		//元画像中心点位置
		unsigned long point_pp;

		//元画像論理位置と元画像中心点位置の距離
		unsigned long distance;

		//近接16の座標
		LONG nearValue[16];

		//バイキュービック値
		long resultValue[4];
	};


	//重みリスト
	long* m_pDignityList;

	long Multiplier;	//乗数、おもみ精度

	long Multiplier2m;	//乗数、2進数よりで

	//ソース画像の計算範囲
	ODPixelMem** m_pPixel;

	void Init();

	double Dignity( double distance )
	{
		return ( 0.0 <= distance && distance < 1.0 ) ? (1.0 - 2.0*distance*distance + distance*distance*distance):
				( 1.0 <= distance && distance < 2.0 ) ? (4.0 - 8.0*distance +5.0*distance*distance - distance*distance*distance):
				0.0 ;
	}

	long Dignity( long distance ) // 乗数10000
	{
		//エラー処理しないが、リストを必ず作成すること。 (DignityList)
		distance = ( distance <= 0L ) ? 0L: ( distance >= 2L << Multiplier2m ) ? (Multiplier-1):distance;

		return m_pDignityList[distance];
	}

	//重みの計算結果リスト作成
	void DignityList();

public:
	OBiCubicMem() ;
	~OBiCubicMem() ;

	OBiCubicMem( const OBiCubicMem& instance )
	{
		Init();

		((OResizeMem&)*this) = instance;
	}

	OBiCubicMem( const OImageProcess& instance )
	{
		Init();

		((OImageProcess&)*this) = instance;
	}

	OBiCubicMem( const OResizeMem& instance )
	{
		Init();

		((OResizeMem&)*this) = instance;
	}

	void operator= ( const OBiCubicMem& instance );

	///
	// 画素数変換
	virtual bool Resize( OImageProcess& imgmem, DWORD dwWidthPrm, DWORD dwHeightPrm );

} ;


