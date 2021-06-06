/*

	���T�C�Y�摜�������Ǘ�

*/


#include <windows.h>

#include "OResizeMem.h"
#include "OPlaneLeafRgb24.h"


void OResizeMem::DettachPlane( std::string name )
{
	//�摜�v���[�����
	PLANELIST::iterator it ;

	for( it = m_planelist.begin(); it!=m_planelist.end() && m_planelist.size()>0; it++ )
	{
		if ( *it != NULL )
		{
			if ( (*it)->LeafName() == name )
			{
				m_planelist.erase( it );
				it = m_planelist.begin();
			}
		}
	}
}

void OResizeMem::GetSrcImage( OImageProcess& imgmem )
{
	//�^�[�Q�b�g��24BitRGB�Ƃ���
	OPlaneLeafRgb24* pRed = (OPlaneLeafRgb24*)imgmem.GetPlaneEx( OPlaneLeafRgb24::REDNAME );
	OPlaneLeafRgb24* pGreen = (OPlaneLeafRgb24*)imgmem.GetPlaneEx( OPlaneLeafRgb24::GREENNAME );
	OPlaneLeafRgb24* pBlue = (OPlaneLeafRgb24*)imgmem.GetPlaneEx( OPlaneLeafRgb24::BLUENAME );

	if ( pRed == NULL ||
		 pGreen == NULL ||
		 pBlue == NULL )
		return ;

	ListDelete();

	//���摜�Ƃ��Ēǉ�
	//�v���[����ǉ�
	m_planelist.push_back( pRed );
	m_planelist.push_back( pGreen );
	m_planelist.push_back( pBlue );
}

