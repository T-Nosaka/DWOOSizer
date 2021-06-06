/*++

	Windows
	�ėp

	�����`���X�摜�������Ǘ�

--*/


#include <windows.h>

#include "OLanczLngMem.h"

const long OLanczLngMem::FIXFLOATSHIFT = 1000000;

OLanczLngMem::~OLanczLngMem( )
{
	for( auto it = m_HorizonWeight.begin(); it!=m_HorizonWeight.end(); it++ )
	{
		delete *it;
	}

	for( auto it = m_VerticalWeight.begin(); it!=m_VerticalWeight.end(); it++ )
	{
		delete *it;
	}
}


bool OLanczLngMem::Resize( OImageProcess& imgmem, DWORD effectWidth, DWORD effectHeight )
{
	//�^�[�Q�b�g��24BitRGB�Ƃ���
	auto pRed = (OPlaneLeafRgb24*)GetPlane( OPlaneLeafRgb24::REDNAME );
	auto pGreen = (OPlaneLeafRgb24*)GetPlane( OPlaneLeafRgb24::GREENNAME );
	auto pBlue = (OPlaneLeafRgb24*)GetPlane( OPlaneLeafRgb24::BLUENAME );

	if ( pRed == NULL ||
		 pGreen == NULL ||
		 pBlue == NULL )
		return false;

	//�摜�T�C�Y�擾
	DWORD dwWidth = GetWidth();
	DWORD dwHeight = GetHeight();

	//���ʗp�v���[��
	auto pLanRed = new OPlaneLeafRgb24( effectWidth, effectHeight, OPlaneLeafRgb24::REDNAME );
	auto pLanGreen = new OPlaneLeafRgb24( effectWidth, effectHeight, OPlaneLeafRgb24::GREENNAME );
	auto pLanBlue = new OPlaneLeafRgb24( effectWidth, effectHeight, OPlaneLeafRgb24::BLUENAME );

	//�����d��
	CreateWeights( m_HorizonWeight, effectWidth, dwWidth );
	//�����d��
	CreateWeights( m_VerticalWeight, effectHeight, dwHeight );

	//�ϊ�
	SizeDown( *pRed, *pLanRed );
	SizeDown( *pGreen, *pLanGreen );
	SizeDown( *pBlue, *pLanBlue );

	//���ʂ𔽉f(���摜�́A�ێ�����)
	RemovePlane( OPlaneLeafRgb24::REDNAME, false );
	RemovePlane( OPlaneLeafRgb24::GREENNAME, false );
	RemovePlane( OPlaneLeafRgb24::BLUENAME, false );
	m_planelist.push_back( pLanRed );
	m_planelist.push_back( pLanGreen );
	m_planelist.push_back( pLanBlue );

	return true;
}


bool OLanczLngMem::CreateWeights( std::vector< Weight<long>* >& vecTarget, DWORD effectLength, DWORD dwSrcLength, DWORD dwDestLength )
{
	//�摜�T�C�Y�擾
	DWORD dwLength = dwSrcLength;

	//�g�k��
	double dMagnification = ((double)effectLength)/((double)dwLength);

	//�^�b�v���擾
	long lTap;
	lTap = (dMagnification < 1.0) ? 6.0/dMagnification:6.0;
	double* pLanczos3Val = new double[lTap];
	long* pLindex = new long[lTap];
	double dLanczos3ValSum;

	int iCnt;
	int dwTapCnt;

	for each (auto vec in vecTarget)
		delete vec;
	vecTarget.clear();

	for( iCnt=0; iCnt<effectLength; iCnt++ )
	{
		//�܂��A�\�[�X�摜�̃|�C���g���Z�o
		long dwFirstPosition = (long)( ((double)(iCnt-3)+0.5) / dMagnification );	//�|�C���g�̒l���Z�o

		dLanczos3ValSum = 0;

		double dDistance;

		//�������d�ݎZ�o
		for( dwTapCnt=0; dwTapCnt<lTap; dwTapCnt++ )
		{
			//�k��
			if (dMagnification < 1.0)
			{
				dDistance =
					((double)iCnt) -
					((double)(dwFirstPosition+dwTapCnt) * dMagnification) ;

				//�[�́A�u������B
				if ( (dwFirstPosition+dwTapCnt) < 0 )
				{
					pLindex[dwTapCnt] = 0;
				}
				else
				if ( (dwFirstPosition+dwTapCnt) >= dwLength )
				{
					pLindex[dwTapCnt] = dwLength-1;
				}
				else
				{
					pLindex[dwTapCnt] = dwFirstPosition+dwTapCnt;
				}
			}
			else
			{
				double dCenter = ((double)iCnt+0.5)/dMagnification;
				double dTapPoint = ((double)dwFirstPosition*dMagnification+dwTapCnt)/dMagnification;

				//�g��
				dDistance = dCenter - dTapPoint;

				//�[�́A�u������B
				if ( dTapPoint < 0.0 )
				{
					pLindex[dwTapCnt] = 0;
				}
				else
				if ( dTapPoint >= dwLength )
				{
					pLindex[dwTapCnt] = dwLength-1;
				}
				else
				{
					pLindex[dwTapCnt] = dTapPoint;
				}
			}

			//�����`���X���֐�
			pLanczos3Val[dwTapCnt] = Lanczos3Weight( dDistance );

			//�w���Z�o�ׂ̈̏W�v
			dLanczos3ValSum += pLanczos3Val[dwTapCnt];
		}

		auto weight = new Weight<long>( pLindex[lTap-1] - pLindex[0]+1, pLindex[0] );

		//0 Div
		dLanczos3ValSum = ( dLanczos3ValSum == 0.0 ) ? 0.000000001:dLanczos3ValSum;

		//�d�݂�茴�悩��̗̍p�����Z�o
		for( dwTapCnt=0; dwTapCnt<lTap; dwTapCnt++ )
		{
			//�w���Z�o
			double dFactor = (pLanczos3Val[dwTapCnt] / dLanczos3ValSum) * FIXFLOATSHIFT;
			weight->Data( pLindex[dwTapCnt] - pLindex[0] ) += dFactor ;
		}

		vecTarget.push_back(weight);
	}

	delete [] pLanczos3Val;
	delete [] pLindex;

	return true;
}

void OLanczLngMem::SizeDown( OPlaneLeafRgb24& SrcLeaf, OPlaneLeafRgb24& DestLeaf )
{
	//�S�s�N�Z���ϊ��ARGB�S�F�T�C�Y�́A���߂��������Ƃ���B�@�C������

	//���Ԍ��ʗp�v���[��
	auto pTempLeaf = new OPlaneLeafRgb24( DestLeaf.Width(), SrcLeaf.Height(), "Temp" );

	int dwHeightCnt;
	int dwWidthCnt;
	long dLanz = 0.0;
	int iTap ;	//�d�݃J�E���^

	int dwHeightUpper;
	int dwWidthUpper;
	int iTapUpper;

	//�������ԕϊ����ʊi�[�����q
	auto itDestFirst = pTempLeaf->begin();

	dwHeightUpper = pTempLeaf->Height();
	dwWidthUpper = pTempLeaf->Width();

	auto itSrc = SrcLeaf.begin();

	Weight<long>* itWeight;
	int iSrcWidth = SrcLeaf.Width();

	//�����ϊ�
	for( dwHeightCnt=0; dwHeightCnt<dwHeightUpper; dwHeightCnt++ )
	{
		for( dwWidthCnt=0; dwWidthCnt<dwWidthUpper; dwWidthCnt++ )
		{
			itWeight = m_HorizonWeight[dwWidthCnt];

			OPlaneLeafRgb24::iterator itSrcFirst = itSrc + itWeight->m_dwSrcPosition + iSrcWidth * dwHeightCnt ;

			//�d�݂̐ώZ
			dLanz = 0;

			iTapUpper = itWeight->m_lCnt;

			//tap���̏d�݂ƃ\�[�X�摜��ώZ���A���ʃs�N�Z�����擾
			for( iTap=0; iTap< iTapUpper; iTap++ )
			{
				auto srcdata = (long)*itSrcFirst;
				auto target = itWeight->m_pdData[iTap];
				dLanz += (srcdata* target) ;
				itSrcFirst++;
			}

			dLanz /= FIXFLOATSHIFT;

			//�l����֏C��
			*itDestFirst = ( dLanz < DestLeaf.MinValue() ) ? DestLeaf.MinValue():( dLanz >= DestLeaf.MaxValue() ) ? DestLeaf.MaxValue():dLanz;

			itDestFirst++;
		}
	}

	dwHeightUpper = DestLeaf.Height();
	dwWidthUpper = DestLeaf.Width();

	auto itTmp = pTempLeaf->begin();
	auto itDestBegin = DestLeaf.begin();

	int iTempWidth = pTempLeaf->Width();

	//�����ϊ�
	for( dwWidthCnt=0; dwWidthCnt<dwWidthUpper; dwWidthCnt++ )
	{
		for( dwHeightCnt=0; dwHeightCnt<dwHeightUpper; dwHeightCnt++ )
		{
			itWeight = m_VerticalWeight[dwHeightCnt];

			OPlaneLeafRgb24::iterator itSrcFirst = itTmp + dwWidthCnt + iTempWidth * itWeight->m_dwSrcPosition  ;

			//�d�݂̐ώZ
			dLanz = 0;

			iTapUpper = itWeight->m_lCnt;

			//tap���̏d�݂ƃ\�[�X�摜��ώZ���A���ʃs�N�Z�����擾
			for( iTap=0; iTap< iTapUpper; iTap++ )
			{
				auto srcdata = (long)*itSrcFirst;
				auto target = itWeight->m_pdData[iTap];
				dLanz += (srcdata * target );
				itSrcFirst += iTempWidth;
			}

			dLanz /= FIXFLOATSHIFT;

			//�l����֏C��
			itDestFirst = itDestBegin + dwWidthCnt + DestLeaf.Width() * dwHeightCnt ;
			*itDestFirst = ( dLanz < DestLeaf.MinValue() ) ? DestLeaf.MinValue():( dLanz >= DestLeaf.MaxValue() ) ? DestLeaf.MaxValue():dLanz;
		}
	}

	delete pTempLeaf;
}


