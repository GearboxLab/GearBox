using GearBox;

namespace GearBoxCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Httpd httpd = new Httpd();

            if (!httpd.IsStarted())
            {
                httpd.Start();
            }
        }
    }
}
