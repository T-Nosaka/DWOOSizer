/*++

	�摜RGB24Bit����������

--*/

#pragma once

#include "OPlaneTplLeaf.h"

class DLLCLASSIMPEXP OPlaneLeafRgb24 : public OPlaneTplLeaf<BYTE>
{
protected:
	//Protected Default Constructor
	OPlaneLeafRgb24() : OPlaneTplLeaf<BYTE>() {}

public:

	const static LPCTSTR REDNAME;			//�v���[���� ��
	const static LPCTSTR GREENNAME;			//�v���[���� ��
	const static LPCTSTR BLUENAME;			//�v���[���� ��

	typedef BYTE RGB24;

	OPlaneLeafRgb24( const OPlaneLeafRgb24& instance )
	{
		*this = instance;
	}

	OPlaneLeafRgb24( DWORD width, DWORD height , std::string leafName ):
	  OPlaneTplLeaf<BYTE>( width, height, leafName )
	{
	}

	bool operator= ( const OPlaneLeafRgb24& instance )
	{
		return (OPlaneTplLeaf<BYTE>&)*this = (OPlaneTplLeaf<BYTE>&)instance;
	}

	//�R�s�[�̐���
	virtual OPlaneLeaf* Instance(  DWORD width = 0, DWORD height = 0, std::string name = "" )
	{
		return new OPlaneLeafRgb24( width, height, ( name == "" ) ? m_leafName:name ) ;
	}
};


