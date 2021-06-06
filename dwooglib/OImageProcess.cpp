/*++

	�摜�������

--*/


#include <windows.h>

#include "OImageProcess.h"


OImageProcess::OImageProcess()
{
	Init();
}

OImageProcess::~OImageProcess()
{
	ListDelete();
}


OImageProcess::OImageProcess( const OImageProcess& target )
{
	Init();

	*this = target;
}

void OImageProcess::Init()
{
	m_TopX = 0;
	m_TopY = 0;
	m_ulHorzRes = 0;
	m_ulVertRes = 0;
}

void OImageProcess::ListDelete()
{
	//�摜�v���[���폜
	for( auto it = m_planelist.begin(); it!=m_planelist.end(); it++ )
	{
		if ( *it != NULL )
		{
			delete *it ;
			*it = NULL;
		}
	}

	m_planelist.clear();
}

void OImageProcess::ListCopy( PLANELIST& destList )
{
	//�摜�v���[������
	for( auto it = destList.begin(); it!=destList.end(); it++ )
	{
		if ( *it != NULL )
		{
			OPlaneLeaf* pInstance = (*it)->Instance();

			*pInstance = *(*it);

			m_planelist.push_back( pInstance );
		}
	}
}

OPlaneLeaf* OImageProcess::GetPlane( std::string name )
{
	OPlaneLeaf* rtnObj = NULL;

	//�摜�v���[���擾
	for( auto it = m_planelist.begin(); it!=m_planelist.end(); it++ )
	{
		if ( *it != NULL )
		{
			if ( (*it)->LeafName() == name )
				return *it ;
		}
	}

	return NULL;
}

void OImageProcess::RemovePlane( std::string name, bool bDestroy )
{
	//�摜�v���[���폜
	do
	{
		auto bFind = false;
		for (auto it = m_planelist.begin(); it != m_planelist.end() && m_planelist.size() > 0; it++)
		{
			if (*it != NULL)
			{
				if ((*it)->LeafName() == name)
				{
					if(bDestroy == true )
						delete *it;
					m_planelist.erase(it);
					bFind = true;
					break;
				}
			}
		}
		if (bFind == false)
			break;
	} 	while (true);
}


void OImageProcess::operator=( const OImageProcess& target )
{
	m_TopX = target.m_TopX;
	m_TopY = target.m_TopY;
	m_ulHorzRes = target.m_ulHorzRes;
	m_ulVertRes = target.m_ulVertRes;

	//���g�̃v���[����j��
	ListDelete();

	//�v���[���R�s�[
	ListCopy( ((OImageProcess&)target).m_planelist );
}


OImageProcess* OImageProcess::operator +( const OImageProcess& target )
{
	if ( m_planelist.size() )
	{
		//�P���ɒǉ��̏ꍇ�A�����ȏ������s��
		if ( GetWidth() == ((OImageProcess&)target).GetWidth() &&
			m_TopY + GetHeight() == target.m_TopY )
		{
			return Append( target );
		}
	}
	else
	{
		return Append( target );
	}

	OImageProcess& RetObj = *Instance();

	RetObj.m_TopX = ( m_TopX < ((OImageProcess&)target).GetTopX() ) ? m_TopX:((OImageProcess&)target).GetTopX();
	RetObj.m_TopY = ( m_TopY < ((OImageProcess&)target).GetTopY() ) ? m_TopY:((OImageProcess&)target).GetTopY();

	//���̌�̗̈�v�Z
	DWORD dwWidth = ( ( m_TopX+GetWidth() > ((OImageProcess&)target).GetTopX()+((OImageProcess&)target).GetWidth() ) ?
		m_TopX+GetWidth() : ((OImageProcess&)target).GetTopX()+((OImageProcess&)target).GetWidth() 
		) - RetObj.m_TopX ;
	DWORD dwHeight = ( ( m_TopY+GetHeight() > ((OImageProcess&)target).GetTopY()+((OImageProcess&)target).GetHeight() ) ?
		m_TopY+GetHeight() : ((OImageProcess&)target).GetTopY()+((OImageProcess&)target).GetHeight() 
		) - RetObj.m_TopY ;

	RetObj.m_ulHorzRes = m_ulHorzRes;
	RetObj.m_ulVertRes = m_ulVertRes;

	//�S�Ẵv���[��������
	for( auto it=m_planelist.begin(); it!=m_planelist.end(); it++ )
	{
		//�V�̈�̃v���[�����쐬
		OPlaneLeaf* pPlane = (*it)->Instance( dwWidth, dwHeight );

		//�܂��A���g�̉摜���R�s�[����B
		DWORD dwLine ;
		for( dwLine=0; dwLine< (*it)->Height(); dwLine++ )
		{
			pPlane->CopyDot( pPlane->GetPoint( m_TopX - RetObj.m_TopX, m_TopY - RetObj.m_TopY + dwLine ),
							(*it)->GetPointObj( 0, dwLine ),
							(*it)->Width() );
		}

		//���ɑ���̉摜���R�s�[����B
		OPlaneLeaf* pDestPlane = ((OImageProcess&)target).GetPlane( (*it)->LeafName() );

		if ( pDestPlane != NULL )
		{
			for( dwLine=0; dwLine< pDestPlane->Height(); dwLine++ )
			{
				pPlane->CopyDot( pPlane->GetPoint( target.m_TopX - RetObj.m_TopX, target.m_TopY - RetObj.m_TopY + dwLine ),
								pDestPlane->GetPointObj( 0, dwLine ),
								pDestPlane->Width() );
			}
		}

		//���������v���[����ǉ�
		RetObj.m_planelist.push_back( pPlane );
	}

	return &RetObj;
}


OImageProcess* OImageProcess::GetLine( DWORD startLine , DWORD endLine )
{
	if ( endLine < startLine )
	{
		DWORD tmp = endLine;
		endLine = startLine;
		startLine = tmp;
	}

	DWORD dwHeight = GetHeight();

	if ( startLine >= dwHeight )
	{
		startLine = dwHeight;
	}
	if ( endLine >= dwHeight )
	{
		endLine = dwHeight;
	}

	OImageProcess* pRetObj = Instance();

	pRetObj->m_TopX = m_TopX;
	pRetObj->m_TopY = m_TopY + startLine;
	pRetObj->m_ulHorzRes = m_ulHorzRes;
	pRetObj->m_ulVertRes = m_ulVertRes;

	DWORD dwPlaneWidth = GetWidth();
	DWORD dwPlaneHeight = endLine - startLine ;

	//�S�Ẵv���[��������
	for( auto it=m_planelist.begin(); it!=m_planelist.end(); it++ )
	{
		//�V�̈�̃v���[�����쐬
		OPlaneLeaf* pPlane = (*it)->Instance( dwPlaneWidth, dwPlaneHeight );

		//�̈�̉摜���R�s�[����B
		pPlane->CopyDot( pPlane->GetPoint( 0, 0 ),
						(*it)->GetPointObj( 0, startLine ),
						pPlane->Width() * pPlane->Height() );

		//���������v���[����ǉ�
		pRetObj->m_planelist.push_back( pPlane );
	}

	return pRetObj;
}


OImageProcess* OImageProcess::GetPerpendicularArea( DWORD startPos , DWORD endPos )
{
	if ( endPos < startPos )
	{
		DWORD tmp = endPos;
		endPos = startPos;
		startPos = tmp;
	}

	DWORD dwWidth = GetWidth();

	if ( startPos >= dwWidth )
	{
		startPos = dwWidth;
	}
	if ( endPos >= dwWidth )
	{
		endPos = dwWidth;
	}

	OImageProcess* pRetObj = Instance();

	pRetObj->m_TopX = m_TopX + startPos;
	pRetObj->m_TopY = m_TopY ;
	pRetObj->m_ulHorzRes = m_ulHorzRes;
	pRetObj->m_ulVertRes = m_ulVertRes;

	DWORD dwPlaneWidth = endPos - startPos;
	DWORD dwPlaneHeight = GetHeight() ;

	//�S�Ẵv���[��������
	for( auto it=m_planelist.begin(); it!=m_planelist.end(); it++ )
	{
		//�V�̈�̃v���[�����쐬
		OPlaneLeaf* pPlane = (*it)->Instance( dwPlaneWidth, dwPlaneHeight );

		DWORD row ;
		for( row=0; row<(*it)->Height(); row++ )
		{
			pPlane->CopyDot( pPlane->GetPoint( 0, row ),
							(*it)->GetPointObj( startPos, row ),
							dwPlaneWidth );
		}

		//���������v���[����ǉ�
		pRetObj->m_planelist.push_back( pPlane );
	}

	return pRetObj;
}


DWORD OImageProcess::GetWidth()
{
	DWORD dwWidth = 0;

	//���L����v���[���̍ő啝
	for( auto it = m_planelist.begin(); it!=m_planelist.end(); it++ )
		if ( *it != NULL )
			dwWidth = ( (*it)->Width() > dwWidth ) ? (*it)->Width():dwWidth ;

	return dwWidth;
}

DWORD OImageProcess::GetHeight()
{
	DWORD dwHeight = 0;

	//���L����v���[���̍ő啝
	for( auto it = m_planelist.begin(); it!=m_planelist.end(); it++ )
		if ( *it != NULL )
			dwHeight = ( (*it)->Height() > dwHeight ) ? (*it)->Height():dwHeight ;

	return dwHeight;
}


OImageProcess* OImageProcess::Append( const OImageProcess& target )
{
	OImageProcess* pRetObj = Instance();

	DWORD dwWidth;
	DWORD dwHeight;

	if ( GetWidth() > 0 && GetHeight() > 0 )
	{
		pRetObj->m_TopX = m_TopX;
		pRetObj->m_TopY = m_TopY;

		dwWidth = GetWidth();
	}
	else
	{
		pRetObj->m_TopX = target.m_TopX;
		pRetObj->m_TopY = target.m_TopY;

		dwWidth = ((OImageProcess&)target).GetWidth();
	}

	dwHeight = GetHeight() + ((OImageProcess&)target).GetHeight();

	pRetObj->m_ulHorzRes = ((OImageProcess&)target).m_ulHorzRes;
	pRetObj->m_ulVertRes = ((OImageProcess&)target).m_ulVertRes;

	//�S�Ẵv���[��������
	for( auto it=m_planelist.begin(); it!=m_planelist.end(); it++ )
	{
		//�V�̈�̃v���[�����쐬
		OPlaneLeaf* pPlane = (*it)->Instance( dwWidth, dwHeight );

		//�܂��A���g�̉摜���R�s�[����B
		pPlane->CopyDot( pPlane->GetPoint( 0, 0 ),
						(*it)->GetPointObj( 0, 0 ),
						(*it)->Width() * (*it)->Height() );

		//���ɑ���̉摜���R�s�[����B
		OPlaneLeaf* pDestPlane = ((OImageProcess&)target).GetPlane( (*it)->LeafName() );

		if ( pDestPlane != NULL )
		{
			pPlane->CopyDot( pPlane->GetPoint( 0, (*it)->Height() ),
							pDestPlane->GetPointObj( 0, 0 ),
							pDestPlane->Width() * pDestPlane->Height() );
		}

		//���������v���[����ǉ�
		pRetObj->m_planelist.push_back( pPlane );
	}

	return pRetObj;
}


//����������f�̃f�[�^���쐬
OImageProcess* OImageProcess::MakeAreaBoxH( DWORD dwSize )
{
	OImageProcess* pRetObj = Instance();

	pRetObj->m_TopX = m_TopX ;
	pRetObj->m_TopY = m_TopY ;

	pRetObj->m_ulHorzRes = m_ulHorzRes ;
	pRetObj->m_ulVertRes = m_ulVertRes ;

	//�S�Ẵv���[��������
	for( auto it=m_planelist.begin(); it!=m_planelist.end(); it++ )
	{
		//�V�̈�̃v���[�����쐬
		OPlaneLeaf* pPlane = (*it)->Instance( GetWidth(), dwSize );

		//���������v���[����ǉ�
		pRetObj->m_planelist.push_back( pPlane );
	}

	return pRetObj;
}

//����������f�̃f�[�^���쐬
OImageProcess* OImageProcess::MakeAreaBoxV( DWORD dwSize )
{
	OImageProcess* pRetObj = Instance();

	pRetObj->m_TopX = m_TopX ;
	pRetObj->m_TopY = m_TopY ;

	pRetObj->m_ulHorzRes = m_ulHorzRes ;
	pRetObj->m_ulVertRes = m_ulVertRes ;

	//�S�Ẵv���[��������
	for( auto it=m_planelist.begin(); it!=m_planelist.end(); it++ )
	{
		//�V�̈�̃v���[�����쐬
		OPlaneLeaf* pPlane = (*it)->Instance( dwSize, GetHeight() );

		//���������v���[����ǉ�
		pRetObj->m_planelist.push_back( pPlane );
	}

	return pRetObj;
}

