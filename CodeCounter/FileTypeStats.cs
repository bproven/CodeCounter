using System;
using System.IO;

namespace CodeCounter
{
	/// <summary>
	/// Summary description for FileTypeStats.
	/// </summary>
	public class FileTypeStats
	{

		string desc = "";
		string ext = "";

		int num_files = 0;
		int num_lines = 0;

        public FileTypeStats( string _ext )
        {
            ext = _ext;
        }

        public FileTypeStats( string _desc, string _ext )
		{
			desc = _desc;
			ext = _ext;
		}

		public int NumFiles
		{
			get
			{
				return num_files;
			}
			set
			{
				num_files = value;
			}
		}

		public int NumLines
		{
			get
			{
				return num_lines;
			}
			set
			{
				num_lines = 0;
			}
		}

		public int IncrementNumFiles()
		{
			return ++num_files;
		}

		public int IncrementNumLines()
		{
			return ++num_lines;
		}

		public void WriteStats()
		{
			WriteStats( Console.Out );
		}

		public void WriteStats( TextWriter tw )
		{
			tw.WriteLine( ext );
			tw.WriteLine( " Number of files: " + num_files.ToString() );
			tw.WriteLine( " Number of lines: " + num_lines.ToString() );
		}

	}

}
