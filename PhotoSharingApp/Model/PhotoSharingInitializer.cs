using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;



namespace PhotoSharingApp.Model
{
    public class PhotoSharingInitializer: DropCreateDatabaseAlways<PhotoSharingContext>
    {
        private byte[] getFileBytes (String filename)
        {
           return System.IO.File.ReadAllBytes(filename);
        }

     
        protected override void Seed(PhotoSharingContext context)
        {

            List < Photo > Ph= new List<Photo>();
            List<Comment> comment = new List<Comment>();
            Ph.Add(new Photo()
            {
                Title = "Test Photo 1",
                Description = "mon livre préféré 1",
                Owner = "NaokiSato1",
                PhotoFile = getFileBytes("\\Photos\\IMG_3497 (1).JPG"),
                CreatedDate = DateTime.Now
            });

            Ph.Add(new Photo()
            {
                Title = "Test Photo 2",
                Description = "mon livre préféré 2",
                Owner = "NaokiSato 2",
                PhotoFile = getFileBytes("\\Photos\\IMG_3500 (1).JPG"),
                CreatedDate = DateTime.Now
            });

            Ph.Add(new Photo()
            {
                Title = "Test Photo 3",
                Description = "mon livre préféré 3",
                Owner = "NaokiSato 3",
                PhotoFile = getFileBytes("\\Photos\\Sanns titre.png"),
                CreatedDate = DateTime.Now
            });

            foreach (Photo s in Ph)
                context.Photos.Add(s);


            Comment com = new Comment();
            com.PhotoID = 1;
            com.User = "NaokiSato";
            com.Subject = "Test Comment";
            com.Body = " This comment should appear in photo 1";
            comment.Add(com);
           
            foreach (Comment c in comment)
            {
                context.Comments.Add(com);

            }
           

            context.SaveChanges();
            base.Seed(context);
        }
    }
}