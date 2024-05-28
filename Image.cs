using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DanfossProject
{
    internal class Photos
    {
        private Photos gasBoiler;
        private Photos oilBoiler;
        private Photos gasMotor;
        private Photos electricBoiler;
        private List<Photos> photos;
        // Constructor that takes an Image parameter
        public Photos(Photos img)
        {
            this.gasBoiler = img;
            this.oilBoiler = img;
            this.gasMotor = img;
            this.electricBoiler = img;
        }
        //Displaying the photo
        public void DisplayImage()
        {
            /*
            foreach (var Photos in photos)
            {

                if (photos != null)
                {
                    // showing the photo
                    Console.WriteLine("`Photo: ");
                    // in progress
                    // Photos myImage = Photos.FromFile(".png");

                    // uncomment
                    Photos myObject = new Photos(myImage);

                    // Call the method to display the image
                    myObject.DisplayImage();
                }
                else
                {
                    Console.WriteLine("Couldn't display the image");

                }
            */
            }





        }
    }
