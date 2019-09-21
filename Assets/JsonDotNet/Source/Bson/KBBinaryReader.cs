using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.IO;
using System.Text;

namespace Newtonsoft.Json.Bson
{
	[ComVisible (true)]
	public class KBBinaryReader : IDisposable
	{
		BinaryReader _reader;

		#region Fields
		// Fields
		private char[] charBuffer;
		private Decoder decoder;
		private byte[] m_buffer;
		private bool m_disposed;
		private Encoding m_encoding;
		private Stream m_stream;
		private const int MaxBufferSize = 0x80;
		#endregion

		#region PublicFunc

		//
		// Properties
		//
		public virtual Stream BaseStream
		{
			get
			{
				return this.m_stream;
			}
		}
		
		//
		// Constructors
		//
		public KBBinaryReader (Stream input, Encoding encoding)
		{
			if (input == null || encoding == null)
			{
				throw new ArgumentNullException ("Input or Encoding is a null reference.");
			}
			if (!input.CanRead)
			{
				throw new ArgumentException ("The stream doesn't support reading.");
			}
			this.m_stream = input;
			this.m_encoding = encoding;
			this.decoder = encoding.GetDecoder ();
			this.m_buffer = new byte[32];

			_reader = new BinaryReader (input, encoding);
		}
		
		public KBBinaryReader (Stream input) : this (input, Encoding.UTF8)
		{
		}
		
		//
		// Methods
		//
		public virtual void Close ()
		{
			this.Dispose (true);
			this.m_disposed = true;
			_reader.Close ();
		}
		
		protected virtual void Dispose (bool disposing)
		{
			if (disposing && this.m_stream != null)
			{
				this.m_stream.Close ();
			}
			this.m_disposed = true;
			this.m_buffer = null;
			this.m_encoding = null;
			this.m_stream = null;
			this.charBuffer = null;
		}
		
		protected virtual void FillBuffer (int numBytes)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException ("BinaryReader", "Cannot read from a closed BinaryReader.");
			}
			if (this.m_stream == null)
			{
				throw new IOException ("Stream is invalid");
			}
			this.CheckBuffer (numBytes);
			int num;
			for (int i = 0; i < numBytes; i += num)
			{
				num = this.m_stream.Read (this.m_buffer, i, numBytes - i);
				if (num == 0)
				{
					throw new EndOfStreamException ();
				}
			}
		}
		
		public virtual int PeekChar ()
		{
			if (this.m_stream == null)
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException ("BinaryReader", "Cannot read from a closed BinaryReader.");
				}
				throw new IOException ("Stream is invalid");
			}
			else
			{
				if (!this.m_stream.CanSeek)
				{
					return -1;
				}
				char[] array = new char[1];
				int num2;
				int num = this.ReadCharBytes (array, 0, 1, out num2);
				this.m_stream.Position -= (long)num2;
				if (num == 0)
				{
					return -1;
				}
				return (int)array [0];
			}
		}
		
		public virtual int Read (char[] buffer, int index, int count)
		{
			if (this.m_stream == null)
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException ("BinaryReader", "Cannot read from a closed BinaryReader.");
				}
				throw new IOException ("Stream is invalid");
			}
			else
			{
				if (buffer == null)
				{
					throw new ArgumentNullException ("buffer is null");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException ("index is less than 0");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException ("count is less than 0");
				}
				if (buffer.Length - index < count)
				{
					throw new ArgumentException ("buffer is too small");
				}
				int num;
				return this.ReadCharBytes (buffer, index, count, out num);
			}
		}
		
		public virtual int Read (byte[] buffer, int index, int count)
		{
			if (this.m_stream == null)
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException ("BinaryReader", "Cannot read from a closed BinaryReader.");
				}
				throw new IOException ("Stream is invalid");
			}
			else
			{
				if (buffer == null)
				{
					throw new ArgumentNullException ("buffer is null");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException ("index is less than 0");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException ("count is less than 0");
				}
				if (buffer.Length - index < count)
				{
					throw new ArgumentException ("buffer is too small");
				}
				return this.m_stream.Read (buffer, index, count);
			}
		}
		
		public virtual int Read ()
		{
			if (this.charBuffer == null)
			{
				this.charBuffer = new char[128];
			}
			if (this.Read (this.charBuffer, 0, 1) == 0)
			{
				return -1;
			}
			return (int)this.charBuffer [0];
		}
		
		protected int Read7BitEncodedInt ()
		{
			int num = 0;
			int num2 = 0;
			int i;
			for (i = 0; i < 5; i++)
			{
				byte b = this.ReadByte ();
				int tmp = (int)(b & 127);
				num |= tmp << num2;
				num2 += 7;
				if ((b & 128) == 0)
				{
					break;
				}
			}
			if (i < 5)
			{
				return num;
			}
			throw new FormatException ("Too many bytes in what should have been a 7 bit encoded Int32.");
		}
		
		public virtual bool ReadBoolean ()
		{
			return this.ReadByte () != 0;
		}
		
		public virtual byte ReadByte ()
		{
			if (this.m_stream == null)
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException ("BinaryReader", "Cannot read from a closed BinaryReader.");
				}
				throw new IOException ("Stream is invalid");
			}
			else
			{
				int num = this.m_stream.ReadByte ();
				if (num != -1)
				{
					return (byte)num;
				}
				throw new EndOfStreamException ();
			}
		}
		
		public virtual byte[] ReadBytes (int count)
		{
			if (this.m_stream == null)
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException ("BinaryReader", "Cannot read from a closed BinaryReader.");
				}
				throw new IOException ("Stream is invalid");
			}
			else
			{
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException ("count is less than 0");
				}
				byte[] array = new byte[count];
				int i;
				int num;
				for (i = 0; i < count; i += num)
				{
					num = this.m_stream.Read (array, i, count - i);
					if (num == 0)
					{
						break;
					}
				}
				if (i != count)
				{
					byte[] array2 = new byte[i];
					Buffer.BlockCopy (array, 0, array2, 0, i);
					return array2;
				}
				return array;
			}
		}
		
		public virtual char ReadChar ()
		{
			int num = this.Read ();
			if (num == -1)
			{
				throw new EndOfStreamException ();
			}
			return (char)num;
		}
		
		public virtual char[] ReadChars (int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException ("count is less than 0");
			}
			if (count == 0)
			{
				return new char[0];
			}
			char[] array = new char[count];
			int num = this.Read (array, 0, count);
			if (num == 0)
			{
				throw new EndOfStreamException ();
			}
			if (num != array.Length)
			{
				char[] array2 = new char[num];
				Array.Copy (array, 0, array2, 0, num);
				return array2;
			}
			return array;
		}
		
		public virtual decimal ReadDecimal ()
		{
			return _reader.ReadDecimal ();
		}
		
		public virtual double ReadDouble ()
		{
			return _reader.ReadDouble ();
		}
		
		public virtual short ReadInt16 ()
		{
			this.FillBuffer (2);
			int val0 = (int)this.m_buffer [0];
			int val1 = (int)this.m_buffer [1];
			return (short)(val0 | val1 << 8);
		}
		
		public virtual int ReadInt32 ()
		{
			this.FillBuffer (4);
			int val0 = (int)this.m_buffer [0];
			int val1 = (int)this.m_buffer [1];
			int val2 = (int)this.m_buffer [2];
			int val3 = (int)this.m_buffer [3];
			return val0 | val1 << 8 | val2 << 16 | val3 << 24;
		}
		
		public virtual long ReadInt64 ()
		{
			this.FillBuffer (8);

			int val0 = (int)this.m_buffer [0];
			int val1 = (int)this.m_buffer [1];
			int val2 = (int)this.m_buffer [2];
			int val3 = (int)this.m_buffer [3];
			int val4 = (int)this.m_buffer [4];
			int val5 = (int)this.m_buffer [5];
			int val6 = (int)this.m_buffer [6];
			int val7 = (int)this.m_buffer [7];

			uint num = (uint)(val0 | val1 << 8 | val2 << 16 | val3 << 24);
			uint num2 = (uint)(val4 | val5 << 8 | val6 << 16 | val7 << 24);
			ulong tmp = (ulong)num2;
			return (long)(tmp << 32 | (ulong)num);
		}
		
		[CLSCompliant (false)]
		public virtual sbyte ReadSByte ()
		{
			return (sbyte)this.ReadByte ();
		}
		
		public virtual float ReadSingle ()
		{
			return _reader.ReadSingle ();
		}
		
		public virtual string ReadString ()
		{
			int num = this.Read7BitEncodedInt ();
			if (num < 0)
			{
				throw new IOException ("Invalid binary file (string len < 0)");
			}
			if (num == 0)
			{
				return string.Empty;
			}
			if (this.charBuffer == null)
			{
				this.charBuffer = new char[128];
			}
			StringBuilder stringBuilder = null;
			int chars;
			while (true)
			{
				int num2 = (num <= 128) ? num : 128;
				this.FillBuffer (num2);
				chars = this.decoder.GetChars (this.m_buffer, 0, num2, this.charBuffer, 0);
				if (stringBuilder == null && num2 == num)
				{
					break;
				}
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder (num);
				}
				stringBuilder.Append (this.charBuffer, 0, chars);
				num -= num2;
				if (num <= 0)
				{
					goto Block_8;
				}
			}
			return new string (this.charBuffer, 0, chars);
		Block_8:
				return stringBuilder.ToString ();
		}
		
		[CLSCompliant (false)]
		public virtual ushort ReadUInt16 ()
		{
			this.FillBuffer (2);

			int val0 = (int)this.m_buffer [0];
			int val1 = (int)this.m_buffer [1];

			return (ushort)(val0 | val1 << 8);
		}
		
		[CLSCompliant (false)]
		public virtual uint ReadUInt32 ()
		{
			this.FillBuffer (4);

			int val0 = (int)this.m_buffer [0];
			int val1 = (int)this.m_buffer [1];
			int val2 = (int)this.m_buffer [2];
			int val3 = (int)this.m_buffer [3];

			return (uint)(val0 | val1 << 8 | val2 << 16 | val3 << 24);
		}
		
		[CLSCompliant (false)]
		public virtual ulong ReadUInt64 ()
		{
			this.FillBuffer (8);

			int val0 = (int)this.m_buffer [0];
			int val1 = (int)this.m_buffer [1];
			int val2 = (int)this.m_buffer [2];
			int val3 = (int)this.m_buffer [3];
			int val4 = (int)this.m_buffer [4];
			int val5 = (int)this.m_buffer [5];
			int val6 = (int)this.m_buffer [6];
			int val7 = (int)this.m_buffer [7];

			uint num = (uint)(val0 | val1 << 8 | val2 << 16 | val3 << 24);
			uint num2 = (uint)(val4 | val5 << 8 | val6 << 16 | val7 << 24);
			ulong tmp = (ulong)num2;
			return tmp << 32 | (ulong)num;
		}

		#endregion

		#region PrivateFunc

		private void CheckBuffer(int length)
		{
			if (this.m_buffer.Length <= length)
			{
				byte[] dest = new byte[length];
				Buffer.BlockCopy(this.m_buffer, 0, dest, 0, this.m_buffer.Length);
				this.m_buffer = dest;
			}
		}
		
		private int ReadCharBytes(char[] buffer, int index, int count, out int bytes_read)
		{
			int num = 0;
			bytes_read = 0;
			while (num < count)
			{
				int byteCount = 0;
				do
				{
					this.CheckBuffer(byteCount + 1);
					int num3 = this.m_stream.ReadByte();
					if (num3 == -1)
					{
						return num;
					}
					this.m_buffer[byteCount++] = (byte) num3;
					bytes_read++;
				}
				while (this.m_encoding.GetChars(this.m_buffer, 0, byteCount, buffer, index + num) <= 0);
				num++;
			}
			return num;
		}

		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		#endregion

	}
}
