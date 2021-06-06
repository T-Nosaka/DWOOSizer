/*++

	�摜����������

--*/

#pragma once

#include "OPlaneLeaf.h"

#pragma warning(disable:4275)

template<class PIXELDOTTYPE >
class DLLCLASSIMPEXP OPlaneTplLeaf : public OPlaneLeaf
{
public:

	//STL���Ƀ|�C���^��
	typedef PIXELDOTTYPE* iterator;

protected:
	PIXELDOTTYPE* m_pMem;				//�摜������

	//�f�t�H���g�R���X�g���N�^���O����́A���肦�Ȃ�
	OPlaneTplLeaf() : OPlaneLeaf()
	{
		m_pMem = NULL;
	}

public:
	OPlaneTplLeaf( const OPlaneTplLeaf<PIXELDOTTYPE>& instance )
	{
		m_pMem = NULL;
		*this = instance;
	}

	OPlaneTplLeaf( DWORD width, DWORD height , std::string leafName ):
	  OPlaneLeaf( width, height, DotSize(), leafName )
	{
		m_pMem = new PIXELDOTTYPE[ m_width*m_height];
	}

	virtual ~OPlaneTplLeaf()
	{
		if ( m_pMem != NULL )
		{
			delete [] m_pMem;
			m_pMem = NULL;
		}
	}

	virtual void operator= ( const OPlaneLeaf& instance )
	{
		OPlaneLeaf::operator= ( instance );

		if ( m_pMem != NULL )
			delete [] m_pMem;

		//�Ƃ肠�����A�����������R�s�[����B
		if ( m_dwMemSize > 0 )
		{
			m_pMem = new PIXELDOTTYPE[m_width*m_height];
			MEMCPY( m_pMem, ((OPlaneLeaf&)instance).FisrtMemPointer(), m_dwMemSize );
		}
	}

	bool operator= ( const OPlaneTplLeaf& instance )
	{
		(OPlaneLeaf&)*this = (OPlaneLeaf&)instance;

		return ( DotSize() == ((OPlaneTplLeaf&)instance).DotSize() ) ? true:false;
	}

	//�R�s�[�̐���
	virtual OPlaneLeaf* Instance(  DWORD width = 0, DWORD height = 0, std::string name = "" )
	{
		return new OPlaneTplLeaf<PIXELDOTTYPE>( width, height, ( name == "" ) ? m_leafName:name ) ;
	}

	iterator begin()
	{
		if ( m_dwMemSize == 0 )
			return (iterator)NULL;
		return (iterator)FisrtMemPointer();
	}
	iterator end()
	{
		if ( m_dwMemSize == 0 )
			return (iterator)NULL;
		return (iterator)((LPBYTE)FisrtMemPointer() + m_dwMemSize );
	}

	DWORD DotSize() { return sizeof(PIXELDOTTYPE); }

	//�ő�l���擾 (�قȂ�΁A�h���Őݒ肷��)
	virtual PIXELDOTTYPE MaxValue() 
	{
		PIXELDOTTYPE Value ;

		MEMSET( &Value, 0xff, sizeof(Value) );

		return Value; 
	}

	//�ő�l���擾 (�قȂ�΁A�h���Őݒ肷��)
	virtual PIXELDOTTYPE MinValue() 
	{
		PIXELDOTTYPE Value ;

		MEMSET( &Value, 0x00, sizeof(Value) );

		return Value; 
	}

	//�̈�𓧖��Ŗ��߂� (memset���g����悤�Ȃ�A�h���Ŋg������)
	virtual void FillClearDot( DISTANCE dest, DWORD dotCnt )
	{
		for( auto it = begin(); it!=end(); it++ )
			*it = MinValue();
	}

	//�̈���ő�l�Ŗ��߂� (memset���g����悤�Ȃ�A�h���Ŋg������)
	virtual void FillMaxDot( DISTANCE dest, DWORD dotCnt )
	{
		for(auto it = begin(); it!=end(); it++ )
			*it = MaxValue();
	}

protected:
	void* FisrtMemPointer()
	{
		return (void*)m_pMem;
	}
};

