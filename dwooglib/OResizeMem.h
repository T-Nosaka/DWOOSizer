/*++

	�摜���T�C�Y���ۃN���X

--*/

#pragma once

#include "ORgbIfMem.h"

class DLLCLASSIMPEXP OResizeMem : public ORgbIfMem
{
public:

	///
	// �{���ϊ�
	virtual bool TransL( double dMagnification = 0.7 )
	{
		return false;
	}

	///
	// ��f���ϊ�
	virtual bool Resize( OImageProcess& , DWORD dwWidthPrm, DWORD dwHeightPrm )
	{
		return false;
	}

	///
	// ���摜�擾
	// ���A���A�|�C���g���ړ����邾���Ȃ̂ŁA�j���֎~
	///
	void GetSrcImage( OImageProcess& imgmem );

protected:

	///
	// �v���[�����
	///
	void DettachPlane( std::string );

};
