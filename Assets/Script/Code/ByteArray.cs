using System;
using System.IO;
using System.Text;

public class ByteArray
{
    MemoryStream m_Stream = new MemoryStream();
    BinaryReader m_Reader = null;
    BinaryWriter m_Writer = null;

    public ByteArray()
    {
        Init();
    }

    public ByteArray(byte[] buffer)
    {
        m_Stream = new MemoryStream(buffer);
        m_Stream.SetLength(buffer.Length);
        m_Stream.Position = 0;
        Init();
    }


    private void Init()
    {
        m_Writer = new BinaryWriter(m_Stream);
        m_Reader = new BinaryReader(m_Stream, Encoding.UTF8);
    }

    /// <summary>
    /// 刷新;
    /// </summary>
    /// <param name="buffer"></param>
    public ByteArray F_ReFresh(byte[] buffer = null)
    {

        m_Stream.Position = 0;
        if (null != buffer)
        {
            m_Stream.Write(buffer, 0, buffer.Length);
            m_Stream.Position = 0;
        }
        m_Stream.Flush();

        if (null == m_Writer)
        {
            m_Writer = new BinaryWriter(m_Stream);
        }

        if (null == m_Reader)
        {
            m_Reader = new BinaryReader(m_Stream, Encoding.UTF8);
        }
        return this;
    }


    public int Length
    {
        get { return (int)m_Stream.Length; }
    }

    public int Postion
    {
        get { return (int)m_Stream.Position; }
        set { m_Stream.Position = value; }
    }

    public byte[] Buffer
    {
        get
        {
            return m_Stream.ToArray();
        }
    }

    internal MemoryStream MemoryStream { get { return m_Stream; } }

    #region 读取内存流

    /// <summary>
    /// 读布尔类型
    /// </summary>
    public bool ReadBoolean()
    {
        return m_Reader.ReadBoolean();
    }

    /// <summary>
    /// 读一个字节
    /// </summary>
    public byte ReadByte()
    {
        return m_Reader.ReadByte();
    }

    /// <summary>
    /// 读一个字节数组
    /// </summary>
    public void ReadBytes(byte[] bytes, uint offset, int length)
    {
        byte[] tmp = m_Reader.ReadBytes(length);
        for (int i = 0; i < tmp.Length; i++)
            bytes[i + offset] = tmp[i];
    }


    /// <summary>
    /// 读一个短整型;
    /// </summary>
    public short ReadShort()
    {
        byte[] tmp = new byte[2];
        m_Writer.BaseStream.Read(tmp, 0, 2);
        return BitConverter.ToInt16(tmp, 0);
    }

    /// <summary>
    /// 读一个字符;
    /// </summary>
    /// <returns></returns>
    public char ReadChar()
    {
        short encoded = ReadShort();
        byte[] bytes = BitConverter.GetBytes(encoded);
        byte[] setyb = inverse(bytes);
        char value = BitConverter.ToChar(setyb, 0);
        return value;
    }

    /// <summary>
    /// 读取一个整型
    /// </summary>
    public int ReadInt()
    {
        byte[] tmp = new byte[4];
        m_Writer.BaseStream.Read(tmp, 0, 4);
        return BitConverter.ToInt32(tmp, 0);
    }

    /// <summary>
    /// 读一个浮点型
    /// </summary>
    public float ReadFloat()
    {
        byte[] tmp = new byte[4];
        m_Writer.BaseStream.Read(tmp, 0, 4);
        return BitConverter.ToSingle(tmp, 0);
    }

    /// <summary>
    /// 读一个长整型
    /// </summary>
    public long ReadLong()
    {
        byte[] tmp = new byte[8];
        m_Writer.BaseStream.Read(tmp, 0, 8);
        return BitConverter.ToInt64(tmp, 0);
    }

    public double ReadDouble()
    {
        long encoded = ReadLong();
        byte[] bytes = BitConverter.GetBytes(encoded);
        byte[] setyb = inverse(bytes);
        double value = BitConverter.ToDouble(setyb, 0);
        return value;
    }
    public string ReadUTF()
    {
        uint len = (uint)ReadShort();

        string str1 = ReadUTFBytes(len);
        return str1;
    }

    public string ReadUTFBytes(uint length)
    {
        if (length == 0)
            return string.Empty;
        UTF8Encoding utf8 = new UTF8Encoding();
        byte[] encodedBytes = m_Reader.ReadBytes((int)length);
        string decodedString = utf8.GetString(encodedBytes, 0, encodedBytes.Length);
        return decodedString;
    }
    #endregion

    #region 写入内存流
    public void WriteBoolean(bool value)
    {
        m_Writer.BaseStream.WriteByte(value ? ((byte)1) : ((byte)0));
    }
    public void WriteByte(byte value)
    {
        m_Writer.BaseStream.WriteByte(value);
    }
    public void WriteBytes(byte[] buffer)
    {
        for (int i = 0; buffer != null && i < buffer.Length; i++)
            m_Writer.BaseStream.WriteByte(buffer[i]);
    }


    public void WriteBytes(byte[] bytes, int offset, int length)
    {
        for (int i = offset; i < offset + length; i++)
            m_Writer.BaseStream.WriteByte(bytes[i]);
    }

    public void WriteShort(int value)
    {
        WriteShort((short)value);
    }

    public void WriteShort(short value)
    {
        byte[] tmp = BitConverter.GetBytes(value);
        WriteBytes(tmp);
    }
    /// <summary>
    /// 写入一个整型
    /// </summary>
    public void WriteInt(int value)
    {
        byte[] tmp = BitConverter.GetBytes(value);
        WriteBytes(tmp);
    }

    /// <summary>
    /// 写入一个浮点型
    /// </summary>
    public void WriteFloat(float value)
    {
        byte[] tmp = BitConverter.GetBytes(value);
        WriteBytes(tmp);
    }

    /// <summary>
    /// 写入一个长整型
    /// </summary>
    public void WriteLong(long value)
    {
        byte[] tmp = BitConverter.GetBytes(value);
        WriteBytes(tmp);
    }

    public void WriteDouble(double value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        byte[] setyb = inverse(bytes);
        long tmp = BitConverter.ToInt64(setyb, 0);
        WriteLong(tmp);
    }

    public void WriteUTF(string value)
    {
        UTF8Encoding utf8Encoding = new UTF8Encoding();
        int byteCount = utf8Encoding.GetByteCount(value);
        byte[] buffer = utf8Encoding.GetBytes(value);
        WriteShort(byteCount);
        if (buffer.Length > 0)
            m_Writer.Write(buffer);
    }

    public void WriteUTFBytes(string value)
    {
        UTF8Encoding utf8Encoding = new UTF8Encoding();
        byte[] buffer = utf8Encoding.GetBytes(value);
        if (buffer.Length > 0)
            m_Writer.Write(buffer);
    }

    public void WriteStringBytes(string value)
    {
        UTF8Encoding utf8Encoding = new UTF8Encoding();
        byte[] buffer = utf8Encoding.GetBytes(value);
        if (buffer.Length > 0)
        {
            m_Writer.Write(buffer.Length);
            m_Writer.Write(buffer);
        }
    }
    #endregion

    #region 帮助方法
    public static byte[] inverse(byte[] source)
    {
        byte[] result = new byte[source.Length];
        for (int i = source.Length - 1; i >= 0; i--)
        {
            result[i] = source[source.Length - i - 1];
        }
        return result;

    }

    public static int shiftSign16(int n)
    {
        return ((n << 1) ^ (n >> 15));
    }

    public static long shiftSign64(long n)
    {
        return ((n << 1) ^ (n >> 63));
    }

    public static short revertSign16(int n)
    {
        return (short)revertSign32(n);
    }

    public static int revertSign32(int n)
    {
        return (int)((uint)n >> 1) ^ -(n & 1);
    }

    public static long revertSign64(long n)
    {
        return (long)((ulong)n >> 1) ^ -(n & 1);
    }
    #endregion

}