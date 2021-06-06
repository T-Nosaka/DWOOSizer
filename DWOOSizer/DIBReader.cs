using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace DWOOSizer
{
	/// <summary>
	/// DIBフォーマットを読み込むクラス
	/// </summary>
	public class DIBReader
	{
		/// <summary>
		/// Bitmapファイルヘッダ
		/// </summary>
		[StructLayout(LayoutKind.Sequential,Pack=2)]
			public struct BITMAPFILEHEADER
		{
			public UInt16 bfType;
			public UInt32 bfSize;
			public UInt16 bfReserved1;
			public UInt16 bfReserved2;
			public UInt32 bfOffBits;
		}

		/// <summary>
		/// Bitmapヘッダ
		/// </summary>
		[StructLayout(LayoutKind.Sequential,Pack=2)]
			public class BITMAPINFOHEADER
		{
			public UInt32 biSize;
			public Int32 biWidth;
			public Int32 biHeight;
			public UInt16 biPlanes;
			public UInt16 biBitCount;
			public UInt32 biCompression;
			public UInt32 biSizeImage;
			public Int32 biXPelsPerMeter;
			public Int32 biYPelsPerMeter;
			public UInt32 biClrUsed;
			public UInt32 biClrImportant;
		}

		/// <summary>
		/// ヘッダ追加
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public static MemoryStream AddFileHead( MemoryStream original )
		{
			MemoryStream deststream = new MemoryStream();
			BITMAPFILEHEADER filehead = new BITMAPFILEHEADER();
			BITMAPINFOHEADER fileinf = new BITMAPINFOHEADER();

			//構造体サイズ
			int iSize = Marshal.SizeOf(fileinf);

			//BMP情報取得
			IntPtr bmpinfoptr = IntPtr.Zero;	//アンマネージ バイナリ
			uint contentoffset = 0;
			try
			{
				byte[] bmpinfobin = new byte[iSize];
				original.Read( bmpinfobin, 0, iSize );

				bmpinfoptr = Marshal.AllocHGlobal(iSize);			//アンマネージメモリ取得
				Marshal.Copy( bmpinfobin, 0, bmpinfoptr, iSize );
				Marshal.PtrToStructure( bmpinfoptr, fileinf );

				if( fileinf.biSizeImage == 0 )
					fileinf.biSizeImage = (UInt32)(((((fileinf.biWidth * fileinf.biBitCount) + 31) & ~31) >> 3) * fileinf.biHeight);

				//画像までのオフセット
				contentoffset = fileinf.biClrUsed;
				if( (contentoffset == 0) && (fileinf.biBitCount <= 8) )
					contentoffset = (uint)(1 << fileinf.biBitCount);
				contentoffset = (contentoffset * 4) + fileinf.biSize;
			}
			finally
			{
				if( bmpinfoptr != IntPtr.Zero )
					Marshal.FreeHGlobal( bmpinfoptr );
			}

			original.Seek( 0, SeekOrigin.Begin );

			//構造体サイズ
			iSize = Marshal.SizeOf(filehead);

			//ヘッダ作成
			filehead.bfType = (byte)'B' + ((byte)'M'<<8);
			filehead.bfSize = (UInt32)((original.Length + iSize)/4);
			filehead.bfOffBits = (UInt32)(iSize + contentoffset);

			IntPtr headptr = IntPtr.Zero;	//アンマネージ バイナリ
			byte[] head_bin = null;			//マネージ バイナリ

			try
			{
				headptr = Marshal.AllocHGlobal(iSize);					//アンマネージメモリ取得
				Marshal.StructureToPtr( filehead, headptr, true );		//アンマネージへ構造体を展開
				head_bin = new byte[iSize];
				Marshal.Copy( headptr, head_bin, 0, iSize );			//マネージへ展開
			}
			finally
			{
				if( headptr != IntPtr.Zero )
					Marshal.FreeHGlobal( headptr );
			}

			if( head_bin != null )
			{
				//Bitmapファイルヘッダを書き込み
				deststream.Write( head_bin, 0, iSize );
			}

			//本体を書き込み
			while( true )
			{
				int contents = original.ReadByte();
				if( contents < 0 )
					break;
				deststream.WriteByte( (Byte)contents );
			}

			//シークをトップにする。
			deststream.Seek( 0, SeekOrigin.Begin );

			return deststream;
		}

	}
}
