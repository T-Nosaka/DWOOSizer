/*

	�o�C�L���[�r�b�N�Ǘ�

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
		//���g�A�搔�A2�i���A�g�嗦�A�\�[�X�摜�̌v�Z�͈́A�l��A���W, ���t���O
		OResourceInfo( OBiCubicMem* parent ,long multiplier2m, long magnification, ODPixelMem** pPixel, DWORD region, DWORD dwValue, bool flg )
		{
			m_parent = parent;

			//�搔����
			long multiplier = 1L << multiplier2m ;

			//�܂��A�\�[�X�摜�̃|�C���g���Z�o
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

				//�����ŁA�`��̒[�����̏ꍇ���l�����邱�ƁB
				nearValue[gCnt] = ( nearValue[gCnt] < 0 ) ? 0:nearValue[gCnt];
				nearValue[gCnt] = ( (DWORD)nearValue[gCnt] >= region ) ? region-1:nearValue[gCnt];

			}

			//�o�C�L���[�r�b�N�l
			resultValue[0] = parent->Dignity( (1L << multiplier2m) + (long)distance );
			resultValue[1] = parent->Dignity( (long)distance ); 
			resultValue[2] = parent->Dignity( (1L << multiplier2m) - (long)distance ); 
			resultValue[3] = parent->Dignity( (2L << multiplier2m) - (long)distance ); 
		}

		//���摜�_���ʒu(����)
		unsigned long point_real;

		//���摜���S�_�ʒu
		unsigned long point_pp;

		//���摜�_���ʒu�ƌ��摜���S�_�ʒu�̋���
		unsigned long distance;

		//�ߐ�16�̍��W
		LONG nearValue[16];

		//�o�C�L���[�r�b�N�l
		long resultValue[4];
	};


	//�d�݃��X�g
	long* m_pDignityList;

	long Multiplier;	//�搔�A�����ݐ��x

	long Multiplier2m;	//�搔�A2�i������

	//�\�[�X�摜�̌v�Z�͈�
	ODPixelMem** m_pPixel;

	void Init();

	double Dignity( double distance )
	{
		return ( 0.0 <= distance && distance < 1.0 ) ? (1.0 - 2.0*distance*distance + distance*distance*distance):
				( 1.0 <= distance && distance < 2.0 ) ? (4.0 - 8.0*distance +5.0*distance*distance - distance*distance*distance):
				0.0 ;
	}

	long Dignity( long distance ) // �搔10000
	{
		//�G���[�������Ȃ����A���X�g��K���쐬���邱�ƁB (DignityList)
		distance = ( distance <= 0L ) ? 0L: ( distance >= 2L << Multiplier2m ) ? (Multiplier-1):distance;

		return m_pDignityList[distance];
	}

	//�d�݂̌v�Z���ʃ��X�g�쐬
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
	// ��f���ϊ�
	virtual bool Resize( OImageProcess& imgmem, DWORD dwWidthPrm, DWORD dwHeightPrm );

} ;


