using MonkFocusLib;

namespace MonkFocusConsoleEnvironment
{
public class Program
{
    public static void Main()
    {
        HostsFileManagement hfm = new HostsFileManagement();
        Console.WriteLine(hfm.FilePath);
        var siteToBlock = Console.ReadLine();
        hfm.blockWebsite(siteToBlock);
        Console.WriteLine("Enter website to block: ");
        Console.ReadKey();

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Enter = add website | Backspace = remove website | Escape = exit");
            ConsoleKeyInfo key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.Enter:
                    Console.WriteLine("Enter website to block: ");
                    siteToBlock = Console.ReadLine();
                    hfm.blockWebsite(siteToBlock);
                    break;
                case ConsoleKey.Backspace:
                    Console.WriteLine("Enter website to unblock: ");
                    siteToBlock = Console.ReadLine();
                    hfm.unblockWebsite(siteToBlock);
                    break;
                case ConsoleKey.Escape:
                    exit = true;
                    break;
            }
        }
    }
}}

//youtube
/*
127.0.0.1 www.youtube.com
*/

//facebook
/*
127.0.0.1 www.facebook.com
*/

//instagram
/*
127.0.0.1 www.instagram.com
*/