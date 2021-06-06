/*++

	Windows
	�ėp

	�摜���������

--*/

#pragma once

#include "OPlaneDllDef.h"

//�������}�N��
#define MEMSET(a,b,c)	memset(a,b,c)
#define MEMCPY(a,b,c)	memcpy(a,b,c)

//�A���P�[�g�֘A�́Aoperator new delete ���I�[�o�[���[�h���邱��

#include <string>

#pragma warning(disable:4251)

class DLLCLASSIMPEXP OPlaneLeaf
{
public:

	typedef DWORD DISTANCE;				//����
	typedef std::pair< OPlaneLeaf*, DISTANCE > DISTANCESRC;

protected:
	std::string m_leafName;		//���̃v���[���̖��O
	DWORD	m_dwMemSize;		//�������T�C�Y
	DWORD	m_width;			//��
	DWORD	m_height;			//��
	DWORD	m_DotSize;			//�h�b�g�T�C�Y

	//Protected Default Constructor
	OPlaneLeaf() ;

public:
	OPlaneLeaf( DWORD width, DWORD height , DWORD dwDotSize, std::string );
	OPlaneLeaf( const OPlaneLeaf& );

	virtual ~OPlaneLeaf() {}

	virtual void operator= ( const OPlaneLeaf& );

	//�R�s�[�̐��� ( ���O���w�肳��Ȃ��ꍇ�A�����o�̃v���[�������w�肳���
	virtual OPlaneLeaf* Instance( DWORD width = 0, DWORD height = 0, std::string name = "" ) = 0;

	std::string& LeafName() { return m_leafName; }

	inline DWORD Width() { return m_width; }
	inline DWORD Height() { return m_height; }
	DWORD MemSize() { return m_dwMemSize; }

	//�擪���������狗���擾
	virtual DISTANCE GetPoint( DWORD dwX, DWORD dwY )
	{
		if ( dwX > m_width || dwY > m_height )
			return 0;

//		return dwX*dwY*DotSize();
		return dwX*DotSize() + Width()*DotSize()* dwY;
	}

	//�擪���������狗���Ǝ��g�擾
	virtual DISTANCESRC GetPointObj( DWORD dwX, DWORD dwY )
	{
		DISTANCESRC rtn;

		rtn.first = this;
		rtn.second = GetPoint( dwX, dwY );

		return rtn;
	}

	virtual LPBYTE Point( DISTANCE dwDistance )
	{
		return ((LPBYTE)FisrtMemPointer())+dwDistance;
	}

	//�P�h�b�g�T�C�Y�擾
	virtual DWORD DotSize() =0 ;//{ return m_DotSize; }
	//�������R�s�[
	virtual bool CopyDot( DISTANCE dest, DISTANCESRC src, DWORD dotCnt );
	virtual bool CopyDot( DISTANCESRC dest, DISTANCESRC src, DWORD dotCnt );
	//�������Z�b�g
	virtual void FillDot( DISTANCE dest, BYTE value, DWORD dotCnt ) ;

	//�̈�𓧖��Ŗ��߂�
	virtual void FillClearDot( DISTANCE dest, DWORD dotCnt ) = 0;
	//�̈���ő�l�Ŗ��߂�
	virtual void FillMaxDot( DISTANCE dest, DWORD dotCnt ) = 0;

public:
	virtual void* FisrtMemPointer() = 0;


private:
	void Init();				//�R���X�g���N�^��p

};


