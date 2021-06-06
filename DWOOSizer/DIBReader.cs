using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace DWOOSizer
{
	/// <summary>
	/// DIB�t�H�[�}�b�g��ǂݍ��ރN���X
	/// </summary>
	public class DIBReader
	{
		/// <summary>
		/// Bitmap�t�@�C���w�b�_
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
		/// Bitmap�w�b�_
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
		/// �w�b�_�ǉ�
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public static MemoryStream AddFileHead( MemoryStream original )
		{
			MemoryStream deststream = new MemoryStream();
			BITMAPFILEHEADER filehead = new BITMAPFILEHEADER();
			BITMAPINFOHEADER fileinf = new BITMAPINFOHEADER();

			//�\���̃T�C�Y
			int iSize = Marshal.SizeOf(fileinf);

			//BMP���擾
			IntPtr bmpinfoptr = IntPtr.Zero;	//�A���}�l�[�W �o�C�i��
			uint contentoffset = 0;
			try
			{
				byte[] bmpinfobin = new byte[iSize];
				original.Read( bmpinfobin, 0, iSize );

				bmpinfoptr = Marshal.AllocHGlobal(iSize);			//�A���}�l�[�W�������擾
				Marshal.Copy( bmpinfobin, 0, bmpinfoptr, iSize );
				Marshal.PtrToStructure( bmpinfoptr, fileinf );

				if( fileinf.biSizeImage == 0 )
					fileinf.biSizeImage = (UInt32)(((((fileinf.biWidth * fileinf.biBitCount) + 31) & ~31) >> 3) * fileinf.biHeight);

				//�摜�܂ł̃I�t�Z�b�g
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

			//�\���̃T�C�Y
			iSize = Marshal.SizeOf(filehead);

			//�w�b�_�쐬
			filehead.bfType = (byte)'B' + ((byte)'M'<<8);
			filehead.bfSize = (UInt32)((original.Length + iSize)/4);
			filehead.bfOffBits = (UInt32)(iSize + contentoffset);

			IntPtr headptr = IntPtr.Zero;	//�A���}�l�[�W �o�C�i��
			byte[] head_bin = null;			//�}�l�[�W �o�C�i��

			try
			{
				headptr = Marshal.AllocHGlobal(iSize);					//�A���}�l�[�W�������擾
				Marshal.StructureToPtr( filehead, headptr, true );		//�A���}�l�[�W�֍\���̂�W�J
				head_bin = new byte[iSize];
				Marshal.Copy( headptr, head_bin, 0, iSize );			//�}�l�[�W�֓W�J
			}
			finally
			{
				if( headptr != IntPtr.Zero )
					Marshal.FreeHGlobal( headptr );
			}

			if( head_bin != null )
			{
				//Bitmap�t�@�C���w�b�_����������
				deststream.Write( head_bin, 0, iSize );
			}

			//�{�̂���������
			while( true )
			{
				int contents = original.ReadByte();
				if( contents < 0 )
					break;
				deststream.WriteByte( (Byte)contents );
			}

			//�V�[�N���g�b�v�ɂ���B
			deststream.Seek( 0, SeekOrigin.Begin );

			return deststream;
		}

	}
}
