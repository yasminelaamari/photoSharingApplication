﻿using PhotoSharingApp.Controllers;
using PhotoSharingApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Diagnostics;

namespace PhotoSharingApp.Controller
{      
    [ValueReporter]
    public class PhotoController : System.Web.Mvc.Controller
    {
        private PhotoSharingContext context= new PhotoSharingContext();
        // GET: Photo
        public ActionResult Index()
        {
            //var photo = new Photo(); 
            return View("Index");
            //context.Photos.First<Photo>()
            //context.Photos.ToList()
        }

        public ActionResult Display(int id)
        {
            List<Photo> photos = context.Photos.ToList();
            var verif = photos.Find(photo => photo.PhotoID == id);
            if (verif != null)
                return View("Display", verif);
            else
                return HttpNotFound();
        }
        public ActionResult Create()
        {
            Photo photo = new Photo();
            photo.CreatedDate= DateTime.Now;
            return View("Create", photo);
        }
        [HttpPost]
        public ActionResult Create(Photo photo, HttpPostedFileBase image)
        {
                photo.CreatedDate= DateTime.Now;
            if (ModelState.IsValid)
            {
                if (image!=null)
                {
                    photo.ImageMimeType = image.ContentType;
                    photo.PhotoFile = new byte[image.ContentLength];
                    image.InputStream.Read(photo.PhotoFile,0,image.ContentLength);
                    context.Photos.Add(photo);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View("Create", photo);
            }
        }

        public ActionResult Delete(int id)
        {
            List<Photo> photos = context.Photos.ToList();
            var verif = photos.Find(photo => photo.PhotoID == id);
            if (verif==null)
            {
                return HttpNotFound();
            }
            else
            {
                return View("Delete", verif);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed (int id)
        {
            List<Photo> photos = context.Photos.ToList();
            var verif = photos.Find(photo => photo.PhotoID == id);
            context.Entry(verif).State = EntityState.Deleted;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public FileContentResult GetImage (int id)
        {
            List<Photo> photos = context.Photos.ToList();
            var verif = photos.Find(photo => photo.PhotoID == id);
            if(verif!=null)
            {
               
                return (new FileContentResult(verif.PhotoFile,verif.ImageMimeType));
            }
            else
            {
                return null;
            }
        }
        [ChildActionOnly]
        public ActionResult _PhotoGallery(int number = 0)
        {
        List<Photo> photos = new List<Photo>();
            if(number==0)
            {
                photos = context.Photos.ToList();
            }
            else
            {
                photos = (from p in context.Photos
                          orderby p.CreatedDate descending
                          select p).Take(number).ToList();
            }
            return PartialView("_PhotoGallery",photos);
        }
    }
    
}