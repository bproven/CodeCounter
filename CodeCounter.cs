using System;
using System.Collections.Generic;
using System.IO;

namespace CodeCounter
{

    /// <summary>
    /// Summary description for CodeCounter.
    /// </summary>
    public class CodeCounter
    {

        static Dictionary<string, FileTypeStats> statDictionary = new Dictionary<string, FileTypeStats>();
        static Dictionary<string, string> ignoreDictionary = new Dictionary<string, string>();
        static Dictionary<string, string> includeDictionary = new Dictionary<string, string>();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main( string[] args )
        {
            try
            {

                ignoreDictionary.Add( ".ipa", string.Empty );
                ignoreDictionary.Add( ".jar", string.Empty );
                ignoreDictionary.Add( ".dll", string.Empty );
                ignoreDictionary.Add( ".exe", string.Empty );

                ignoreDictionary.Add( ".pdf", string.Empty );
                ignoreDictionary.Add( ".txt", string.Empty );
                ignoreDictionary.Add( ".ppt", string.Empty );
                ignoreDictionary.Add( ".eps", string.Empty );

                ignoreDictionary.Add( ".iso", string.Empty );

                ignoreDictionary.Add( ".tar", string.Empty );
                ignoreDictionary.Add( ".tgz", string.Empty );
                ignoreDictionary.Add( ".zip", string.Empty );

                ignoreDictionary.Add( ".html", string.Empty );
                ignoreDictionary.Add( ".htm", string.Empty );
                ignoreDictionary.Add( ".old", string.Empty );
                ignoreDictionary.Add( ".jsp", string.Empty );
                ignoreDictionary.Add( ".png", string.Empty );
                ignoreDictionary.Add( ".gif", string.Empty );
                ignoreDictionary.Add( ".jpg", string.Empty );
                ignoreDictionary.Add( ".bmp", string.Empty );

                ignoreDictionary.Add( ".cvsignore", string.Empty );
                ignoreDictionary.Add( ".rc-010-fixup_old_problems", string.Empty );

                includeDictionary.Add( ".", string.Empty );
                includeDictionary.Add( ".pm", string.Empty );
                includeDictionary.Add( ".pl", string.Empty );
                includeDictionary.Add( ".h", string.Empty );
                includeDictionary.Add( ".m", string.Empty );
                includeDictionary.Add( ".c", string.Empty );
                includeDictionary.Add( ".cs", string.Empty);
                includeDictionary.Add( ".xml", string.Empty );
                includeDictionary.Add( ".sql", string.Empty );
                includeDictionary.Add( ".java", string.Empty );

                string folder = @".";
                TextWriter ow = Console.Out;
                if ( args.Length > 0 )
                {
                    folder = args[ 0 ];
                }
                if ( args.Length > 1 )
                {
                    ow = new StreamWriter( args[ 1 ] );
                }
                CountFolder( folder );
                WriteStats( ow );
                if ( ow != Console.Out )
                {
                    ow.Flush();
                    ow.Close();
                }
            }
            catch ( Exception ex )
            {
                Console.WriteLine( ex.ToString() );
            }
            Console.WriteLine( "Press any key to continue..." );
            Console.ReadKey();
        }

        static void WriteStats( TextWriter tw )
        {
            int num_files = 0;
            int num_lines = 0;
            foreach ( string ext in statDictionary.Keys )
            {
                FileTypeStats stat = statDictionary[ ext ];
                stat.WriteStats( tw );
                num_files += stat.NumFiles;
                num_lines += stat.NumLines;
            }
            tw.WriteLine( "Total" );
            tw.WriteLine( "Number of files: " + num_files.ToString() );
            tw.WriteLine( "Number of lines: " + num_lines.ToString() );
        }

        static void CountFolder( string path )
        {
            CountFolder( new DirectoryInfo( path ) );
        }

        static void CountFolder( DirectoryInfo di )
        {
            CountFiles( di );
            CountSubFolders( di );
        }

        static void CountFiles( DirectoryInfo di )
        {
            FileInfo[] fis = di.GetFiles();
            foreach ( FileInfo fi in fis )
            {
                CountFile( fi );
            }
        }

        static void CountSubFolders( DirectoryInfo di )
        {
            DirectoryInfo[] dis = di.GetDirectories();
            foreach( DirectoryInfo sdi in dis )
            {
                CountFolder( sdi );
            }
        }

        static void CountFile( FileInfo fi )
        {

            string ext = fi.Extension.ToLower();

            if ( includeDictionary.ContainsKey(ext) && !ignoreDictionary.ContainsKey( ext ) )
            {

                FileTypeStats stats = null;

                if ( statDictionary.ContainsKey( ext ) )
                {
                    stats = statDictionary[ ext ];
                }
                else
                {
                    stats = new FileTypeStats( ext );
                    statDictionary.Add( ext, stats );
                }

                CountFile( fi, stats );
            }

        }

        static void CountFile( FileInfo fi, FileTypeStats stats )
        {
            CountFile( fi.FullName, stats );
        }

        static void CountFile( string path, FileTypeStats stats )
        {
            CountFile( File.OpenText( path ), stats );
        }

        static void CountFile( StreamReader rdr, FileTypeStats stats )
        {
            stats.IncrementNumFiles();
            for ( string line = rdr.ReadLine(); line != null; line = rdr.ReadLine() )
            {
                stats.IncrementNumLines();
            }
        }

    }

}
