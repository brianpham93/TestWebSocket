using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSEP_DLL.Connectors;
using GSEP_DLL.Models;
namespace DLL_Debug
{
    class Program
    {
        static void Main(string[] args)
        {
            //REGISTER
            //RegisterBindingModel model = new RegisterBindingModel("trungnt@fpt.edu.vn", "trungnt", "Gsep2015!", "Gsep2015!");
            //String result = AuthenticationConnector.Register(model);
            //Console.WriteLine(result);
            //Console.ReadKey();

            //LOGIN
            String token = AuthenticationConnector.Login("tungnt", "Gsep2015!");
            Console.WriteLine("Logged in successfully");
            //var result = SocialConnector.SeeProfile(token, "NguyenVanQuyet");
            //Console.WriteLine(result.Username);k
            //IEnumerable<SocialUser> users = SocialConnector.SearchFriend(token, "Nguyen");
            //Console.WriteLine(users.FirstOrDefault().Username);
            //Console.ReadKey();

            
            //POST
            //for (int i = 2; i < 30; i++)
            //{
            //    PostCreateAndEditModel content = new PostCreateAndEditModel(i + ". FPT University");
            //    Post returnedPost = SocialConnector.Post(token, content);
            //    Console.WriteLine(returnedPost.PostID + " " + returnedPost.Content + " "
            //        + returnedPost.Time);
            //}
            
            //Console.ReadKey();
            
            //Console.ReadKey();

            //UPDATE POST
            //PostCreateAndEditModel editedContent = new PostCreateAndEditModel("Cam on, thay khong phai la Trung");
            //Post edittedPost = SocialConnector.EditPost(token, "1432564015914.11", editedContent);
            //Console.WriteLine(edittedPost.PostID + " " + edittedPost.Content + " " + edittedPost.Time);

            //DELTE POST
            //Console.WriteLine(SocialConnector.DeletePost(token, "1432564015914.11"));
            //Console.ReadKey();

            //List<PostResultModel> posts = SocialConnector.GetNewsfeed(token);
            //foreach (var item in posts)
            //{
            //    Console.WriteLine(item.Post.PostID + " " + item.Post.Content + " " + item.Post.Time + " " + item.Fullname + " " + item.Username);
            //}
            //Console.ReadKey();

            //Console.WriteLine(SocialConnector.LikePost(token, "1432823020640.24"));
            //Console.ReadKey();

            //for (int i = 0; i < 30; i++)
            //{
            //    Comment comment = SocialConnector.Comment(token, "1433260508023.85", new CommentCreateAndEditModel(i + ". Hi there"));
            //    Console.WriteLine(comment.CommentID + " " + comment.Content + " " + comment.Time);
            //}
            //Console.ReadKey();
            
            //List<CommentResultModel> comments = SocialConnector.GetComments(token, "1433260508023.85");
            //foreach (var item in comments)
            //{
            //    Console.WriteLine(item.Comment.CommentID + " " + item.Comment.Content + " " + item.Comment.Time + " " + item.Username + " " + item.Fullname);
            //}
            //Console.ReadKey();
            //Console.WriteLine(SocialConnector.LikeComment(token, "1433000509542.36"));
            //Console.ReadKey();


            //GET QUESTION BY ID

            //QuestionResultModel question = EducationConnector.GetQuestion(token, "1433410264592.32");
            //Console.WriteLine(question.Content + " " + question.Difficulty + " " +question.QuestionID);
            //foreach (var item in question.Answers)
            //{
            //    Console.WriteLine(item.AnswerID + " " + item.Content);
            //}

            //foreach (var item in question.HashTags)
            //{
            //    Console.WriteLine(item);
            //}

            //Console.ReadKey();

            //List<QuestionResultModel> questions = EducationConnector.GetQuestionByDifficulty(token, 3, "Sinhhoc");
            //foreach (var item in questions)
            //{                
            //    Console.WriteLine(item.Content + " " + item.Difficulty + " " + item.QuestionID);
            //    foreach (var item2 in item.Answers)
            //    {
            //        Console.WriteLine(item2.AnswerID + " " + item2.Content);
            //    }

            //    foreach (var item3 in item.HashTags)
            //    {
            //        Console.WriteLine(item3);
            //    }
            //}

            //Console.ReadKey();


            //List<QuestionResultModel> questions = EducationConnector.GetQuestionByHashtag(token, "tebao", "Sinhhoc");
            //foreach (var item in questions)
            //{                
            //    Console.WriteLine(item.Content + " " + item.Difficulty + " " + item.QuestionID);
            //    foreach (var item2 in item.Answers)
            //    {
            //        Console.WriteLine(item2.AnswerID + " " + item2.Content);
            //    }

            //    foreach (var item3 in item.HashTags)
            //    {
            //        Console.WriteLine(item3);
            //    }
            //}

            //Console.ReadKey();


            //List<QuestionResultModel> questions = EducationConnector.GetQuestionByDificultyAndHashtag(token, 3, "tebao", "Sinhhoc");
            //foreach (var item in questions)
            //{
            //    Console.WriteLine(item.Content + " " + item.Difficulty + " " + item.QuestionID);
            //    foreach (var item2 in item.Answers)
            //    {
            //        Console.WriteLine(item2.AnswerID + " " + item2.Content);
            //    }

            //    foreach (var item3 in item.HashTags)
            //    {
            //        Console.WriteLine(item3);
            //    }
            //}

            //Console.ReadKey();

            
            //Console.Write(EducationConnector.CheckAnswer(token, "1433410291895.79", "1433410293597.07"));
            //Console.ReadKey();


            //Console.WriteLine(EducationConnector.ShareQuestion(token, "1433410261327.18"));
            //Console.ReadKey();

            //List<Achievement> achievements = EducationConnector.GetAchievements(token, "Sinhhoc");
            //foreach (var item in achievements)
            //{
            //    Console.WriteLine(item.AchievementID + "----" + item.AchievementName + "----"
            //        + item.Description + "----" + item.Difficulty + "----" + item.GameName + "----" + item.IconURL);                
            //}
            //Console.ReadKey();

            //List<Achievement> achievements = EducationConnector.GetAchievementsByDifficulty(token, "Sinhhoc",2);
            //foreach (var item in achievements)
            //{
            //    Console.WriteLine(item.AchievementID + "----" + item.AchievementName + "----"
            //        + item.Description + "----" + item.Difficulty + "----" + item.GameName + "----" + item.IconURL);
            //}
            //Console.ReadKey();

            //Console.WriteLine(EducationConnector.ShareAchievement(token, "SinhhocAchi2"));
            //Console.ReadKey();

            Achievement achievement = EducationConnector.GetAchievement(token, "SinhhocAchi2");
            Console.WriteLine(achievement.AchievementID + "----" + achievement.AchievementName + "----"
                    + achievement.Description + "----" + achievement.Difficulty + "----" + achievement.GameName + "----" + achievement.IconURL);
            Console.ReadKey();
        }
    }
}
