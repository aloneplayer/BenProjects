using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DesignPatternDemo.Structural.Decorator
{
    class PhotoDemo
    {
        // The original Photo class
        public class Photo : Form
        {
            private Image image;

            public Photo()
            {
                image = new Bitmap("finger.jpg");
                this.Text = "Lemonade";
                this.Paint += new PaintEventHandler(Drawer);
            }

            public virtual void Drawer(Object source, PaintEventArgs e)
            {
                e.Graphics.DrawImage(image, 30, 20);
            }
        }

        /// <summary>
        /// This simple BorderedPhoto decorator adds a colored border 
        /// of fixed size 
        /// </summary>
        public class BorderedPhoto : Photo
        {
            private Photo photo;
            Color color;

            public BorderedPhoto(Photo p, Color c)
            {
                photo = p;
                color = c;
            }

            public override void Drawer(Object source, PaintEventArgs e)
            {
                photo.Drawer(source, e);
                e.Graphics.DrawRectangle(new Pen(color, 10), 25, 15, 215, 225);
            }
        }

        /// <summary>
        /// The TaggedPhoto decorator keeps track of the tag number which gives it
        /// a specific place to be written
        /// </summary>
        class TaggedPhoto : Photo
        {
            Photo photo;
            string tag;
            int number;
            static int count;
            List<string> tags = new List<string>();

            public TaggedPhoto(Photo p, string t)
            {
                photo = p;
                tag = t;
                tags.Add(t);
                number = ++count;
            }
            public override void Drawer(Object source, PaintEventArgs e)
            {
                photo.Drawer(source, e);
                e.Graphics.DrawString(tag,
                new Font("Arial", 16),
                new SolidBrush(Color.Black),
                new PointF(80, 100 + number * 20));
            }

            public string ListTaggedPhotos()
            {
                string s = "Tags are: ";
                foreach (string t in tags) s += t + " ";
                return s;
            }
        }

        public static void Demo()
        {
            // Application.Run acts as a simple client
            Photo photo;
            TaggedPhoto foodTaggedPhoto, colorTaggedPhoto, tag;
            BorderedPhoto composition;
            // Compose a photo with two TaggedPhotos and a blue BorderedPhoto
            photo = new Photo();
            Application.Run(photo);

            foodTaggedPhoto = new TaggedPhoto(photo, "Food");
            colorTaggedPhoto = new TaggedPhoto(foodTaggedPhoto, "Yellow");
            composition = new BorderedPhoto(colorTaggedPhoto, Color.Blue);
            Application.Run(composition);
            Console.WriteLine(colorTaggedPhoto.ListTaggedPhotos());
          
            // Compose a photo with one TaggedPhoto and a yellow BorderedPhoto
            photo = new Photo();
            tag = new TaggedPhoto(photo, "Jug");
            composition = new BorderedPhoto(tag, Color.Yellow);
            Application.Run(composition);
        }
    }
}
