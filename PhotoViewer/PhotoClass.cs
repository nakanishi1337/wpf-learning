using System;

namespace PhotoViewer
{

    public static class AppState
    {
        public static string CurrentDirectory { get; set; }
    }

    public class Photo
    {
        public string FilePath { get; set; }
    }
}