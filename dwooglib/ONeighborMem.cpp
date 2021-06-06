/*

	�j�A���X�g�l�C�o�Ǘ�

*/

#include <windows.h>

#include "ONeighborMem.h"

#include "OPlaneLeafRgb24.h"

bool ONeighborMem::Resize( OImageProcess& imgmem, DWORD dwWidthPrm, DWORD dwHeightPrm )
{
	//�^�[�Q�b�g��24BitRGB�Ƃ���
	OPlaneLeafRgb24* pRed = (OPlaneLeafRgb24*)GetPlane( OPlaneLeafRgb24::REDNAME );
	OPlaneLeafRgb24* pGreen = (OPlaneLeafRgb24*)GetPlane( OPlaneLeafRgb24::GREENNAME );
	OPlaneLeafRgb24* pBlue = (OPlaneLeafRgb24*)GetPlane( OPlaneLeafRgb24::BLUENAME );

	if ( pRed == NULL ||
		 pGreen == NULL ||
		 pBlue == NULL )
		return false;

	//�摜�T�C�Y�擾
	DWORD dwWidth = GetWidth();
	DWORD dwHeight = GetHeight();

	//�ŏI���ʃT�C�Y
	DWORD effectWidth = (DWORD)dwWidthPrm;
	DWORD effectHeight = (DWORD)dwHeightPrm;
	//�g�k��
	double dMagnificationH = ((double)effectWidth)/((double)dwWidth);
	double dMagnificationV = ((double)effectHeight)/((double)dwHeight);

	//�N���b�v���ꂽ���ʃT�C�Y
	RECTL effectRect;
	effectRect.top = 0;
	effectRect.left = 0;
	effectRect.right = effectWidth-1;
	effectRect.bottom = effectHeight-1;

	//���ʗp�v���[��
	OPlaneLeafRgb24* pLanRed = new OPlaneLeafRgb24( effectWidth, effectHeight, OPlaneLeafRgb24::REDNAME );
	OPlaneLeafRgb24* pLanGreen = new OPlaneLeafRgb24( effectWidth, effectHeight, OPlaneLeafRgb24::GREENNAME );
	OPlaneLeafRgb24* pLanBlue = new OPlaneLeafRgb24( effectWidth, effectHeight, OPlaneLeafRgb24::BLUENAME );

	//�ϊ����ʊi�[�����q
	OPlaneLeafRgb24::iterator itDestR = pLanRed->begin();
	OPlaneLeafRgb24::iterator itDestG = pLanGreen->begin();
	OPlaneLeafRgb24::iterator itDestB = pLanBlue->begin();

	//���ʂɑ΂��đS�������A�s�N�Z���ϊ�
	DWORD dwX, dwY;
	for( dwY= effectRect.top ; dwY<= (DWORD)effectRect.bottom; dwY++ )
	{
		for( dwX= effectRect.left; dwX<= (DWORD)effectRect.right; dwX++ )
		{
			//�܂��A�\�[�X�摜�̃|�C���g���Z�o
			double pointx_real= (double)dwX / dMagnificationH;
			double pointy_real= (double)dwY / dMagnificationV;
			DWORD pointx = (DWORD)( pointx_real+0.5 );	//���������l�̌ܓ����āA�|�C���g�̐�Βl���Z�o
			DWORD pointy = (DWORD)( pointy_real+0.5 );

			//�����ŁA�`��̒[�����̏ꍇ���l�����邱�ƁB
			pointx = ( pointx < 0 ) ? pointx=0:pointx;
			pointx = ( (DWORD)pointx >= dwWidth ) ? pointx=dwWidth-1:pointx;
			pointy = ( pointy < 0 ) ? pointy=0:pointy;
			pointy = ( (DWORD)pointy >= dwHeight ) ? pointy=dwHeight-1:pointy;

			OPlaneLeafRgb24::iterator itSrcFirstR = pRed->begin() + pointx + pRed->Width() * pointy ;
			OPlaneLeafRgb24::iterator itSrcFirstG = pGreen->begin() + pointx + pGreen->Width() * pointy ;
			OPlaneLeafRgb24::iterator itSrcFirstB = pBlue->begin() + pointx + pBlue->Width() * pointy ;

			//�l���f
			*( itDestR ) = ( *itSrcFirstR > 255 ) ? 255:( *itSrcFirstR< 0 ) ? (BYTE)0:(BYTE)*itSrcFirstR ;
			*( itDestG ) = ( *itSrcFirstG > 255 ) ? 255:( *itSrcFirstG< 0 ) ? (BYTE)0:(BYTE)*itSrcFirstG ;
			*( itDestB ) = ( *itSrcFirstB > 255 ) ? 255:( *itSrcFirstB< 0 ) ? (BYTE)0:(BYTE)*itSrcFirstB ;

			itDestR++;
			itDestG++;
			itDestB++;
		}
	}

	//���ʂ𔽉f(���摜�́A�ێ�����)
	RemovePlane(OPlaneLeafRgb24::REDNAME, false);
	RemovePlane(OPlaneLeafRgb24::GREENNAME, false);
	RemovePlane(OPlaneLeafRgb24::BLUENAME, false);
	m_planelist.push_back(pLanRed);
	m_planelist.push_back(pLanGreen);
	m_planelist.push_back(pLanBlue);

	return true;
}




