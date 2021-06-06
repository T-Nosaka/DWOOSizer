/*

	�o�C���j�A�Ǘ�

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
		//�搔�A2�i���A�g�嗦�A�\�[�X�摜�̌v�Z�͈́A�l��A���W, ���t���O
		OResourceInfo( long multiplier2m, long magnification, ODPixelMem** pPixel, DWORD region, DWORD dwValue, bool flg )
		{
			//�搔����
			long multiplier = 1L << multiplier2m ;

			//�܂��A�\�[�X�摜�̃|�C���g���Z�o
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

				//�����ŁA�`��̒[�����̏ꍇ���l�����邱�ƁB
				nearValue[gCnt] = ( nearValue[gCnt] < 0 ) ? 0:nearValue[gCnt];
				nearValue[gCnt] = ( (DWORD)nearValue[gCnt] >= region ) ? region-1:nearValue[gCnt];
			}
		}

		//���摜�_���ʒu(����)
		unsigned long point_real;

		//���摜���S�_�ʒu
		unsigned long point_pp;

		//���摜�_���ʒu�ƌ��摜���S�_�ʒu�̋���
		unsigned long distance;

		//�ߐ�4�̍��W
		LONG nearValue[4];
	};


	static long Multiplier;	//�搔�A�����ݐ��x

	static long Multiplier2m;	//�搔�A2�i������

	//�\�[�X�摜�̌v�Z�͈�
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
	// ��f���ϊ�
	virtual bool Resize( OImageProcess& imgmem, DWORD dwWidthPrm, DWORD dwHeightPrm );
};


