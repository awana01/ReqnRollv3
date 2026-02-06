
namespace ReqnRollv3.Configs
{
    public class BrowserOptions
    {
        public bool Headless { get; set; } = true;   // default: headless
        public int SlowMo { get; set; } = 0;         // default: no slow motion
        public string Channel { get; set; } = "chromium"; // default: chromium
    }


    public class ContextOptions
    {
        public int ViewportWidth { get; set; } = 1920;   // default: full HD
        public int ViewportHeight { get; set; } = 1080;
        public bool IgnoreHTTPSErrors { get; set; } = false;
        public string? RecordVideoDir { get; set; } = null; // default: no video recording
        public VideoSize RecordVideoSize { get; set; } = new VideoSize { Width = 640, Height = 480 };
    }

    public class VideoSize
    {
        public int Width { get; set; } = 640;
        public int Height { get; set; } = 480;

        //public static implicit operator RecordVideoSize(VideoSize v)
        //{
        //    //throw new NotImplementedException();
        //}
    }






}
