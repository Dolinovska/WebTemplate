namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Parse();
            p.Run();

            var o = new OriginalContent();
            o.Run();
        }
    }
}