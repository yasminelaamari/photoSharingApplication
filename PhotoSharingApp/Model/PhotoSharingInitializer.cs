using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;



namespace PhotoSharingApp.Model
{
    public class PhotoSharingInitializer: DropCreateDatabaseAlways<PhotoSharingContext>
    {
       
     
        protected override void Seed(PhotoSharingContext context)
        {

            Debug.WriteLine(" houni aaaaaaaaa ");


            List<Comment> comments = new List<Comment>();
            List<Photo> photos = new List<Photo>();

            Photo photo = new Photo();
            photo.Title = "Title de la photo";
            photo.Description = "Description ....";
            photo.Owner = "Smisem";
            photo.PhotoFile = System.IO.File.ReadAllBytes("\\Users\\Yasmine\\Documents\\GitHub\\photoSharingApplication\\PhotoSharingApp\\Photos\\IMG_3500 (1).JPG");
            photo.CreatedDate = DateTime.Now;
            photo.ImageMimeType = "image/jpeg";
            photos.Add(photo);
            foreach (Photo p in photos)
                context.Photos.Add(p);
            context.SaveChanges();


            Comment comment = new Comment();
            comment.PhotoID = 1;
            comment.User = "NaokiSato";
            comment.Subject = "Test Comment";
            comment.Body = "This comment should be appear in photo";
            comments.Add(comment);
            foreach (Comment c in comments)
                context.Comments.Add(c);
            context.SaveChanges();

            // base.Seed(context);
        }
    }
}