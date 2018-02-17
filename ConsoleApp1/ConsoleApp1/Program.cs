using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GDIDrawer;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            CDrawer cDrawer = new CDrawer(900,600,false);

            double a, b, c;
            int xi=0, xe=0;
            a = UserInputDouble("Enter the value of a: ");
            b = UserInputDouble("Enter the value of b: ");
            c = UserInputDouble("Enter the value of c: ");
            do
            {
                if (xi > xe)
                {
                    Console.WriteLine("The minimum x value is larger then the max!!");
                }
                xi = UserInputInt("Enter the min x value: ");
                xe = UserInputInt("Enter the max x value: ");
            } while (xi > xe);
            Graph(a, b, c, xi, xe, cDrawer);
            cDrawer.Render();
            Console.ReadKey();
        }
        static private void Graph(double a,double b,double c,int minX, int maxX,CDrawer cDrawer)
        {
            DrawCartisan(minX, maxX, -5, 5, cDrawer, out int scaleX, out int scaleY);
            for (double x = minX*scaleX; x < maxX*scaleX; x += 1.0/scaleX)//at 50 scale
            {
                double x1 = x - (1.0 / scaleX);
                double yc =  a * x * x + b * x + c;
                double yc1= a * x1 * x1 + b * x1 + c;
                int y = Convert.ToInt32(-yc*scaleY+(scaleY*5));
                int yl = Convert.ToInt32(-yc1 * scaleY + (scaleY * 5));
                int xp = Convert.ToInt32(x*scaleX+(-minX*scaleX));
                int xpl = Convert.ToInt32((x - (1.0 / scaleX))*scaleX + (-minX*scaleX));
                cDrawer.AddLine(xp, y, xpl , yl, Color.White, 3);
            }
        }
        static private double UserInputDouble(string question)
        {
            double value;
            Console.Write(question);
            while (!double.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("invalid entry enter a valid number");
                Console.Write(question);
            }
            return value;
        }

        static private int UserInputInt(string question)
        {
            int value;
            Console.Write(question);
            while (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("invalid entry enter a valid number");
                Console.Write(question);
            }
            return value;
        }
        static private void DrawCartisan(int minX,int maxX,int minY,int maxY,CDrawer cDrawer,out int scaleX,out int scaleY)
        {
            scaleX = cDrawer.DrawerWindowSize.Width/ (maxX - minX);
            int centerX = (-minX *scaleX);
            scaleY =  (cDrawer.DrawerWindowSize.Height-43)/ (maxY - minY) ;//not the minus 43 is because cdraw does not return hiegt of draw space crrectly
            //Console.WriteLine(scaleY);
            int centerY = (-minY *scaleY);
            //Console.Write(cDrawer.DrawerWindowSize.Height);
            cDrawer.AddLine(0, centerY, 900, centerY, Color.GreenYellow, 1);//adds horizontal line
            for(int tick = centerX-scaleX*-minX; tick <= scaleX * centerX * 2; tick += scaleX)//add hrizontal scale
            {
                //Console.WriteLine(tick);
                cDrawer.AddLine(tick, centerY - 5, tick, centerY + 5, Color.AliceBlue, 1);
            }
            cDrawer.AddLine(centerX, 0, centerX, 600, Color.GreenYellow, 1);//add vertical line
            for (int tick = centerY - scaleY * -minY; tick <= scaleY * centerY * 2; tick += scaleY)//add vertical scale
            {
                cDrawer.AddLine(centerX - 5, tick, centerX + 5,tick, Color.AliceBlue, 1);
            }
        }
    }
}
