using System;
using Microsoft.SPOT;
using WS2811;
using Microsoft.SPOT.Hardware;
using System.Threading;

namespace Devhammer
{
    class StarLEDs
    {
        WS2811Led MyWS2811Strip;

        public StarLEDs()
        {
            int NumberOfLeds = 13; // Modify here the led number of your strip !

            // Initialize the strip : here using SPI2 and 800Khz model and using the linear human perceived luminosity PWM conversion factor of 2.25
            MyWS2811Strip = new WS2811Led(NumberOfLeds, SPI.SPI_module.SPI1, WS2811Led.WS2811Speed.S800KHZ, 2.25);

            //byte[] buff = new byte[] { 0, 0, 255, 0, 0, 255, 0, 0, 255, 0, 0, 255, 0, 0, 255, 0, 0, 255, 0, 0, 255, 0, 0, 255, 0, 0, 255, 0, 0, 255, 0, 0, 255, 0, 0, 255, 0, 0, 255 };
            //MyWS2811Strip.SetRange(buff, 0, 13);
            //MyWS2811Strip.Transmit();

            //ringsSolid();

            //rotateLines();

            //rings(1);
        }

        // set up LED arrangement functions:
        void setCenterLED(byte red, byte green, byte blue, bool clear = false)
        {
            if (clear)
                MyWS2811Strip.Clear();
            MyWS2811Strip.Set(2, red, green, blue);
            MyWS2811Strip.Transmit();
        }

        void setRing1LEDs(byte red, byte green, byte blue, bool clear = false)
        {
            if (clear)
                MyWS2811Strip.Clear();
            MyWS2811Strip.Set(1, red, green, blue);
            MyWS2811Strip.Set(3, red, green, blue);
            MyWS2811Strip.Set(5, red, green, blue);
            MyWS2811Strip.Set(7, red, green, blue);
            MyWS2811Strip.Set(9, red, green, blue);
            MyWS2811Strip.Set(11, red, green, blue);
            MyWS2811Strip.Transmit();
        }

        void setRing2LEDs(byte red, byte green, byte blue, bool clear = false)
        {
            if (clear)
                MyWS2811Strip.Clear();
            MyWS2811Strip.Set(0, red, green, blue);
            MyWS2811Strip.Set(4, red, green, blue);
            MyWS2811Strip.Set(6, red, green, blue);
            MyWS2811Strip.Set(8, red, green, blue);
            MyWS2811Strip.Set(10, red, green, blue);
            MyWS2811Strip.Set(12, red, green, blue);
            MyWS2811Strip.Transmit();
        }

        //void setRing3LEDs(byte red, byte green, byte blue, bool clear = false)
        //{
        //    if (clear)
        //        MyWS2811Strip.Clear();
        //    MyWS2811Strip.Set(0, red, green, blue);
        //    MyWS2811Strip.Set(6, red, green, blue);
        //    MyWS2811Strip.Set(9, red, green, blue);
        //    MyWS2811Strip.Set(12, red, green, blue);
        //    MyWS2811Strip.Set(15, red, green, blue);
        //    MyWS2811Strip.Set(18, red, green, blue);
        //    MyWS2811Strip.Transmit();
        //}

        void setVerticalLEDs(byte red, byte green, byte blue, bool clear = false)
        {
            if (clear)
                MyWS2811Strip.Clear();
            MyWS2811Strip.Set(0, red, green, blue);
            MyWS2811Strip.Set(1, red, green, blue);
            MyWS2811Strip.Set(2, red, green, blue);
            MyWS2811Strip.Set(3, red, green, blue);
            MyWS2811Strip.Set(4, red, green, blue);
            //MyWS2811Strip.Set(5, red, green, blue);
            //MyWS2811Strip.Set(6, red, green, blue);
            MyWS2811Strip.Transmit();
        }

        void setSWtoNELEDs(byte red, byte green, byte blue, bool clear = false)
        {
            if (clear)
                MyWS2811Strip.Clear();
            MyWS2811Strip.Set(6, red, green, blue);
            MyWS2811Strip.Set(5, red, green, blue);
            MyWS2811Strip.Set(2, red, green, blue);
            MyWS2811Strip.Set(11, red, green, blue);
            MyWS2811Strip.Set(12, red, green, blue);
            //MyWS2811Strip.Set(14, red, green, blue);
            //MyWS2811Strip.Set(15, red, green, blue);
            MyWS2811Strip.Transmit();
        }

        void setNWtoSELEDs(byte red, byte green, byte blue, bool clear = false)
        {
            if (clear)
                MyWS2811Strip.Clear();
            MyWS2811Strip.Set(8, red, green, blue);
            MyWS2811Strip.Set(7, red, green, blue);
            MyWS2811Strip.Set(2, red, green, blue);
            MyWS2811Strip.Set(9, red, green, blue);
            MyWS2811Strip.Set(10, red, green, blue);
            //MyWS2811Strip.Set(17, red, green, blue);
            //MyWS2811Strip.Set(18, red, green, blue);
            MyWS2811Strip.Transmit();
        }

        public void clear()
        {
            MyWS2811Strip.Clear();
            MyWS2811Strip.Transmit();
        }

        public void chaseRedGreen()
        {
            clear();
            for (int i = 0; i < 130; i++)
            {
                byte r = 0;
                byte g = 0;
                byte b = 0;

                if (i % 13 == 0)
                {
                    clear();
                }

                if (i % 2 == 0)
                {
                    g = 255;
                }
                else
                {
                    r = 255;
                }

                if (i > 0)
                {
                    MyWS2811Strip.Shift();
                }

                MyWS2811Strip.Set(0, r, g, b);
                MyWS2811Strip.Transmit();

                Thread.Sleep(200);
            }
            clear();
        }

        public void fadeInOut()
        {
            clear();
            for (int i = 0; i < 10; i++)
            {
                byte r = 0;
                byte g = 0;
                byte b = 0;
                byte red = 0;
                byte green = 0;
                byte blue = 0;


                if (i % 2 == 0)
                {
                    green = 255;
                }
                else
                {
                    red = 255;
                }

                // fade in 
                for (double p = 0.05; p < 1; p = p + 0.05)
                {
                    r = (byte)(red * p);
                    g = (byte)(green * p);
                    b = (byte)(blue * p);
                    setCenterLED(r, g, b);
                    setRing1LEDs(r, g, b);
                    setRing2LEDs(r, g, b);
                    //setRing3LEDs(r, g, b);

                    Thread.Sleep(50);
                }

                // fade out 
                for (double p = 1; p > 0; p = p - 0.05)
                {
                    r = (byte)(red * p);
                    g = (byte)(green * p);
                    b = (byte)(blue * p);
                    setCenterLED(r, g, b);
                    setRing1LEDs(r, g, b);
                    setRing2LEDs(r, g, b);
                    //setRing3LEDs(r, g, b);

                    Thread.Sleep(50);
                }

                clear();

            }
        }

        public void randomRedGreenPixels()
        {
            clear();
            Random rnd = new Random();
            for (int i = 0; i < 250; i++)
            {
                byte r = 0;
                byte g = 0;
                byte b = 0;
                int pixelToBlink = 0;

                pixelToBlink = rnd.Next(13);

                if (i % 2 == 0)
                {
                    g = 255;
                }
                else
                {
                    clear();
                    r = 255;
                }

                if (i % 3 == 0)
                {
                    r = 255;
                    g = 255;
                    b = 255;
                }

                MyWS2811Strip.Set(pixelToBlink, r, g, b);
                MyWS2811Strip.Transmit();

                Thread.Sleep(50);
            }
            clear();
        }

        public void heartbeat()
        {
            clear();

            setCenterLED(64, 0, 0, true);
            setRing1LEDs(64, 0, 0, true);
            setRing2LEDs(64, 0, 0, true);

            Thread.Sleep(150);

            setCenterLED(100, 0, 0, true);

            Thread.Sleep(50);

            setCenterLED(64, 0, 0, true);
            setRing1LEDs(100, 0, 0, true);

            Thread.Sleep(50);

            setRing1LEDs(64, 0, 0, true);
            setRing2LEDs(100, 0, 0, true);

            Thread.Sleep(50);

            setRing2LEDs(64, 0, 0, true);

            Thread.Sleep(150);

            clear();
        }

        public void rings()
        {
            clear();
            for (int i = 0; i < 12; i++)
            {
                byte r = 0;
                byte g = 0;
                byte b = 0;

                if (i % 2 == 0)
                {
                    g = 255;
                }
                else
                {
                    r = 255;
                }

                setCenterLED(r, g, b, true);

                Thread.Sleep(150);

                setRing1LEDs(r, g, b, true);

                Thread.Sleep(150);

                setRing2LEDs(r, g, b, true);

                Thread.Sleep(150);

                //setRing3LEDs(r, g, b, true);

                //Thread.Sleep(150);

                clear();
            }
        }

        public void ringsIn()
        {
            clear();
            for (int i = 0; i < 12; i++)
            {
                byte r = 0;
                byte g = 0;
                byte b = 0;

                if (i % 2 == 0)
                {
                    g = 255;
                }
                else
                {
                    r = 255;
                }

                //setRing3LEDs(r, g, b, true);

                //Thread.Sleep(150);

                setRing2LEDs(r, g, b, true);

                Thread.Sleep(150);

                setRing1LEDs(r, g, b, true);

                Thread.Sleep(150);

                setCenterLED(r, g, b, true);

                Thread.Sleep(150);

                clear();
            }
        }

        public void ringsSolid()
        {
            clear();
            for (int i = 0; i < 12; i++)
            {
                byte r = 0;
                byte g = 0;
                byte b = 0;

                if (i % 2 == 0)
                {
                    g = 255;
                }
                else
                {
                    r = 255;
                }

                setCenterLED(r, g, b);

                Thread.Sleep(250);

                setRing1LEDs(r, g, b);

                Thread.Sleep(250);

                setRing2LEDs(r, g, b);

                Thread.Sleep(250);

                //setRing3LEDs(r, g, b);

                //Thread.Sleep(250);

                clear();

            }
        }

        public void ringsSolidIn()
        {
            clear();
            for (int i = 0; i < 12; i++)
            {
                byte r = 0;
                byte g = 0;
                byte b = 0;

                if (i % 2 == 0)
                {
                    g = 255;
                }
                else
                {
                    r = 255;
                }


                //setRing3LEDs(r, g, b);

                //Thread.Sleep(250);

                setRing2LEDs(r, g, b);

                Thread.Sleep(250);

                setRing1LEDs(r, g, b);

                Thread.Sleep(250);

                setCenterLED(r, g, b);

                Thread.Sleep(250);

                clear();

            }
        }

        public void rotateLines()
        {
            clear();
            for (int i = 0; i < 12; i++)
            {
                byte r = 0;
                byte g = 0;
                byte b = 0;

                if (i % 2 == 0)
                {
                    g = 255;
                }
                else
                {
                    r = 255;
                }

                setVerticalLEDs(r, g, b, true);

                Thread.Sleep(100);

                setSWtoNELEDs(r, g, b, true);

                Thread.Sleep(100);

                setNWtoSELEDs(r, g, b, true);

                Thread.Sleep(100);

                clear();

            }

            //if (NextPattern > 0)
            //    startNextPattern(NextPattern);
        }

        public void rotateLinesRGB()
        {
            clear();
            for (int i = 0; i < 12; i++)
            {
                byte r = 0;
                byte g = 0;
                byte b = 0;

                if (i % 3 == 0)
                {
                    g = 255;
                }
                else if (i % 3 == 1)
                {
                    r = 255;
                }
                else
                {
                    b = 255;
                }

                setVerticalLEDs(r, g, b, true);

                Thread.Sleep(100);

                setSWtoNELEDs(r, g, b, true);

                Thread.Sleep(100);

                setNWtoSELEDs(r, g, b, true);

                Thread.Sleep(100);

                clear();

            }
        }


    }
}
