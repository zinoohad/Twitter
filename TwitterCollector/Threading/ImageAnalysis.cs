﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopicSentimentAnalysis.Classes;
using Twitter.Classes;
using TwitterCollector.Common;
using MoreLinq;
using TopicSentimentAnalysis.Interfaces;
using TwitterCollector.Objects;
using System.Threading;

namespace TwitterCollector.Threading
{
    public class ImageAnalysis : BaseThread, Update
    {

        private TopicSentimentAnalysis.ImageAnalysis imageAnalysis = new TopicSentimentAnalysis.ImageAnalysis();

        private static object locker = new object();

        private static int threadNumber = 0;

        private static List<long> globalUsersIDs = new List<long>();

        public static object usersLocker = new object();

        public override void RunThread()
        {
            while (ThreadOn)
            {
                try
                {
                    int topUsersForImageAnalysis = int.Parse(db.GetValueByKey("TopUsersForImageAnalysis", 30).ToString());
                    int imageAnalysisMultiRequestLimit = int.Parse(db.GetValueByKey("ImageAnalysisMultiRequestLimit", 5).ToString());
                    List<User> users = db.GetUsersToImageAnalysis();
                    ImageAnalysisStatus imageStatus;
                    if(users.Count == 0) 
                    {
                        Global.Sleep(60);
                    }

                    foreach (User user in users)
                    {
                        if (!SafeAddToUsersList(user.ID))   //If the user id alrady in the list continue. It means there is a thread that working on this user.
                            continue;
                        imageStatus = new ImageAnalysisStatus() { UserID = user.ID, ImageURL = user.ProfileImage, UserPropertiesID = user.UserPropertiesID };
                        SafeAddThread(imageAnalysisMultiRequestLimit);
                        new Thread(new ThreadStart(() => GetFaceDetectAndImageAnalysis(imageStatus))).Start();
                    }
                }
                catch (Exception e)
                {
                    Global.Sleep(10);
                    new TwitterException(e);
                }
            }   
        }

        private void GetFaceDetectAndImageAnalysis(ImageAnalysisStatus imageStatus)
        {
            try 
	        {
                imageAnalysis.GetFaceDetectAndImageAnalysis(imageStatus, this);
	        }
	        catch (Exception e)
	        {
                if (!e.Message.Equals("The remote server returned an error: (429) Too Many Requests."))
                    new TwitterException(e);
                SafeRemoveFromUsersList(imageStatus.UserID);
                SafeSubThread();
	        }
        }

        private void UpdateUserGenderAndAge(long userPropertiesID, FaceDetectObject faceDetect)
        {
            List<Face> faces = (List<Face>)faceDetect.images[0].faces;
            List<Classifier> classifiers = (List<Classifier>)faceDetect.images[0].classifiers;

            if (faces != null && faces.Count > 0)
            {
                Face face = faces.MaxBy(f => f.age.score);
                db.UpdateUserPropertiesAnalysisByAPI(userPropertiesID, face.gender.gender, face.gender.score, face.age.min, face.age.max, face.age.score);
            }
            else // Need to use faces.classifiers
            {
                if (classifiers.Count == 0) // Can't get any result for this image
                {
                    db.BadUrlProfileImage(userPropertiesID);
                    return;
                }

                string[] Men = { "man", "male", "muscle man", "strong man", "young man" };
                string[] Women = { "woman", "young lady", "female", "womans portrait photo", "woman at work", "makeup", "cosmetic", "old woman", "uplift bra", "womens shorts",
                                     "skirt", "miniskirt", "" };
                int menCount = 0, womenCount = 0;
                foreach (Class c in classifiers[0].classes)
                {
                    if (Men.Contains(c.category)) menCount++;
                    if (Women.Contains(c.category)) womenCount++;  
                }
                if (Men.Length == Women.Length)
                    db.BadUrlProfileImage(userPropertiesID);
                else if (Men.Length > Women.Length)
                {
                    db.UpdateUserPropertiesAnalysisByAPI(userPropertiesID, "MALE", Men.Length / (Men.Length + Women.Length), 0, 0, 0);
                }
                else
                {
                    db.UpdateUserPropertiesAnalysisByAPI(userPropertiesID, "FEMALE", Women.Length / (Men.Length + Women.Length), 0, 0, 0);
                }
            }
        }

        public void Update(object obj)
        {
            ImageAnalysisStatus ias = (ImageAnalysisStatus)obj;
            DBHandler db = Global.DB;
            try
            {
                if (ias.FaceDetect.images[0].error != null)
                {
                    //Update the user was check and the image is corrupted.
                    User user = twitter.GetUserProfile("", ias.UserID);

                    if (user.errors != null)
                    {
                        new TwitterException(string.Format("While trying to get UserID '{0}', error was return from server: '{1}'", ias.UserID, user.errors[0].message));
                        if (user.errors.Count(item => item.code == 50) > 0)
                        {
                            // User not exists
                            db.BadUrlProfileImage(ias.UserPropertiesID);
                        }
                        SafeSubThread();
                        return;
                    }

                    string oldImageURL = user.ProfileImage;
                    user.ProfileImage = ias.ImageURL = user.ProfileImage.Replace("_normal", "");
                    db.UpdateUserProfile(user); // Save user changes
                    ias.FaceDetect = imageAnalysis.GetFaceDetectAndImageAnalysis(ias);    // Try to detect the image again
                    if (ias.FaceDetect.images[0].error != null)
                    {
                        if (ias.FaceDetect.images[0].error.error_id.Equals("input_error"))
                        {
                            // Try another version of the picture
                            user.ProfileImage = ias.ImageURL = oldImageURL.Replace("_normal", "_400x400");
                            db.UpdateUserProfile(user); // Save user changes
                            ias.FaceDetect = imageAnalysis.GetFaceDetectAndImageAnalysis(ias);    // Try to detect the image again
                            if (ias.FaceDetect.images[0].error != null)
                            {
                                db.BadUrlProfileImage(ias.UserPropertiesID);
                            }
                            else
                                UpdateUserGenderAndAge(ias.UserPropertiesID, ias.FaceDetect);
                        }
                        else
                            db.BadUrlProfileImage(ias.UserPropertiesID);
                    }
                    else
                    {
                        UpdateUserGenderAndAge(ias.UserPropertiesID, ias.FaceDetect);
                    }

                }
                else
                {
                    UpdateUserGenderAndAge(ias.UserPropertiesID, ias.FaceDetect);
                }
            }
            catch (Exception e)
            {
                if (!e.Message.Equals("The remote server returned an error: (429) Too Many Requests."))
                    new TwitterException(e);                
            }
            finally
            {
                SafeRemoveFromUsersList(ias.UserID);
                SafeSubThread();
            }
        }

        public void EndRequest()
        {
            throw new NotImplementedException();
        }

        public void SafeAddThread(int threadLimit)
        {
            int currentThreadNumber;
            lock (locker)   // Get current thread number
            {
                currentThreadNumber = threadNumber;
            }

            while (currentThreadNumber >= threadLimit)  // Wait until thread will finish.
            {
                Global.Sleep(2);
                lock (locker)
                {
                    currentThreadNumber = threadNumber;
                }
            }

            lock (locker)   // Add new thread
            {
                threadNumber++;
            }

        }

        public void SafeSubThread()
        {
            lock (locker)
            {
                threadNumber--;
            }
        }

        public bool SafeAddToUsersList(long userID)
        {
            lock (usersLocker)
            {
                if (globalUsersIDs.Contains(userID))
                    return false;
                //Console.WriteLine("Add " + userID);
                globalUsersIDs.Add(userID);
                return true;
            }
        }

        public void SafeRemoveFromUsersList(long userID)
        {
            //Console.WriteLine("Remove " + userID);
            lock (usersLocker)
            {
                globalUsersIDs.Remove(userID);
            }
        }
    }
}
