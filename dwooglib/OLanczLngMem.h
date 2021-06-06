/*

	�����`���X�摜�������Ǘ�

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
		long m_lCnt;		//�L���f�[�^��
		DATATYPE* m_pdData;		//�����`���X�w��
		DWORD m_dwSrcPosition;	//���W�i�\�[�X�摜�̍sor��j-3...3
//		long m_margin;		//���ꏈ���p�Ƀ������Ƀ}�[�W������������T�C�Y

		//�R���X�g���N�^
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

	//�����`���X�ϊ��d�ݒ�`
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

	//�d�݌v�Z
	bool CreateWeights( std::vector< Weight<long>* >& vecTarget, double dMagnification, DWORD dwSrcLength, DWORD dwDestLength )
	{
		//�摜�T�C�Y�擾
		DWORD dwLength = dwSrcLength;
		//�ŏI���ʃT�C�Y
		DWORD effectLength = (dwDestLength ==0 ) ? (DWORD)((double)dwLength * dMagnification): dwDestLength;

		return CreateWeights( vecTarget, effectLength, dwSrcLength, dwDestLength );
	}
	//�d�݌v�Z
	virtual bool CreateWeights( std::vector< Weight<long>* >& vecTarget, DWORD effectLength, DWORD dwSrcLength, DWORD dwDestLength=0 );

	//�摜�T�C�Y�ϊ�
	virtual void SizeDown( OPlaneLeafRgb24& SrcLeaf, OPlaneLeafRgb24& DestLeaf );

	//����f���S���̋������ɑ΂���Lanczos�d�݂��Z�o
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

