using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WFAdmissionDocuments.Code.TemplateStuff
{


    public static class FontHelper
    {
        public static List<string> GetFontFilePaths()
        {
            //%SystemRoot%\Fonts
            List<string> ret = new List<string>();
            string windir = Environment.GetEnvironmentVariable("WINDIR");
            
            foreach (var file in Directory.EnumerateFiles(Path.Combine(windir, "Fonts"), "*.ttf"))
            {
                ret.Add(Path.Combine(windir, file));
            }

            //%SystemRoot%\Fonts\Classic
            string windir2 = Path.Combine(windir, "Fonts", "Classic");
            if (Directory.Exists(windir2))
            {
                foreach (var file in Directory.EnumerateFiles(windir2, "*.ttf"))
                {
                    ret.Add(Path.Combine(windir, file));
                }
            }

            //%LocalAppData%\Microsoft\Windows\Fonts
            string localappdir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            localappdir = Path.Combine(localappdir, "Microsoft", "Windows", "Fonts");
            if (Directory.Exists(localappdir))
            {
                foreach (var file in Directory.EnumerateFiles(localappdir, "*.ttf"))
                {
                    ret.Add(Path.Combine(windir, file));
                }
            }

            return ret;
        }

        public class FontCollectionInfo
        {
            public string RegularFontPath { get; set; }
            public string BoldFontPath { get; set; }
            public string ItalicFontPath { get; set; }
            public string BoldItalicFontPath { get; set; }
        }

        public static Dictionary<string, string> GetAllFontsAndFileNames()
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            var fontFilePaths = GetFontFilePaths();

            foreach (var fontFilePath in fontFilePaths)
            {
                using (PrivateFontCollection privateFontCollection = new PrivateFontCollection())
                {
                    try
                    {
                        privateFontCollection.AddFontFile(fontFilePath);

                        var family = privateFontCollection.Families[0];

                        var bold = family.IsStyleAvailable(FontStyle.Bold);
                        var Italic = family.IsStyleAvailable(FontStyle.Italic);
                        var Strikeout = family.IsStyleAvailable(FontStyle.Strikeout);
                        var Underline = family.IsStyleAvailable(FontStyle.Underline);

                        if (ret.ContainsKey(privateFontCollection.Families[0].Name))
                        {
                            continue;
                        }

                        //var font = new Font(privateFontCollection.Families[0], 12);

                        ret[privateFontCollection.Families[0].Name] = fontFilePath;
                    }
                    finally
                    {

                    }
                }
            }
            return ret;
        }
    
    }
}

//public class TrueTypeFontParser
//{
//    public string FilePath { get; set; }

//    public TrueTypeFontParser(string filePath)
//    {
//        FilePath = filePath;
//    }

//    public void Parse()
//    {
//        using (var stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
//        using (var reader = new BinaryReader(stream, Encoding.BigEndianUnicode))
//        {
//            // Read the offset table
//            var scalerType = reader.ReadUInt32();
//            var numTables = reader.ReadUInt16();
//            var searchRange = reader.ReadUInt16();
//            var entrySelector = reader.ReadUInt16();
//            var rangeShift = reader.ReadUInt16();

//            // Read each table directory entry
//            for (int i = 0; i < numTables; i++)
//            {
//                var tag = new string(reader.ReadChars(4));
//                var checkSum = reader.ReadUInt32();
//                var offset = reader.ReadUInt32();
//                var length = reader.ReadUInt32();

//                Console.WriteLine($"Table: {tag}, Offset: {offset}, Length: {length}");

//                // Process individual tables based on the tag
//                switch (tag)
//                {
//                    case "head":
//                        ParseHeadTable(reader, offset);
//                        break;
//                    case "name":
//                        ParseNameTable(reader, offset, length);
//                        break;
//                        // Add cases for other tables as needed
//                }
//            }
//        }
//    }

//    private void ParseHeadTable(BinaryReader reader, uint offset)
//    {
//        reader.BaseStream.Seek(offset, SeekOrigin.Begin);

//        var version = reader.ReadUInt32();
//        var fontRevision = reader.ReadUInt32();
//        var checkSumAdjustment = reader.ReadUInt32();
//        var magicNumber = reader.ReadUInt32();
//        var flags = reader.ReadUInt16();
//        var unitsPerEm = reader.ReadUInt16();
//        var created = reader.ReadInt64();
//        var modified = reader.ReadInt64();
//        var xMin = reader.ReadInt16();
//        var yMin = reader.ReadInt16();
//        var xMax = reader.ReadInt16();
//        var yMax = reader.ReadInt16();
//        var macStyle = reader.ReadUInt16();
//        var lowestRecPPEM = reader.ReadUInt16();
//        var fontDirectionHint = reader.ReadInt16();
//        var indexToLocFormat = reader.ReadInt16();
//        var glyphDataFormat = reader.ReadInt16();

//        Console.WriteLine($"Head Table: Version {version >> 16}.{version & 0xFFFF}, Units per EM: {unitsPerEm}");
//    }

//    private void ParseNameTable(BinaryReader reader, uint offset, uint length)
//    {
//        reader.BaseStream.Seek(offset, SeekOrigin.Begin);

//        var format = reader.ReadUInt16();
//        var count = reader.ReadUInt16();
//        var stringOffset = reader.ReadUInt16();

//        for (int i = 0; i < count; i++)
//        {
//            var platformID = reader.ReadUInt16();
//            var encodingID = reader.ReadUInt16();
//            var languageID = reader.ReadUInt16();
//            var nameID = reader.ReadUInt16();
//            var lengthName = reader.ReadUInt16();
//            var offsetName = reader.ReadUInt16();

//            var position = offset + stringOffset + offsetName;
//            reader.BaseStream.Seek(position, SeekOrigin.Begin);
//            var nameBytes = reader.ReadBytes(lengthName);
//            var name = Encoding.BigEndianUnicode.GetString(nameBytes);

//            Console.WriteLine($"Name Table: {nameID} - {name}");
//        }
//    }
//}