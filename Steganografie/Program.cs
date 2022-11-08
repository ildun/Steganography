using System;
using System.Drawing;

namespace Steganografie
{
    class Program
    {
        string textToWork;
        Bitmap bitMap;



        static Bitmap DoSteganography(Bitmap bm, string msg)
        {
            int k = 0;
            for(int j=0, i=0; j+i+ k * bm.Width < msg.Length;)
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
            return bm;
        }

        static string UndoSteganography(Bitmap bm, int len)
        {
            int k = 0;
            string msg = "";
            for (int j = 0, i = 0; j + i + k * bm.Width < len;)
            {
                msg += (char)bm.GetPixel(j, i).B;
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
                    throw new Exception("Length is bigger then bitmap");
                }
            }
            return msg;
        }


        static void Main(string[] args)
        {
            
            if (args.Length < 3)
            {
                Console.WriteLine("Not enough or invalid command line arguments.");
                return;
            }
            string mode = args[0];
            if(mode == "Do")
            {
                string bmpLocation = args[1];
                string msg = args[2];
                Bitmap bm = (Bitmap)Bitmap.FromFile(bmpLocation);
                DoSteganography(bm, msg).Save(bmpLocation.Insert(bmpLocation.Length - 4, "_edited"));

            }
            else if(mode == "Undo")
            {
                string bmpLocation = args[1];
                int msgLength = Int32.Parse(args[2]);
                Bitmap bm = (Bitmap)Bitmap.FromFile(bmpLocation);
                Console.WriteLine("message:" + UndoSteganography(bm, msgLength));
            }
            else
            {
                Console.WriteLine("Wrong mode.");
                return;
            }
            

            //Bitmap bm = (Bitmap)Bitmap.FromFile(@"C:\Users\lyhin.il.2019\Downloads\sample_1920×1280.bmp");
            //string msg = "The real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the followingThe real behavior of this is the following";
            //Console.WriteLine(UnSteganography(DoSteganography(bm, msg), msg.Length));
        }
    }
}
