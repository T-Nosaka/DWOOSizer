/*++

	色相変換管理

--*/

#include <windows.h>

#include <math.h>

#include "OColorHue.h"

double OColorHue::PAI = (atan((double)1.0) * 4.0);
double OColorHue::R_OFFSSET = 0.0 ;
double OColorHue::G_OFFSSET = -120.0 ;
double OColorHue::B_OFFSSET = +120.0 ;

double OColorHue::HLSMAX = 1000.0; // H,L, S over 0-HLSMAX
double OColorHue::RGBMAX = 255.0;  // R,G, B over 0-RGBMAX
double OColorHue::UNDEFINED = (HLSMAX*2.0/3.0);

double OColorHue::SetBlackHueRANGE = 360.0;
double OColorHue::SetBlackLevelRANGE = 100.0;
double OColorHue::SetSaturationRANGE = 100.0;
double OColorHue::SetLightnessRANGE = 100.0;

OColorHue::OColorHue()
{
	Init();
}


void OColorHue::Init()
{
	m_SetRed = 0;		
	m_SetYellow = 0;
	m_SetGreen = 0;
	m_SetCyan = 0;
	m_SetBlue = 0;
	m_SetMazenta = 0;

	m_SetBlackEnable = false;
	m_SetBlackHue = 0;	
	m_SetBlackLevel = 0;

	m_SetSaturation = 0;
	m_SetLightness = 0;

	m_inR = 0;
	m_inG = 0;
	m_inB = 0;

	//色相テーブル初期化
	for( long lCnt=0; lCnt<360; lCnt++ )
		m_HueWheel[lCnt] = lCnt;

	m_Lightness = 0;
	m_Saturation = 0;
	m_Hue = 0;

	m_outR = 0;
	m_outG = 0;
	m_outB = 0;

}


bool OColorHue::HueWheel()
{
	long sCnt;

	long sVal ;

	//各角度により、マッピング角度を設定する。
	for( sCnt=0; sCnt<360; sCnt++ )
	{
		//赤色のゾーン
		if ( (sCnt>=330 && sCnt <=359 ) || (sCnt>=0 && sCnt <=29) )
		{
			sVal = ( sCnt + m_SetRed >= 0 ) ? sCnt + m_SetRed: 360 + sCnt + m_SetRed;
			sVal = ( sVal >= 360 ) ? sVal - 360:sVal;
		}
		else
		//黄色のゾーン
		if ( sCnt>=30 && sCnt <=89 )
		{
			sVal = sCnt + m_SetYellow ;
		}
		else
		//緑色のゾーン
		if ( sCnt>=90 && sCnt <=149 )
		{
			sVal = sCnt + m_SetGreen ;
		}
		else
		//シアン色のゾーン
		if ( sCnt>=150 && sCnt <=209 )
		{
			sVal = sCnt + m_SetCyan ;
		}
		else
		//青色のゾーン
		if ( sCnt>=210 && sCnt <=269 )
		{
			sVal = sCnt + m_SetBlue ; 
		}
		else
		//マゼンダ色のゾーン
		if ( sCnt>=270 && sCnt <=329 )
		{
			sVal = ( sCnt + m_SetMazenta <= 359 ) ? sCnt + m_SetMazenta: sCnt + m_SetMazenta - 360;
		}

		m_HueWheel[sCnt] = sVal;
	}

	return true;
}

void OColorHue::RGBtoHLS()
{
	LPBYTE pMax = MaximumValue();
	LPBYTE pMin = MinimumValue();

	bool bSameValue = false;

	//値が同一ということは、何でもいっしょ
	if ( pMax == NULL || pMin == NULL )
	{
		pMax = &m_inR;
		pMin = &m_inR;

		bSameValue = true;
	}

	//計算用にキャスト
	double dMax = *pMax;
	double dMin = *pMin;

	double dinR = m_inR;
	double dinG = m_inG;
	double dinB = m_inB;

	// 輝度の計算
	m_Lightness = ( ((dMax+dMin)*HLSMAX) + RGBMAX )/(2*RGBMAX);

	if (dMax == dMin)
	{								// r=g=b --> achromatic case
		m_Saturation = 0;			// saturation
		m_Hue = UNDEFINED;			// hue
	}
	else
	{	
		// 色彩がある場合
		if (m_Lightness <= (HLSMAX/2.0))
		{
			m_Saturation = ( ((dMax-dMin)*HLSMAX) + ((dMax+dMin)/2.0) ) / (dMax+dMin);
		}
		else
		{
			m_Saturation = ( ((dMax-dMin)*HLSMAX) + ((2.0*RGBMAX-dMax-dMin)/2.0) ) / (2.0*RGBMAX-dMax-dMin);
		}

		double  Rdelta,Gdelta,Bdelta;

		// 色調
		Rdelta = ( ((dMax-dinR)*(HLSMAX/6.0)) + ((dMax-dMin)/2.0) ) / (dMax-dMin);
		Gdelta = ( ((dMax-dinG)*(HLSMAX/6.0)) + ((dMax-dMin)/2.0) ) / (dMax-dMin);
		Bdelta = ( ((dMax-dinB)*(HLSMAX/6.0)) + ((dMax-dMin)/2.0) ) / (dMax-dMin);

		if (dinR == dMax)
		{
			m_Hue = Bdelta - Gdelta;
		}
		else
		{
			if (dinG == dMax)
			{
				m_Hue = (HLSMAX/3.0) + Rdelta - Bdelta;
			}
			else
			{
				m_Hue = ((2.0*HLSMAX)/3.0) + Gdelta - Rdelta;
			}
		}

		//色調値域の調整
		if (m_Hue < 0)
			m_Hue += HLSMAX;
		if (m_Hue > HLSMAX)
			m_Hue -= HLSMAX;
	}
}


bool OColorHue::HLStoRGB()
{
	double  Magic1,Magic2;
	double dr,dg,db;

	//色調値域の調整
	if (m_Hue < 0)
		m_Hue += HLSMAX;
	if (m_Hue > HLSMAX)
		m_Hue -= HLSMAX;

	//色相変換
	m_Hue = m_HueWheel[ (long)(m_Hue / HLSMAX * 360.0) ] / 360.0 * HLSMAX ;

	//輝度の補間
	m_Lightness += ((double)(m_SetLightness/SetLightnessRANGE ) * HLSMAX );
	m_Lightness = ( m_Lightness < 0.0 ) ? 0:( m_Lightness > HLSMAX ) ? HLSMAX:m_Lightness;

	//無彩色の場合
	if ( m_Saturation == 0.0 )
	{
		if ( m_SetBlackEnable == true )
		{
			//このスコープは、無彩色に対する補間処理
			//黒レベルの補間
			m_Saturation += ( ( (double)m_SetBlackLevel ) / SetBlackLevelRANGE )*HLSMAX + ((double)m_SetSaturation)/SetSaturationRANGE*HLSMAX ;
			m_Saturation = ( m_Saturation < 0.0 ) ? 0:( m_Saturation > HLSMAX ) ? HLSMAX:m_Saturation;
		}

		//補間後、無彩色の場合
		if ( m_Saturation == 0.0 )
		{
			//無彩色の値
			double dNoStatuationValue = (m_Lightness*RGBMAX)/HLSMAX;

			m_outR=m_outG=m_outB=(BYTE)(( dNoStatuationValue<=0.0 ) ? 0:( dNoStatuationValue>=RGBMAX ) ? RGBMAX: dNoStatuationValue);

			return true;
		}
		else
		{
			//黒色相の補間
			m_Hue = ( ( (double)m_SetBlackHue )/SetBlackHueRANGE ) * HLSMAX ;
		}
	}
	else
	{
		//このスコープは、有彩色に対する補間処理
		//彩度の補間
		m_Saturation += ( ( (double)m_SetSaturation ) / SetSaturationRANGE )*HLSMAX;
		m_Saturation = ( m_Saturation < 0.0 ) ? 0:( m_Saturation > HLSMAX ) ? HLSMAX:m_Saturation;
	}

	// HLS -> RGB変換
	if (m_Lightness <= (HLSMAX/2.0))
		Magic2 = (m_Lightness*(HLSMAX + m_Saturation) + (HLSMAX/2.0))/HLSMAX;
	else
		Magic2 = m_Lightness + m_Saturation - ((m_Lightness*m_Saturation) + (HLSMAX/2.0))/HLSMAX;

	Magic1 = 2.0*m_Lightness-Magic2;

	// get RGB, change units from HLSMAX to RGBMAX
	dr = (Function(Magic1,Magic2,m_Hue+(HLSMAX/3.0))*RGBMAX + (HLSMAX/2.0))/HLSMAX ;
	dg = (Function(Magic1,Magic2,m_Hue)*RGBMAX + (HLSMAX/2.0)) / HLSMAX ;
	db = (Function(Magic1,Magic2,m_Hue-(HLSMAX/3.0))*RGBMAX + (HLSMAX/2.0))/HLSMAX ;

	m_outR = (BYTE)(( dr<=0.0 ) ? 0:( dr>=RGBMAX ) ? RGBMAX: dr);
	m_outG = (BYTE)(( dg<=0.0 ) ? 0:( dg>=RGBMAX ) ? RGBMAX: dg);
	m_outB = (BYTE)(( db<=0.0 ) ? 0:( db>=RGBMAX ) ? RGBMAX: db);

	return true;
}


double OColorHue::Function( double n1, double n2,double hue )
{
	if (hue < 0)
		hue += HLSMAX;

	if (hue > HLSMAX)
		hue -= HLSMAX;

	if (hue < (HLSMAX/6.0))
		return ( n1 + (((n2-n1)*hue+(HLSMAX/12.0))/(HLSMAX/6.0)) );
	if (hue < (HLSMAX/2.0))
		return ( n2 );
	if (hue < ((HLSMAX*2.0)/3.0))
		return ( n1 +    (((n2-n1)*(((HLSMAX*2.0)/3.0)-hue)+(HLSMAX/12.0))/(HLSMAX/6.0)));
	else
		return ( n1 );
}


bool OColorHue::ColorWheel( double dSeta )
{
	if (dSeta < 0)
		dSeta += 360.0;
	if (dSeta > 360.0 )
		dSeta -= 360.0;

	double cmin = 0.0;
	double cmax = RGBMAX;
	double dr,dg,db;

	dr = Function( cmin, cmax, (double)(dSeta + 120.0)/360.0*HLSMAX );
	dg = Function( cmin, cmax, (double)(dSeta)/360.0*HLSMAX );
	db = Function( cmin, cmax, (double)(dSeta - 120.0)/360.0*HLSMAX );

	m_outR = (BYTE)(( dr<=0.0 ) ? 0:( dr>=RGBMAX ) ? RGBMAX: dr);
	m_outG = (BYTE)(( dg<=0.0 ) ? 0:( dg>=RGBMAX ) ? RGBMAX: dg);
	m_outB = (BYTE)(( db<=0.0 ) ? 0:( db>=RGBMAX ) ? RGBMAX: db);

	return true;
}
