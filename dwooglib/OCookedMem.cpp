/*++

	HLS�ϊ� I/F �F���ϊ��Ǘ�
--*/


#include <windows.h>

#include "OCookedMem.h"
#include "OColorHue.h"
#include "OPlaneLeafRgb24.h"

//HLS�ϊ�
bool OCookedMem::Function()
{
	//HLS�ϊ��N���X�쐬
	OColorHue hue ;

	hue.SetLightness() = SetLightness();
	hue.SetSaturation() = SetSaturation();

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

	//���ʗp�v���[��
	OPlaneLeafRgb24* pLanRed = new OPlaneLeafRgb24( dwWidth, dwHeight, OPlaneLeafRgb24::REDNAME );
	OPlaneLeafRgb24* pLanGreen = new OPlaneLeafRgb24( dwWidth, dwHeight, OPlaneLeafRgb24::GREENNAME );
	OPlaneLeafRgb24* pLanBlue = new OPlaneLeafRgb24( dwWidth, dwHeight, OPlaneLeafRgb24::BLUENAME );

	//�ϊ����ʊi�[�����q
	OPlaneLeafRgb24::iterator itDestR = pLanRed->begin();
	OPlaneLeafRgb24::iterator itDestG = pLanGreen->begin();
	OPlaneLeafRgb24::iterator itDestB = pLanBlue->begin();

	//���ʂɑ΂��đS�������A�s�N�Z���ϊ�
	DWORD dwX, dwY;
	for( dwY= 0 ; dwY< dwHeight; dwY++ )
	{
		for( dwX= 0; dwX< dwWidth; dwX++ )
		{
			OPlaneLeafRgb24::iterator itSrcFirstR = pRed->begin() + dwX + pRed->Width() * dwY ;
			OPlaneLeafRgb24::iterator itSrcFirstG = pGreen->begin() + dwX + pGreen->Width() * dwY ;
			OPlaneLeafRgb24::iterator itSrcFirstB = pBlue->begin() + dwX + pBlue->Width() * dwY ;

			hue.inR() = *itSrcFirstR ;
			hue.inG() = *itSrcFirstG ;
			hue.inB() = *itSrcFirstB ;

			//HLS�ϊ�
			hue.RGBtoHLS();

			//RGB�ϊ�
			hue.HLStoRGB();

			//�l���f
			*( itDestR ) = hue.outR() ;
			*( itDestG ) = hue.outG() ;
			*( itDestB ) = hue.outB() ;

			itDestR++;
			itDestG++;
			itDestB++;
		}
	}

	//���ʂ𔽉f
	RemovePlane( OPlaneLeafRgb24::REDNAME );
	RemovePlane( OPlaneLeafRgb24::GREENNAME );
	RemovePlane( OPlaneLeafRgb24::BLUENAME );

	//�v���[����ǉ�
	m_planelist.push_back( pLanRed );
	m_planelist.push_back( pLanGreen );
	m_planelist.push_back( pLanBlue );

	return true;
}


