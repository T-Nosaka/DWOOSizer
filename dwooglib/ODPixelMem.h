/*++

	�P�x���

--*/

#pragma once

class ODPixelMem
{
protected:

	//���[�J�����W
	double m_x;
	double m_y;

	long	m_lx;
	long	m_ly;

public:
	ODPixelMem( double x, double y )
	{
		m_x = x;
		m_y = y;

		m_lx = (long)x;
		m_ly = (long)y;
	}

	ODPixelMem( const ODPixelMem& instance )
	{
		*this = instance;
	}

	void operator= ( const ODPixelMem& instance )
	{
		m_x = instance.m_x;
		m_y = instance.m_y;

		m_lx = instance.m_lx;
		m_ly = instance.m_ly;
	}

	//�\�[�X�摜�̃|�C���g�擾
	LONG GetSrcPointX( double x ) { return (LONG)(x + m_x); }
	LONG GetSrcPointY( double y ) { return (LONG)(y + m_y); }

	//�\�[�X�摜�̃|�C���g�擾
	LONG GetSrcPointX( LONG x ) { return x + m_lx; }
	LONG GetSrcPointY( LONG y ) { return y + m_ly; }

} ;


