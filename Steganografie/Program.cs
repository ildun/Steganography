using System;
using System.Drawing;

namespace Steganografie
{
    class Program
    {
        static Bitmap DoSteganography(Bitmap bm, string msg)
        {
            int j = 0, i = 0,k = 0;
            for (; j + i + k * bm.Width < msg.Length;)
            {
                Color c = bm.GetPixel(j, i);
                Color setPix = Color.FromArgb(c.R, c.G, Convert.ToByte(msg[j + i +k*bm.Width]));
                bm.SetPixel(j, i, setPix);
                if (j < bm.Width-1)
                {
                    ++j;
                }
                else if (i < bm.Height-1)
                {
                    j = 0;
                    ++k;
                    ++i;
                }
                else
                {
                    throw new Exception("Bitmap is not big enough");
                }
            }
            if (!(j < bm.Width - 1 && i < bm.Height - 1)) throw new Exception("Bitmap is not big enough");
            Color pix = bm.GetPixel(j, i);
            Color setNullPix = Color.FromArgb(pix.R, pix.G, Convert.ToByte('\0'));
            bm.SetPixel(j, i, setNullPix);
            return bm;
        }

        static string UndoSteganography(Bitmap bm)
        {
            string msg = "";
            for (int j = 0, i = 0; ;)
            {
                char appendChar = (char)bm.GetPixel(j, i).B;
                if (appendChar == '\0') break;
                msg += appendChar;
                if (j < bm.Width-1)
                {
                    ++j;
                }
                else if (i < bm.Height-1)
                {
                    j = 0;
                    ++i;
                }
                else
                {
                    break;
                }
            }
            return msg;
        }


        static void Main(string[] args)
        {
            //todo: rewrite arguments handling 
            if (args.Length < 1)
            {
                Console.WriteLine("Not enough or invalid command line arguments.");
                return;
            }
            string mode = args[0];
            if(mode == "do")
            {
                if (args.Length < 3)
                {
                    Console.WriteLine("Not enough or invalid command line arguments.");
                    return;
                }
                string bmpLocation = args[1];
                string msg = args[2];
                Bitmap bm = (Bitmap)Bitmap.FromFile(bmpLocation);
                DoSteganography(bm, msg).Save(bmpLocation.Insert(bmpLocation.Length - 4, "_edited"));

            }
            else if(mode == "undo")
            {
                if (args.Length < 2)
                {
                    Console.WriteLine("Not enough or invalid command line arguments.");
                    return;
                }
                string bmpLocation = args[1];
                Bitmap bm = (Bitmap)Bitmap.FromFile(bmpLocation);
                Console.WriteLine("message: " + UndoSteganography(bm));
            }
            else
            {
                Console.WriteLine("Wrong mode.");
                return;
            }
            
        }
    }
}
