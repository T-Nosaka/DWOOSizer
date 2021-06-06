/*

	ランチョス画像メモリ管理

*/

#pragma once

#include <math.h>
#include <limits>

#include "OResizeMem.h"
#include "OPlaneLeafRgb24.h"

#include <vector>


#ifndef PI
#define PI (atan((double)1.0)*4.0)
#endif

class DLLCLASSIMPEXP OLanczLngMem : public OResizeMem
{
protected:

	static const long FIXFLOATSHIFT;

	template<class DATATYPE >
	class Weight
	{
	public:
		long m_lCnt;		//有効データ数
		DATATYPE* m_pdData;		//ランチョス指数
		DWORD m_dwSrcPosition;	//座標（ソース画像の行or列）-3...3
//		long m_margin;		//特殊処理用にメモリにマージンを持たせるサイズ

		//コンストラクタ
		Weight( long lTap, DWORD dwPosition  )
		{
			m_dwSrcPosition = dwPosition;
			m_lCnt = lTap;
			m_pdData = new DATATYPE[lTap];
			ClearData();
		}

		~Weight()
		{
			delete [] m_pdData;
			m_pdData = NULL;
		}

		Weight( const Weight& instance )
		{
			m_pdData = NULL;

			*this = instance;
		}

		void operator=( const Weight& instance )
		{
			m_lCnt = instance.m_lCnt;

			if ( m_pdData )
				delete [] m_pdData;
			m_pdData = new int[m_lCnt];
			ClearData();
			memcpy( m_pdData, instance.m_pdData, sizeof(DATATYPE)*m_lCnt );

			m_dwSrcPosition = instance.m_dwSrcPosition;
		}

		inline DATATYPE& Data( long lPosition )
		{
			return m_pdData[lPosition];
		}

		inline long Count()
		{
			return m_lCnt;
		}

		inline DWORD SrcPosition()
		{
			return m_dwSrcPosition;
		}

private:
		void ClearData()
		{
			for( int iCnt=0; iCnt<m_lCnt; iCnt++ )
			{
				m_pdData[ iCnt ] = 0;
			}
		}
	};

	//ランチョス変換重み定義
	std::vector< Weight<long>* > m_HorizonWeight;
	std::vector< Weight<long>* > m_VerticalWeight;

public:

	OLanczLngMem() : OResizeMem()
	{
		Init();
	}

	OLanczLngMem( const OImageProcess& instance )
	{
		Init();

		(OImageProcess&)*this = instance;
	}

	OLanczLngMem( const OResizeMem& instance )
	{
		Init();

		(OResizeMem&)*this = instance;
	}

	OLanczLngMem( const OLanczLngMem& instance )
	{
		Init();

		*this = instance;
	}

	~OLanczLngMem( );


	void operator=(const OLanczLngMem& instance )
	{
		(OResizeMem&)*this = (OResizeMem&)instance;

	}

	virtual bool Resize( OImageProcess& imgmem, DWORD dwWidthPrm, DWORD dwHeightPrm );

protected:

	virtual void Init()
	{
	}

	virtual OImageProcess* Instance()
	{
		OImageProcess* pInstance = new OLanczLngMem();

		return pInstance;
	}

	//重み計算
	bool CreateWeights( std::vector< Weight<long>* >& vecTarget, double dMagnification, DWORD dwSrcLength, DWORD dwDestLength )
	{
		//画像サイズ取得
		DWORD dwLength = dwSrcLength;
		//最終結果サイズ
		DWORD effectLength = (dwDestLength ==0 ) ? (DWORD)((double)dwLength * dMagnification): dwDestLength;

		return CreateWeights( vecTarget, effectLength, dwSrcLength, dwDestLength );
	}
	//重み計算
	virtual bool CreateWeights( std::vector< Weight<long>* >& vecTarget, DWORD effectLength, DWORD dwSrcLength, DWORD dwDestLength=0 );

	//画像サイズ変換
	virtual void SizeDown( OPlaneLeafRgb24& SrcLeaf, OPlaneLeafRgb24& DestLeaf );

	//元画素中心よりの距離差に対してLanczos重みを算出
	inline double Lanczos3Weight(double dPhase)
	{
		double dRet;

		if (fabs(dPhase) < DBL_EPSILON)
		{
			return 1.0;
		}
		if (fabs(dPhase) >= 3.0)
		{
			return 0.0;
		}
		dRet = sin(PI * dPhase) * sin(PI * dPhase / 3) / (PI * PI * dPhase * dPhase / 3);
		return dRet;
	}
};

