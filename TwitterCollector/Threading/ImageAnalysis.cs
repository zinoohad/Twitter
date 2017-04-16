using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopicSentimentAnalysis.Classes;
using Twitter.Classes;
using TwitterCollector.Common;
using MoreLinq;

namespace TwitterCollector.Threading
{
    public class ImageAnalysis : BaseThread
    {
        public override void RunThread()
        {
            while (ThreadOn)
            {
                try
                {
                    int topUsersForImageAnalysis = int.Parse(db.GetValueByKey("TopUsersForImageAnalysis", 30).ToString());
                    List<User> users = db.GetUsersToImageAnalysis();
                    FaceDetectObject faceDetect;
                    TopicSentimentAnalysis.ImageAnalysis imageAnalysis = new TopicSentimentAnalysis.ImageAnalysis();
                    if(users.Count == 0) 
                    {
                        Global.Sleep(60);
                    }

                    foreach (User user in users)
                    {
                        faceDetect = imageAnalysis.GetFaceDetectAndImageAnalysis(user.ProfileImage);
                        if (faceDetect.images[0].error != null)
                        {
                            //Update the user was check and the image is corrupted.
                            User updateUser = twitter.GetUserProfile("", user.ID);
                            db.UpdateUserProfile(updateUser);
                            faceDetect = imageAnalysis.GetFaceDetectAndImageAnalysis(updateUser.ProfileImage.Replace("_normal", ""));
                            if (faceDetect.images[0].error != null)
                            {
                                db.BadUrlProfileImage(user.UserPropertiesID);
                            }
                            else
                            {
                                UpdateUserGenderAndAge(user.UserPropertiesID, faceDetect);
                            }
                            
                        }
                        else
                        {
                            UpdateUserGenderAndAge(user.UserPropertiesID, faceDetect);
                        }
                    }
                }
                catch (Exception e)
                {
                    new TwitterException(e);
                }
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
    }
}
