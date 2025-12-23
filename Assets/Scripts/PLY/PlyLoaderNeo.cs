using System.IO;
using System.Text;
using UnityEngine;

//Binary PLY loader (no Unity API yet)
public static class PlyLoaderNeo
{
    public static PlyVertex[] Load(string path)
    {
        using var fs = File.OpenRead(path);
        using var br = new BinaryReader(fs);

        int vertexCount = ReadHeader(br);

        var vertices = new PlyVertex[vertexCount];
        for (int i = 0; i < vertexCount; i++)
        {
            vertices[i] = ReadVertex(br);
        }

        return vertices;
    }

    static int ReadHeader(BinaryReader br)
    {
        int vertexCount = 0;

        while (true)
        {
            string line = ReadLine(br);
            if (line.StartsWith("element vertex"))
                vertexCount = int.Parse(line.Split(' ')[2]);

            if (line == "end_header")
                break;
        }

        return vertexCount;
    }

    static PlyVertex ReadVertex(BinaryReader br)
    {
        return new PlyVertex
        {
            x = br.ReadDouble(),
            y = br.ReadDouble(),
            z = br.ReadDouble(),
            r = br.ReadUInt16(),
            g = br.ReadUInt16(),
            b = br.ReadUInt16()
        };
    }

    static string ReadLine(BinaryReader br)
    {
        var sb = new StringBuilder();
        char c;
        while ((c = (char)br.ReadByte()) != '\n')
        {
            if (c != '\r') sb.Append(c);
        }
        return sb.ToString();
    }
}